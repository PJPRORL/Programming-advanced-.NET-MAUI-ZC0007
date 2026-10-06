// Nakijkscript — motor, deel 8: alles samen, en het verslag Nakijk_<oefening>_V#.md / .docx.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Nakijkscript
{
    public class Nakijker
    {
        // instellingen, gezet door Nakijk.ps1
        public string ProjectMap, Oefening, NakijkbestandenMap, CatalogusPad, UitvoerMap, BasisUrl, Container;
        public int Poort = 5199;
        public List<string> SqlScripts = new List<string>();
        public bool ZonderDocx;
        public string TestProgramma, TestArgumenten;   // enkel om het script zelf te testen: start de API zonder dotnet
        public Action<string> Meld = delegate (string s) { };

        // resultaat
        public string VerslagMd, VerslagDocx;
        public int Juist, Totaal, Versie;

        JsonWaarde nb;
        Uitvoering uitvoering;
        Analyse analyse;
        List<CompileerFout> compileerFouten = new List<CompileerFout>();
        public int Compileerfouten { get { return compileerFouten.Count; } }
        JsonWaarde vorige;
        int vorigeVersie;

        static readonly string[] Maanden = { "januari", "februari", "maart", "april", "mei", "juni", "juli", "augustus", "september", "oktober", "november", "december" };
        public static string Datum(DateTime d)
        {
            return d.Day + " " + Maanden[d.Month - 1] + " " + d.Year + ", " + d.ToString("HH:mm", CultureInfo.InvariantCulture);
        }

        public void Voer()
        {
            string pad = Path.Combine(NakijkbestandenMap, "Nakijk_" + Oefening + ".json");
            if (!File.Exists(pad)) throw new Exception("Er is geen nakijkbestand voor oefening " + Oefening + " (gezocht: " + pad + ").");
            nb = Json.Lees(File.ReadAllText(pad, Encoding.UTF8));
            List<Valkuil> catalogus = Catalogus.Lees(CatalogusPad);

            Api api;
            if (!string.IsNullOrEmpty(BasisUrl)) api = Api.ExterneApi(BasisUrl);
            else
            {
                api = new Api(ProjectMap, Poort);
                if (TestProgramma != null) { api.TestProgramma = TestProgramma; api.TestArgumenten = TestArgumenten; }
                else
                {
                    if (api.CsProj == null) throw new Exception("In " + ProjectMap + " staat geen .csproj. Geef de map van je project mee met -Project.");
                    Meld("Bouwen met dotnet build ...");
                    string uit;
                    compileerFouten = api.Bouw(out uit);
                }
            }

            CodeAnalyse code = null;
            if (!string.IsNullOrEmpty(ProjectMap) && Directory.Exists(ProjectMap) && string.IsNullOrEmpty(BasisUrl)) code = new CodeAnalyse(ProjectMap);
            else if (!string.IsNullOrEmpty(ProjectMap) && Directory.Exists(ProjectMap)) code = new CodeAnalyse(ProjectMap);

            if (compileerFouten.Count == 0)
            {
                Database db = null;
                if (nb.Veld("soort").Tekst == "database" && !api.Extern)
                {
                    Meld("Database zoeken in Docker ...");
                    db = new Database(ProjectMap, api.CsProj, nb.Veld("database").Veld("naam").Tekst, Container);
                }
                uitvoering = new Uitvoering(api, db, nb);
                if (nb.Veld("soort").Tekst == "database" && nb.Veld("database").Veld("opbouwen").Tekst == "sqlscript" && !api.Extern)
                {
                    if (SqlScripts.Count == 0)
                    {
                        List<string> gevonden = new List<string>();
                        foreach (string f in Directory.GetFiles(ProjectMap, "*.sql", SearchOption.AllDirectories))
                        {
                            string l = f.ToLowerInvariant().Replace('\\', '/');
                            if (!l.Contains("/bin/") && !l.Contains("/obj/")) gevonden.Add(f);
                        }
                        gevonden.Sort(StringComparer.OrdinalIgnoreCase);
                        SqlScripts.AddRange(gevonden);
                    }
                    uitvoering.SqlScripts = SqlScripts;
                }
                Meld("Verzoeken uitvoeren ...");
                uitvoering.Voer();
            }

            analyse = new Analyse(uitvoering, code, catalogus, nb);
            analyse.Voer(compileerFouten);

            if (uitvoering != null)
                foreach (StapResultaat r in uitvoering.Resultaten)
                {
                    if ((!r.Uitgevoerd && !r.TeltMee) || r.Soort == "actie") continue;
                    Totaal++;
                    if (r.Geslaagd) Juist++;
                }

            Directory.CreateDirectory(UitvoerMap);
            BepaalVersie();
            Document d = MaakVerslag();
            VerslagMd = Path.Combine(UitvoerMap, "Nakijk_" + Oefening + "_V" + Versie + ".md");
            File.WriteAllText(VerslagMd, d.AlsMarkdown(), new UTF8Encoding(false));
            if (!ZonderDocx)
            {
                VerslagDocx = Path.Combine(UitvoerMap, "Nakijk_" + Oefening + "_V" + Versie + ".docx");
                d.BewaarDocx(VerslagDocx);
            }
            BewaarResultaat();
        }

        // ------------------------------------------------------------ versies

        void BepaalVersie()
        {
            int hoogste = 0, hoogsteMetJson = 0;
            Regex re = new Regex("^Nakijk_" + Regex.Escape(Oefening) + @"_V(\d+)\.(md|docx|json)$", RegexOptions.IgnoreCase);
            foreach (string f in Directory.GetFiles(UitvoerMap))
            {
                Match m = re.Match(Path.GetFileName(f));
                if (!m.Success) continue;
                int n = int.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
                if (n > hoogste) hoogste = n;
                if (m.Groups[2].Value.ToLowerInvariant() == "json" && n > hoogsteMetJson) hoogsteMetJson = n;
            }
            Versie = hoogste + 1;
            if (hoogsteMetJson > 0)
            {
                JsonWaarde v;
                if (Json.ProbeerLees(File.ReadAllText(Path.Combine(UitvoerMap, "Nakijk_" + Oefening + "_V" + hoogsteMetJson + ".json"), Encoding.UTF8), out v))
                {
                    vorige = v; vorigeVersie = hoogsteMetJson;
                }
            }
        }

        void BewaarResultaat()
        {
            JsonWaarde o = JsonWaarde.MaakObject();
            o.ZetVeld("versie", JsonWaarde.MaakGetal(Versie));
            o.ZetVeld("oefening", JsonWaarde.MaakTekst(Oefening));
            o.ZetVeld("datum", JsonWaarde.MaakTekst(DateTime.Now.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)));
            o.ZetVeld("juist", JsonWaarde.MaakGetal(Juist));
            o.ZetVeld("totaal", JsonWaarde.MaakGetal(Totaal));
            JsonWaarde b = JsonWaarde.MaakLijst();
            foreach (Bevinding x in analyse.Bevindingen)
            {
                JsonWaarde e = JsonWaarde.MaakObject();
                e.ZetVeld("sleutel", JsonWaarde.MaakTekst(x.Sleutel));
                e.ZetVeld("titel", JsonWaarde.MaakTekst(x.Titel));
                e.ZetVeld("ernst", JsonWaarde.MaakTekst(x.ZonderGevolg ? "afwijking" : x.Ernst));
                b.Lijst.Add(e);
            }
            o.ZetVeld("bevindingen", b);
            JsonWaarde s = JsonWaarde.MaakObject();
            if (uitvoering != null)
                foreach (StapResultaat r in uitvoering.Resultaten)
                    s.ZetVeld(r.Id, JsonWaarde.MaakTekst(!r.Uitgevoerd ? "niet" : r.Geslaagd ? "juist" : "fout"));
            o.ZetVeld("stappen", s);
            File.WriteAllText(Path.Combine(UitvoerMap, "Nakijk_" + Oefening + "_V" + Versie + ".json"), Json.Schrijf(o, true), new UTF8Encoding(false));
        }

        // ------------------------------------------------------------ verslag

        static string Code(string t) { return "`" + (t ?? "").Replace("`", "'") + "`"; }

        List<Bevinding> Met(string ernst, bool zonderGevolg)
        {
            List<Bevinding> l = new List<Bevinding>();
            foreach (Bevinding b in analyse.Bevindingen)
                if (b.Ernst == ernst && b.ZonderGevolg == zonderGevolg) l.Add(b);
            return l;
        }

        Document MaakVerslag()
        {
            Document d = new Document();
            string titel = nb.Veld("titel").Tekst;
            string opgave = nb.Veld("opgave").Tekst;
            string h = ((int)nb.Veld("hoofdstuk").Getal).ToString("00", CultureInfo.InvariantCulture);
            d.Kop(1, "Nakijk " + Oefening + " — V" + Versie);
            d.Meta("NAKIJK V" + Versie + " · **Hoofdstuk " + h + "** · " + titel + " · bij " + Code(opgave) + " · " +
                   Datum(DateTime.Now) + " · nagekeken door het nakijkscript");
            List<string> noot = new List<string>();
            noot.Add("Dit is een nakijk van jouw code, geen oplossing. Er staat geen verbeterde code in. Per fout lees je waar ze zit, wat je API nu antwoordt, waarom, en in welke richting je moet zoeken.");
            if (vorige != null) noot.Add("Dit is **V" + Versie + "**. De vergelijking hieronder gaat over V" + vorigeVersie + ".");
            else if (Versie > 1) noot.Add("Dit is **V" + Versie + "**. Van de vorige versies vond het script geen resultaat om mee te vergelijken.");
            d.Kader("NOTE", noot.ToArray());
            d.Lijn();

            // ---------------------------------------------------- in één oogopslag
            d.Kop(2, "In één oogopslag");
            if (compileerFouten.Count > 0)
                d.Alinea("Je project **bouwt niet**: " + compileerFouten.Count + " compileerfout(en). Zolang dat zo is, kan het script geen enkel verzoek uitvoeren. Zie fout 1.");
            else if (uitvoering != null)
            {
                string zin = "Je API is getest met de verzoeken en regels uit " + Code(opgave) + ". **" + Juist + " van de " + Totaal + "** stappen geven het gevraagde antwoord.";
                if (uitvoering.PrefixAfwijkend) zin += " Het script zette " + Code(uitvoering.Prefix) + " voor elk verzoek, want zo antwoordt je API.";
                d.Alinea(zin);
                int niet = 0;
                foreach (StapResultaat r in uitvoering.Resultaten) if (!r.Uitgevoerd) niet++;
                if (niet > 0) d.Alinea(niet + " stap(pen) kon het script niet uitvoeren: zie het einde van dit verslag.");
                PerPunt(d);
            }
            if (vorige != null) VergelijkMetVorige(d);
            d.Lijn();

            // ---------------------------------------------------- A
            d.Kop(2, "A. Wat een verkeerd antwoord geeft");
            List<Bevinding> fouten = Met("fout", false);
            if (fouten.Count == 0) d.Alinea("Geen enkele stap geeft een verkeerd antwoord.");
            int n = 0;
            foreach (Bevinding b in fouten) SchrijfFout(d, b, ++n);

            // ---------------------------------------------------- B
            List<Bevinding> afw = Met("afwijking", false);
            afw.AddRange(Met("fout", true));
            if (afw.Count > 0)
            {
                d.Kop(2, "B. Werkt, maar wijkt af");
                Blok t = d.Tabel(new string[] { "Wat", "Waar", "Waarom het toch aandacht vraagt" }, new double[] { 0.28, 0.32, 0.40 });
                foreach (Bevinding b in afw)
                {
                    List<string> rij = new List<string>();
                    rij.Add(b.Titel);
                    rij.Add(b.Waar.Count == 0 ? "—" : string.Join("\n", Beperk(b.Waar, 4).ToArray()));
                    rij.Add(b.Waarom + (b.ZonderGevolg ? " Geen enkel voorbeeld uit de opgave faalt hierdoor, maar een ander verzoek wel." : "") + " **Richting.** " + b.Richting);
                    t.Rijen.Add(rij);
                }
            }

            // ---------------------------------------------------- C
            List<Bevinding> opkuis = Met("opkuis", false);
            if (opkuis.Count > 0)
            {
                d.Kop(2, "C. Opkuis, zonder invloed op het antwoord");
                List<string> items = new List<string>();
                foreach (Bevinding b in opkuis)
                    items.Add("**" + b.Titel + ".** " + b.Waarom + " " + b.Richting + (b.Waar.Count > 0 ? " Waar: " + string.Join("; ", Beperk(b.Waar, 5).ToArray()) + "." : ""));
                d.Lijst(items);
            }

            // ---------------------------------------------------- D
            if (vorige != null) WatBeter(d);

            // ---------------------------------------------------- E
            d.Lijn();
            d.Kop(2, "E. Zelf nakijken");
            if (fouten.Count > 0)
            {
                d.Alinea("Gebruik enkel de verzoeken uit " + Code(opgave) + ".");
                Blok t = d.Tabel(new string[] { "Na het oplossen van", "Doe deze verzoeken", "Je moet krijgen" }, new double[] { 0.25, 0.40, 0.35 });
                n = 0;
                foreach (Bevinding b in fouten)
                {
                    n++;
                    List<string> v = new List<string>(), k = new List<string>();
                    foreach (StapResultaat r in Beperk(b.Stappen, 4)) { v.Add(Code(r.Verzoek)); k.Add(r.Verwacht); }
                    t.Rijen.Add(new List<string>(new string[] { "fout " + n + " — " + b.Titel, v.Count == 0 ? "start je API opnieuw" : string.Join("\n", v.ToArray()), k.Count == 0 ? "geen fout bij het starten" : string.Join("\n", k.ToArray()) }));
                }
            }
            List<string> hand = new List<string>();
            JsonWaarde h2 = nb.Veld("handmatig");
            if (h2 != null) foreach (JsonWaarde x in h2.Lijst) hand.Add("Punt " + x.Veld("punt").Tekst + ": " + x.Veld("tekst").Tekst);
            if (hand.Count > 0)
            {
                d.Alinea("Wat het script niet zelf kan nakijken, kijk je met de hand na:");
                d.Lijst(hand);
            }
            NietUitgevoerd(d);
            Bijlage(d);
            return d;
        }

        static List<T> Beperk<T>(List<T> l, int max) { return l.Count <= max ? l : l.GetRange(0, max); }

        void PerPunt(Document d)
        {
            List<string> punten = new List<string>();
            Dictionary<string, int[]> tel = new Dictionary<string, int[]>();
            foreach (StapResultaat r in uitvoering.Resultaten)
            {
                if (r.Soort == "actie") continue;
                if (!tel.ContainsKey(r.Punt)) { tel[r.Punt] = new int[3]; punten.Add(r.Punt); }
                if (!r.Uitgevoerd) tel[r.Punt][2]++;
                else if (r.Geslaagd) tel[r.Punt][0]++;
                else tel[r.Punt][1]++;
            }
            punten.Sort(delegate (string a, string b)
            {
                int x, y;
                bool ax = int.TryParse(a, out x), by = int.TryParse(b, out y);
                if (ax && by) return x.CompareTo(y);
                return string.CompareOrdinal(a, b);
            });
            Blok t = d.Tabel(new string[] { "Punt in de opgave", "Stappen", "Juist", "Fout", "Niet uitgevoerd" }, new double[] { 0.28, 0.18, 0.18, 0.18, 0.18 });
            foreach (string p in punten)
            {
                int[] c = tel[p];
                t.Rijen.Add(new List<string>(new string[] { "punt " + p, (c[0] + c[1] + c[2]).ToString(), c[0].ToString(), c[1] == 0 ? "0" : "**" + c[1] + "**", c[2].ToString() }));
            }
        }

        void VergelijkMetVorige(Document d)
        {
            Dictionary<string, string> nu = new Dictionary<string, string>();
            foreach (Bevinding b in analyse.Bevindingen) nu[b.Sleutel] = b.Titel;
            Blok t = d.Tabel(new string[] { "Bevinding uit V" + vorigeVersie, "Status in V" + Versie }, new double[] { 0.7, 0.3 });
            List<string> oud = new List<string>();
            foreach (JsonWaarde b in vorige.Veld("bevindingen").Lijst)
            {
                string s = b.Veld("sleutel").Tekst;
                oud.Add(s);
                t.Rijen.Add(new List<string>(new string[] { b.Veld("titel").Tekst, nu.ContainsKey(s) ? "<span style=\"color:#f08c00\">**nog open**</span>" : "<span style=\"color:#37b24d\">**opgelost**</span>" }));
            }
            foreach (Bevinding b in analyse.Bevindingen)
                if (!oud.Contains(b.Sleutel)) t.Rijen.Add(new List<string>(new string[] { b.Titel, "<span style=\"color:#f03e3e\">**nieuw**</span>" }));
            if (t.Rijen.Count == 0) d.Blokken.Remove(t);
            d.Alinea("In V" + vorigeVersie + " waren " + vorige.Veld("juist").AlsTekst() + " van de " + vorige.Veld("totaal").AlsTekst() + " stappen juist, nu " + Juist + " van de " + Totaal + ".");
        }

        void WatBeter(Document d)
        {
            List<string> items = new List<string>();
            Dictionary<string, bool> nu = new Dictionary<string, bool>();
            foreach (Bevinding b in analyse.Bevindingen) nu[b.Sleutel] = true;
            foreach (JsonWaarde b in vorige.Veld("bevindingen").Lijst)
                if (!nu.ContainsKey(b.Veld("sleutel").Tekst)) items.Add("**Opgelost:** " + b.Veld("titel").Tekst + ".");
            int beter = 0;
            JsonWaarde st = vorige.Veld("stappen");
            if (uitvoering != null && st != null)
                foreach (StapResultaat r in uitvoering.Resultaten)
                {
                    JsonWaarde v = st.Veld(r.Id);
                    if (v != null && v.Tekst != "juist" && r.Uitgevoerd && r.Geslaagd) beter++;
                }
            if (beter > 0) items.Add(beter + " stap(pen) die in V" + vorigeVersie + " fout waren of niet uitgevoerd, zijn nu juist.");
            if (items.Count == 0) return;
            d.Kop(2, "D. Wat beter is dan in V" + vorigeVersie);
            d.Lijst(items);
        }

        void SchrijfFout(Document d, Bevinding b, int n)
        {
            d.Kop(3, "Fout " + n + " — " + b.Titel);
            if (b.Waar.Count == 1) d.Alinea("**Waar.** " + b.Waar[0]);
            else if (b.Waar.Count > 1)
            {
                d.Alinea("**Waar.**");
                List<string> plaatsen = new List<string>(Beperk(b.Waar, 8));
                if (b.Waar.Count > 8) plaatsen.Add("en nog " + (b.Waar.Count - 8) + " plaats(en)");
                d.Lijst(plaatsen);
            }
            else if (b.Stappen.Count > 0)
            {
                List<string> e = new List<string>();
                foreach (StapResultaat r in b.Stappen) { if (r.Gevolg) continue; string s = Code(r.Verzoek); if (!e.Contains(s)) e.Add(s); }
                d.Alinea("**Waar.** Het endpoint achter " + string.Join(", ", Beperk(e, 4).ToArray()) + (e.Count > 4 ? " en nog " + (e.Count - 4) + " verzoek(en)" : "") + ".");
            }
            if (b.Stappen.Count > 0)
            {
                Blok t = d.Tabel(new string[] { "Verzoek", "Je API antwoordt nu", "De opgave vraagt" }, new double[] { 0.30, 0.35, 0.35 });
                foreach (StapResultaat r in Beperk(b.Stappen, 8))
                {
                    string verzoek = Code(r.Verzoek) + (r.BodyVerstuurd != null ? "\nbody " + Code(Uitvoering.Kort(r.BodyVerstuurd, 160)) : "") + "\n*" + r.Uitleg + "*" + (r.Gevolg ? "\n*Gevolg van het mislukte verzoek hierboven.*" : "");
                    string kreeg = r.Gekregen ?? "";
                    foreach (string m in r.Meldingen) if (!m.StartsWith("Statuscode")) kreeg += "\n" + m;
                    t.Rijen.Add(new List<string>(new string[] { verzoek, kreeg, r.Verwacht ?? "" }));
                }
                if (b.Stappen.Count > 8) d.Alinea("En nog " + (b.Stappen.Count - 8) + " verzoek(en) met dezelfde oorzaak.");
                List<string> details = new List<string>();
                foreach (StapResultaat r in b.Stappen)
                    if (!r.Gevolg)
                    foreach (Verschil v in r.Verschillen)
                        if (v.Pad != "tekst" && details.Count < 6) details.Add(Code(r.Verzoek) + " — " + (v.Pad == "$" ? "het antwoord" : Code(v.Pad)) + ": de opgave vraagt " + Code(v.Verwacht) + ", je API geeft " + Code(v.Gekregen) + ".");
                if (details.Count > 0) { d.Alinea("**Verschillen in de JSON.**"); d.Lijst(details); }
            }
            if (b.Bewijs.Count > 0) d.Alinea("**In de uitvoer van je API.** " + string.Join(" · ", Beperk(b.Bewijs, 3).ToArray()));
            if (!string.IsNullOrEmpty(b.Waarom)) d.Alinea("**Waarom.** " + b.Waarom);
            if (!string.IsNullOrEmpty(b.Richting)) d.Alinea("**Richting.** " + b.Richting);
            if (b.Tips.Count > 0)
            {
                List<string> tips = new List<string>();
                tips.Add("**Zo vang je dit voortaan sneller**");
                foreach (string tip in b.Tips) tips.Add("- " + tip);
                d.Kader("TIP", tips.ToArray());
            }
        }

        void NietUitgevoerd(Document d)
        {
            List<string> items = new List<string>();
            if (uitvoering != null)
            {
                foreach (StapResultaat r in uitvoering.Resultaten)
                    if (!r.Uitgevoerd && r.Soort != "actie") items.Add(Code(r.Id) + " " + r.Uitleg + ": " + r.NietUitgevoerdOmdat + ".");
                foreach (string v in uitvoering.Verloop) if (v.StartsWith("Scenario")) items.Add(v);
                if (uitvoering.Db != null && uitvoering.Db.Omgeving) items.Insert(0, "**De database was niet bereikbaar voor het script:** " + uitvoering.Db.Probleem);
                if (uitvoering.SqlScripts.Count > 1)
                {
                    List<string> namen = new List<string>();
                    foreach (string s in uitvoering.SqlScripts) namen.Add(Code(Path.GetFileName(s)));
                    items.Add("De SQL-scripts liepen in deze volgorde: " + string.Join(", ", namen.ToArray()) + ". Klopt die volgorde niet, geef ze dan zelf mee met `-SqlScript`.");
                }
            }
            if (items.Count == 0) return;
            d.Kop(3, "Wat het script niet kon nakijken");
            d.Lijst(Beperk(items, 30));
        }

        void Bijlage(Document d)
        {
            if (uitvoering == null || uitvoering.Resultaten.Count == 0) return;
            d.Lijn();
            d.Kop(2, "Bijlage: alle stappen");
            Blok t = d.Tabel(new string[] { "Stap", "Punt", "Verzoek", "Resultaat" }, new double[] { 0.10, 0.08, 0.60, 0.22 });
            foreach (StapResultaat r in uitvoering.Resultaten)
            {
                string res = !r.Uitgevoerd ? "niet uitgevoerd" : r.Geslaagd ? "juist" : "**fout**";
                string verzoek = r.Soort == "verzoek" ? Code(r.Verzoek) + " — " + r.Uitleg : r.Uitleg;
                t.Rijen.Add(new List<string>(new string[] { r.Id, r.Punt, verzoek, res }));
            }
        }
    }
}
