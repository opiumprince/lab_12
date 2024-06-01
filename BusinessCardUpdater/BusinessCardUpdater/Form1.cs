using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace BusinessCardUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpdateCard_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string title = txtTitle.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;

            string templatePath = @"C:\Users\Bohdan\Desktop\test.dotm";

            UpdateBusinessCard(templatePath, name, title, phone, email);
        }

        private void UpdateBusinessCard(string templatePath, string name, string title, string phone, string email)
        {
            Word.Application wordApp = new Word.Application();
            Word.Document doc = null;

            try
            {
                doc = wordApp.Documents.Open(templatePath);

                ReplaceText("{Name}", name, doc);
                ReplaceText("{Title}", title, doc);
                ReplaceText("{Phone}", phone, doc);
                ReplaceText("{Email}", email, doc);

                doc.Save();
                MessageBox.Show("Візитку успішно оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час оновлення візитки: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();
                wordApp.Quit();
            }
        }

        private void ReplaceText(string placeholder, string newValue, Word.Document doc)
        {
            foreach (Word.Range range in doc.StoryRanges)
            {
                Word.Find find = range.Find;
                find.Text = placeholder;
                find.Replacement.Text = newValue;
                find.Execute(Replace: Word.WdReplace.wdReplaceAll);
            }
        }
    }
}
