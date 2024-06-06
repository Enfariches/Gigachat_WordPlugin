using System;
using static GlobalsKey;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.IO.Pipes;
using System.Drawing;

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
            textBoxChat.Clear();

            if (firstMessage)
            {
                dialogBox.SelectedText = "> " + text;
                firstMessage = false;
            }
            else
            {
                dialogBox.SelectedText = "\r\n" + "> " + text;
            }

            dialogBox.SelectionColor = Color.Green;
            var response = await gigaChatApi.CompletionsAsync(text);

            dialogBox.BeginInvoke(new Action(() =>
            {
                try
                {
                    dialogBox.AppendText("\r\n" + "> " + response.choices[0].message.content);
                    dialogBox.SelectionColor = Color.Black;
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
                            
            }));
            
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Неа");
            }
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(this, new EventArgs());
            }
        }
    }
}
