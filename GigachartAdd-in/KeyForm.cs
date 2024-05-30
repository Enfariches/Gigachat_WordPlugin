using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using static GlobalsKey;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace GigachartAdd_in
{
    public partial class KeyForm : Form
    {
        public KeyForm()
        {
            InitializeComponent();
  
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            TextBox textBoxChat = (TextBox)Controls.Find("textBoxKey", true)[0];
            secretKey = textBoxChat.Text;

            gigaChatApi = new GigaChatClass(secretKey);
            var response = await gigaChatApi.CompletionsAsync("");

            if (response != null)
            {
                string docPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                using StreamWriter outfile = new StreamWriter(Path.Combine(docPath, "secretKey.txt"));
                outfile.Write(secretKey);
                MessageBox.Show("secretKey.txt успешно сохранен в " + docPath);
            }

            else
            {
                MessageBox.Show("Ключ не валидный");
            }

        }

        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void KeyForm_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HelpForm newfrm = new HelpForm();
            newfrm.Show();
        }
    }
}
