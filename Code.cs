using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;
using MaterialSkin;

namespace CFC
{
    public partial class CFC_Main : Form
    {
        //var (Name Datei)
        string Stärke = "00", Farbe = "00", Klebend = "00", DateiName = "00";

        // var (SVG Datei)
        int width = 0, height = 0, x = 1000, y = 1000, E_counter=0 , Space=1000;

        // Var (Addidtional)
        bool E = false;
        string folder;
        int width_C = 610, height_C = 310, width_O =0, height_O = 0;

        // Var (XML Doc)
        XmlDocument svgDatei;
        XmlDeclaration svgDeclaration;
        XmlDocumentType docType;
        XmlElement svgRoot;
        XmlElement defsElement;
        XmlElement style;
        XmlCDataSection cdata;
        XmlElement g;
        XmlElement metadata;

        public class FontInstaller
        {
            [DllImport("gdi32.dll")]
            private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

            public static void InstallFont(byte[] fontData)
            {
                IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
                Marshal.Copy(fontData, 0, fontPtr, fontData.Length);

                uint dummy = 0;
                AddFontMemResourceEx(fontPtr, (uint)fontData.Length, IntPtr.Zero, ref dummy);

                Marshal.FreeCoTaskMem(fontPtr);
            }
        }



        public CFC_Main()
        {
            InitializeComponent();
            this.Size = new Size(658, 730);

            if(!System.IO.Directory.Exists(@"C:\\CFC-Dateien")) // Erstellen der Directory wenn noch nicht exestiert
            {
                System.IO.Directory.CreateDirectory(@"C:\\CFC-Dateien");
            }

            x = Convert.ToInt32(textBox1.Text);
            y = Convert.ToInt32(textBox2.Text);
            Space = Convert.ToInt32(textBox3.Text);
            folder = textBox4.Text;

            // skin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green900, Primary.Green900, Primary.Green900, Accent.Purple700, TextShade.WHITE);

            // Install font
            byte[] fontData = Properties.Resources.Zentenar_Fraktur;
            FontInstaller.InstallFont(fontData);
            

        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                width_C = Convert.ToInt32(textBox6.Text);
            }
            catch
            {
                width_C = 610;
                textBox6.Text = "610";
                MessageBox.Show("Fehlerhafter Canvas Width(x) Wert\nWurde auf standart(610) gesetzt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                height_C = Convert.ToInt32(textBox5.Text);
            }
            catch
            {
                height_C = 310;
                textBox5.Text = "310";
                MessageBox.Show("Fehlerhafter Canvas height(y) Wert\nWurde auf standart(310) gesetzt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Space = Convert.ToInt32(textBox3.Text);
            }
            catch
            {
                Space = 1000;
                textBox3.Text = "1000";
                MessageBox.Show("Fehlerhafter Spacing Wert\nWurde auf standart(1000) gesetzt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                x = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                x = 1000;
                textBox1.Text = "1000";
                MessageBox.Show("Fehlerhafter Gen Spacing(x) Wert\nWurde auf standart(1000) gesetzt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                y = Convert.ToInt32(textBox2.Text);
            }
            catch
            {
                y = 1000;
                textBox2.Text = "1000";
                MessageBox.Show("Fehlerhafter Gen Spacing(y) Wert\nWurde auf standart(1000) gesetzt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "1000";
            textBox2.Text = "1000";
            textBox3.Text = "1000";
            textBox4.Text = "C:\\\\CFC-Dateien\\\\";
            textBox5.Text = "310";
            textBox6.Text = "610";
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            folder = textBox4.Text;
            if (!System.IO.Directory.Exists(@"" + folder)) // Erstellen der Directory wenn noch nicht exestiert
            {
                System.IO.Directory.CreateDirectory(@"" + folder);
            }
        }
        private void But_Settings_Click(object sender, EventArgs e)
        {
            if (But_Settings.BackColor == Color.Black)
            {
                //#aa00ff
                But_Settings.BackColor = Color.White;
                this.Size = new Size(1021, 730);
            }
            else
            {
                But_Settings.BackColor = Color.Black;
                this.Size = new Size(658, 730);
            }
        }



        private void Combo_Größe_TextChanged(object sender, EventArgs e)
        {
            if (Combo_Größe.Text == "40x10mm")
            {
                width = 4000;
                height = 1000;
            }
            if (Combo_Größe.Text == "40x15mm")
            {
                width = 4000;
                height = 1500;
            }
            if (Combo_Größe.Text == "50x15mm")
            {
                width = 5000;
                height = 1500;
            }
            if (Combo_Größe.Text == "50x20mm")
            {
                width = 5000;
                height = 2000;
            }
            if (Combo_Größe.Text == "55x15mm")
            {
                width = 5500;
                height = 1500;
            }
            if (Combo_Größe.Text == "60x15mm")
            {
                width = 6000;
                height = 1500;
            }
            if (Combo_Größe.Text == "60x20mm")
            {
                width = 6000;
                height = 2000;
            }
            if (Combo_Größe.Text == "60x30mm")
            {
                width = 6000;
                height = 3000;
            }
            if (Combo_Größe.Text == "60x40mm")
            {
                width = 6000;
                height = 4000;
            }
            if (Combo_Größe.Text == "70x20mm")
            {
                width = 7000;
                height = 2000;
            }
            if (Combo_Größe.Text == "70x25mm")
            {
                width = 7000;
                height = 2500;
            }
            if (Combo_Größe.Text == "70x30mm")
            {
                width = 7000;
                height = 3000;
            }
            if (Combo_Größe.Text == "80x30mm")
            {
                width = 8000;
                height = 3000;
            }
            if (Combo_Größe.Text == "90x40mm")
            {
                width = 9000;
                height = 4000;
            }
            if (Combo_Größe.Text == "100x30mm")
            {
                width = 10000;
                height = 3000;
            }
            if (Combo_Größe.Text == "100x40mm")
            {
                width = 10000;
                height = 4000;
            }
            if (Combo_Größe.Text == "100x50mm")
            {
                width = 10000;
                height = 5000;
            }
            if (Combo_Größe.Text == "100x60mm")
            {
                width = 10000;
                height = 6000;
            }
            if (Combo_Größe.Text == "110x50mm")
            {
                width = 11000;
                height = 5000;
            }
            if (Combo_Größe.Text == "130x45mm")
            {
                width = 13000;
                height = 4500;
            }
            if (Combo_Größe.Text == "150x100mm")
            {
                width = 15000;
                height = 10000;
            }
        }
        private void But_Export_Click(object sender, EventArgs e)
        {
            E = true;
            if (Combo_Typ.Text == "A")
            {
                this.Add_Rect_Typ_A();
            }
            else if (Combo_Typ.Text == "B")
            {
                this.Add_Rect_Typ_B();
            }
            else if (Combo_Typ.Text == "C")
            {
                this.Add_Rect_Typ_C();
            }
            else
            {
                MessageBox.Show("Wähle einen Schild Typen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            E = false;
            E_counter = 0;
            But_Export.Text = "Export";
            x = Convert.ToInt32(textBox1.Text);
            y = Convert.ToInt32(textBox2.Text);
            height_O = 0;
            width_O = 0;
        }
        private void But_Add_Click(object sender, EventArgs e)
        {
            if (Combo_Größe.Text != "Größe")
            {
                if (Combo_Typ.Text == "A")
                {
                    this.Add_Rect_Typ_A();
                }
                else if (Combo_Typ.Text == "B")
                {
                    this.Add_Rect_Typ_B();
                }
                else if (Combo_Typ.Text == "C")
                {
                    this.Add_Rect_Typ_C();
                }
                else
                {
                    MessageBox.Show("Wähle einen Schild Typen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Wähle eine Schild Größe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void Add_Rect_Typ_A()
        {
            if (E_counter < 1)
            {
                // Create the SVG document
                svgDatei = new XmlDocument();
                svgDeclaration = svgDatei.CreateXmlDeclaration("1.0", "UTF-8", "");
                svgDatei.AppendChild(svgDeclaration);
                docType = svgDatei.CreateDocumentType("svg", "-//W3C//DTD SVG 1.1//EN", "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd", null);
                svgDatei.AppendChild(docType);
                // Create the root <svg> element
                svgRoot = svgDatei.CreateElement("svg", "http://www.w3.org/2000/svg");
                svgRoot.SetAttribute("xmlns:space", "preserve");
                svgRoot.SetAttribute("width", "" + width_C + "mm");
                svgRoot.SetAttribute("height", "" + height_C + "mm");
                svgRoot.SetAttribute("version", "1.1");
                svgRoot.SetAttribute("style", "shape-rendering:geometricPrecision; text-rendering:geometricPrecision; image-rendering:optimizeQuality; fill-rule:evenodd; clip-rule:evenodd");
                //svgRoot.SetAttribute("viewBox", "0 0 61000 31000");
                svgRoot.SetAttribute("viewBox", "0 0 "+width_C+"00 "+height_C+"00");
                svgRoot.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");
                // Defs element
                defsElement = svgDatei.CreateElement("defs", "http://www.w3.org/2000/svg");
                svgRoot.AppendChild(defsElement);
                // style element
                style = svgDatei.CreateElement("style", "http://www.w3.org/2000/svg");
                style.SetAttribute("type", "text/css");
                cdata = svgDatei.CreateCDataSection(" .str0 {stroke:red;stroke-width:7.62} .fil0 {fill:none}]]>");
                style.AppendChild(cdata);
                defsElement.AppendChild(style);
                // Create a <g> element with <metadata> and <rect class>
                g = svgDatei.CreateElement("g", "http://www.w3.org/2000/svg");
                g.SetAttribute("id", "Ebene_x0020_1");
                svgRoot.AppendChild(g);
                metadata = svgDatei.CreateElement("metadata", "http://www.w3.org/2000/svg");
                metadata.SetAttribute("id", "CorelCorpID_0Corel-Layer");
                g.AppendChild(metadata);
            }
            if (E == false)
            {
                this.Location_Calculator();
                XmlElement rect = svgDatei.CreateElement("rect", "http://www.w3.org/2000/svg");
                rect.SetAttribute("class", "fil0 str0");
                rect.SetAttribute("x", "" + x);
                rect.SetAttribute("y", "" + y);
                rect.SetAttribute("width", "" + width);
                rect.SetAttribute("height", "" + height);
                g.AppendChild(rect);


                int width2 = width, height2 = height, x2 = x, y2 = y;


                // test text
                XmlElement text = svgDatei.CreateElement("text", "http://www.w3.org/2000/svg");

                text.InnerText = "Test text";
                text.SetAttribute("text-anchor", "middle");
                text.SetAttribute("dy", "0.35em");

                text.SetAttribute("fill", "black");
                width2 = width2 / 2;
                height2 = height2 / 2;
                x2 = x2 + width2;
                y2 = y2 + height2;

                text.SetAttribute("x", ""+x2);
                text.SetAttribute("y", ""+y2);
                text.SetAttribute("font-size", "200");
                text.SetAttribute("font-family", "" + Properties.Resources.Zentenar_Fraktur);

                g.AppendChild(text);


            }
            if (E == true)
            {
                // erst nach export
                svgDatei.AppendChild(svgRoot);

                // Datei name var erstezen
                Stärke = Combo_Stärke.Text;
                Farbe = Combo_Farbe.Text;
                Klebend = Combo_Selbst_Klebend.Text;
                if (Input_Datei_Name.Text != "Name")
                {
                    DateiName = Input_Datei_Name.Text;
                }
                else
                {
                    DateiName = "CFC";
                }
                using (var writer = XmlWriter.Create(@""+folder+ "\\\\" + Stärke + "_" + Farbe + "_" + Klebend + "_" + DateiName + ".svg"))
                {
                    svgDatei.Save(writer);
                }
                MessageBox.Show("Datei gespeichert", "Erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            E_counter++;
            But_Export.Text = "Export (" + E_counter + ")";



           
        }
        public void Add_Rect_Typ_B()
        {
            MessageBox.Show("Dieses Schild ist noch nicht verfügbar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void Add_Rect_Typ_C()
        {
            MessageBox.Show("Dieses Schild ist noch nicht verfügbar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void Location_Calculator()
        {
            if (E_counter >= 1)
            {
                // neu  rand  e größe  e abstand
                x = x + width_O + Space;
                if (x + width >= Convert.ToInt32(width_C + "00"))
                {
                    x = Convert.ToInt32(textBox1.Text);
                    y = y + height_O + Space;

                    if (y + height >= Convert.ToInt32(height_C + "00"))
                    {
                        MessageBox.Show("Bitte Exportiere die Datei jetzt\n out of bounds Error möglich", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            width_O = width;
            if (height >= height_O)
            {
                height_O = height;
            }
        }


    }
}
