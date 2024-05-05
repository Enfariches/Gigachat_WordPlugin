using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using Microsoft.Office.Core;
using System.Windows.Forms;
using System.IO;

namespace GigachartAdd_in
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            if(GlobalsKey.secretKey == null)
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                try
                {
                    using StreamReader reader = new StreamReader(Path.Combine(docPath, "secretKey.txt"));
                    GlobalsKey.secretKey = reader.ReadToEnd();
                    GlobalsKey.gigaChatApi = new GigaChatClass(GlobalsKey.secretKey);
                }
                catch
                {

                }
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {

        }

        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon2();
        }

        #region Код, автоматически созданный VSTO

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
