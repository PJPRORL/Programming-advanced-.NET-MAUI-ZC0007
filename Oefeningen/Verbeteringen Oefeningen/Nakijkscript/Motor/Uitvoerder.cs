// Nakijkscript — motor, deel 4: de stappen van een nakijkbestand uitvoeren.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Nakijkscript
{
    public class StapResultaat
    {
        public JsonWaarde Stap;
        public string Id, Punt, Uitleg, ScenarioId, ScenarioTitel;
        public string Soort;                 // verzoek, actie, sql
        public bool Uitgevoerd, Geslaagd;
        public string NietUitgevoerdOmdat;
        public string Methode, Pad, Url, BodyVerstuurd;
        public int Status;
        public string Body = "", Location, ContentType;
        public List<Verschil> Verschillen = new List<Verschil>();
        public List<string> Meldingen = new List<string>();
        public List<string> LogRegels = new List<string>();
        public int Queries;
        public string Verwacht, Gekregen;    // korte samenvattingen voor het verslag
        public bool TekstBijna;              // enkel verschil in hoofdletters, spaties of leestekens
        public bool StatusFout;
        public bool TeltMee;                 // niet uitgevoerd, maar telt mee als niet juist
        public bool Gevolg;                  // mislukt door een eerder mislukt verzoek
        public string Verzoek { get { return Soort == "verzoek" ? Methode + " /" + Pad : (Soort == "sql" ? "SQL" : Uitleg); } }
    }

    public class Uitvoering
    {
        public Api Api;
        public Database Db;
        public JsonWaarde Nb;
        public string Prefix;
        public bool PrefixAfwijkend;
        public Dictionary<string, string> Vars = new Dictionary<string, string>();
        public Dictionary<string, StapResultaat> VarBron = new Dictionary<string, StapResultaat>();
        public List<StapResultaat> Resultaten = new List<StapResultaat>();
        public List<string> Verloop = new List<string>();
        public List<string> SqlScripts = new List<string>();
        public List<string> StartLog = new List<string>();
        public string ApiStartFout;
        HttpClient http;

        public Uitvoering(Api api, Database db, JsonWaarde nakijkbestand)
        {
            Api = api; Db = db; Nb = nakijkbestand;
            HttpClientHandler h = new HttpClientHandler();
            h.AllowAutoRedirect = false;
            http = new HttpClient(h);
            http.Timeout = TimeSpan.FromSeconds(30);
            JsonWaarde rp = Nb.Veld("routePrefix");
            Prefix = rp.Veld("standaard").Tekst;
        }

        static string T(JsonWaarde w, string veld) { JsonWaarde v = w.Veld(veld); return v == null ? null : v.AlsTekst(); }

        public void Voer()
        {
            bool prefixBepaald = false;
            foreach (JsonWaarde sc in Nb.Veld("scenarios").Lijst)
            {
                string start = T(sc, "start");
                bool ok = StartScenario(start, T(sc, "titel"));
                if (ok && !prefixBepaald) { BepaalPrefix(); prefixBepaald = true; }
                foreach (JsonWaarde st in sc.Veld("stappen").Lijst)
                {
                    StapResultaat r = new StapResultaat();
                    r.Stap = st; r.Id = T(st, "id"); r.Punt = T(st, "punt"); r.Uitleg = T(st, "uitleg");
                    r.ScenarioId = T(sc, "id"); r.ScenarioTitel = T(sc, "titel");
                    r.Soort = st.HeeftVeld("verzoek") ? "verzoek" : (st.HeeftVeld("sql") ? "sql" : "actie");
                    if (!ok) { r.NietUitgevoerdOmdat = ApiStartFout ?? "de API startte niet"; Resultaten.Add(r); continue; }
                    try
                    {
                        if (r.Soort == "verzoek") VoerVerzoek(st, r);
                        else if (r.Soort == "sql") VoerSql(st, r);
                        else ok = VoerActie(st, r);
                    }
                    catch (Exception ex)
                    {
                        r.Uitgevoerd = true; r.Geslaagd = false;
                        r.Meldingen.Add("Het script kon deze stap niet uitvoeren: " + ex.Message);
                    }
                    Resultaten.Add(r);
                }
            }
            Api.Stop();
        }

        bool StartScenario(string start, string titel)
        {
            if (Api.Extern)
            {
                if (start != "doorgaan") Verloop.Add("Scenario \"" + titel + "\" vraagt een " + start + ", maar je API draaide al: het script ging verder met de toestand die je API had.");
                return true;
            }
            if (start == "doorgaan" && Api.Draait) return true;
            Api.Stop();
            VerwijderBestanden();
            if (start == "opbouwen")
            {
                if (Db == null || Db.Probleem != null || !Db.Opbouwen(SqlScripts, Verloop))
                {
                    ApiStartFout = "de database kon niet opnieuw opgebouwd worden: " + (Db == null ? "geen database" : Db.Probleem);
                    return false;
                }
            }
            int voor = Api.LogLengte;
            if (!Api.Start(90))
            {
                ApiStartFout = Api.StartFout;
                StartLog = Api.LogVanaf(voor);
                return false;
            }
            Thread.Sleep(300);
            if (StartLog.Count == 0) StartLog = Api.LogVanaf(voor);
            return true;
        }

        void VerwijderBestanden()
        {
            JsonWaarde b = Nb.Veld("bestanden");
            if (b == null || Api.Extern) return;
            foreach (JsonWaarde f in b.Lijst)
            {
                string pad = Path.Combine(Api.ProjectMap, f.Tekst);
                if (File.Exists(pad)) { File.Delete(pad); Verloop.Add(f.Tekst + " gewist voor een nieuw scenario."); }
            }
        }

        // ---------------------------------------------------------------- prefix

        void BepaalPrefix()
        {
            JsonWaarde rp = Nb.Veld("routePrefix");
            List<string> kandidaten = new List<string>();
            kandidaten.Add(rp.Veld("standaard").Tekst);
            foreach (JsonWaarde p in rp.Veld("probeer").Lijst) kandidaten.Add(p.Tekst);
            if (kandidaten.Count == 1) return;
            string proefPad = null;
            foreach (JsonWaarde sc in Nb.Veld("scenarios").Lijst)
                foreach (JsonWaarde st in sc.Veld("stappen").Lijst)
                {
                    JsonWaarde v = st.Veld("verzoek");
                    if (proefPad == null && v != null && v.Veld("methode").Tekst == "GET" && v.Veld("pad").Tekst.IndexOf("{{") < 0)
                        proefPad = v.Veld("pad").Tekst;
                }
            if (proefPad == null) return;
            foreach (string k in kandidaten)
            {
                int status; string body, loc, ct;
                if (!Stuur("GET", k + proefPad, null, out status, out body, out loc, out ct)) continue;
                if (status != 404 || (body.Trim().Length > 0 && !IsProblemDetails(body)))
                {
                    if (k != Prefix) { PrefixAfwijkend = true; Prefix = k; }
                    return;
                }
            }
        }

        // ---------------------------------------------------------------- HTTP

        string MaakUrl(string pad)
        {
            string[] delen = pad.Split('/');
            for (int i = 0; i < delen.Length; i++) delen[i] = Uri.EscapeDataString(delen[i]);
            return Api.BasisUrl + string.Join("/", delen);
        }

        bool Stuur(string methode, string pad, string body, out int status, out string antwoord, out string location, out string contentType)
        {
            status = 0; antwoord = ""; location = null; contentType = null;
            HttpRequestMessage req = new HttpRequestMessage(new HttpMethod(methode), MaakUrl(pad));
            if (body != null) req.Content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage resp;
            try { resp = http.SendAsync(req).Result; }
            catch (Exception ex)
            {
                Exception binnen = ex.InnerException ?? ex;
                while (binnen.InnerException != null) binnen = binnen.InnerException;
                antwoord = "(geen antwoord: " + binnen.Message + ")";
                return false;
            }
            status = (int)resp.StatusCode;
            antwoord = resp.Content == null ? "" : resp.Content.ReadAsStringAsync().Result;
            if (resp.Headers.Location != null) location = resp.Headers.Location.OriginalString;
            if (resp.Content != null && resp.Content.Headers.ContentType != null) contentType = resp.Content.Headers.ContentType.MediaType;
            return true;
        }

        public static bool IsProblemDetails(string body)
        {
            JsonWaarde j;
            if (!Json.ProbeerLees(body, out j) || !j.IsObject) return false;
            return j.HeeftVeld("status") && (j.HeeftVeld("title") || j.HeeftVeld("type"));
        }

        JsonWaarde VulJson(JsonWaarde w, Vergelijker v)
        {
            if (w == null) return null;
            switch (w.Soort)
            {
                case JsonSoort.Tekst:
                    {
                        Match m = Regex.Match(w.Tekst, @"^\{\{(\w+)\}\}$");
                        string waarde;
                        decimal d;
                        if (m.Success && Vars.TryGetValue(m.Groups[1].Value, out waarde) && decimal.TryParse(waarde, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
                        {
                            JsonWaarde g = JsonWaarde.MaakGetal(d); g.GetalTekst = waarde; return g;
                        }
                        return JsonWaarde.MaakTekst(v.Vul(w.Tekst));
                    }
                case JsonSoort.Lijst:
                    {
                        JsonWaarde l = JsonWaarde.MaakLijst();
                        foreach (JsonWaarde e in w.Lijst) l.Lijst.Add(VulJson(e, v));
                        return l;
                    }
                case JsonSoort.Object:
                    {
                        JsonWaarde o = JsonWaarde.MaakObject();
                        foreach (KeyValuePair<string, JsonWaarde> kv in w.Velden) o.Velden.Add(new KeyValuePair<string, JsonWaarde>(kv.Key, VulJson(kv.Value, v)));
                        return o;
                    }
                default: return w;
            }
        }

        void VoerVerzoek(JsonWaarde st, StapResultaat r)
        {
            Vergelijker vg = new Vergelijker(Vars);
            JsonWaarde v = st.Veld("verzoek");
            r.Methode = v.Veld("methode").Tekst;
            // een waarde die een eerder verzoek had moeten opleveren, ontbreekt: dan is deze stap niet na te kijken
            JsonWaarde eigen = st.Veld("bewaar");
            foreach (Match m in Regex.Matches(Json.Schrijf(st, false), @"\{\{(\w+)\}\}"))
                if (!Vars.ContainsKey(m.Groups[1].Value) && !(eigen != null && eigen.HeeftVeld(m.Groups[1].Value)))
                {
                    r.Pad = Prefix + v.Veld("pad").Tekst;
                    r.NietUitgevoerdOmdat = "een eerder verzoek leverde de waarde `" + m.Groups[1].Value + "` niet op (" + BronVan(m.Groups[1].Value) + ")";
                    r.TeltMee = true;
                    return;
                }
            r.Pad = Prefix + vg.Vul(v.Veld("pad").Tekst);
            if (v.HeeftVeld("body")) r.BodyVerstuurd = Json.Schrijf(VulJson(v.Veld("body"), vg), false);
            else if (v.HeeftVeld("bodyRuw")) r.BodyVerstuurd = vg.Vul(v.Veld("bodyRuw").Tekst);
            r.Url = MaakUrl(r.Pad);
            int voor = Api.LogLengte;
            int status; string body, loc, ct;
            bool antwoord = Stuur(r.Methode, r.Pad, r.BodyVerstuurd, out status, out body, out loc, out ct);
            Thread.Sleep(Api.Extern ? 50 : 250);
            r.LogRegels = Api.LogVanaf(voor);
            foreach (string l in r.LogRegels) if (l.Contains("Executed DbCommand")) r.Queries++;
            r.Uitgevoerd = true;
            r.Status = status; r.Body = body ?? ""; r.Location = loc; r.ContentType = ct;
            JsonWaarde ve = st.Veld("verwacht");
            r.Verwacht = SamenvattingVerwacht(ve, vg);
            r.Gekregen = antwoord ? SamenvattingGekregen(r) : body;
            if (!antwoord) { r.Meldingen.Add("Je API gaf geen antwoord: " + body); r.Geslaagd = false; return; }

            JsonWaarde bodyJson;
            bool isJson = Json.ProbeerLees(r.Body, out bodyJson);
            // eerst bewaren, dan controleren
            JsonWaarde bewaar = st.Veld("bewaar");
            if (bewaar != null)
                foreach (KeyValuePair<string, JsonWaarde> kv in bewaar.Velden)
                {
                    string pad = kv.Value.Tekst;
                    if (pad.StartsWith("header:")) { if (loc != null) { Vars[kv.Key] = loc; VarBron[kv.Key] = r; } continue; }
                    if (!isJson) continue;
                    JsonWaarde w = Json.ZoekPad(bodyJson, pad);
                    if (w != null && w.Soort != JsonSoort.Null) { Vars[kv.Key] = w.AlsTekst(); VarBron[kv.Key] = r; }
                }

            JsonWaarde s = ve.Veld("status");
            if (s != null && (int)s.Getal != status)
            {
                r.StatusFout = true;
                r.Meldingen.Add("Statuscode " + status + " in plaats van " + (int)s.Getal + ".");
                // bij een andere statuscode zegt de inhoud niets meer: de tabel in het verslag toont ze wel
                r.Geslaagd = false;
                return;
            }
            JsonWaarde tekst = ve.Veld("tekst");
            if (tekst != null)
            {
                string verwacht = vg.Vul(tekst.Tekst);
                string kreeg = r.Body.Trim();
                JsonWaarde alsJson;
                if (Json.ProbeerLees(kreeg, out alsJson) && alsJson.Soort == JsonSoort.Tekst) kreeg = alsJson.Tekst;
                if (kreeg != verwacht)
                {
                    Verschil d = new Verschil();
                    d.Pad = "tekst"; d.Verwacht = verwacht; d.Gekregen = kreeg.Length == 0 ? "(geen tekst)" : kreeg; d.Soort = "tekst";
                    r.Verschillen.Add(d);
                    r.TekstBijna = Kaal(kreeg) == Kaal(verwacht) && Kaal(kreeg).Length > 0;
                }
            }
            JsonWaarde json = ve.Veld("json");
            if (json != null)
            {
                if (!isJson)
                {
                    Verschil d = new Verschil();
                    d.Pad = "$"; d.Verwacht = "JSON"; d.Gekregen = r.Body.Trim().Length == 0 ? "(geen inhoud)" : "geen geldige JSON"; d.Soort = "type";
                    r.Verschillen.Add(d);
                }
                else if (IsProblemDetails(r.Body) && !(s != null && (int)s.Getal == status))
                {
                    Verschil d = new Verschil();
                    d.Pad = "$"; d.Verwacht = "het gevraagde antwoord"; d.Gekregen = "de standaardfoutmelding van ASP.NET" + ProblemDetailsFouten(r.Body); d.Soort = "type";
                    r.Verschillen.Add(d);
                }
                else r.Verschillen.AddRange(Vergelijker.Vergelijk(json, bodyJson, Vars));
            }
            JsonWaarde geen = ve.Veld("geenBody");
            if (geen != null && geen.Bool && r.Body.Trim().Length > 0 && !IsProblemDetails(r.Body))
                r.Meldingen.Add("Het antwoord heeft inhoud, maar de opgave vraagt er geen.");
            JsonWaarde lo = ve.Veld("location");
            if (lo != null)
            {
                string eind = vg.Vul(lo.Veld("eindigtOp").Tekst);
                if (loc == null) r.Meldingen.Add("De header Location ontbreekt.");
                else if (!loc.EndsWith(eind, StringComparison.OrdinalIgnoreCase))
                    r.Meldingen.Add("De header Location is " + loc + ", maar moet eindigen op " + eind + ".");
            }
            JsonWaarde q = ve.Veld("queries");
            if (q != null && !Api.Extern && r.Queries != (int)q.Getal)
                r.Meldingen.Add("Je API stuurde " + r.Queries + " query's naar de database, de opgave vraagt er " + (int)q.Getal + ".");
            r.Geslaagd = r.Meldingen.Count == 0 && r.Verschillen.Count == 0;
        }

        string BronVan(string var)
        {
            foreach (JsonWaarde sc in Nb.Veld("scenarios").Lijst)
                foreach (JsonWaarde st in sc.Veld("stappen").Lijst)
                {
                    JsonWaarde b = st.Veld("bewaar");
                    if (b != null && b.HeeftVeld(var)) return "stap " + T(st, "id");
                }
            return "geen stap bewaart ze";
        }

        static string Kaal(string t)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in t.ToLowerInvariant()) if (char.IsLetterOrDigit(c)) sb.Append(c);
            return sb.ToString();
        }

        string SamenvattingVerwacht(JsonWaarde ve, Vergelijker vg)
        {
            List<string> d = new List<string>();
            JsonWaarde s = ve.Veld("status");
            if (s != null) d.Add(StatusNaam((int)s.Getal));
            if (ve.HeeftVeld("tekst")) d.Add(vg.Vul(ve.Veld("tekst").Tekst));
            if (ve.HeeftVeld("json")) d.Add(Kort(Leesbaar(ve.Veld("json"), vg), 400));
            if (ve.HeeftVeld("geenBody")) d.Add("zonder inhoud");
            if (ve.HeeftVeld("location")) d.Add("Location eindigt op " + vg.Vul(ve.Veld("location").Veld("eindigtOp").Tekst));
            if (ve.HeeftVeld("queries")) d.Add(((int)ve.Veld("queries").Getal) + " query naar de database");
            return string.Join(" · ", d.ToArray());
        }

        /// <summary>De verwachte JSON zoals een student ze kan lezen: variabelen ingevuld, matchers in woorden.</summary>
        public static string Leesbaar(JsonWaarde w, Vergelijker vg)
        {
            if (w == null) return "";
            if (Vergelijker.IsMatcher(w))
            {
                string naam = w.Velden[0].Key;
                JsonWaarde a = w.Velden[0].Value;
                switch (naam)
                {
                    case "$elk": return "…";
                    case "$ontbreekt": return "(geen veld)";
                    case "$aantal": return ((int)a.Getal) + " elementen";
                    case "$tekstBevat": return "tekst met " + Json.Tekst(vg.Vul(a.Tekst));
                    case "$ids":
                    case "$idsInVolgorde":
                        {
                            List<string> ids = new List<string>();
                            foreach (JsonWaarde x in a.Lijst) ids.Add(vg.Vul(x.AlsTekst()));
                            return "de elementen met Id " + string.Join(", ", ids.ToArray()) + (naam == "$ids" ? " (volgorde vrij)" : ", in die volgorde");
                        }
                    case "$reken":
                        {
                            decimal d; string fout;
                            return vg.Reken(a.Tekst, out d, out fout) ? d.ToString(CultureInfo.InvariantCulture) : a.Tekst;
                        }
                    case "$bevat":
                        {
                            List<string> delen = new List<string>();
                            foreach (KeyValuePair<string, JsonWaarde> kv in a.Velden) delen.Add(Json.Tekst(kv.Key) + ": " + Leesbaar(kv.Value, vg));
                            return "{ " + string.Join(", ", delen.ToArray()) + ", … }";
                        }
                    case "$verzameling":
                        {
                            List<string> delen = new List<string>();
                            foreach (JsonWaarde x in a.Lijst) delen.Add(Leesbaar(x, vg));
                            return "[" + string.Join(", ", delen.ToArray()) + "] (volgorde vrij)";
                        }
                    case "$elkElement": return "elk element: " + Leesbaar(a, vg);
                    case "$heeftElement": return "met een element " + Leesbaar(a, vg);
                    case "$geenElement": return "zonder element " + Leesbaar(a, vg);
                }
            }
            switch (w.Soort)
            {
                case JsonSoort.Tekst:
                    {
                        string v = vg.Vul(w.Tekst);
                        decimal d;
                        if (System.Text.RegularExpressions.Regex.IsMatch(w.Tekst, @"^\{\{\w+\}\}$") && decimal.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out d)) return v;
                        return Json.Tekst(v);
                    }
                case JsonSoort.Lijst:
                    {
                        List<string> delen = new List<string>();
                        foreach (JsonWaarde x in w.Lijst) delen.Add(Leesbaar(x, vg));
                        return "[" + string.Join(", ", delen.ToArray()) + "]";
                    }
                case JsonSoort.Object:
                    {
                        List<string> delen = new List<string>();
                        foreach (KeyValuePair<string, JsonWaarde> kv in w.Velden)
                        {
                            if (Vergelijker.IsMatcher(kv.Value) && kv.Value.Velden[0].Key == "$ontbreekt") { delen.Add("zonder " + Json.Tekst(kv.Key)); continue; }
                            delen.Add(Json.Tekst(kv.Key) + ": " + Leesbaar(kv.Value, vg));
                        }
                        return "{ " + string.Join(", ", delen.ToArray()) + " }";
                    }
                default: return Json.Schrijf(w, false);
            }
        }

        static string SamenvattingGekregen(StapResultaat r)
        {
            List<string> d = new List<string>();
            d.Add(StatusNaam(r.Status));
            string b = r.Body.Trim();
            JsonWaarde j;
            if (b.Length == 0) d.Add("zonder inhoud");
            else if (Json.ProbeerLees(b, out j) && j.Soort == JsonSoort.Tekst) d.Add(j.Tekst);
            else if (IsProblemDetails(b)) d.Add("standaardfoutmelding van ASP.NET" + ProblemDetailsFouten(b));
            else d.Add(Kort(b, 400));
            if (r.Location != null) d.Add("Location " + r.Location);
            return string.Join(" · ", d.ToArray());
        }

        public static string ProblemDetailsFouten(string body)
        {
            JsonWaarde j;
            if (!Json.ProbeerLees(body, out j) || !j.IsObject) return "";
            JsonWaarde e = j.Veld("errors");
            if (e == null || !e.IsObject) return "";
            List<string> fouten = new List<string>();
            foreach (KeyValuePair<string, JsonWaarde> kv in e.Velden)
            {
                string tekst = kv.Value.IsLijst && kv.Value.Lijst.Count > 0 ? kv.Value.Lijst[0].AlsTekst() : kv.Value.AlsTekst();
                fouten.Add(tekst);
            }
            return fouten.Count == 0 ? "" : ": " + string.Join(" ", fouten.ToArray());
        }

        public static string Kort(string t, int max)
        {
            t = t.Replace("\r", "").Replace("\n", " ");
            return t.Length > max ? t.Substring(0, max - 3) + "..." : t;
        }

        public static string StatusNaam(int s)
        {
            switch (s)
            {
                case 200: return "200 OK";
                case 201: return "201 Created";
                case 204: return "204 No Content";
                case 400: return "400 Bad Request";
                case 404: return "404 Not Found";
                case 405: return "405 Method Not Allowed";
                case 415: return "415 Unsupported Media Type";
                case 500: return "500 Internal Server Error";
                case 0: return "geen antwoord";
                default: return s.ToString(CultureInfo.InvariantCulture);
            }
        }

        // ---------------------------------------------------------------- SQL en acties

        void VoerSql(JsonWaarde st, StapResultaat r)
        {
            if (Db == null || Db.Probleem != null)
            {
                r.NietUitgevoerdOmdat = Db == null ? "er is geen database" : Db.Probleem;
                return;
            }
            Vergelijker vg = new Vergelijker(Vars);
            string sql = vg.Vul(st.Veld("sql").Tekst);
            JsonWaarde ve = st.Veld("verwacht");
            bool foutVerwacht = ve.HeeftVeld("fout");
            string fout;
            List<string[]> rijen = Db.Sql(sql, foutVerwacht, out fout);
            r.Uitgevoerd = true;
            r.Methode = "SQL"; r.Pad = sql;
            if (foutVerwacht)
            {
                string stuk = ve.Veld("fout").Tekst;
                r.Verwacht = "de database weigert (" + stuk + ")";
                r.Gekregen = fout == null ? "de database voerde de opdracht uit" : Kort(fout, 300);
                if (fout == null || fout.IndexOf(stuk, StringComparison.OrdinalIgnoreCase) < 0) r.Meldingen.Add("De database weigert deze opdracht niet, of om een andere reden.");
                r.Geslaagd = r.Meldingen.Count == 0;
                return;
            }
            if (fout != null) { r.Gekregen = Kort(fout, 300); r.Verwacht = "rijen"; r.Meldingen.Add("De query gaf een fout: " + Kort(fout, 300)); return; }
            List<string> echte = new List<string>();
            foreach (string[] rij in rijen) echte.Add(string.Join(" | ", rij));
            r.Gekregen = rijen.Count == 0 ? "geen rijen" : string.Join("; ", echte.ToArray());
            if (ve.HeeftVeld("aantal"))
            {
                int n = (int)ve.Veld("aantal").Getal;
                r.Verwacht = n + " rij(en)";
                if (rijen.Count != n) r.Meldingen.Add(rijen.Count + " rij(en) in plaats van " + n + ".");
            }
            if (ve.HeeftVeld("rijen"))
            {
                List<string> verwacht = new List<string>();
                foreach (JsonWaarde rij in ve.Veld("rijen").Lijst)
                {
                    List<string> cellen = new List<string>();
                    foreach (JsonWaarde c in rij.Lijst) cellen.Add(vg.Vul(c.Tekst));
                    verwacht.Add(string.Join(" | ", cellen.ToArray()));
                }
                r.Verwacht = verwacht.Count == 0 ? "geen rijen" : string.Join("; ", verwacht.ToArray());
                List<string> a = new List<string>(verwacht), b = new List<string>(echte);
                if (ve.Veld("volgorde").Tekst == "vrij") { a.Sort(StringComparer.Ordinal); b.Sort(StringComparer.Ordinal); }
                if (string.Join("\n", a.ToArray()) != string.Join("\n", b.ToArray())) r.Meldingen.Add("De rijen in de database verschillen van wat de opgave vraagt.");
            }
            r.Geslaagd = r.Meldingen.Count == 0;
        }

        bool VoerActie(JsonWaarde st, StapResultaat r)
        {
            string actie = T(st, "actie");
            r.Methode = actie;
            if (Api.Extern)
            {
                r.NietUitgevoerdOmdat = "het script kan een API die je zelf startte, niet herstarten of opnieuw opbouwen";
                return true;
            }
            Api.Stop();
            if (actie == "verwijderBestand")
            {
                string pad = Path.Combine(Api.ProjectMap, T(st, "pad"));
                if (File.Exists(pad)) File.Delete(pad);
            }
            if (actie == "opbouwen" && (Db == null || Db.Probleem != null || !Db.Opbouwen(SqlScripts, Verloop)))
            {
                r.Uitgevoerd = true; r.Geslaagd = false;
                r.Meldingen.Add("De database kon niet opnieuw opgebouwd worden: " + (Db == null ? "geen database" : Db.Probleem));
                return false;
            }
            bool ok = Api.Start(90);
            r.Uitgevoerd = true;
            r.Geslaagd = ok;
            if (!ok) { r.Meldingen.Add("De API startte niet opnieuw: " + Api.StartFout); ApiStartFout = Api.StartFout; }
            return ok;
        }
    }
}
