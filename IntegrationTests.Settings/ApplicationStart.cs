using System;
using System.Drawing;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Text;

namespace IntegrationTests.Settings
{
    public partial class ApplicationStart : Form
    {
        private wdSettings _webDriverSettings = new wdSettings();

        public ApplicationStart()
        {
            GetSettings();
            InitializeComponent();

            if (String.IsNullOrEmpty(_webDriverSettings.ApplicationPath)) _webDriverSettings.ApplicationPath = "http://www.PORDL.com";
            if (String.IsNullOrEmpty(_webDriverSettings.ApplicationName)) _webDriverSettings.ApplicationName = "PORDL - Feeds Transformed";
            BindForm();
        }

        private void BindForm()
        {
            textBox1.DataBindings.Add(new Binding("Text", _webDriverSettings, "AdminUserName"));
            textBox2.DataBindings.Add(new Binding("Text", _webDriverSettings, "AdminPassword"));
            textBox3.DataBindings.Add(new Binding("Text", _webDriverSettings, "ApplicationPath"));
            textBox4.DataBindings.Add(new Binding("Text", _webDriverSettings, "ApplicationName")); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _webDriverSettings.AdminUserName = textBox1.Text;
            _webDriverSettings.AdminPassword = textBox2.Text;
            _webDriverSettings.ApplicationPath = textBox3.Text;
            _webDriverSettings.ApplicationName = textBox4.Text;

            SaveSettings();
        }

        #region settings            

        private void GetSettings()
        {
            var reader = default(System.Xml.XmlReader);
            try
            {
                reader = System.Xml.XmlReader.Create(_webDriverSettings.SettingsFileTempPath);
            }
            catch (FileNotFoundException ex)
            {
                SaveSettings();
                reader = System.Xml.XmlReader.Create(_webDriverSettings.SettingsFileTempPath);
            }


            var mySerializer = new XmlSerializer(typeof(wdSettings));
            _webDriverSettings = (wdSettings)mySerializer.Deserialize(reader);

            reader.Close();
            reader.Dispose();
        }

        private void SaveSettings()
        {
            try
            {
                var ser = default(XmlSerializer);
                ser = new XmlSerializer(typeof(wdSettings));
                var memStream = new MemoryStream();
                var xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8) { Namespaces = true };
                ser.Serialize(xmlWriter, _webDriverSettings);
                xmlWriter.Close();
                memStream.Close();

                var xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
                xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));

                System.IO.File.WriteAllText(_webDriverSettings.SettingsFileTempPath, xml);

                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                button1.Enabled = false;
                label5.Text = "Settings Saved";
                label5.ForeColor = Color.Firebrick;
            }
            catch (Exception)
            {
                this.Close();
                throw;
            }
        }

        #endregion
    
    }
}
