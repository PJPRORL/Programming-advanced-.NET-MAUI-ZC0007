// Nakijkscript — motor, deel 6: van mislukte stappen naar bevindingen met een oorzaak.
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Nakijkscript
{
    public class Analyse
    {
        readonly Uitvoering u;
        readonly CodeAnalyse code;
        readonly int hoofdstuk;
        readonly string soort;
        readonly List<Valkuil> actief = new List<Valkuil>();
        readonly Dictionary<string, List<Treffer>> treffers = new Dictionary<string, List<Treffer>>();
        public List<Bevinding> Bevindingen = new List<Bevinding>();
        public List<CompileerFout> CompileerFouten;

        public Analyse(Uitvoering uitvoering, CodeAnalyse code, List<Valkuil> catalogus, JsonWaarde nb)
        {
            u = uitvoering;
            this.code = code;
            hoofdstuk = (int)nb.Veld("hoofdstuk").Getal;
            soort = nb.Veld("soort").Tekst;
            List<string> extra = new List<string>(), niet = new List<string>();
            JsonWaarde vk = nb.Veld("valkuilen");
            if (vk != null)
            {
                if (vk.HeeftVeld("extra")) foreach (JsonWaarde x in vk.Veld("extra").Lijst) extra.Add(x.Tekst);
                if (vk.HeeftVeld("niet")) foreach (JsonWaarde x in vk.Veld("niet").Lijst) niet.Add(x.Tekst);
            }
            foreach (Valkuil v in catalogus)
                if (((v.Vanaf <= hoofdstuk && hoofdstuk <= v.Tot) || extra.Contains(v.Code)) && !niet.Contains(v.Code)) actief.Add(v);
        }

        Valkuil Zoek(string c) { foreach (Valkuil v in actief) if (v.Code == c) return v; return null; }

        Bevinding Haal(string sleutel, Valkuil v)
        {
            foreach (Bevinding b in Bevindingen) if (b.Sleutel == sleutel) return b;
            Bevinding n = new Bevinding();
            n.Sleutel = sleutel;
            if (v != null)
            {
                n.Code = v.Code; n.Titel = v.Titel; n.Ernst = v.Ernst; n.Waarom = v.Waarom; n.Richting = v.Richting;
                n.Tips.AddRange(v.Tips);
                List<Treffer> t;
                if (treffers.TryGetValue(v.Code, out t)) foreach (Treffer x in t) n.Waar.Add(x.Plaats + ": " + x.Tekst);
            }
            Bevindingen.Add(n);
            return n;
        }

        public void Voer(List<CompileerFout> compileerFouten)
        {
            CompileerFouten = compileerFouten;
            if (code != null)
                foreach (Valkuil v in actief)
                    if (v.Statisch != null) treffers[v.Code] = code.Detecteer(v.Statisch, v.Code);

            if (compileerFouten != null && compileerFouten.Count > 0)
            {
                Bevinding b = Haal("BUILD", Zoek("BUILD") ?? Basis("BUILD"));
                foreach (CompileerFout f in compileerFouten)
                    b.Waar.Add(f.Bestand + (f.Regel > 0 ? ", regel " + f.Regel : "") + ": " + f.Code + " " + f.Bericht);
                StatischeBevindingen();
                return;
            }

            // de API startte niet
            if (u != null && u.ApiStartFout != null)
            {
                bool omgeving = u.Db != null && u.Db.Omgeving;
                if (!omgeving)
                {
                    string c = LogCode(u.StartLog) ?? "API-START";
                    if (u.ApiStartFout.StartsWith("de database kon niet opnieuw opgebouwd")) c = "DATABASE-OPBOUWEN";
                    Bevinding b = Haal(c, Zoek(c) ?? Basis(c));
                    b.Bewijs.Add(u.ApiStartFout);
                    b.Bewijs.AddRange(Excepties(u.StartLog, 3));
                }
            }

            if (u != null)
            {
                List<StapResultaat> fout = new List<StapResultaat>();
                foreach (StapResultaat r in u.Resultaten) if (r.Uitgevoerd && !r.Geslaagd) fout.Add(r);
                bool alles404 = Alles404();
                Dictionary<StapResultaat, Bevinding> bij = new Dictionary<StapResultaat, Bevinding>();
                foreach (StapResultaat r in fout)
                {
                    string c = alles404 && Zoek("CONTROLLER-NIET-GEVONDEN") != null ? "CONTROLLER-NIET-GEVONDEN" : Oorzaak(r);
                    Bevinding b;
                    StapResultaat oorzaak = c == null ? EerderMislukt(r) : null;
                    if (oorzaak != null && bij.ContainsKey(oorzaak))
                    {
                        // een gevolg van een eerder mislukt verzoek in dezelfde reeks: hoort bij die fout
                        r.Gevolg = true;
                        b = bij[oorzaak];
                    }
                    else if (c == null)
                    {
                        b = Haal("PUNT-" + r.Punt, null);
                        b.Code = "PUNT"; b.Ernst = "fout";
                        b.Titel = "Punt " + r.Punt + ": het antwoord wijkt af van de opgave";
                        b.Waarom = "Het script herkende hier geen bekende valkuil. De tabel toont per verzoek wat je API antwoordt en wat de opgave vraagt; daaronder staan de verschillen één voor één.";
                        b.Richting = "Lees punt " + r.Punt + " van de opgave opnieuw, en vergelijk het met het eerste verzoek in de tabel. Kijk ook of een eerder verzoek in dezelfde reeks iets veranderde wat hier meetelt.";
                    }
                    else b = Haal(c, Zoek(c) ?? Basis(c));
                    bij[r] = b;
                    b.Stappen.Add(r);
                    foreach (string e in Excepties(r.LogRegels, 2)) if (!b.Bewijs.Contains(e)) b.Bewijs.Add(e);
                }
                if (u.PrefixAfwijkend && Zoek("ROUTE-PREFIX") != null) Haal("ROUTE-PREFIX", Zoek("ROUTE-PREFIX"));
            }
            StatischeBevindingen();
        }

        /// <summary>Valkuilen die enkel in de code staan, zonder mislukte stap.</summary>
        void StatischeBevindingen()
        {
            foreach (Valkuil v in actief)
            {
                if (v.Statisch == null || v.Log.Count > 0) continue;
                List<Treffer> t;
                if (!treffers.TryGetValue(v.Code, out t) || t.Count == 0) continue;
                bool bestaat = false;
                foreach (Bevinding b in Bevindingen) if (b.Code == v.Code) bestaat = true;
                if (bestaat) continue;
                Bevinding n = Haal(v.Code, v);
                if (v.Ernst == "fout") n.ZonderGevolg = true;
            }
        }

        static Valkuil Basis(string c)
        {
            Valkuil v = new Valkuil();
            v.Code = c; v.Ernst = "fout";
            if (c == "DATABASE-OPBOUWEN")
            {
                v.Titel = "Je database kon niet opgebouwd worden met je migraties";
                v.Waarom = "Het script verwijdert je database en laat ze opnieuw opbouwen met `dotnet ef database update`, zoals de opgave vraagt. Dat mislukte, dus ook de startgegevens kwamen niet in de database.";
                v.Richting = "Voer `dotnet ef database update` zelf uit op een lege database en lees de eerste fout.";
            }
            else
            {
                v.Titel = c;
                v.Waarom = ""; v.Richting = "";
            }
            return v;
        }

        bool Alles404()
        {
            int n = 0;
            foreach (StapResultaat r in u.Resultaten)
            {
                if (!r.Uitgevoerd || r.Soort != "verzoek") continue;
                n++;
                if (r.Status != 404) return false;
            }
            return n > 0;
        }

        string LogCode(List<string> regels)
        {
            if (regels == null || regels.Count == 0) return null;
            string alles = string.Join("\n", regels.ToArray());
            foreach (Valkuil v in actief)
                foreach (string p in v.Log)
                    if (Regex.IsMatch(alles, p)) return v.Code;
            return null;
        }

        static readonly string[] Vroeg = { "geenAntwoord", "accoladeZonderDollar", "tekstBijna", "extraTekst", "status405" };

        bool HeeftTreffer(string code)
        {
            List<Treffer> t;
            return treffers.TryGetValue(code, out t) && t.Count > 0;
        }

        string Oorzaak(StapResultaat r)
        {
            // CreatedAtAction vindt geen adres, omdat de route van de GET een andere naam gebruikt dan het Id
            bool linkFout = Past("location", r) || (r.LogRegels != null && string.Join("\n", r.LogRegels.ToArray()).Contains("No route matches the supplied values"));
            if (linkFout && HeeftTreffer("ROUTE-PARAMNAAM") && !HeeftTreffer("CREATEDATACTION-ASYNC")) return "ROUTE-PARAMNAAM";
            string c = LogCode(r.LogRegels);
            if (c != null) return c;
            foreach (Valkuil v in actief)
                if (v.Antwoord != null && Array.IndexOf(Vroeg, v.Antwoord) >= 0 && Past(v.Antwoord, r)) return v.Code;
            foreach (KeyValuePair<string, List<Treffer>> kv in treffers)
                foreach (Treffer t in kv.Value)
                    if (t.Route != null && r.Pad != null && Regex.IsMatch(r.Pad.Split('?')[0], CodeAnalyse.RouteNaarRegex(t.Route), RegexOptions.IgnoreCase))
                        if (kv.Key == "ROUTE-PARAMNAAM" || kv.Key == "ROUTE-SLASH" || kv.Key == "ARGUMENT-VOLGORDE") return kv.Key;
            foreach (Valkuil v in actief)
                if (v.Antwoord != null && Past(v.Antwoord, r)) return v.Code;
            return null;
        }

        JsonWaarde Verwacht(StapResultaat r) { return r.Stap.Veld("verwacht"); }

        int VerwachteStatus(StapResultaat r)
        {
            JsonWaarde ve = Verwacht(r);
            return ve != null && ve.HeeftVeld("status") ? (int)ve.Veld("status").Getal : -1;
        }

        bool HeeftVerschil(StapResultaat r, string soortVerschil)
        {
            foreach (Verschil v in r.Verschillen) if (v.Soort == soortVerschil) return true;
            return false;
        }

        List<StapResultaat> Eerder(StapResultaat r)
        {
            List<StapResultaat> l = new List<StapResultaat>();
            foreach (StapResultaat x in u.Resultaten)
            {
                if (x == r) break;
                if (x.ScenarioId == r.ScenarioId) l.Add(x);
            }
            return l;
        }

        /// <summary>Het laatste mislukte verzoek dat iets veranderde (POST, PUT, DELETE), eerder in dezelfde reeks.</summary>
        StapResultaat EerderMislukt(StapResultaat r)
        {
            StapResultaat gevonden = null;
            foreach (StapResultaat x in Eerder(r))
                if (x.Uitgevoerd && !x.Geslaagd && x.Soort == "verzoek" && x.Methode != "GET") gevonden = x;
            return gevonden;
        }

        bool VarUitPost(StapResultaat r)
        {
            string ruw = r.Stap.Veld("verzoek") == null ? "" : r.Stap.Veld("verzoek").Veld("pad").Tekst;
            foreach (Match m in Regex.Matches(ruw, @"\{\{(\w+)\}\}"))
            {
                StapResultaat bron;
                if (u.VarBron.TryGetValue(m.Groups[1].Value, out bron) && bron.Methode == "POST" && bron.Status == 201 && bron.ScenarioId == r.ScenarioId) return true;
            }
            return false;
        }

        static bool IsLeeg(string g) { return g == "null" || g == "0" || g == "\"\"" || g == "false" || g == "(veld ontbreekt)" || g == "(ontbreekt)"; }

        bool Past(string naam, StapResultaat r)
        {
            int vs = VerwachteStatus(r);
            JsonWaarde ve = Verwacht(r);
            switch (naam)
            {
                case "geenAntwoord": return r.Soort == "verzoek" && r.Status == 0;
                case "status405": return r.Status == 405;
                case "tekstBijna": return r.TekstBijna;
                case "accoladeZonderDollar":
                    foreach (Verschil v in r.Verschillen)
                        if (v.Pad == "tekst" && v.Verwacht.IndexOf('{') < 0 && Regex.IsMatch(v.Gekregen, @"\{\w+(\.\w+)*\}")) return true;
                    return false;
                case "extraTekst":
                    foreach (Verschil v in r.Verschillen)
                        if (v.Pad == "tekst" && v.Verwacht.Length > 0 && v.Gekregen != v.Verwacht && v.Gekregen.Contains(v.Verwacht)) return true;
                    return false;
                case "berichtOntbreekt":
                    return ve != null && ve.HeeftVeld("tekst") && vs == r.Status && (r.Body.Trim().Length == 0 || Uitvoering.IsProblemDetails(r.Body));
                case "status201": return vs == 201 && r.Status == 200;
                case "location":
                    foreach (string m in r.Meldingen) if (m.Contains("Location")) return true;
                    return false;
                case "putIdBody":
                    return r.Methode == "PUT" && r.Status == 400 && vs != 400 && Uitvoering.ProblemDetailsFouten(r.Body).Length == 0;
                case "verplichtVeld":
                    return r.Status == 400 && Uitvoering.ProblemDetailsFouten(r.Body).Length > 0 && (vs != 400 || (ve != null && ve.HeeftVeld("tekst")));
                case "volgorde": return HeeftVerschil(r, "volgorde");
                case "hoofdletters": return HeeftVerschil(r, "hoofdletters");
                case "toestandVerloren": return r.Status == 404 && vs != 404 && VarUitPost(r) && HeeftTreffer("TOESTAND-VERLOREN");
                case "naHerstart":
                    if (soort != "bestand") return false;
                    foreach (StapResultaat x in Eerder(r)) if (x.Soort == "actie" && x.Methode == "herstart") return true;
                    return false;
                case "navigatieGevuld":
                    foreach (Verschil v in r.Verschillen)
                        if (v.Verwacht == "null" && v.Gekregen != null && (v.Gekregen.StartsWith("{") || (v.Gekregen.StartsWith("[") && v.Gekregen != "[]"))) return true;
                    return false;
                case "navigatieLegeLijst":
                    foreach (Verschil v in r.Verschillen) if (v.Verwacht == "null" && v.Gekregen == "[]") return true;
                    return false;
                case "veldOverschreven":
                    {
                        bool put = false;
                        foreach (StapResultaat x in Eerder(r)) if (x.Methode == "PUT" && x.Status == 204) put = true;
                        if (!put) return false;
                        foreach (Verschil v in r.Verschillen) if (v.Soort == "waarde" && IsLeeg(v.Gekregen) && !IsLeeg(v.Verwacht)) return true;
                        return false;
                    }
                case "nietBewaard":
                    {
                        if (r.Location != null && r.Location.EndsWith("/0")) return true;
                        JsonWaarde j;
                        if (r.Methode == "POST" && r.Status == 201 && Json.ProbeerLees(r.Body, out j) && j.IsObject && j.Veld("id") != null && j.Veld("id").AlsTekst() == "0") return true;
                        return r.Status == 404 && vs != 404 && VarUitPost(r);
                    }
                case "queries":
                    foreach (string m in r.Meldingen) if (m.Contains("query")) return true;
                    return false;
                case "includeVergeten":
                    foreach (Verschil v in r.Verschillen)
                        if ((v.Verwacht.StartsWith("[") || v.Verwacht.StartsWith("{") || v.Verwacht == "een lijst" || v.Verwacht == "een object" || v.Verwacht.StartsWith("Id's"))
                            && (v.Gekregen == "null" || v.Gekregen == "(veld ontbreekt)" || v.Gekregen == "(ontbreekt)")) return true;
                    return false;
                case "mapsterLeeg":
                    foreach (Verschil v in r.Verschillen)
                        if ((v.Soort == "waarde" || v.Soort == "ontbreekt" || v.Soort == "type") && IsLeeg(v.Gekregen) && !IsLeeg(v.Verwacht)) return true;
                    return false;
                case "modelTerug":
                    foreach (Verschil v in r.Verschillen)
                        if (v.Soort == "extra" && (v.Gekregen == "null" || v.Gekregen.StartsWith("{") || v.Gekregen.StartsWith("["))) return true;
                    return false;
                default: return false;
            }
        }

        /// <summary>De exception en de eerste regel uit de code van de student, uit de uitvoer van de API.</summary>
        public List<string> Excepties(List<string> regels, int max)
        {
            List<string> l = new List<string>();
            if (regels == null) return l;
            for (int i = 0; i < regels.Count && l.Count < max; i++)
            {
                Match m = Regex.Match(regels[i], @"^\s*((?:[\w]+\.)+\w*Exception)(?::\s*(.*))?$");
                if (!m.Success) continue;
                string tekst = "`" + m.Groups[1].Value + "`" + (m.Groups[2].Success && m.Groups[2].Value.Length > 0 ? ": " + Uitvoering.Kort(m.Groups[2].Value, 220) : "");
                for (int j = i + 1; j < regels.Count && j < i + 40; j++)
                {
                    Match f = Regex.Match(regels[j], @"^\s*at (.+?) in (.+?):line (\d+)");
                    if (!f.Success) continue;
                    string bestand = u != null && u.Api != null ? u.Api.RelatiefPad(f.Groups[2].Value.Trim()) : f.Groups[2].Value;
                    if (bestand.Contains(":") || bestand.StartsWith("/")) continue;   // niet in het project
                    tekst += " — in `" + bestand + "`, regel " + f.Groups[3].Value;
                    break;
                }
                if (!l.Contains(tekst)) l.Add(tekst);
            }
            return l;
        }
    }
}
