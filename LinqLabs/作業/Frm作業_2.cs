using LinqLabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            this.productPhotoTableAdapter1.Fill(this.awDataSet1.ProductPhoto);
            this.productTableAdapter1.Fill(this.awDataSet1.Product);
            this.productProductPhotoTableAdapter1.Fill(this.awDataSet1.ProductProductPhoto);
            LoadYearData();
        }

        private void LoadYearData()//將年load進去
        {
            IEnumerable<String> Year = (from p in this.awDataSet1.ProductProductPhoto
                                        orderby p.ModifiedDate.Year ascending
                                        select p.ModifiedDate.Year.ToString()).Distinct(); //去除重複項
            foreach (String s in Year)
            {
                this.comboBox3.Items.Add(s);
            }
        }


        private void button11_Click(object sender, EventArgs e)//全部腳踏車
        {
            var P = from p in this.awDataSet1.ProductPhoto
                    select p;
            this.dataGridView1.DataSource = P.ToList();
            var q = from pq in this.awDataSet1.ProductPhoto
                    where pq.ProductPhotoID.ToString() == dataGridView1.CurrentRow.Cells["ProductPhotoID"].Value.ToString()
                    select pq.LargePhoto;
            foreach (var x in q)
            {
                byte[] bytes = x;
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                this.pictureBox1.Image = Image.FromStream(ms);
            }
            //byte[] bytes = P.
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            //this.pictureBox1.Image = Image.FromStream(ms);
            //this.pictureBox1.DataBindings= P.ToList();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)//當主頁有變動時腳踏車跟者改變
        {
            var q = from p in this.awDataSet1.ProductPhoto
                    where p.ProductPhotoID.ToString() == dataGridView1.CurrentRow.Cells["ProductPhotoID"].Value.ToString()
                    select p.LargePhoto;
            foreach (var x in q)
            {
                byte[] bytes = x;
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                this.pictureBox1.Image = Image.FromStream(ms);
            }
        }//).ToString()

        private void button5_Click(object sender, EventArgs e)//查年
        {

            var P = from p in this.awDataSet1.ProductPhoto                  
                    //where p.ModifiedDate.Year== Convert.ToInt32( comboBox3.Text)
                    where (p.ModifiedDate.Year).ToString() == comboBox3.Text
                    select p;
           // MessageBox.Show((p.ModifiedDate.Year).ToString());
            this.dataGridView1.DataSource = P.ToList();
        }

        private void button3_Click(object sender, EventArgs e)//查區間
        {
            var P = from p in this.awDataSet1.ProductPhoto
                    where p.ModifiedDate>= dateTimePicker1.Value&&p.ModifiedDate<= dateTimePicker2.Value
                    select p;
            this.dataGridView1.DataSource = P.ToList();
        }

        private void button10_Click(object sender, EventArgs e)//判斷幾季
        {
            if (comboBox2.Text == "第一季")
            {      
                var q = this.awDataSet1.ProductPhoto.Where(t => (t.ModifiedDate.Year).ToString() == comboBox3.Text && t.ModifiedDate.Month <= 3);//.Select(n => n.ModifiedDate.Month<=3);
                //var q = this.awDataSet1.ProductPhoto.Where(t => (t.ModifiedDate.Month) <=3);
                this.dataGridView1.DataSource = q.ToList();
            }
            else if (comboBox2.Text == "第二季")
            {
                var q = this.awDataSet1.ProductPhoto.Where(t => (t.ModifiedDate.Year).ToString() == comboBox3.Text && t.ModifiedDate.Month >= 4 && t.ModifiedDate.Month<=6);
                this.dataGridView1.DataSource = q.ToList();
            }
            else if (comboBox2.Text == "第三季")
            {
                var q = this.awDataSet1.ProductPhoto.Where(t => (t.ModifiedDate.Year).ToString() == comboBox3.Text && t.ModifiedDate.Month >= 7 && t.ModifiedDate.Month <= 9);
                this.dataGridView1.DataSource = q.ToList();
            }
            else if (comboBox2.Text == "第四季")
            {
                var q = this.awDataSet1.ProductPhoto.Where(t => (t.ModifiedDate.Year).ToString() == comboBox3.Text && t.ModifiedDate.Month >= 10);
                this.dataGridView1.DataSource = q.ToList();
            }
            else {
                MessageBox.Show("請輸入第幾年第幾季");
            }
        }
    }
}
