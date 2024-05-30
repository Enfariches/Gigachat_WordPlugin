using System;
using static GlobalsKey;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.IO.Pipes;

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

            if (firstMessage)
            {
                dialogBox.SelectedText = "> " + text;
                firstMessage = false;
            }
            else
            {
                dialogBox.SelectedText = "\r\n" + "> " + text;
            }

            var response = await gigaChatApi.CompletionsAsync(text);
            dialogBox.BeginInvoke(new Action(() => { dialogBox.AppendText("\r\n" + "> " + response.choices[0].message.content); }));
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void dialogBox_TextChanged(object sender, EventArgs e)
        {

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            firstMessage = true;

        }


    }
}
