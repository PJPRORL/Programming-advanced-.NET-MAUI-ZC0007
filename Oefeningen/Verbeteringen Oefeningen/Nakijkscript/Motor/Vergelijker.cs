// Nakijkscript — motor, deel 2: een antwoord vergelijken met wat de opgave verwacht.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Nakijkscript
{
    public class Verschil
    {
        public string Pad;        // bv. $.artikelen[1].prijs
        public string Verwacht;
        public string Gekregen;
        public string Soort;      // waarde, ontbreekt, extra, hoofdletters, volgorde, aantal, type
        public override string ToString() { return Pad + ": verwacht " + Verwacht + ", gekregen " + Gekregen; }
    }

    public class Vergelijker
    {
        public const decimal Tolerantie = 0.005m;
        static readonly Regex VarPatroon = new Regex(@"\{\{(\w+)\}\}");
        readonly Dictionary<string, string> vars;
        public List<Verschil> Verschillen = new List<Verschil>();

        public Vergelijker(Dictionary<string, string> variabelen) { vars = variabelen ?? new Dictionary<string, string>(); }

        public static List<Verschil> Vergelijk(JsonWaarde verwacht, JsonWaarde gekregen, Dictionary<string, string> variabelen)
        {
            Vergelijker v = new Vergelijker(variabelen);
            v.Vergelijk(verwacht, gekregen, "$");
            return v.Verschillen;
        }

        public string Vul(string t)
        {
            if (t == null) return null;
            return VarPatroon.Replace(t, delegate (Match m)
            {
                string w;
                return vars.TryGetValue(m.Groups[1].Value, out w) ? w : m.Value;
            });
        }

        void Fout(string pad, string verwacht, string gekregen, string soort)
        {
            Verschil v = new Verschil();
            v.Pad = pad; v.Verwacht = verwacht; v.Gekregen = gekregen; v.Soort = soort;
            Verschillen.Add(v);
        }

        string Toon(JsonWaarde w)
        {
            if (w == null) return "(niets)";
            if (IsMatcher(w) || w.IsObject || w.IsLijst || w.Soort == JsonSoort.Tekst)
            {
                string l = Uitvoering.Leesbaar(w, this);
                return l.Length > 160 ? l.Substring(0, 157) + "..." : l;
            }
            string t = Json.Schrijf(w, false);
            return t.Length > 160 ? t.Substring(0, 157) + "..." : t;
        }

        public static bool IsMatcher(JsonWaarde w)
        {
            return w != null && w.IsObject && w.Velden.Count == 1 && w.Velden[0].Key.StartsWith("$");
        }

        /// <summary>True als er geen verschil is, zonder iets aan de lijst toe te voegen.</summary>
        bool Past(JsonWaarde verwacht, JsonWaarde gekregen)
        {
            Vergelijker v = new Vergelijker(vars);
            v.Vergelijk(verwacht, gekregen, "$");
            return v.Verschillen.Count == 0;
        }

        public void Vergelijk(JsonWaarde verwacht, JsonWaarde gekregen, string pad)
        {
            if (IsMatcher(verwacht)) { Matcher(verwacht.Velden[0].Key, verwacht.Velden[0].Value, gekregen, pad); return; }
            if (gekregen == null) { Fout(pad, Toon(verwacht), "(ontbreekt)", "ontbreekt"); return; }

            switch (verwacht.Soort)
            {
                case JsonSoort.Null:
                    if (gekregen.Soort != JsonSoort.Null) Fout(pad, "null", Toon(gekregen), "waarde");
                    return;
                case JsonSoort.Bool:
                    if (gekregen.Soort != JsonSoort.Bool || gekregen.Bool != verwacht.Bool) Fout(pad, Toon(verwacht), Toon(gekregen), "waarde");
                    return;
                case JsonSoort.Getal:
                    if (gekregen.Soort != JsonSoort.Getal || Math.Abs(gekregen.Getal - verwacht.Getal) > Tolerantie)
                        Fout(pad, verwacht.GetalTekst, Toon(gekregen), gekregen.Soort == JsonSoort.Getal ? "waarde" : "type");
                    return;
                case JsonSoort.Tekst:
                    VergelijkTekst(verwacht.Tekst, gekregen, pad);
                    return;
                case JsonSoort.Lijst:
                    if (!gekregen.IsLijst) { Fout(pad, "een lijst", Toon(gekregen), "type"); return; }
                    if (gekregen.Lijst.Count != verwacht.Lijst.Count)
                    {
                        Fout(pad, verwacht.Lijst.Count + " elementen", gekregen.Lijst.Count + " elementen", "aantal");
                        return;
                    }
                    for (int i = 0; i < verwacht.Lijst.Count; i++) Vergelijk(verwacht.Lijst[i], gekregen.Lijst[i], pad + "[" + i + "]");
                    return;
                default:
                    VergelijkObject(verwacht, gekregen, pad, true);
                    return;
            }
        }

        void VergelijkTekst(string verwacht, JsonWaarde gekregen, string pad)
        {
            Match m = Regex.Match(verwacht, @"^\{\{(\w+)\}\}$");
            string w;
            if (m.Success && vars.TryGetValue(m.Groups[1].Value, out w) && gekregen.Soort == JsonSoort.Getal)
            {
                decimal d;
                if (decimal.TryParse(w, NumberStyles.Float, CultureInfo.InvariantCulture, out d) && Math.Abs(d - gekregen.Getal) <= Tolerantie) return;
                Fout(pad, w, Toon(gekregen), "waarde");
                return;
            }
            string ingevuld = Vul(verwacht);
            if (gekregen.Soort == JsonSoort.Tekst && gekregen.Tekst == ingevuld) return;
            if (gekregen.Soort == JsonSoort.Getal && m.Success)
            {
                decimal d;
                if (decimal.TryParse(ingevuld, NumberStyles.Float, CultureInfo.InvariantCulture, out d) && Math.Abs(d - gekregen.Getal) <= Tolerantie) return;
            }
            Fout(pad, Json.Tekst(ingevuld), Toon(gekregen), gekregen.Soort == JsonSoort.Tekst ? "waarde" : "type");
        }

        void VergelijkObject(JsonWaarde verwacht, JsonWaarde gekregen, string pad, bool exact)
        {
            if (!gekregen.IsObject) { Fout(pad, "een object", Toon(gekregen), "type"); return; }
            foreach (KeyValuePair<string, JsonWaarde> kv in verwacht.Velden)
            {
                string sub = pad + "." + kv.Key;
                JsonWaarde g = gekregen.Veld(kv.Key);
                bool moetOntbreken = IsMatcher(kv.Value) && kv.Value.Velden[0].Key == "$ontbreekt";
                if (moetOntbreken)
                {
                    string echt;
                    JsonWaarde g2 = g ?? gekregen.VeldZonderHoofdletters(kv.Key, out echt);
                    if (g2 != null) Fout(sub, "geen veld " + kv.Key, Toon(g2), "extra");
                    continue;
                }
                if (g == null)
                {
                    string echt;
                    JsonWaarde g2 = gekregen.VeldZonderHoofdletters(kv.Key, out echt);
                    if (g2 != null) { Fout(sub, "veld \"" + kv.Key + "\"", "veld \"" + echt + "\"", "hoofdletters"); Vergelijk(kv.Value, g2, pad + "." + echt); }
                    else Fout(sub, Toon(kv.Value), "(veld ontbreekt)", "ontbreekt");
                    continue;
                }
                Vergelijk(kv.Value, g, sub);
            }
            if (!exact) return;
            foreach (KeyValuePair<string, JsonWaarde> kv in gekregen.Velden)
            {
                if (verwacht.Veld(kv.Key) != null) continue;
                string echt;
                if (verwacht.VeldZonderHoofdletters(kv.Key, out echt) != null) continue;   // al gemeld als hoofdletters
                Fout(pad + "." + kv.Key, "(geen veld " + kv.Key + ")", Toon(kv.Value), "extra");
            }
        }

        void Matcher(string naam, JsonWaarde arg, JsonWaarde gekregen, string pad)
        {
            switch (naam)
            {
                case "$elk":
                    if (gekregen == null) Fout(pad, "een waarde", "(ontbreekt)", "ontbreekt");
                    return;
                case "$ontbreekt":
                    if (gekregen != null) Fout(pad, "(geen veld)", Toon(gekregen), "extra");
                    return;
                case "$bevat":
                    if (gekregen == null) { Fout(pad, "een object", "(ontbreekt)", "ontbreekt"); return; }
                    VergelijkObject(arg, gekregen, pad, false);
                    return;
                case "$tekstBevat":
                    {
                        string stuk = Vul(arg.Tekst);
                        if (gekregen == null || gekregen.Soort != JsonSoort.Tekst || gekregen.Tekst.IndexOf(stuk, StringComparison.Ordinal) < 0)
                            Fout(pad, "tekst met " + Json.Tekst(stuk), Toon(gekregen), "waarde");
                        return;
                    }
                case "$reken":
                    {
                        decimal d;
                        string fout;
                        if (!Reken(arg.Tekst, out d, out fout)) { Fout(pad, "berekening " + arg.Tekst + " (" + fout + ")", Toon(gekregen), "waarde"); return; }
                        if (gekregen == null || gekregen.Soort != JsonSoort.Getal || Math.Abs(gekregen.Getal - d) > Tolerantie)
                            Fout(pad, d.ToString(CultureInfo.InvariantCulture), Toon(gekregen), "waarde");
                        return;
                    }
            }
            // vanaf hier: matchers op lijsten
            if (gekregen == null || !gekregen.IsLijst) { Fout(pad, "een lijst", Toon(gekregen), gekregen == null ? "ontbreekt" : "type"); return; }
            List<JsonWaarde> l = gekregen.Lijst;
            switch (naam)
            {
                case "$aantal":
                    if (l.Count != (int)arg.Getal) Fout(pad, ((int)arg.Getal) + " elementen", l.Count + " elementen", "aantal");
                    return;
                case "$ids":
                case "$idsInVolgorde":
                    {
                        List<string> verwachteIds = new List<string>();
                        foreach (JsonWaarde x in arg.Lijst) verwachteIds.Add(NormaalId(Vul(x.AlsTekst())));
                        List<string> echteIds = new List<string>();
                        foreach (JsonWaarde e in l)
                        {
                            string echt;
                            JsonWaarde id = e.IsObject ? (e.Veld("id") ?? e.VeldZonderHoofdletters("id", out echt)) : null;
                            echteIds.Add(id == null ? "?" : NormaalId(id.AlsTekst()));
                        }
                        string v = "[" + string.Join(", ", verwachteIds.ToArray()) + "]";
                        string g = "[" + string.Join(", ", echteIds.ToArray()) + "]";
                        if (naam == "$idsInVolgorde")
                        {
                            if (v != g)
                            {
                                List<string> a = new List<string>(verwachteIds); a.Sort(StringComparer.Ordinal);
                                List<string> b = new List<string>(echteIds); b.Sort(StringComparer.Ordinal);
                                bool zelfde = string.Join(",", a.ToArray()) == string.Join(",", b.ToArray());
                                Fout(pad, "Id's " + v, "Id's " + g, zelfde ? "volgorde" : "waarde");
                            }
                        }
                        else
                        {
                            List<string> a = new List<string>(verwachteIds); a.Sort(StringComparer.Ordinal);
                            List<string> b = new List<string>(echteIds); b.Sort(StringComparer.Ordinal);
                            if (string.Join(",", a.ToArray()) != string.Join(",", b.ToArray())) Fout(pad, "Id's " + v + " (volgorde vrij)", "Id's " + g, "waarde");
                        }
                        return;
                    }
                case "$verzameling":
                    {
                        if (l.Count != arg.Lijst.Count) { Fout(pad, arg.Lijst.Count + " elementen", l.Count + " elementen", "aantal"); return; }
                        bool[] gebruikt = new bool[l.Count];
                        for (int i = 0; i < arg.Lijst.Count; i++)
                        {
                            bool gevonden = false;
                            for (int j = 0; j < l.Count && !gevonden; j++)
                                if (!gebruikt[j] && Past(arg.Lijst[i], l[j])) { gebruikt[j] = true; gevonden = true; }
                            if (!gevonden) Fout(pad, "een element " + Toon(arg.Lijst[i]), "geen passend element in " + Toon(gekregen), "waarde");
                        }
                        return;
                    }
                case "$elkElement":
                    for (int i = 0; i < l.Count; i++) Vergelijk(arg, l[i], pad + "[" + i + "]");
                    return;
                case "$heeftElement":
                    foreach (JsonWaarde e in l) if (Past(arg, e)) return;
                    Fout(pad, "een element " + Toon(arg), "geen passend element in " + Toon(gekregen), "waarde");
                    return;
                case "$geenElement":
                    for (int i = 0; i < l.Count; i++)
                        if (Past(arg, l[i])) Fout(pad + "[" + i + "]", "geen element " + Toon(arg), Toon(l[i]), "extra");
                    return;
            }
            Fout(pad, "bekende matcher", naam, "type");
        }

        static string NormaalId(string t)
        {
            decimal d;
            if (decimal.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out d)) return decimal.Truncate(d) == d ? decimal.Truncate(d).ToString(CultureInfo.InvariantCulture) : d.ToString(CultureInfo.InvariantCulture);
            return t;
        }

        public bool Reken(string uitdrukking, out decimal uitkomst, out string fout)
        {
            uitkomst = 0; fout = null;
            string t = Vul(uitdrukking);
            if (VarPatroon.IsMatch(t)) { fout = "onbekende variabele"; return false; }
            MatchCollection delen = Regex.Matches(t, @"\s*([+-]?)\s*(-?\d+(?:\.\d+)?)");
            int gelezen = 0;
            bool eerste = true;
            foreach (Match m in delen)
            {
                if (m.Index != gelezen) { fout = "onleesbaar"; return false; }
                gelezen = m.Index + m.Length;
                string teken = m.Groups[1].Value;
                if (!eerste && teken.Length == 0) { fout = "operator ontbreekt"; return false; }
                decimal d = decimal.Parse(m.Groups[2].Value, NumberStyles.Float, CultureInfo.InvariantCulture);
                uitkomst += teken == "-" ? -d : d;
                eerste = false;
            }
            if (t.Substring(gelezen).Trim().Length > 0 || eerste) { fout = "onleesbaar"; return false; }
            return true;
        }
    }
}
