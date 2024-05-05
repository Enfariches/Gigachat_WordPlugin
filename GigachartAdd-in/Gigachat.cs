using System;
using static GlobalsKey;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace GigachartAdd_in
{
    public partial class GigachatChatForm : Form
    {
        public GigachatChatForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Range currentRange = Globals.ThisAddIn.Application.Selection.Range;

            // try/catch maybe bad?
            try
            {
                currentRange.Copy();
                textBox1.Text = Clipboard.GetText();
            }
            catch
            {

            }
            Clipboard.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            TextBox textBoxChat = (TextBox)Controls.Find("textBox1", true)[0];     
            string text = textBoxChat.Text;


            var response = await gigaChatApi.CompletionsAsync(text);
            MessageBox.Show(response.choices[0].message.content);
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
