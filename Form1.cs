using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buoi2_Bai2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        class Student
        {
            public string studentId;
            public string fullName;
            public string gender;
            public string faculty;
            public string averageScore;

            public Student() { }

            public Student(string studentId, string fullName, string gender, string faculty, string averageScore)
            {
                this.studentId = studentId;
                this.fullName = fullName;
                this.gender = gender;
                this.faculty = faculty;
                this.averageScore = averageScore;
            }
        }
        List<Student> students = new List<Student>();

        private void Form1_Load(object sender, EventArgs e)
        {
            txtFaculty.SelectedIndex = 0;
            txtSumMale.Text = "0";
            txtSumFemale.Text = "0";
        }
        public int Value(object sender, EventArgs e)
        {
            Student student = new Student();
            student.studentId = txtStudentId.Text;
            student.fullName = txtFullName.Text;
            student.gender = optFemale.Checked ? "Nu" : "Nam";
            student.faculty = txtFaculty.Text;
            student.averageScore = txtAverageScore.Text;
            for (int i = 0; i < students.Count; i++)
            {
                if (student.studentId == students[i].studentId)
                {
                    students[i].studentId = txtStudentId.Text;
                    students[i].fullName = txtFullName.Text;
                    students[i].gender = optFemale.Checked ? "Nu" : "Nam";
                    students[i].averageScore = txtAverageScore.Text;
                    students[i].faculty = txtFaculty.Text;
                    return i;
                }
            }
            students.Add(student);
            return -1;
        }
        private void InserUpdate(int selectedRow)
        {
            dgvStudent.Rows[selectedRow].Cells[0].Value = txtStudentId.Text;
            dgvStudent.Rows[selectedRow].Cells[1].Value = txtFullName.Text;
            dgvStudent.Rows[selectedRow].Cells[2].Value = optFemale.Checked ? "Nu" : "Nam";
            dgvStudent.Rows[selectedRow].Cells[3].Value = float.Parse(txtAverageScore.Text).ToString();
            dgvStudent.Rows[selectedRow].Cells[4].Value = txtFaculty.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStudentId.Text == "" || txtFullName.Text == "" || txtAverageScore.Text == "")
                {
                    throw new Exception("Vui long nhap day du thong tin sinh vien!");
                }
                int getRow = Value(sender, e);
                if (getRow == -1) //Them
                {
                    getRow = dgvStudent.Rows.Add();
                    InserUpdate(getRow);
                    MessageBox.Show("Them du lieu thanh cong!", "Thong bao", MessageBoxButtons.OK);
                }
                else //Cap nhat
                {
                    InserUpdate(getRow);
                    MessageBox.Show("Cap nhat du lieu thanh cong!", "Thong bao", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            int male = 0;
            int female = 0;
            foreach (var std in students)
            {
                if (std.gender == "Nam")
                {
                    male++;
                }
                else
                {
                    female++;
                }
            }
            txtSumMale.Text = Convert.ToString(male);
            txtSumFemale.Text = Convert.ToString(female);


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = Value(sender, e);
                if (selectedRow == -1)
                {
                    throw new Exception("Khong tim thay MSSV can xoa!");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Ban co muon xoa ?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        students.RemoveAt(selectedRow);
                        dgvStudent.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xoa sinh vien thanh cong!", "Thong bao", MessageBoxButtons.OK);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            int male = 0;
            int female = 0;
            foreach (var std in students)
            {
                if (std.gender == "Nam")
                {
                    male++;
                }
                else
                {
                    female++;
                }
            }
            txtSumMale.Text = Convert.ToString(male);
            txtSumFemale.Text = Convert.ToString(female);

        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvStudent.CurrentRow.Selected = true;
            txtStudentId.Text = dgvStudent.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (dgvStudent.Rows[e.RowIndex].Cells[0].Value.ToString() == "Nam")
            {
                optMale.Checked = true;
                optFemale.Checked = false;

            }
            else
            {
                optFemale.Checked = false;
                optMale.Checked = true;
            }
            txtAverageScore.Text = dgvStudent.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtFaculty.Text = dgvStudent.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
