// Nakijkscript — motor, deel 1: JSON lezen en schrijven.
// Geschreven in C# 5, zodat Windows PowerShell 5.1 het met Add-Type kan compileren.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Nakijkscript
{
    public enum JsonSoort { Null, Bool, Getal, Tekst, Lijst, Object }

    public class JsonWaarde
    {
        public JsonSoort Soort;
        public bool Bool;
        public decimal Getal;
        public string GetalTekst;   // het getal zoals het in de JSON stond
        public string Tekst;
        public List<JsonWaarde> Lijst;
        public List<KeyValuePair<string, JsonWaarde>> Velden;

        public static JsonWaarde MaakNull() { JsonWaarde w = new JsonWaarde(); w.Soort = JsonSoort.Null; return w; }
        public static JsonWaarde MaakTekst(string t) { JsonWaarde w = new JsonWaarde(); w.Soort = JsonSoort.Tekst; w.Tekst = t; return w; }
        public static JsonWaarde MaakGetal(decimal g)
        {
            JsonWaarde w = new JsonWaarde(); w.Soort = JsonSoort.Getal; w.Getal = g;
            w.GetalTekst = g.ToString(CultureInfo.InvariantCulture); return w;
        }
        public static JsonWaarde MaakBool(bool b) { JsonWaarde w = new JsonWaarde(); w.Soort = JsonSoort.Bool; w.Bool = b; return w; }
        public static JsonWaarde MaakLijst() { JsonWaarde w = new JsonWaarde(); w.Soort = JsonSoort.Lijst; w.Lijst = new List<JsonWaarde>(); return w; }
        public static JsonWaarde MaakObject() { JsonWaarde w = new JsonWaarde(); w.Soort = JsonSoort.Object; w.Velden = new List<KeyValuePair<string, JsonWaarde>>(); return w; }

        public bool IsObject { get { return Soort == JsonSoort.Object; } }
        public bool IsLijst { get { return Soort == JsonSoort.Lijst; } }

        /// <summary>Veld op exacte naam, of null.</summary>
        public JsonWaarde Veld(string naam)
        {
            if (Velden == null) return null;
            foreach (KeyValuePair<string, JsonWaarde> kv in Velden) if (kv.Key == naam) return kv.Value;
            return null;
        }

        /// <summary>Veld zonder op hoofdletters te letten, of null.</summary>
        public JsonWaarde VeldZonderHoofdletters(string naam, out string echteNaam)
        {
            echteNaam = null;
            if (Velden == null) return null;
            foreach (KeyValuePair<string, JsonWaarde> kv in Velden)
                if (string.Equals(kv.Key, naam, StringComparison.OrdinalIgnoreCase)) { echteNaam = kv.Key; return kv.Value; }
            return null;
        }

        public bool HeeftVeld(string naam) { return Veld(naam) != null; }

        public void ZetVeld(string naam, JsonWaarde w)
        {
            for (int i = 0; i < Velden.Count; i++)
                if (Velden[i].Key == naam) { Velden[i] = new KeyValuePair<string, JsonWaarde>(naam, w); return; }
            Velden.Add(new KeyValuePair<string, JsonWaarde>(naam, w));
        }

        public string AlsTekst()
        {
            if (Soort == JsonSoort.Tekst) return Tekst;
            if (Soort == JsonSoort.Getal) return GetalTekst;
            if (Soort == JsonSoort.Bool) return Bool ? "true" : "false";
            if (Soort == JsonSoort.Null) return "";
            return Json.Schrijf(this, false);
        }

        public override string ToString() { return Json.Schrijf(this, false); }
    }

    public class JsonFout : Exception
    {
        public JsonFout(string bericht) : base(bericht) { }
    }

    public static class Json
    {
        public static JsonWaarde Lees(string tekst)
        {
            if (tekst == null) throw new JsonFout("geen tekst");
            int i = 0;
            OverslaWit(tekst, ref i);
            JsonWaarde w = LeesWaarde(tekst, ref i);
            OverslaWit(tekst, ref i);
            if (i != tekst.Length) throw new JsonFout("onverwachte tekens na de JSON op positie " + i);
            return w;
        }

        public static bool ProbeerLees(string tekst, out JsonWaarde w)
        {
            w = null;
            if (tekst == null) return false;
            string t = tekst.Trim();
            if (t.Length == 0) return false;
            char c = t[0];
            if (c != '{' && c != '[' && c != '"' && c != '-' && !char.IsDigit(c) && t != "true" && t != "false" && t != "null") return false;
            try { w = Lees(t); return true; }
            catch (JsonFout) { return false; }
        }

        static void OverslaWit(string s, ref int i)
        {
            while (i < s.Length && (s[i] == ' ' || s[i] == '\t' || s[i] == '\r' || s[i] == '\n' || s[i] == '﻿')) i++;
        }

        static JsonWaarde LeesWaarde(string s, ref int i)
        {
            if (i >= s.Length) throw new JsonFout("onverwacht einde");
            char c = s[i];
            if (c == '{') return LeesObject(s, ref i);
            if (c == '[') return LeesLijst(s, ref i);
            if (c == '"') return JsonWaarde.MaakTekst(LeesTekst(s, ref i));
            if (c == 't' && Begint(s, i, "true")) { i += 4; return JsonWaarde.MaakBool(true); }
            if (c == 'f' && Begint(s, i, "false")) { i += 5; return JsonWaarde.MaakBool(false); }
            if (c == 'n' && Begint(s, i, "null")) { i += 4; return JsonWaarde.MaakNull(); }
            if (c == '-' || (c >= '0' && c <= '9')) return LeesGetal(s, ref i);
            throw new JsonFout("onverwacht teken '" + c + "' op positie " + i);
        }

        static bool Begint(string s, int i, string woord)
        {
            return string.CompareOrdinal(s, i, woord, 0, woord.Length) == 0;
        }

        static JsonWaarde LeesObject(string s, ref int i)
        {
            JsonWaarde o = JsonWaarde.MaakObject();
            i++;
            OverslaWit(s, ref i);
            if (i < s.Length && s[i] == '}') { i++; return o; }
            while (true)
            {
                OverslaWit(s, ref i);
                if (i >= s.Length || s[i] != '"') throw new JsonFout("sleutel verwacht op positie " + i);
                string sleutel = LeesTekst(s, ref i);
                OverslaWit(s, ref i);
                if (i >= s.Length || s[i] != ':') throw new JsonFout("':' verwacht op positie " + i);
                i++;
                OverslaWit(s, ref i);
                JsonWaarde w = LeesWaarde(s, ref i);
                o.Velden.Add(new KeyValuePair<string, JsonWaarde>(sleutel, w));
                OverslaWit(s, ref i);
                if (i < s.Length && s[i] == ',') { i++; continue; }
                if (i < s.Length && s[i] == '}') { i++; return o; }
                throw new JsonFout("',' of '}' verwacht op positie " + i);
            }
        }

        static JsonWaarde LeesLijst(string s, ref int i)
        {
            JsonWaarde l = JsonWaarde.MaakLijst();
            i++;
            OverslaWit(s, ref i);
            if (i < s.Length && s[i] == ']') { i++; return l; }
            while (true)
            {
                OverslaWit(s, ref i);
                l.Lijst.Add(LeesWaarde(s, ref i));
                OverslaWit(s, ref i);
                if (i < s.Length && s[i] == ',') { i++; continue; }
                if (i < s.Length && s[i] == ']') { i++; return l; }
                throw new JsonFout("',' of ']' verwacht op positie " + i);
            }
        }

        static string LeesTekst(string s, ref int i)
        {
            StringBuilder sb = new StringBuilder();
            i++;
            while (i < s.Length)
            {
                char c = s[i];
                if (c == '"') { i++; return sb.ToString(); }
                if (c == '\\')
                {
                    i++;
                    if (i >= s.Length) break;
                    char e = s[i];
                    switch (e)
                    {
                        case '"': sb.Append('"'); break;
                        case '\\': sb.Append('\\'); break;
                        case '/': sb.Append('/'); break;
                        case 'b': sb.Append('\b'); break;
                        case 'f': sb.Append('\f'); break;
                        case 'n': sb.Append('\n'); break;
                        case 'r': sb.Append('\r'); break;
                        case 't': sb.Append('\t'); break;
                        case 'u':
                            if (i + 4 >= s.Length) throw new JsonFout("onvolledige \\u");
                            sb.Append((char)int.Parse(s.Substring(i + 1, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture));
                            i += 4;
                            break;
                        default: throw new JsonFout("ongeldige escape \\" + e);
                    }
                    i++;
                    continue;
                }
                sb.Append(c);
                i++;
            }
            throw new JsonFout("tekst zonder einde");
        }

        static JsonWaarde LeesGetal(string s, ref int i)
        {
            int start = i;
            if (s[i] == '-') i++;
            while (i < s.Length && (char.IsDigit(s[i]) || s[i] == '.' || s[i] == 'e' || s[i] == 'E' || s[i] == '+' || s[i] == '-')) i++;
            string t = s.Substring(start, i - start);
            JsonWaarde w = new JsonWaarde();
            w.Soort = JsonSoort.Getal;
            w.GetalTekst = t;
            decimal d;
            if (decimal.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out d)) w.Getal = d;
            else
            {
                double db;
                if (!double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out db)) throw new JsonFout("ongeldig getal " + t);
                try { w.Getal = (decimal)db; } catch (OverflowException) { w.Getal = db > 0 ? decimal.MaxValue : decimal.MinValue; }
            }
            return w;
        }

        public static string Schrijf(JsonWaarde w, bool ingesprongen)
        {
            StringBuilder sb = new StringBuilder();
            Schrijf(w, sb, ingesprongen ? 0 : -1);
            return sb.ToString();
        }

        static void Schrijf(JsonWaarde w, StringBuilder sb, int niveau)
        {
            if (w == null) { sb.Append("null"); return; }
            switch (w.Soort)
            {
                case JsonSoort.Null: sb.Append("null"); return;
                case JsonSoort.Bool: sb.Append(w.Bool ? "true" : "false"); return;
                case JsonSoort.Getal: sb.Append(w.GetalTekst ?? w.Getal.ToString(CultureInfo.InvariantCulture)); return;
                case JsonSoort.Tekst: SchrijfTekst(w.Tekst, sb); return;
                case JsonSoort.Lijst:
                    if (w.Lijst.Count == 0) { sb.Append("[]"); return; }
                    sb.Append('[');
                    for (int k = 0; k < w.Lijst.Count; k++)
                    {
                        if (k > 0) sb.Append(niveau >= 0 ? "," : ", ");
                        if (niveau >= 0) { sb.Append('\n'); sb.Append(' ', (niveau + 1) * 2); }
                        Schrijf(w.Lijst[k], sb, niveau >= 0 ? niveau + 1 : -1);
                    }
                    if (niveau >= 0) { sb.Append('\n'); sb.Append(' ', niveau * 2); }
                    sb.Append(']');
                    return;
                default:
                    if (w.Velden.Count == 0) { sb.Append("{}"); return; }
                    sb.Append(niveau >= 0 ? "{" : "{ ");
                    for (int k = 0; k < w.Velden.Count; k++)
                    {
                        if (k > 0) sb.Append(niveau >= 0 ? "," : ", ");
                        if (niveau >= 0) { sb.Append('\n'); sb.Append(' ', (niveau + 1) * 2); }
                        SchrijfTekst(w.Velden[k].Key, sb);
                        sb.Append(": ");
                        Schrijf(w.Velden[k].Value, sb, niveau >= 0 ? niveau + 1 : -1);
                    }
                    if (niveau >= 0) { sb.Append('\n'); sb.Append(' ', niveau * 2); sb.Append('}'); }
                    else sb.Append(" }");
                    return;
            }
        }

        public static void SchrijfTekst(string t, StringBuilder sb)
        {
            sb.Append('"');
            foreach (char c in t)
            {
                switch (c)
                {
                    case '"': sb.Append("\\\""); break;
                    case '\\': sb.Append("\\\\"); break;
                    case '\n': sb.Append("\\n"); break;
                    case '\r': sb.Append("\\r"); break;
                    case '\t': sb.Append("\\t"); break;
                    default:
                        if (c < 0x20) sb.Append("\\u").Append(((int)c).ToString("x4", CultureInfo.InvariantCulture));
                        else sb.Append(c);
                        break;
                }
            }
            sb.Append('"');
        }

        public static string Tekst(string t)
        {
            StringBuilder sb = new StringBuilder();
            SchrijfTekst(t, sb);
            return sb.ToString();
        }

        /// <summary>Zoekt een waarde op een pad als "id", "lijnen[0].id", "[0].id" of "" (alles).</summary>
        public static JsonWaarde ZoekPad(JsonWaarde w, string pad)
        {
            if (string.IsNullOrEmpty(pad)) return w;
            JsonWaarde huidig = w;
            int i = 0;
            while (i < pad.Length && huidig != null)
            {
                if (pad[i] == '.') { i++; continue; }
                if (pad[i] == '[')
                {
                    int einde = pad.IndexOf(']', i);
                    int index = int.Parse(pad.Substring(i + 1, einde - i - 1), CultureInfo.InvariantCulture);
                    if (!huidig.IsLijst || index >= huidig.Lijst.Count) return null;
                    huidig = huidig.Lijst[index];
                    i = einde + 1;
                    continue;
                }
                int j = i;
                while (j < pad.Length && pad[j] != '.' && pad[j] != '[') j++;
                string naam = pad.Substring(i, j - i);
                if (!huidig.IsObject) return null;
                string echt;
                JsonWaarde v = huidig.Veld(naam) ?? huidig.VeldZonderHoofdletters(naam, out echt);
                huidig = v;
                i = j;
            }
            return huidig;
        }
    }
}
