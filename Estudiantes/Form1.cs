using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estudiantes
{
    public partial class Form1 : Form
    {
        private Students s;
        public Form1()
        {
            InitializeComponent();

            var listTextBox = new List<TextBox>();
            var listLabel= new List<Label>();
           // var dgv = new DataGridView();
            listTextBox.Add(txtIDNumber);
            listTextBox.Add(txtName);
            listTextBox.Add(txtLastName);
            listTextBox.Add(txtEmail);

            listLabel.Add(lblIdNumber);
            listLabel.Add(lblName);
            listLabel.Add(lblLastName);
            listLabel.Add(lblEmail);
            listLabel.Add(lblPage);
           // dgv = dgvStudents;

            Object[] objects = {
                profileImage,
                Properties.Resources.csharpIcon,
                dgvStudents,
                numericUpDown
            
            };

            s = new Students(listTextBox, listLabel, objects);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void profileImage_Click(object sender, EventArgs e)
        {
            
            s.uploadImage.LoadImage(profileImage);
        }

        private void txtIDNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtIDNumber.Text.Equals(""))
            {
                lblIdNumber.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lblIdNumber.ForeColor = Color.Green;
                lblIdNumber.Text = "ID Number: ";
            }
        }

        private void txtIDNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            s.textBoxEvent.numberKeyPress(e);
            
            lblIdNumber.Text = "ID Number";
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(""))
            {
                lblName.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lblName.ForeColor = Color.Green;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            s.textBoxEvent.textKeyPress(e);
            lblName.Text = "Name";
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            if (txtLastName.Text.Equals(""))
            {
                lblLastName.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lblLastName.ForeColor = Color.Green;
            }
        }

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            s.textBoxEvent.textKeyPress(e);
            lblLastName.Text = "Last Name";
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text.Equals(""))
            {
                lblEmail.ForeColor = Color.LightSlateGray;
                lblEmail.Text = "Email";

            }
            else
            {
                lblEmail.Text = "Email";
                lblEmail.ForeColor = Color.Green;
            }
        }

     /*   private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            s.textBoxEvent.textKeyPress(e);
        }*/

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            s.Register();
           // s.textBoxEvent.emailChecked(txtEmail.Text.ToString());
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            s.GetStudents(txtSearch.Text);
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            s.Page("First");
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            s.Page("Preview");
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            s.Page("Next");
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            s.Page("Last");
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            s.Register_Pages();
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvStudents.Rows.Count != 0)
            {
              
                 s.DgvGetCliente();
            }


        }

        private void dgvStudents_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvStudents.Rows.Count != 0)
            {
                
                 s.DgvGetCliente();
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            s.Remove();

        }
    }
}
