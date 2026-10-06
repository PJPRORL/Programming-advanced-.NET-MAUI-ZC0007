// Nakijkscript — motor, deel 5: valkuilen herkennen in de code, in de uitvoer van de API en in de antwoorden.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Nakijkscript
{
    public class Valkuil
    {
        public string Code, Titel, Ernst, Statisch, Antwoord, Waarom, Richting;
        public int Vanaf, Tot;
        public List<string> Log = new List<string>();
        public List<string> Tips = new List<string>();
    }

    public class Treffer
    {
        public string Code, Bestand, Tekst, Route;
        public int Regel;
        public string Plaats { get { return Bestand + (Regel > 0 ? ", regel " + Regel : ""); } }
    }

    public class Bevinding
    {
        public string Code, Sleutel, Titel, Ernst, Waarom, Richting;
        public List<string> Tips = new List<string>();
        public List<string> Waar = new List<string>();
        public List<string> Bewijs = new List<string>();
        public List<StapResultaat> Stappen = new List<StapResultaat>();
        public bool ZonderGevolg;      // fout in de code, maar geen voorbeeld faalt erdoor
    }

    public class CsBestand
    {
        public string Pad;      // relatief
        public string Tekst;
        public string[] Regels;
    }

    public class Methode
    {
        public string Klasse, Naam, Verb, Route, Bestand;
        public int Regel, BeginIndex, EindIndex;
        public List<string> Parameters = new List<string>();
        public string RouteTemplate, MethodeTemplate;
    }

    public static class Catalogus
    {
        public static List<Valkuil> Lees(string pad)
        {
            List<Valkuil> lijst = new List<Valkuil>();
            JsonWaarde j = Json.Lees(File.ReadAllText(pad, Encoding.UTF8));
            foreach (JsonWaarde v in j.Veld("valkuilen").Lijst)
            {
                Valkuil k = new Valkuil();
                k.Code = v.Veld("code").Tekst; k.Titel = v.Veld("titel").Tekst; k.Ernst = v.Veld("ernst").Tekst;
                k.Vanaf = (int)v.Veld("vanaf").Getal; k.Tot = (int)v.Veld("tot").Getal;
                k.Waarom = v.Veld("waarom").Tekst; k.Richting = v.Veld("richting").Tekst;
                if (v.HeeftVeld("statisch")) k.Statisch = v.Veld("statisch").Tekst;
                if (v.HeeftVeld("antwoord")) k.Antwoord = v.Veld("antwoord").Tekst;
                if (v.HeeftVeld("log")) foreach (JsonWaarde l in v.Veld("log").Lijst) k.Log.Add(l.Tekst);
                foreach (JsonWaarde t in v.Veld("tips").Lijst) k.Tips.Add(t.Tekst);
                lijst.Add(k);
            }
            return lijst;
        }
    }

    /// <summary>Kijkt de C#-code van de student na zonder ze uit te voeren.</summary>
    public class CodeAnalyse
    {
        public List<CsBestand> Bestanden = new List<CsBestand>();
        public List<Methode> Methodes = new List<Methode>();
        public CsBestand Program;

        public CodeAnalyse(string projectMap)
        {
            foreach (string f in Directory.GetFiles(projectMap, "*.cs", SearchOption.AllDirectories))
            {
                string rel = f.Substring(projectMap.Length).TrimStart('\\', '/').Replace('\\', '/');
                string laag = "/" + rel.ToLowerInvariant();
                if (laag.Contains("/bin/") || laag.Contains("/obj/") || laag.Contains("/migrations/")) continue;
                CsBestand b = new CsBestand();
                b.Pad = rel;
                b.Tekst = File.ReadAllText(f).Replace("\r\n", "\n");
                b.Regels = b.Tekst.Split('\n');
                Bestanden.Add(b);
                if (Path.GetFileName(f).Equals("Program.cs", StringComparison.OrdinalIgnoreCase)) Program = b;
            }
            foreach (CsBestand b in Bestanden) LeesControllers(b);
        }

        static int RegelVan(string tekst, int index)
        {
            int n = 1;
            for (int i = 0; i < index && i < tekst.Length; i++) if (tekst[i] == '\n') n++;
            return n;
        }

        /// <summary>Aantal open accolades voor positie i, zonder die in tekst en commentaar.</summary>
        public static int Diepte(string t, int tot)
        {
            int d = 0;
            for (int i = 0; i < tot && i < t.Length; i++)
            {
                char c = t[i];
                if (c == '/' && i + 1 < t.Length && t[i + 1] == '/') { while (i < tot && t[i] != '\n') i++; continue; }
                if (c == '/' && i + 1 < t.Length && t[i + 1] == '*') { i += 2; while (i + 1 < t.Length && !(t[i] == '*' && t[i + 1] == '/')) i++; i++; continue; }
                if (c == '\'') { i++; if (i < t.Length && t[i] == '\\') i++; i++; continue; }
                if (c == '"')
                {
                    bool woordelijk = i > 0 && (t[i - 1] == '@' || (i > 1 && t[i - 1] == '$' && t[i - 2] == '@'));
                    i++;
                    while (i < t.Length && i < tot)
                    {
                        if (!woordelijk && t[i] == '\\') { i += 2; continue; }
                        if (t[i] == '"') { if (woordelijk && i + 1 < t.Length && t[i + 1] == '"') { i += 2; continue; } break; }
                        i++;
                    }
                    continue;
                }
                if (c == '{') d++;
                else if (c == '}') d--;
            }
            return d;
        }

        static readonly Regex KlasseRe = new Regex(@"class\s+(\w+)\s*:\s*[^{]*\b(ControllerBase|Controller)\b");
        static readonly Regex AttrRe = new Regex(@"\[\s*(HttpGet|HttpPost|HttpPut|HttpDelete|Route)\s*(?:\(\s*(?:template\s*:\s*)?""([^""]*)""[^\)]*\))?\s*\]");

        void LeesControllers(CsBestand b)
        {
            Match k = KlasseRe.Match(b.Tekst);
            if (!k.Success) return;
            string klasse = k.Groups[1].Value;
            string voor = b.Tekst.Substring(0, k.Index);
            string klasseRoute = "";
            foreach (Match a in AttrRe.Matches(voor)) if (a.Groups[1].Value == "Route") klasseRoute = a.Groups[2].Value;
            string naam = klasse.EndsWith("Controller") ? klasse.Substring(0, klasse.Length - 10) : klasse;
            klasseRoute = klasseRoute.Replace("[controller]", naam);

            // elk HTTP-attribuut, gevolgd door de kop van een methode
            Regex kop = new Regex(@"\G(?:\s*\[[^\]]*\])*\s*(?:public|private|protected|internal)[^\(;{=]*?\s(\w+)\s*\(");
            string t = b.Tekst;
            int van = k.Index;
            List<Methode> deze = new List<Methode>();
            foreach (Match a in AttrRe.Matches(t, van))
            {
                if (a.Groups[1].Value == "Route" && !IsMethodeAttribuut(t, a.Index + a.Length)) continue;
                Match m = kop.Match(t, a.Index + a.Length);
                if (!m.Success) continue;
                Methode me = null;
                foreach (Methode x in deze) if (x.BeginIndex == m.Groups[1].Index) me = x;
                if (me == null)
                {
                    me = new Methode();
                    me.Klasse = klasse; me.Naam = m.Groups[1].Value; me.Bestand = b.Pad;
                    me.BeginIndex = m.Groups[1].Index;
                    me.Regel = RegelVan(t, m.Groups[1].Index);
                    me.Parameters = Parameters(t, m.Index + m.Length);
                    deze.Add(me);
                }
                string verb = a.Groups[1].Value;
                string tpl = a.Groups[2].Success ? a.Groups[2].Value : "";
                if (verb != "Route") me.Verb = verb.Substring(4).ToUpperInvariant();
                if (a.Groups[2].Success || me.MethodeTemplate == null) me.MethodeTemplate = tpl;
            }
            deze.Sort(delegate (Methode x, Methode y) { return x.BeginIndex.CompareTo(y.BeginIndex); });
            for (int i = 0; i < deze.Count; i++)
            {
                Methode me = deze[i];
                me.EindIndex = i + 1 < deze.Count ? deze[i + 1].BeginIndex : t.Length;
                me.RouteTemplate = klasseRoute;
                string mt = me.MethodeTemplate ?? "";
                if (mt.StartsWith("/") || mt.StartsWith("~/")) me.Route = mt.TrimStart('~').TrimStart('/');
                else me.Route = (klasseRoute.Trim('/') + "/" + mt).Trim('/');
                if (me.Verb == null) me.Verb = "GET";
                Methodes.Add(me);
            }
        }

        static bool IsMethodeAttribuut(string t, int na)
        {
            // een [Route] vlak voor "class" hoort bij de klasse
            Match m = Regex.Match(t.Substring(na, Math.Min(400, t.Length - na)), @"^(?:\s*\[[^\]]*\])*\s*(public|private|protected|internal|sealed|abstract)?\s*(?:partial\s+)?class\b");
            return !m.Success;
        }

        static List<string> Parameters(string t, int na)
        {
            List<string> namen = new List<string>();
            int diepte = 0, i = na;
            StringBuilder huidig = new StringBuilder();
            List<string> stukken = new List<string>();
            for (; i < t.Length; i++)
            {
                char c = t[i];
                if (c == '(' || c == '<' || c == '[') diepte++;
                if (c == ')' && diepte == 0) break;
                if (c == ')' || c == '>' || c == ']') diepte--;
                if (c == ',' && diepte == 0) { stukken.Add(huidig.ToString()); huidig.Length = 0; continue; }
                huidig.Append(c);
            }
            if (huidig.ToString().Trim().Length > 0) stukken.Add(huidig.ToString());
            foreach (string s in stukken)
            {
                string zonder = Regex.Replace(s, @"\[[^\]]*\]", " ");
                int gelijk = zonder.IndexOf('=');
                if (gelijk >= 0) zonder = zonder.Substring(0, gelijk);
                Match n = Regex.Match(zonder.Trim(), @"(\w+)$");
                if (n.Success) namen.Add(n.Groups[1].Value);
            }
            return namen;
        }

        static List<string> Argumenten(string t, int na)
        {
            List<string> stukken = new List<string>();
            int diepte = 0;
            StringBuilder huidig = new StringBuilder();
            bool inTekst = false;
            for (int i = na; i < t.Length; i++)
            {
                char c = t[i];
                if (c == '"' && (i == 0 || t[i - 1] != '\\')) inTekst = !inTekst;
                if (!inTekst)
                {
                    if (c == '(' || c == '[' || c == '{') diepte++;
                    if (c == ')' && diepte == 0) break;
                    if (c == ')' || c == ']' || c == '}') diepte--;
                    if (c == ',' && diepte == 0) { stukken.Add(huidig.ToString().Trim()); huidig.Length = 0; continue; }
                }
                huidig.Append(c);
            }
            if (huidig.ToString().Trim().Length > 0) stukken.Add(huidig.ToString().Trim());
            return stukken;
        }

        public static string RouteNaarRegex(string route)
        {
            StringBuilder sb = new StringBuilder("^/?");
            string[] delen = route.Trim('/').Split('/');
            for (int i = 0; i < delen.Length; i++)
            {
                if (i > 0) sb.Append('/');
                string d = delen[i];
                if (d.StartsWith("{") && d.EndsWith("}")) sb.Append(d.StartsWith("{*") ? ".*" : "[^/]+");
                else sb.Append(Regex.Escape(Uri.EscapeDataString(d)));
            }
            sb.Append("/?$");
            return sb.ToString();
        }

        public static List<string> RouteParameters(string route)
        {
            List<string> p = new List<string>();
            foreach (Match m in Regex.Matches(route ?? "", @"\{\*{0,2}(\w+)(?::[^}]*)?\??\}")) p.Add(m.Groups[1].Value);
            return p;
        }

        Treffer Maak(string code, CsBestand b, int index, string tekst)
        {
            Treffer t = new Treffer();
            t.Code = code; t.Bestand = b.Pad; t.Regel = RegelVan(b.Tekst, index); t.Tekst = tekst;
            return t;
        }

        CsBestand BestandVan(Methode m)
        {
            foreach (CsBestand b in Bestanden) if (b.Pad == m.Bestand) return b;
            return null;
        }

        static bool IsDto(CsBestand b) { string l = "/" + b.Pad.ToLowerInvariant(); return l.Contains("/dtos/") || l.Contains("/dto/") || Regex.IsMatch(b.Tekst, @"class\s+\w+Dto\b") || Regex.IsMatch(b.Tekst, @"record\s+\w+Dto\b"); }
        static bool IsModel(CsBestand b) { return ("/" + b.Pad.ToLowerInvariant()).Contains("/models/"); }

        public List<Treffer> Detecteer(string regel, string code)
        {
            List<Treffer> r = new List<Treffer>();
            switch (regel)
            {
                case "routeParamNaam":
                    foreach (Methode m in Methodes)
                    {
                        List<string> ontbrekend = new List<string>();
                        foreach (string p in RouteParameters(m.Route))
                        {
                            bool gevonden = false;
                            foreach (string q in m.Parameters) if (string.Equals(p, q, StringComparison.OrdinalIgnoreCase)) gevonden = true;
                            if (!gevonden) ontbrekend.Add("{" + p + "}");
                        }
                        if (ontbrekend.Count == 0) continue;
                        Treffer t = new Treffer();
                        t.Code = code; t.Bestand = m.Bestand; t.Regel = m.Regel; t.Route = m.Route;
                        t.Tekst = "`" + m.Naam + "`: de route `" + m.Route + "` gebruikt " + string.Join(" en ", ontbrekend.ToArray()) +
                                  ", de methode heeft " + (m.Parameters.Count == 0 ? "geen parameters" : "de parameters `" + string.Join("`, `", m.Parameters.ToArray()) + "`") + ".";
                        r.Add(t);
                    }
                    break;
                case "argumentVolgorde":
                    foreach (CsBestand b in Bestanden)
                    {
                        Dictionary<string, List<string>> sig = new Dictionary<string, List<string>>();
                        Dictionary<string, int> sigIndex = new Dictionary<string, int>();
                        foreach (Match s in Regex.Matches(b.Tekst, @"(?:public|private|protected|internal)[^\(;{=\n]*?\s(\w+)\s*\("))
                        {
                            List<string> ps = Parameters(b.Tekst, s.Index + s.Length);
                            if (ps.Count >= 2 && !sig.ContainsKey(s.Groups[1].Value)) { sig[s.Groups[1].Value] = ps; sigIndex[s.Groups[1].Value] = s.Groups[1].Index; }
                        }
                        foreach (Match c in Regex.Matches(b.Tekst, @"\b(\w+)\s*\("))
                        {
                            List<string> ps;
                            if (!sig.TryGetValue(c.Groups[1].Value, out ps) || sigIndex[c.Groups[1].Value] == c.Groups[1].Index) continue;
                            List<string> args = Argumenten(b.Tekst, c.Index + c.Length);
                            for (int i = 0; i < args.Count && i < ps.Count; i++)
                            {
                                if (!Regex.IsMatch(args[i], @"^\w+$") || string.Equals(args[i], ps[i], StringComparison.OrdinalIgnoreCase)) continue;
                                string arg = args[i].ToLowerInvariant(), eigen = ps[i].ToLowerInvariant();
                                int j = -1;
                                for (int k2 = 0; k2 < ps.Count; k2++)
                                {
                                    if (k2 == i) continue;
                                    string ander = ps[k2].ToLowerInvariant();
                                    if (arg == ander || (ander.Length >= 4 && arg.Contains(ander) && !arg.Contains(eigen) && !(eigen.Length >= 4 && eigen.Contains(ander)))) { j = k2; break; }
                                }
                                if (j < 0) continue;
                                Treffer t = Maak(code, b, c.Index, "`" + c.Groups[1].Value + "(" + string.Join(", ", args.ToArray()) + ")`: `" + args[i] + "` staat op de plaats van `" + ps[i] + "`; de methode verwacht `" + c.Groups[1].Value + "(" + string.Join(", ", ps.ToArray()) + ")`.");
                                foreach (Methode m in Methodes) if (m.Bestand == b.Pad && c.Index >= m.BeginIndex && c.Index < m.EindIndex) t.Route = m.Route;
                                r.Add(t);
                                break;
                            }
                        }
                    }
                    break;
                case "routeSlash":
                    foreach (Methode m in Methodes)
                        if (m.MethodeTemplate != null && (m.MethodeTemplate.StartsWith("/") || m.MethodeTemplate.StartsWith("~/")) && m.RouteTemplate.Length > 0)
                        {
                            Treffer t = new Treffer();
                            t.Code = code; t.Bestand = m.Bestand; t.Regel = m.Regel;
                            t.Route = (m.RouteTemplate.Trim('/') + "/" + m.MethodeTemplate.TrimStart('~').TrimStart('/')).Trim('/');
                            t.Tekst = "`" + m.Naam + "`: de route `" + m.MethodeTemplate + "` begint met een `/`, de controller heeft `" + m.RouteTemplate + "`.";
                            r.Add(t);
                        }
                    break;
                case "createdAtActionNaarZichzelf":
                    foreach (Methode m in Methodes)
                    {
                        CsBestand b = BestandVan(m);
                        string lijf = b.Tekst.Substring(m.BeginIndex, m.EindIndex - m.BeginIndex);
                        Match c = Regex.Match(lijf, @"CreatedAtAction\s*\(\s*nameof\s*\(\s*" + Regex.Escape(m.Naam) + @"\s*\)");
                        if (c.Success) r.Add(Maak(code, b, m.BeginIndex + c.Index, "`" + m.Naam + "` verwijst in `CreatedAtAction` naar zichzelf."));
                    }
                    break;
                case "createdAtActionAsync":
                    if (Program != null && Program.Tekst.Contains("SuppressAsyncSuffixInActionNames")) break;
                    foreach (CsBestand b in Bestanden)
                        foreach (Match c in Regex.Matches(b.Tekst, @"CreatedAtAction\s*\(\s*nameof\s*\(\s*(\w+Async)\s*\)"))
                            r.Add(Maak(code, b, c.Index, "`CreatedAtAction(nameof(" + c.Groups[1].Value + "), ...)`"));
                    break;
                case "putIdBody":
                    foreach (Methode m in Methodes)
                    {
                        if (m.Verb != "PUT") continue;
                        CsBestand b = BestandVan(m);
                        string lijf = b.Tekst.Substring(m.BeginIndex, m.EindIndex - m.BeginIndex);
                        Match c = Regex.Match(lijf, @"(\b\w+\s*!=\s*\w+\.Id\b|\b\w+\.Id\s*!=\s*\w+\b)");
                        if (c.Success) r.Add(Maak(code, b, m.BeginIndex + c.Index, "`" + m.Naam + "`: `" + c.Value.Trim() + "`"));
                    }
                    break;
                case "lijstNietStatisch":
                    foreach (CsBestand b in Bestanden)
                    {
                        Match kl = Regex.Match(b.Tekst, @"class\s+(\w*(Repository|Controller))\b");
                        if (!kl.Success) continue;
                        // een repository die als singleton geregistreerd is, leeft zo lang als de API: dan mag de lijst gewoon zijn
                        if (Program != null && Regex.IsMatch(Program.Tekst, @"AddSingleton\s*<\s*(\w+\s*,\s*)?" + kl.Groups[1].Value + @"\s*>")) continue;
                        int klasseDiepte = Diepte(b.Tekst, kl.Index);
                        // enkel velden van de klasse zelf, geen lokale variabelen in een methode
                        foreach (Match c in Regex.Matches(b.Tekst, @"^[ \t]*(?:(?:private|public|protected|internal)[ \t]+)?(?:readonly[ \t]+)?List<\w+>[ \t]+(\w+)[ \t]*(=|;)", RegexOptions.Multiline))
                            if (Diepte(b.Tekst, c.Index) == klasseDiepte + 1)
                                r.Add(Maak(code, b, c.Index, "`" + c.Groups[1].Value + "` is een gewone lijst in `" + Path.GetFileName(b.Pad) + "`."));
                    }
                    break;
                case "saveChangesInRepository":
                    foreach (CsBestand b in Bestanden)
                    {
                        if (!b.Pad.ToLowerInvariant().Contains("repositor") || b.Pad.ToLowerInvariant().Contains("unitofwork")) continue;
                        if (Regex.IsMatch(b.Tekst, @"class\s+\w*UnitOfWork")) continue;
                        Match c = Regex.Match(b.Tekst, @"SaveChanges(Async)?\s*\(");
                        if (c.Success) r.Add(Maak(code, b, c.Index, "`" + Path.GetFileName(b.Pad) + "` roept `" + c.Value.TrimEnd('(').Trim() + "` aan."));
                    }
                    break;
                case "programRepositories":
                    if (Program == null) break;
                    foreach (Match c in Regex.Matches(Program.Tekst, @"Add(Scoped|Transient|Singleton)\s*<\s*(I?\w*Repository\w*)"))
                        r.Add(Maak(code, Program, c.Index, "`" + c.Value + "...`"));
                    break;
                case "controllerRepository":
                    foreach (CsBestand b in Bestanden)
                    {
                        Match k = KlasseRe.Match(b.Tekst);
                        if (!k.Success) continue;
                        Match c = Regex.Match(b.Tekst, @"public\s+" + Regex.Escape(k.Groups[1].Value) + @"\s*\(([^)]*)\)");
                        if (!c.Success) continue;
                        Match p = Regex.Match(c.Groups[1].Value, @"\bI\w*Repository\w*\b");
                        if (p.Success && !Regex.IsMatch(p.Value, "UnitOfWork")) r.Add(Maak(code, b, c.Index, "`" + k.Groups[1].Value + "` vraagt `" + p.Value + "`."));
                    }
                    break;
                case "includeInGeneriek":
                    foreach (CsBestand b in Bestanden)
                    {
                        if (!Regex.IsMatch(b.Tekst, @"class\s+Generic\w*Repository")) continue;
                        Match c = Regex.Match(b.Tekst, @"\.Include\s*\(");
                        if (c.Success) r.Add(Maak(code, b, c.Index, "`" + Path.GetFileName(b.Pad) + "` gebruikt `Include`."));
                    }
                    break;
                case "ignoreCyclesOntbreekt":
                    if (Program != null && !Program.Tekst.Contains("IgnoreCycles")) { Treffer t = Maak(code, Program, 0, "Program.cs"); t.Regel = 0; r.Add(t); }
                    break;
                case "ignoreCyclesAanwezig":
                    if (Program != null && Program.Tekst.Contains("IgnoreCycles")) r.Add(Maak(code, Program, Program.Tekst.IndexOf("IgnoreCycles"), "Program.cs"));
                    break;
                case "mapsterScanOntbreekt":
                    {
                        CsBestand profiel = null;
                        foreach (CsBestand b in Bestanden) if (Regex.IsMatch(b.Tekst, @":\s*IRegister\b")) profiel = b;
                        if (profiel != null && Program != null && !Regex.IsMatch(Program.Tekst, @"\.Scan\s*\("))
                            r.Add(Maak(code, Program, 0, "`" + Path.GetFileName(profiel.Pad) + "` implementeert `IRegister`, Program.cs scant niet."));
                        break;
                    }
                case "modelInDto":
                    {
                        List<string> modellen = new List<string>();
                        foreach (CsBestand b in Bestanden) if (IsModel(b)) foreach (Match c in Regex.Matches(b.Tekst, @"(?:class|record)\s+(\w+)")) modellen.Add(c.Groups[1].Value);
                        foreach (CsBestand b in Bestanden)
                        {
                            if (!IsDto(b) || IsModel(b)) continue;
                            foreach (Match c in Regex.Matches(b.Tekst, @"public\s+([\w<>\[\]?,\s]+?)\s+(\w+)\s*\{\s*get"))
                                foreach (string m in modellen)
                                    if (Regex.IsMatch(c.Groups[1].Value, @"\b" + Regex.Escape(m) + @"\b"))
                                        r.Add(Maak(code, b, c.Index, "`" + c.Groups[2].Value + "` heeft het type `" + c.Groups[1].Value.Trim() + "`."));
                        }
                        break;
                    }
                case "dubbeleNewConfig":
                    {
                        Dictionary<string, int> paren = new Dictionary<string, int>();
                        foreach (CsBestand b in Bestanden)
                            foreach (Match c in Regex.Matches(b.Tekst, @"NewConfig\s*<\s*(\w+)\s*,\s*(\w+)\s*>"))
                            {
                                string paar = c.Groups[1].Value + " → " + c.Groups[2].Value;
                                int n; paren.TryGetValue(paar, out n); paren[paar] = n + 1;
                                if (n + 1 == 2) r.Add(Maak(code, b, c.Index, "`NewConfig<" + c.Groups[1].Value + ", " + c.Groups[2].Value + ">` staat er minstens twee keer."));
                            }
                        break;
                    }
                case "dataAnnotations":
                    foreach (CsBestand b in Bestanden)
                    {
                        if (!IsDto(b)) continue;
                        Match c = Regex.Match(b.Tekst, @"\[\s*(Required|MaxLength|MinLength|StringLength|Range|EmailAddress)\b");
                        if (c.Success) r.Add(Maak(code, b, c.Index, "`[" + c.Groups[1].Value + "]` in `" + Path.GetFileName(b.Pad) + "`."));
                    }
                    break;
                case "navigatieNietNullable":
                    foreach (CsBestand b in Bestanden)
                    {
                        if (!IsModel(b)) continue;
                        foreach (Match c in Regex.Matches(b.Tekst, @"public\s+(?:List|ICollection|IEnumerable)<\w+>\s+(\w+)\s*\{\s*get;\s*set;\s*\}(\s*=\s*(default!|null!))?"))
                        {
                            if (c.Groups[2].Success && c.Groups[2].Value.Length > 0 && !c.Groups[3].Success) continue;
                            r.Add(Maak(code, b, c.Index, "`" + c.Groups[1].Value + "` in `" + Path.GetFileName(b.Pad) + "`."));
                        }
                    }
                    break;
                case "dollarZonderAccolade":
                    foreach (CsBestand b in Bestanden)
                        foreach (Match c in Regex.Matches(b.Tekst, @"(?<![@\w])\$""((?:[^""{\\\n]|\\.)*)"""))
                            r.Add(Maak(code, b, c.Index, "`$\"" + Uitvoering.Kort(c.Groups[1].Value, 50) + "\"`"));
                    break;
                case "accoladeZonderDollar":
                    // een gewone tekst met {naam} erin, buiten attributen en logberichten (die gebruiken {Naam} bewust zonder $)
                    foreach (CsBestand b in Bestanden)
                        for (int i = 0; i < b.Regels.Length; i++)
                        {
                            string lijn = b.Regels[i];
                            string kaal = lijn.Trim();
                            if (kaal.StartsWith("[") || kaal.StartsWith("//") || Regex.IsMatch(lijn, @"\bLog\w*\s*\(|\.Log\(|MapGet|MapPost|string\.Format")) continue;
                            foreach (Match c in Regex.Matches(lijn, @"(?<![$@\w""])""((?:[^""\\]|\\.)*\{[A-Za-z_]\w*(?:\.\w+)*\}(?:[^""\\]|\\.)*)"""))
                            {
                                Treffer t = new Treffer();
                                t.Code = code; t.Bestand = b.Pad; t.Regel = i + 1; t.Tekst = "`\"" + Uitvoering.Kort(c.Groups[1].Value, 60) + "\"`";
                                r.Add(t);
                            }
                        }
                    break;
                case "legeRegelAttribuut":
                    foreach (CsBestand b in Bestanden)
                        for (int i = 0; i + 2 < b.Regels.Length; i++)
                            if (Regex.IsMatch(b.Regels[i], @"^\s*\[(Http\w+|Route)\b.*\]\s*$") && b.Regels[i + 1].Trim().Length == 0 && Regex.IsMatch(b.Regels[i + 2], @"^\s*(\[|public)"))
                            {
                                Treffer t = new Treffer();
                                t.Code = code; t.Bestand = b.Pad; t.Regel = i + 1; t.Tekst = "`" + b.Regels[i].Trim() + "`";
                                r.Add(t);
                            }
                    break;
            }
            return r;
        }
    }
}
