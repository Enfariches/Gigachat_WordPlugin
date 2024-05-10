using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using static GlobalsKey;
using static System.Net.Mime.MediaTypeNames;

namespace GigachartAdd_in
{
    public partial class KeyForm : Form
    {
        public KeyForm()
        {
            InitializeComponent();
  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBox textBoxChat = (TextBox)Controls.Find("textBoxKey", true)[0];
            secretKey = textBoxChat.Text;

            // To Think
            string docPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            using StreamWriter outfile = new StreamWriter(Path.Combine(docPath, "secretKey.txt"));
            outfile.Write(secretKey);
            MessageBox.Show("secretKey.txt успешно сохранен в " + docPath);
            gigaChatApi = new GigaChatClass(secretKey);
        }

        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void KeyForm_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("1. Необходимо авторизироваться на сайте developers.sber.ru \n" +
                "2. На сайте зайти в Личное пространство и выбрать GigaChat API \n" +
                "3. На правой стороне страницы нажать на кнопку для генерации Client Secret \n" +
                "4. Скопировать Авторизационные данные и вставить в форму плагина");
        }

        async private void button2_Click(object sender, EventArgs e)
        {
            TextBox textBoxChat = (TextBox)Controls.Find("textBoxKey", true)[0];
            secretKey = textBoxChat.Text;

            gigaChatApi = new GigaChatClass(secretKey);
            var response = await gigaChatApi.CompletionsAsync("");

            if (response != null)
            {
                MessageBox.Show("Ключ валидный");
            }

            else
            {
                MessageBox.Show("Ключ не валидный");
            }
                
        }
    }
}
