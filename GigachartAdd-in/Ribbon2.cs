using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using static GlobalsKey;
using static System.Net.Mime.MediaTypeNames;
using GigachartAdd_in.Properties;
using Microsoft.Office.Core;
using System.Drawing;


// TODO:  Выполните эти шаги, чтобы активировать элемент XML ленты:

// 1: Скопируйте следующий блок кода в класс ThisAddin, ThisWorkbook или ThisDocument.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon2();
//  }

// 2. Создайте методы обратного вызова в области "Обратные вызовы ленты" этого класса, чтобы обрабатывать действия
//    пользователя, например нажатие кнопки. Примечание: если эта лента экспортирована из конструктора ленты,
//    переместите свой код из обработчиков событий в методы обратного вызова и модифицируйте этот код, чтобы работать с
//    моделью программирования расширения ленты (RibbonX).

// 3. Назначьте атрибуты тегам элементов управления в XML-файле ленты, чтобы идентифицировать соответствующие методы обратного вызова в своем коде.  

// Дополнительные сведения можно найти в XML-документации для ленты в справке набора средств Visual Studio для Office.



namespace GigachartAdd_in
{
    [ComVisible(true)]
    public class Ribbon2 : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;


        public Ribbon2()
        {

        }

        public async void GetButton(Office.IRibbonControl control)
        {
            if (secretKey != null)
            {
                GigachatChatForm form = new GigachatChatForm();
                form.Show();
            }
            else
            {
                MessageBox.Show("Необходимо вставить ключ Gigachat");
            }
        }

        public void GetButtonKey(Office.IRibbonControl control)
        {
            KeyForm testDialog = new KeyForm();
            testDialog.Show();
        }
        // TODO
        public async void GetButtonConclusion(Office.IRibbonControl control)
        {
            Range currentRange = Globals.ThisAddIn.Application.Selection.Range;
            if (currentRange.Text != null && secretKey != null)
            { 
                string text = "Сделай краткий вывод по этому тексту: " + currentRange.Text;
                try
                {
                    var response = await gigaChatApi.CompletionsAsync(text);
                    currentRange.Text = response.choices[0].message.content;
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Выделенная область пуста или нет ключа Gigachat");
            }
        }

        public async void GetButtonReduce(Office.IRibbonControl control)
        {
            Range currentRange = Globals.ThisAddIn.Application.Selection.Range;
            if (currentRange.Text != null && secretKey != null)
            {
                string text = "Сократи этот текст: " + currentRange.Text;
                try
                {
                    var response = await gigaChatApi.CompletionsAsync(text);
                    currentRange.Text = response.choices[0].message.content;
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Выделенная область пуста или нет ключа Gigachat");
            }
        }

        public async void GetButtonContinue(Office.IRibbonControl control)
        {
            Range currentRange = Globals.ThisAddIn.Application.Selection.Range;
            if (currentRange.Text != null && secretKey != null)
            {
                string text = "Продолжи этот текст: " + currentRange.Text;
                try
                {
                    var response = await gigaChatApi.CompletionsAsync(text);
                    currentRange.Text = response.choices[0].message.content;
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Выделенная область пуста или нет ключа Gigachat");
            }
        }

        public Bitmap getIcon(IRibbonControl control)
        {
            return Resources.icon.ToBitmap();
        }


        #region Элементы IRibbonExtensibility

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("GigachartAdd_in.Ribbon2.xml");
        }

        #endregion

        #region Обратные вызовы ленты
        //Информацию о методах создания обратного вызова см. здесь. Дополнительные сведения о методах добавления обратного вызова см. по ссылке https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        #region Вспомогательные методы

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
