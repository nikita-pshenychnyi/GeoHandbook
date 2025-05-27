namespace GeoHandbookPro.Forms
{
    using System;
    using System.Windows.Forms;
    using System.Reflection; // Для отримання версії

    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            SetProgramInfo();
        }

        private void SetProgramInfo()
        {
            // Встановлюємо назву програми (можна взяти з властивостей проекту або вказати вручну)
            // Переконайтеся, що у вас є Label з іменем lblProgramTitle
            if (this.Controls.ContainsKey("lblProgramTitle"))
            {
                (this.Controls["lblProgramTitle"] as Label).Text = "Довідник географа";
            }
            else if (lblProgramTitle != null) // Якщо поле вже існує
            {
                lblProgramTitle.Text = "Довідник географа";
            }


            // Встановлюємо версію програми
            // Переконайтеся, що у вас є Label з іменем lblVersionInfo
            string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0";
            if (this.Controls.ContainsKey("lblVersionInfo"))
            {
                (this.Controls["lblVersionInfo"] as Label).Text = $"Версія: {version}";
            }
            else if (lblVersionInfo != null)
            {
                lblVersionInfo.Text = $"Версія: {version}";
            }


            // Встановлюємо інформацію про автора
            // Переконайтеся, що у вас є Label з іменем lblAuthorInfo
            if (this.Controls.ContainsKey("lblAuthorInfo"))
            {
                (this.Controls["lblAuthorInfo"] as Label).Text = "";
            }
            else if (lblAuthorInfo != null)
            {
                lblAuthorInfo.Text = "";
            }

            // Можна додати іншу інформацію, наприклад, опис, посилання тощо.
            // Переконайтеся, що у вас є Label з іменем lblDescription
            if (this.Controls.ContainsKey("lblDescription"))
            {
                (this.Controls["lblDescription"] as Label).Text = "Курсовий проект для роботи з географічними даними.";
            }
            else if (lblDescription != null)
            {
                lblDescription.Text = "Курсовий проект для роботи з географічними даними.";
            }
        }

        // Обробник для кнопки "OK" або "Закрити"
        // Переконайтеся, що у вас є Button з іменем btnOkAbout
        private void btnOkAbout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 
    }
}
