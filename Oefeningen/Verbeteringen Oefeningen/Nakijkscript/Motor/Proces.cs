// Nakijkscript — motor, deel 3: het project bouwen, de API starten en stoppen, de database bedienen.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Nakijkscript
{
    public class ProcesUitkomst
    {
        public int Code;
        public string Uitvoer = "";
        public string Fouten = "";
        public bool TijdOp;
        public string Alles { get { return (Uitvoer + "\n" + Fouten).Trim(); } }
    }

    public static class Opdracht
    {
        /// <summary>Voert een programma uit en wacht tot het klaar is.</summary>
        public static ProcesUitkomst Voer(string programma, string argumenten, string map, string invoer, int seconden, Dictionary<string, string> omgeving)
        {
            ProcesUitkomst u = new ProcesUitkomst();
            ProcessStartInfo psi = new ProcessStartInfo(programma, argumenten);
            psi.WorkingDirectory = map;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = invoer != null;
            psi.CreateNoWindow = true;
            psi.StandardOutputEncoding = Encoding.UTF8;
            psi.StandardErrorEncoding = Encoding.UTF8;
            psi.EnvironmentVariables["DOTNET_NOLOGO"] = "1";
            psi.EnvironmentVariables["DOTNET_CLI_TELEMETRY_OPTOUT"] = "1";
            psi.EnvironmentVariables["DOTNET_CLI_UI_LANGUAGE"] = "en";
            if (omgeving != null) foreach (KeyValuePair<string, string> kv in omgeving) psi.EnvironmentVariables[kv.Key] = kv.Value;
            StringBuilder uit = new StringBuilder(), fout = new StringBuilder();
            Process p = new Process();
            p.StartInfo = psi;
            p.OutputDataReceived += delegate (object s, DataReceivedEventArgs e) { if (e.Data != null) lock (uit) uit.AppendLine(e.Data); };
            p.ErrorDataReceived += delegate (object s, DataReceivedEventArgs e) { if (e.Data != null) lock (fout) fout.AppendLine(e.Data); };
            try { p.Start(); }
            catch (Exception ex) { u.Code = -1; u.Fouten = programma + " kon niet starten: " + ex.Message; return u; }
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            if (invoer != null)
            {
                StreamWriter w = new StreamWriter(p.StandardInput.BaseStream, new UTF8Encoding(false));
                w.Write(invoer);
                w.Close();
            }
            if (!p.WaitForExit(seconden * 1000))
            {
                u.TijdOp = true;
                try { p.Kill(); } catch (Exception) { }
            }
            p.WaitForExit();
            u.Code = u.TijdOp ? -2 : p.ExitCode;
            lock (uit) u.Uitvoer = uit.ToString();
            lock (fout) u.Fouten = fout.ToString();
            return u;
        }
    }

    public class CompileerFout
    {
        public string Bestand;
        public int Regel;
        public string Code;
        public string Bericht;
    }

    /// <summary>Het project van de student: bouwen, starten, stoppen, en de uitvoer bijhouden.</summary>
    public class Api
    {
        public string ProjectMap;
        public string CsProj;
        public string Dll;
        public int Poort;
        public string BasisUrl;          // http://localhost:5199/
        public bool Extern;              // de student startte zijn API zelf (-BasisUrl)
        public List<string> Log = new List<string>();
        public string StartFout;
        public string TestProgramma, TestArgumenten;   // enkel om het script zelf te testen
        Process proces;

        public Api(string projectMap, int poort)
        {
            ProjectMap = projectMap;
            Poort = poort;
            BasisUrl = "http://127.0.0.1:" + poort.ToString(CultureInfo.InvariantCulture) + "/";
            string[] p = Directory.GetFiles(projectMap, "*.csproj");
            if (p.Length > 0) CsProj = p[0];
        }

        public static Api ExterneApi(string basisUrl)
        {
            Api a = new Api(Path.GetTempPath(), 0);
            a.BasisUrl = basisUrl.EndsWith("/") ? basisUrl : basisUrl + "/";
            a.Extern = true;
            return a;
        }

        public List<CompileerFout> Bouw(out string uitvoer)
        {
            List<CompileerFout> fouten = new List<CompileerFout>();
            ProcesUitkomst u = Opdracht.Voer("dotnet", "build \"" + CsProj + "\" --nologo -tl:off", ProjectMap, null, 300, null);
            uitvoer = u.Alles;
            Regex fout = new Regex(@"^\s*(.+?)\((\d+),(\d+)\): error (\w+): (.+?)(\s\[.+\])?\s*$");
            HashSet<string> gezien = new HashSet<string>();
            foreach (string r in uitvoer.Split('\n'))
            {
                Match m = fout.Match(r);
                if (m.Success)
                {
                    string sleutel = m.Groups[1].Value + m.Groups[2].Value + m.Groups[4].Value;
                    if (!gezien.Add(sleutel)) continue;
                    CompileerFout f = new CompileerFout();
                    f.Bestand = RelatiefPad(m.Groups[1].Value.Trim());
                    f.Regel = int.Parse(m.Groups[2].Value, CultureInfo.InvariantCulture);
                    f.Code = m.Groups[4].Value;
                    f.Bericht = m.Groups[5].Value.Trim();
                    fouten.Add(f);
                }
                Match d = Regex.Match(r, @"^\s*\S+ -> (.+\.dll)\s*$");
                if (d.Success) Dll = d.Groups[1].Value.Trim();
            }
            if (u.Code != 0 && fouten.Count == 0)
            {
                CompileerFout f = new CompileerFout();
                f.Bestand = Path.GetFileName(CsProj); f.Code = "BUILD";
                f.Bericht = u.TijdOp ? "dotnet build duurde langer dan 5 minuten." : "dotnet build mislukte zonder herkenbare foutmelding.";
                fouten.Add(f);
            }
            return fouten;
        }

        public string RelatiefPad(string pad)
        {
            try
            {
                string vol = Path.GetFullPath(pad);
                string basis = Path.GetFullPath(ProjectMap).TrimEnd('\\', '/') + Path.DirectorySeparatorChar;
                if (vol.StartsWith(basis, StringComparison.OrdinalIgnoreCase)) return vol.Substring(basis.Length).Replace('\\', '/');
            }
            catch (Exception) { }
            return pad;
        }

        public bool Draait { get { return Extern || (proces != null && !proces.HasExited); } }

        public bool Start(int seconden)
        {
            StartFout = null;
            if (Extern) return true;
            Stop();
            ProcessStartInfo psi;
            if (TestProgramma != null)
                psi = new ProcessStartInfo(TestProgramma, TestArgumenten.Replace("{project}", ProjectMap).Replace("{poort}", Poort.ToString(CultureInfo.InvariantCulture)));
            else
            {
                if (Dll == null || !File.Exists(Dll)) { StartFout = "Het script vond na het bouwen geen .dll om te starten."; return false; }
                psi = new ProcessStartInfo("dotnet", "\"" + Dll + "\"");
            }
            psi.WorkingDirectory = ProjectMap;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.CreateNoWindow = true;
            psi.StandardOutputEncoding = Encoding.UTF8;
            psi.StandardErrorEncoding = Encoding.UTF8;
            psi.EnvironmentVariables["ASPNETCORE_URLS"] = BasisUrl.TrimEnd('/');
            psi.EnvironmentVariables["ASPNETCORE_ENVIRONMENT"] = "Development";
            psi.EnvironmentVariables["DOTNET_NOLOGO"] = "1";
            psi.EnvironmentVariables["Logging__Console__FormatterName"] = "simple";
            psi.EnvironmentVariables["Logging__LogLevel__Microsoft.EntityFrameworkCore.Database.Command"] = "Information";
            proces = new Process();
            proces.StartInfo = psi;
            proces.OutputDataReceived += delegate (object s, DataReceivedEventArgs e) { if (e.Data != null) lock (Log) Log.Add(e.Data); };
            proces.ErrorDataReceived += delegate (object s, DataReceivedEventArgs e) { if (e.Data != null) lock (Log) Log.Add(e.Data); };
            int vanaf;
            lock (Log) { Log.Add("=== API gestart ==="); vanaf = Log.Count; }
            proces.Start();
            proces.BeginOutputReadLine();
            proces.BeginErrorReadLine();
            DateTime tot = DateTime.Now.AddSeconds(seconden);
            while (DateTime.Now < tot)
            {
                if (proces.HasExited)
                {
                    Thread.Sleep(300);
                    StartFout = "De API stopte meteen na het starten (exitcode " + proces.ExitCode + ").";
                    return false;
                }
                lock (Log) { for (int i = vanaf; i < Log.Count; i++) if (Log[i].Contains("Now listening on") || Log[i].Contains("Application started")) return true; }
                if (Bereikbaar()) return true;
                Thread.Sleep(400);
            }
            StartFout = "De API antwoordde na " + seconden + " seconden nog altijd niet.";
            return false;
        }

        bool Bereikbaar()
        {
            try
            {
                using (HttpClient c = new HttpClient())
                {
                    c.Timeout = TimeSpan.FromSeconds(2);
                    HttpResponseMessage r = c.GetAsync(BasisUrl).Result;
                    return r != null;
                }
            }
            catch (Exception) { return false; }
        }

        public void Stop()
        {
            if (Extern || proces == null) return;
            try
            {
                if (!proces.HasExited)
                {
                    proces.Kill();
                    proces.WaitForExit(10000);
                }
            }
            catch (Exception) { }
            proces = null;
            Thread.Sleep(500);
            lock (Log) Log.Add("=== API gestopt ===");
        }

        public int LogLengte { get { lock (Log) return Log.Count; } }

        public List<string> LogVanaf(int index)
        {
            lock (Log)
            {
                if (index >= Log.Count) return new List<string>();
                return Log.GetRange(index, Log.Count - index);
            }
        }
    }

    /// <summary>PostgreSQL in Docker, en de migraties van Entity Framework Core.</summary>
    public class Database
    {
        public string Container;
        public string Gebruiker = "postgres";
        public string Naam;
        public string Probleem;     // waarom de database niet bruikbaar is
        public bool Omgeving;       // het probleem ligt aan de computer (Docker, dotnet ef), niet aan de code
        string projectMap, csproj;

        public Database(string projectMap, string csproj, string naamUitNakijkbestand, string container)
        {
            this.projectMap = projectMap;
            this.csproj = csproj;
            Naam = naamUitNakijkbestand;
            LeesConnectionString();
            if (!string.IsNullOrEmpty(container)) Container = container;
            else ZoekContainer();
        }

        void LeesConnectionString()
        {
            foreach (string bestand in new string[] { "appsettings.Development.json", "appsettings.json" })
            {
                string pad = Path.Combine(projectMap, bestand);
                if (!File.Exists(pad)) continue;
                JsonWaarde j;
                if (!Json.ProbeerLees(File.ReadAllText(pad, Encoding.UTF8), out j) || !j.IsObject) continue;
                string echt;
                JsonWaarde cs = j.Veld("ConnectionStrings") ?? j.VeldZonderHoofdletters("connectionstrings", out echt);
                if (cs == null || !cs.IsObject || cs.Velden.Count == 0) continue;
                string tekst = cs.Velden[0].Value.AlsTekst();
                foreach (string deel in tekst.Split(';'))
                {
                    int i = deel.IndexOf('=');
                    if (i < 0) continue;
                    string k = deel.Substring(0, i).Trim().ToLowerInvariant().Replace(" ", "");
                    string v = deel.Substring(i + 1).Trim();
                    if (k == "username" || k == "userid" || k == "user") Gebruiker = v;
                    if (k == "database" || k == "initialcatalog") Naam = v;   // de echte naam uit het project gaat voor
                }
                return;
            }
        }

        void ZoekContainer()
        {
            ProcesUitkomst u = Opdracht.Voer("docker", "ps --format \"{{.Names}}\t{{.Image}}\t{{.Ports}}\"", projectMap, null, 30, null);
            if (u.Code != 0) { Probleem = "Docker antwoordt niet (" + Eerste(u.Alles) + "). Draait Docker Desktop?"; Omgeving = true; return; }
            foreach (string r in u.Uitvoer.Split('\n'))
            {
                string[] d = r.Trim().Split('\t');
                if (d.Length < 2) continue;
                if (d[1].ToLowerInvariant().Contains("postgres") || (d.Length > 2 && d[2].Contains("5432"))) { Container = d[0]; return; }
            }
            Probleem = "Er draait geen Docker-container met PostgreSQL."; Omgeving = true;
        }

        static string Eerste(string t)
        {
            foreach (string r in t.Split('\n')) if (r.Trim().Length > 0) return r.Trim();
            return "geen uitvoer";
        }

        /// <summary>Voert SQL uit via psql in de container. Geeft de rijen terug, of een fout.</summary>
        public List<string[]> Sql(string sql, bool terugdraaien, out string fout)
        {
            fout = null;
            List<string[]> rijen = new List<string[]>();
            if (Probleem != null) { fout = Probleem; return rijen; }
            string invoer = terugdraaien ? "BEGIN;\n" + sql.TrimEnd().TrimEnd(';') + ";\nROLLBACK;\n" : sql.TrimEnd().TrimEnd(';') + ";\n";
            string arg = "exec -i " + Container + " psql -U " + Gebruiker + " -d " + Naam + " -q -A -t -F \"|\" -v ON_ERROR_STOP=1";
            ProcesUitkomst u = Opdracht.Voer("docker", arg, projectMap, invoer, 60, null);
            if (u.Code != 0)
            {
                fout = u.Fouten.Trim().Length > 0 ? u.Fouten.Trim() : u.Alles;
                return rijen;
            }
            foreach (string r in u.Uitvoer.Replace("\r", "").Split('\n'))
            {
                if (r.Length == 0) continue;
                rijen.Add(r.Split('|'));
            }
            return rijen;
        }

        /// <summary>Database verwijderen en opnieuw opbouwen met de migraties, en daarna eventueel de SQL-scripts.</summary>
        public bool Opbouwen(List<string> sqlScripts, List<string> verslag)
        {
            ProcesUitkomst d = Opdracht.Voer("dotnet", "ef database drop --force --no-build --project \"" + csproj + "\"", projectMap, null, 180, null);
            if (d.Code != 0)
            {
                if (d.Alles.Contains("Could not execute because the specified command or file was not found") || d.Alles.Contains("dotnet-ef"))
                {
                    Probleem = "Het commando `dotnet ef` werkt niet op deze computer. Installeer het eenmalig met `dotnet tool install --global dotnet-ef`.";
                    Omgeving = true;
                }
                else Probleem = "De database verwijderen mislukte: " + Eerste(d.Alles);
                return false;
            }
            ProcesUitkomst m = Opdracht.Voer("dotnet", "ef database update --no-build --project \"" + csproj + "\"", projectMap, null, 300, null);
            if (m.Code != 0) { Probleem = "De migraties uitvoeren mislukte: " + LaatsteFout(m.Alles); return false; }
            verslag.Add("Database " + Naam + " opnieuw opgebouwd met de migraties.");
            if (sqlScripts != null)
            {
                foreach (string s in sqlScripts)
                {
                    string fout;
                    Sql(File.ReadAllText(s, Encoding.UTF8), false, out fout);
                    if (fout != null) { Probleem = "Het SQL-script " + Path.GetFileName(s) + " gaf een fout: " + Eerste(fout); return false; }
                    verslag.Add("SQL-script " + Path.GetFileName(s) + " uitgevoerd.");
                }
            }
            return true;
        }

        static string LaatsteFout(string t)
        {
            string laatste = null;
            foreach (string r in t.Split('\n'))
                if (r.Contains("rror") || r.Contains("xception")) laatste = r.Trim();
            return laatste ?? Eerste(t);
        }
    }
}
