using System;
using System.Windows.Forms;

namespace lab_2_
{
    public partial class Form1 : Form
    {
        private readonly TeacherRepository _repository;

        public Form1()
        {
            InitializeComponent();
            _repository = new TeacherRepository();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Виклик вибірки EF Core
                dataGridView1.DataSource = _repository.GetTeachersOverview();

                // Форматування назв стовпчиків
                if (dataGridView1.Columns["ПІБ_Викладача"] != null)
                    dataGridView1.Columns["ПІБ_Викладача"].HeaderText = "ПІБ Викладача";
                if (dataGridView1.Columns["Місце_роботи"] != null)
                    dataGridView1.Columns["Місце_роботи"].HeaderText = "Місце роботи";
                if (dataGridView1.Columns["Погодинна_ставка"] != null)
                    dataGridView1.Columns["Погодинна_ставка"].HeaderText = "Погодинна ставка";
                if (dataGridView1.Columns["Прочитані_години"] != null)
                    dataGridView1.Columns["Прочитані_години"].HeaderText = "Прочитані години";
                if (dataGridView1.Columns["Домашня_адреса"] != null)
                    dataGridView1.Columns["Домашня_адреса"].HeaderText = "Домашня адреса";

                dataGridView1.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка Entity Framework Core: " + ex.Message,
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 