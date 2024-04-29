using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using static GlobalsKey;

namespace GigachartAdd_in
{
    public partial class KeyForm : Form
    {
        public KeyForm()
        {
            InitializeComponent();
            if (secretKey == null)
            {
                string docPath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                try
                {
                    using StreamReader reader = new StreamReader(Path.Combine(docPath, "secretKey.txt"));
                    secretKey = reader.ReadToEnd();
                }
                catch (Exception ex)
                {

                }
            }
            textBoxKey.Text = secretKey;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBox textBoxChat = (TextBox)Controls.Find("textBoxKey", true)[0];
            secretKey = textBoxChat.Text;

            // To Think
            string docPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using StreamWriter outfile = new StreamWriter(Path.Combine(docPath, "secretKey.txt"));
            outfile.Write(secretKey);
        }

        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
