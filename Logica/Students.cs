using Data;
using LinqToDB;
using LinqToDB.Data;
using Logic.Library;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica
{
    public class Students  : Libraries
    {
        private List<TextBox> list = new List<TextBox>();
        private List<Label> labels = new List<Label>();
        private PictureBox image;
        private Bitmap _imgBitMap;
        private DataGridView _dgv;
        private NumericUpDown _numericUpDown;
        private Page<Student> _page;
        private string accion = "insert";

       // public Libraries libraries;
        public Students(List<TextBox> textBoxList,List<Label> listLabel, object[] objects) { 
            
            this.list = textBoxList;
            this.labels = listLabel;
            this.image= (PictureBox)objects[0];
            this._imgBitMap = (Bitmap)objects[1];
            this._dgv = (DataGridView)objects[2];
            this._numericUpDown = (NumericUpDown)objects[3];
            ResetTheFields();
          //  this.libraries = new Libraries();
        }
        public void Remove()
        {

            _Student.Where(s => s.id.Equals(this.idEstudiante)).Delete();
            this.ResetTheFields();
        }

        public void Register()
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].Text.Equals(""))
                {
                    this.labels[i].Text = "Este campo es requerido";
                    this.labels[i].ForeColor = Color.Red;
                    list[i].Focus();
                }
            }
            if (textBoxEvent.emailChecked(list[3].Text.ToString()))
            {
                
                labels[3].Text = "Email";
                var db = new Connection();

                var students = _Student.Where(s => s.email.Equals(list[3].Text)).ToList();
                var studentsId = _Student.Where(s => s.id.Equals(Convert.ToInt64(list[0].Text))).ToList();

                if (students.Count.Equals(0) || studentsId.Count.Equals(0))
                {
                    Save();
                    
                }
                else
                {
                    if (students[0].id.Equals(idEstudiante))
                    {
                        Save();
                    }
                    else
                    {
                        labels[3].Text = "El email ya está tomado";
                        labels[3].ForeColor = Color.Red;
                        labels[3].Focus();
                    }
                    
                    

                }


            }
            else
            {
               
                
                labels[3].Text = "Email no valido";
                labels[3].ForeColor = Color.Red;
                labels[3].Focus();

            }
        }
        
        private void Save(/*Connection db*/)
        {
            var imageArray = uploadImage.ImageToByte(image.Image);
           

            BeginTransactionAsync();
            try
            {
                switch (accion)
                {
                    case "update":
                        _Student.Where(s => s.id.Equals(idEstudiante))
                         .Set(s => s.name, this.list[1].Text)
                         .Set(s => s.lastName, this.list[2].Text)
                         .Set(s => s.email, this.list[3].Text)
                         .Set(s => s.image, imageArray)
                          .Update();
                        break;
                    case "insert":
                        _Student.Value(s => s.idNumber, this.list[0].Text)
                         .Value(s => s.name, this.list[1].Text)
                         .Value(s => s.lastName, this.list[2].Text)
                         .Value(s => s.email, this.list[3].Text)
                         .Value(s => s.image, imageArray)
                          .Insert();
                        break;
                }


               


                 /* db.Insert(new Student()
                {
                    idNumber = list[0].Text,
                    name = list[1].Text,
                    lastName = list[2].Text,
                    email = list[3].Text,
                    image = imageArray
                });*/
                CommitTransaction();
                ResetTheFields();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }
        private int _num_page=1, _reg_by_page=2;
        public void GetStudents(string fields)
        {
            List<Student> query = new List<Student>();
            int index = (_num_page - 1) * _reg_by_page;

            if (fields.Equals(""))
            {
                query = _Student.ToList();
            }
            else
            {
                query = _Student.Where(s => s.idNumber.StartsWith(fields) || s.name.StartsWith(fields) || s.lastName.StartsWith(fields)).ToList();
            }
            if (0 < query.Count)
            {
                this._dgv.DataSource = query.Select(s => new
                {
                    s.id,
                    s.idNumber,
                    s.name,
                    s.lastName,
                    s.email,
                    s.image
                }).Skip(index).Take(_reg_by_page).ToList();
                this._dgv.Columns[0].Visible = false;
                this._dgv.Columns[5].Visible = false;
                this._dgv.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                this._dgv.Columns[1].HeaderText= "ID Number";
                this._dgv.Columns[2].HeaderText = "Name";
                this._dgv.Columns[3].HeaderText = "Last Name";
                this._dgv.Columns[4].HeaderText = "Email";
                this._dgv.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            else
            {
                this._dgv.DataSource = query.Select(s => new
                {
                    s.id,
                    s.idNumber,
                    s.name,
                    s.lastName,
                    s.email,
                }).ToList();
            }
            
        }
        private int idEstudiante = 0;
        public void DgvGetCliente()
        {
            this.accion = "update";
            
            this.idEstudiante = Convert.ToInt16(_dgv.CurrentRow.Cells[0].Value);//To select the ID of the row that the user select
            list[0].Text = Convert.ToString(_dgv.CurrentRow.Cells[1].Value);
            list[1].Text = Convert.ToString(_dgv.CurrentRow.Cells[2].Value);
            list[2].Text = Convert.ToString(_dgv.CurrentRow.Cells[3].Value);
            list[3].Text = Convert.ToString(_dgv.CurrentRow.Cells[4].Value);
            try
            {
                byte[] arrayImage = (byte[])_dgv.CurrentRow.Cells[5].Value;
                image.Image = uploadImage.byteArrayToImage(arrayImage);
                

            }catch(Exception ex)
            {
                image.Image = _imgBitMap;
               // throw ex;
            }

            
        }

        private List<Student> studentList;
        public void Page(string method)
        {
            switch(method)
            {
                case "First" :
                    this._num_page = _page.first();
                    break;
                case "Preview":
                    this._num_page = _page.preview();
                    break;
                case "Next":
                    this._num_page = _page.next();
                    break;
                case "Last":
                    this._num_page = _page.last();
                    break;

            }
        }
        public void Register_Pages()
        {
            _num_page = 1;
            this._reg_by_page=(int) this._numericUpDown.Value;
            var list= _Student.ToList();

            if (0 < list.Count)
            {
                _page = new Page<Student>(studentList, labels[4], this._reg_by_page);
                GetStudents("");
            }
        }
        private void ResetTheFields()
        {
            image.Image = _imgBitMap;
            labels[0].Text = "ID Number";
            labels[1].Text = "Name";
            labels[2].Text = "Last Name";
            labels[3].Text = "Email";

            labels[0].ForeColor = Color.LightGray;
            labels[1].ForeColor = Color.LightGray;
            labels[2].ForeColor = Color.LightGray;
            labels[3].ForeColor = Color.LightGray;

            list[0].Text = "";
            list[1].Text = "";
            list[2].Text = "";
            list[3].Text = "";

            accion = "insert";
            this.idEstudiante = 0;
            _num_page = 1;
            studentList = _Student.ToList();

            if (0 < studentList.Count)
            {
                _page = new Page<Student>(studentList, labels[4], this._reg_by_page);
            }
            GetStudents("");


        }
    }
}
