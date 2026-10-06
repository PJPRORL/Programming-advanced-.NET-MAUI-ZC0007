// Nakijkscript — motor, deel 7: een verslag als blokken, en die blokken als Markdown en als .docx.
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace Nakijkscript
{
    public enum BlokSoort { Kop1, Kop2, Kop3, Alinea, Meta, Kader, Tabel, Lijst, Lijn }

    public class Blok
    {
        public BlokSoort Soort;
        public string Tekst;                       // kop, alinea, meta
        public string Kader;                       // NOTE, TIP, IMPORTANT, WARNING
        public List<string> Regels = new List<string>();      // kader-alinea's, lijst-items
        public List<string> Kolommen = new List<string>();
        public List<List<string>> Rijen = new List<List<string>>();
        public List<double> Breedtes = new List<double>();
    }

    public class Document
    {
        public List<Blok> Blokken = new List<Blok>();
        public Blok Kop(int n, string t) { Blok b = new Blok(); b.Soort = n == 1 ? BlokSoort.Kop1 : n == 2 ? BlokSoort.Kop2 : BlokSoort.Kop3; b.Tekst = t; Blokken.Add(b); return b; }
        public Blok Alinea(string t) { Blok b = new Blok(); b.Soort = BlokSoort.Alinea; b.Tekst = t; Blokken.Add(b); return b; }
        public Blok Meta(string t) { Blok b = new Blok(); b.Soort = BlokSoort.Meta; b.Tekst = t; Blokken.Add(b); return b; }
        public Blok Lijn() { Blok b = new Blok(); b.Soort = BlokSoort.Lijn; Blokken.Add(b); return b; }
        public Blok Kader(string soort, params string[] regels) { Blok b = new Blok(); b.Soort = BlokSoort.Kader; b.Kader = soort; b.Regels.AddRange(regels); Blokken.Add(b); return b; }
        public Blok Lijst(List<string> items) { Blok b = new Blok(); b.Soort = BlokSoort.Lijst; b.Regels.AddRange(items); Blokken.Add(b); return b; }
        public Blok Tabel(string[] kolommen, double[] breedtes)
        {
            Blok b = new Blok(); b.Soort = BlokSoort.Tabel; b.Kolommen.AddRange(kolommen);
            if (breedtes != null) b.Breedtes.AddRange(breedtes);
            Blokken.Add(b); return b;
        }

        // ------------------------------------------------------------ Markdown

        static string Cel(string t) { return (t ?? "").Replace("|", "\\|").Replace("\r", "").Replace("\n", "<br>"); }

        public string AlsMarkdown()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Blok b in Blokken)
            {
                switch (b.Soort)
                {
                    case BlokSoort.Kop1: sb.Append("# ").Append(b.Tekst).Append("\n\n"); break;
                    case BlokSoort.Kop2: sb.Append("## ").Append(b.Tekst).Append("\n\n"); break;
                    case BlokSoort.Kop3: sb.Append("### ").Append(b.Tekst).Append("\n\n"); break;
                    case BlokSoort.Alinea:
                    case BlokSoort.Meta: sb.Append(b.Tekst).Append("\n\n"); break;
                    case BlokSoort.Lijn: sb.Append("---\n\n"); break;
                    case BlokSoort.Kader:
                        sb.Append("> [!").Append(b.Kader).Append("]\n");
                        for (int i = 0; i < b.Regels.Count; i++)
                        {
                            if (i > 0 && !(b.Regels[i].StartsWith("- ") && b.Regels[i - 1].StartsWith("- "))) sb.Append(">\n");
                            string r = b.Regels[i];
                            if (r.StartsWith("- ")) sb.Append("> ").Append(r).Append('\n');
                            else sb.Append("> ").Append(r.Replace("\n", "\n> ")).Append('\n');
                        }
                        sb.Append('\n');
                        break;
                    case BlokSoort.Lijst:
                        foreach (string r in b.Regels) sb.Append("- ").Append(r.Replace("\n", " ")).Append('\n');
                        sb.Append('\n');
                        break;
                    case BlokSoort.Tabel:
                        sb.Append("| ");
                        foreach (string k in b.Kolommen) sb.Append(Cel(k)).Append(" | ");
                        sb.Length -= 1; sb.Append('\n').Append('|');
                        foreach (string k in b.Kolommen) sb.Append("---|");
                        sb.Append('\n');
                        foreach (List<string> rij in b.Rijen)
                        {
                            sb.Append("| ");
                            foreach (string c in rij) sb.Append(Cel(c)).Append(" | ");
                            sb.Length -= 1; sb.Append('\n');
                        }
                        sb.Append('\n');
                        break;
                }
            }
            return sb.ToString().TrimEnd() + "\n";
        }

        // ------------------------------------------------------------ .docx

        static string X(string t) { return SecurityElement.Escape(t ?? ""); }

        static readonly Regex Inline = new Regex(@"(\*\*.+?\*\*|`[^`]+`|\*[^*\s][^*]*?\*)");

        static string Runs(string tekst, bool vet, string kleur)
        {
            StringBuilder sb = new StringBuilder();
            tekst = Regex.Replace(tekst ?? "", @"<[^>]+>", "");    // geen HTML in Word
            int pos = 0;
            foreach (Match m in Inline.Matches(tekst))
            {
                if (m.Index > pos) sb.Append(Run(tekst.Substring(pos, m.Index - pos), vet, false, false, kleur));
                string s = m.Value;
                if (s.StartsWith("**")) sb.Append(Run(s.Substring(2, s.Length - 4), true, false, false, kleur));
                else if (s.StartsWith("`")) sb.Append(Run(s.Substring(1, s.Length - 2), vet, true, false, kleur));
                else sb.Append(Run(s.Substring(1, s.Length - 2), vet, false, true, kleur));
                pos = m.Index + m.Length;
            }
            if (pos < tekst.Length) sb.Append(Run(tekst.Substring(pos), vet, false, false, kleur));
            return sb.ToString();
        }

        static string Run(string t, bool vet, bool code, bool cursief, string kleur)
        {
            StringBuilder sb = new StringBuilder("<w:r><w:rPr>");
            if (code) sb.Append("<w:rFonts w:ascii=\"Consolas\" w:hAnsi=\"Consolas\" w:cs=\"Consolas\"/>");
            if (vet) sb.Append("<w:b/>");
            if (cursief) sb.Append("<w:i/>");
            if (kleur != null) sb.Append("<w:color w:val=\"" + kleur + "\"/>");
            if (code) sb.Append("<w:color w:val=\"9C2B2B\"/><w:shd w:val=\"clear\" w:color=\"auto\" w:fill=\"F2F2F2\"/>");
            sb.Append("</w:rPr>");
            string[] stukken = t.Replace("\r", "").Split('\n');
            for (int i = 0; i < stukken.Length; i++)
            {
                if (i > 0) sb.Append("<w:br/>");
                sb.Append("<w:t xml:space=\"preserve\">").Append(X(stukken[i])).Append("</w:t>");
            }
            sb.Append("</w:r>");
            return sb.ToString();
        }

        static string P(string stijl, string inhoud, string extraPPr)
        {
            return "<w:p><w:pPr>" + (stijl != null ? "<w:pStyle w:val=\"" + stijl + "\"/>" : "") + (extraPPr ?? "") + "</w:pPr>" + inhoud + "</w:p>";
        }

        public void BewaarDocx(string pad)
        {
            StringBuilder body = new StringBuilder();
            foreach (Blok b in Blokken)
            {
                switch (b.Soort)
                {
                    case BlokSoort.Kop1: body.Append(P("Heading1", Runs(b.Tekst, false, null), null)); break;
                    case BlokSoort.Kop2: body.Append(P("Heading2", Runs(b.Tekst, false, null), null)); break;
                    case BlokSoort.Kop3: body.Append(P("Heading3", Runs(b.Tekst, false, null), null)); break;
                    case BlokSoort.Alinea: body.Append(P(null, Runs(b.Tekst, false, null), null)); break;
                    case BlokSoort.Meta: body.Append(P(null, Runs(b.Tekst, false, "595959"), null)); break;
                    case BlokSoort.Lijn:
                        body.Append(P(null, "", "<w:pBdr><w:bottom w:val=\"single\" w:sz=\"6\" w:space=\"1\" w:color=\"BFBFBF\"/></w:pBdr>"));
                        break;
                    case BlokSoort.Lijst:
                        foreach (string r in b.Regels) body.Append(P(null, Run("•\t", false, false, false, null) + Runs(r, false, null), "<w:ind w:left=\"360\" w:hanging=\"360\"/>"));
                        break;
                    case BlokSoort.Kader:
                        {
                            string kleur = b.Kader == "TIP" ? "2F9E44" : b.Kader == "WARNING" ? "E8590C" : b.Kader == "IMPORTANT" ? "7048E8" : "1C7ED6";
                            string fill = b.Kader == "TIP" ? "EBFBEE" : b.Kader == "WARNING" ? "FFF4E6" : b.Kader == "IMPORTANT" ? "F3F0FF" : "E7F5FF";
                            string label = b.Kader == "TIP" ? "Tip" : b.Kader == "WARNING" ? "Let op" : b.Kader == "IMPORTANT" ? "Belangrijk" : "Opmerking";
                            string ppr = "<w:pBdr><w:left w:val=\"single\" w:sz=\"24\" w:space=\"8\" w:color=\"" + kleur + "\"/></w:pBdr><w:shd w:val=\"clear\" w:color=\"auto\" w:fill=\"" + fill + "\"/><w:ind w:left=\"200\"/><w:spacing w:after=\"60\"/>";
                            body.Append(P(null, Run(label, true, false, false, kleur), ppr));
                            foreach (string r in b.Regels)
                            {
                                if (r.StartsWith("- ")) body.Append(P(null, Run("•\t", false, false, false, null) + Runs(r.Substring(2), false, null), ppr));
                                else body.Append(P(null, Runs(r, false, null), ppr));
                            }
                            body.Append(P(null, "", null));
                            break;
                        }
                    case BlokSoort.Tabel:
                        {
                            int n = b.Kolommen.Count;
                            int totaal = 9360;
                            body.Append("<w:tbl><w:tblPr><w:tblStyle w:val=\"Rooster\"/><w:tblW w:w=\"" + totaal + "\" w:type=\"dxa\"/><w:tblLayout w:type=\"fixed\"/></w:tblPr><w:tblGrid>");
                            int[] w = new int[n];
                            for (int i = 0; i < n; i++)
                            {
                                double f = b.Breedtes.Count == n ? b.Breedtes[i] : 1.0 / n;
                                w[i] = (int)(totaal * f);
                                body.Append("<w:gridCol w:w=\"" + w[i] + "\"/>");
                            }
                            body.Append("</w:tblGrid>");
                            body.Append("<w:tr><w:trPr><w:tblHeader/></w:trPr>");
                            for (int i = 0; i < n; i++)
                                body.Append("<w:tc><w:tcPr><w:tcW w:w=\"" + w[i] + "\" w:type=\"dxa\"/><w:shd w:val=\"clear\" w:color=\"auto\" w:fill=\"D9E2F3\"/></w:tcPr>" + P(null, Runs(b.Kolommen[i], true, null), "<w:spacing w:after=\"0\"/>") + "</w:tc>");
                            body.Append("</w:tr>");
                            foreach (List<string> rij in b.Rijen)
                            {
                                body.Append("<w:tr>");
                                for (int i = 0; i < n; i++)
                                {
                                    string c = i < rij.Count ? rij[i] : "";
                                    body.Append("<w:tc><w:tcPr><w:tcW w:w=\"" + w[i] + "\" w:type=\"dxa\"/></w:tcPr>" + P(null, Runs(c, false, null), "<w:spacing w:after=\"0\"/>") + "</w:tc>");
                                }
                                body.Append("</w:tr>");
                            }
                            body.Append("</w:tbl>");
                            body.Append(P(null, "", null));
                            break;
                        }
                }
            }
            string document = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
                "<w:document xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"><w:body>" + body +
                "<w:sectPr><w:pgSz w:w=\"11906\" w:h=\"16838\"/><w:pgMar w:top=\"1134\" w:right=\"1134\" w:bottom=\"1134\" w:left=\"1134\" w:header=\"567\" w:footer=\"567\" w:gutter=\"0\"/></w:sectPr></w:body></w:document>";
            string stijlen = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
                "<w:styles xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\">" +
                "<w:docDefaults><w:rPrDefault><w:rPr><w:rFonts w:ascii=\"Calibri\" w:hAnsi=\"Calibri\" w:cs=\"Calibri\"/><w:sz w:val=\"21\"/><w:lang w:val=\"nl-BE\"/></w:rPr></w:rPrDefault>" +
                "<w:pPrDefault><w:pPr><w:spacing w:after=\"120\" w:line=\"264\" w:lineRule=\"auto\"/></w:pPr></w:pPrDefault></w:docDefaults>" +
                "<w:style w:type=\"paragraph\" w:default=\"1\" w:styleId=\"Normal\"><w:name w:val=\"Normal\"/></w:style>" +
                Kopstijl("Heading1", "heading 1", 34, "1F3864", 360, 160, 0) +
                Kopstijl("Heading2", "heading 2", 28, "2F5496", 300, 120, 1) +
                Kopstijl("Heading3", "heading 3", 24, "2F5496", 240, 80, 2) +
                "<w:style w:type=\"table\" w:styleId=\"Rooster\"><w:name w:val=\"Rooster\"/><w:tblPr><w:tblBorders>" +
                "<w:top w:val=\"single\" w:sz=\"4\" w:color=\"BFBFBF\"/><w:left w:val=\"single\" w:sz=\"4\" w:color=\"BFBFBF\"/><w:bottom w:val=\"single\" w:sz=\"4\" w:color=\"BFBFBF\"/><w:right w:val=\"single\" w:sz=\"4\" w:color=\"BFBFBF\"/>" +
                "<w:insideH w:val=\"single\" w:sz=\"4\" w:color=\"BFBFBF\"/><w:insideV w:val=\"single\" w:sz=\"4\" w:color=\"BFBFBF\"/></w:tblBorders>" +
                "<w:tblCellMar><w:top w:w=\"60\" w:type=\"dxa\"/><w:left w:w=\"100\" w:type=\"dxa\"/><w:bottom w:w=\"60\" w:type=\"dxa\"/><w:right w:w=\"100\" w:type=\"dxa\"/></w:tblCellMar></w:tblPr></w:style>" +
                "</w:styles>";
            string types = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
                "<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">" +
                "<Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>" +
                "<Default Extension=\"xml\" ContentType=\"application/xml\"/>" +
                "<Override PartName=\"/word/document.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml\"/>" +
                "<Override PartName=\"/word/styles.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml\"/>" +
                "</Types>";
            string rels = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
                "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
                "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"word/document.xml\"/></Relationships>";
            string docRels = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
                "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
                "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles\" Target=\"styles.xml\"/></Relationships>";
            if (File.Exists(pad)) File.Delete(pad);
            using (FileStream fs = new FileStream(pad, FileMode.Create))
            using (ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Create))
            {
                Schrijf(zip, "[Content_Types].xml", types);
                Schrijf(zip, "_rels/.rels", rels);
                Schrijf(zip, "word/document.xml", document);
                Schrijf(zip, "word/styles.xml", stijlen);
                Schrijf(zip, "word/_rels/document.xml.rels", docRels);
            }
        }

        static string Kopstijl(string id, string naam, int grootte, string kleur, int voor, int na, int niveau)
        {
            return "<w:style w:type=\"paragraph\" w:styleId=\"" + id + "\"><w:name w:val=\"" + naam + "\"/><w:basedOn w:val=\"Normal\"/><w:next w:val=\"Normal\"/><w:qFormat/>" +
                   "<w:pPr><w:keepNext/><w:spacing w:before=\"" + voor + "\" w:after=\"" + na + "\"/><w:outlineLvl w:val=\"" + niveau + "\"/></w:pPr>" +
                   "<w:rPr><w:b/><w:color w:val=\"" + kleur + "\"/><w:sz w:val=\"" + grootte + "\"/></w:rPr></w:style>";
        }

        static void Schrijf(ZipArchive zip, string naam, string inhoud)
        {
            ZipArchiveEntry e = zip.CreateEntry(naam, CompressionLevel.Optimal);
            using (StreamWriter w = new StreamWriter(e.Open(), new UTF8Encoding(false))) w.Write(inhoud);
        }
    }
}
