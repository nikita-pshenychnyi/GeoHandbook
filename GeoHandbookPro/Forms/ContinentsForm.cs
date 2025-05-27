namespace GeoHandbookPro.Forms
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using GeoHandbookPro.Models;
    using GeoHandbookPro.Services;

    public partial class ContinentsForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private Continent _selectedContinent = null;

        // Конструктор, що приймає DatabaseManager
        public ContinentsForm(DatabaseManager dbManager)
        {
            InitializeComponent();
            _dbManager = dbManager ?? throw new ArgumentNullException(nameof(dbManager));
            LoadContinents();
            ClearForm(); // Початкове очищення полів
        }

        // Конструктор за замовчуванням (якщо потрібен для дизайнера, 
        // але краще уникати його використання без dbManager, якщо форма залежить від нього)
        public ContinentsForm()
        {
            InitializeComponent();
            // УВАГА: Якщо цей конструктор викликається, _dbManager буде null.
            // Це може призвести до помилок, якщо не обробити належним чином.
            // Розгляньте можливість видалення цього конструктора або додавання логіки
            // для створення екземпляра _dbManager тут, якщо форма може працювати автономно.
            // Для нашого випадку, коли форма викликається з MainForm, цей конструктор не потрібен.
            // Якщо дизайнер форм його вимагає, переконайтеся, що логіка, яка залежить від _dbManager,
            // не виконується, або _dbManager ініціалізується.
            // _dbManager = new DatabaseManager(); // Наприклад, якщо форма може працювати автономно
            // LoadContinents();
            // ClearForm();
            MessageBox.Show("Увага: ContinentsForm була створена без DatabaseManager. Функціональність може бути обмежена.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void LoadContinents()
        {
            try
            {
                var continents = _dbManager.GetAllContinents();
                dgvContinents.DataSource = null; // Очистити попереднє джерело
                dgvContinents.DataSource = continents;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження материків: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvContinents.Columns.Contains("ContinentID"))
            {
                dgvContinents.Columns["ContinentID"].HeaderText = "ID";
                // dgvContinents.Columns["ContinentID"].Visible = false; // Можна приховати ID
            }
            if (dgvContinents.Columns.Contains("Name"))
            {
                dgvContinents.Columns["Name"].HeaderText = "Назва материка";
                dgvContinents.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void ClearForm()
        {
            txtContinentId.Clear();
            txtContinentName.Clear();
            _selectedContinent = null;
            btnAddContinent.Enabled = true;
            btnEditContinent.Enabled = false;
            btnDeleteContinent.Enabled = false;
            txtContinentName.Focus();
        }

        private void dgvContinents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvContinents.CurrentRow != null && dgvContinents.CurrentRow.DataBoundItem is Continent selected)
            {
                _selectedContinent = selected;
                txtContinentId.Text = _selectedContinent.ContinentID.ToString();
                txtContinentName.Text = _selectedContinent.Name;

                btnAddContinent.Enabled = false;
                btnEditContinent.Enabled = true;
                btnDeleteContinent.Enabled = true;
            }
            else
            {
                // ClearForm(); // Можна очищати, якщо нічого не вибрано, але це може бути незручно
            }
        }

        private void btnAddContinent_Click(object sender, EventArgs e)
        {
            string continentName = txtContinentName.Text.Trim();
            if (string.IsNullOrEmpty(continentName))
            {
                MessageBox.Show("Назва материка не може бути порожньою.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContinentName.Focus();
                return;
            }

            var newContinent = new Continent { Name = continentName };
            if (_dbManager.AddContinent(newContinent))
            {
                MessageBox.Show("Материк успішно додано.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadContinents();
                ClearForm();
            }
            // Повідомлення про помилку вже обробляється в AddContinent
        }

        private void btnEditContinent_Click(object sender, EventArgs e)
        {
            if (_selectedContinent == null)
            {
                MessageBox.Show("Будь ласка, виберіть материк для редагування.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string continentName = txtContinentName.Text.Trim();
            if (string.IsNullOrEmpty(continentName))
            {
                MessageBox.Show("Назва материка не може бути порожньою.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContinentName.Focus();
                return;
            }

            // Перевірка, чи назва змінилася, щоб уникнути зайвого запиту до БД,
            // якщо користувач просто натиснув "Редагувати" без змін.
            if (_selectedContinent.Name == continentName)
            {
                MessageBox.Show("Змін не виявлено.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            _selectedContinent.Name = continentName;
            if (_dbManager.UpdateContinent(_selectedContinent))
            {
                MessageBox.Show("Материк успішно оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadContinents();
                ClearForm();
            }
            // Повідомлення про помилку вже обробляється в UpdateContinent
        }

        private void btnDeleteContinent_Click(object sender, EventArgs e)
        {
            if (_selectedContinent == null)
            {
                MessageBox.Show("Будь ласка, виберіть материк для видалення.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmResult = MessageBox.Show($"Ви впевнені, що хочете видалити материк '{_selectedContinent.Name}'? Це також може видалити пов'язані країни, регіони та міста, якщо налаштовано каскадне видалення.", "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                if (_dbManager.DeleteContinent(_selectedContinent.ContinentID))
                {
                    MessageBox.Show("Материк успішно видалено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadContinents();
                    ClearForm();
                }
                // Повідомлення про помилку вже обробляється в DeleteContinent
            }
        }

        private void btnCancelChangesContinent_Click(object sender, EventArgs e)
        {
            ClearForm();
            // Якщо був вибраний рядок, можна відновити його дані або просто зняти виділення
            dgvContinents.ClearSelection();
            if (dgvContinents.Rows.Count > 0)
                dgvContinents.CurrentCell = null; // Знімає фокус з комірки
            btnAddContinent.Enabled = true;
            btnEditContinent.Enabled = false;
            btnDeleteContinent.Enabled = false;
        }

        private void btnCloseContinents_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Переконайтеся, що у вас є ці елементи керування на формі ContinentsForm
        // та їхні імена відповідають:
        // DataGridView dgvContinents;
        // TextBox txtContinentId; (Властивість ReadOnly = true)
        // TextBox txtContinentName;
        // Button btnAddContinent;
        // Button btnEditContinent;
        // Button btnDeleteContinent;
        // Button btnCancelChangesContinent; (Або btnClearForm)
        // Button btnCloseContinents; (Або btnClose)
    }
}
