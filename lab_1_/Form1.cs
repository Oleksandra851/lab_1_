using System;
using System.Windows.Forms;

namespace lab_1_
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
                dataGridView1.DataSource = _repository.GetTeachersOverview();
                dataGridView1.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка підключення до БД: " + ex.Message,
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}