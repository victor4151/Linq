using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LinqLabs.Properties;
using System.Data.SqlClient;

namespace LinqLabs.作業
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            LoadyearData();
        }

        private void LoadyearData()
        {

            IEnumerable<String> Year = (from o in this.nwDataSet1.Orders
                                         orderby o.OrderDate.Year ascending
                                         select o.OrderDate.Year.ToString ()).Distinct(); //去除重複項
            foreach (String s in Year)
            {
                this.comboBox1.Items.Add(s);
            }
        }

        private void button14_Click(object sender, EventArgs e)//log
        {
           
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files=dir.GetFiles();
            var q = from f in files
                    where f.Extension ==".log"
                    select f;
            dataGridView1.Columns.Clear();
            this.dataGridView1.DataSource = files;
            //this.dataGridView2.DataSource = files;
        }

        private void button2_Click(object sender, EventArgs e)//2021
        {
            
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();
            var q = from y in files
                    where y.CreationTime.Year==2021
                    select y;
            dataGridView1.Columns.Clear();
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)//大檔案
        {
            
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();
            var q = from b in files
                    where b.Length >2000
                    select b;
            dataGridView1.Columns.Clear();
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button6_Click(object sender, EventArgs e)//all訂單 
        {
            //將訂單載入
            IEnumerable<NWDataSet.OrdersRow> O =from o in this.nwDataSet1.Orders
                                                select o;
            this.dataGridView1.DataSource=O.ToList();
            

            OD = from od in this.nwDataSet1.Order_Details
                 select od;
            this.bindingSource1.DataSource = O.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;
             LoadOrderDetails();
            LoadDetailsSkillTake();
        }


        IEnumerable<NWDataSet.Order_DetailsRow> OD = null;
        int index = 0;//一頁幾筆的資訊預設為0
        
        private void LoadDetailsSkillTake()//取得要印出幾筆資訊
        {
            int IDcount = Convert.ToInt32(textBox1.Text);
            this.dataGridView2.DataSource = OD.Take(IDcount).ToList();
            index += IDcount;

        }
        private void button12_Click(object sender, EventArgs e)//上一頁
        {
            int IDcount = Convert.ToInt32(textBox1.Text);
            int skip;
            if ((index - IDcount) > 0)
            { 
                skip = index - IDcount*2;
                this.dataGridView2.DataSource=OD.Skip(skip).Take(skip).ToList();
                index-= IDcount;
            }
            else
            {
                MessageBox.Show("第一頁了別再按了");
            }
            //var order = from o in this.nwDataSet1.Orders
            //            where o.OrderDate.Year == Convert.ToInt32(comboBox1.Text)
            //            select o;

            //var orderdetails = from o in this.nwDataSet1.Order_Details
            //                       //        where o..Year == o.OrderDate.Year
            //                   select o;
            //var ood = from o in order
            //          join od in orderdetails on o.OrderID equals od.OrderID
            //          select new { o.OrderDate, od.OrderID, od.ProductID, od.UnitPrice, od.Quantity, od.Discount };

            //this.dataGridView1.DataSource = order.ToList();

            //this.dataGridView2.DataSource = ood.Skip(3).Take(index).ToList();


        }

        private void button13_Click(object sender, EventArgs e)//下一頁
        {
            int IDcount = Convert.ToInt32(textBox1.Text);
            if (OD.Count() <= index)
            {
                MessageBox.Show("最後一頁了");
            }
            else
            {
                this.dataGridView2.DataSource = OD.Skip(index).Take(IDcount).ToList();
                index += IDcount;
            }
        }



        private void button1_Click(object sender, EventArgs e)//查看訂單明細
        {
            if (comboBox1.Text!="")
            {
                var O = from o in this.nwDataSet1.Orders
                            where o.OrderDate.Year == Convert.ToInt32(comboBox1.Text)
                            select o;
                OD = from od in this.nwDataSet1.Order_Details
                     select od;

                //var ood = from o in order
                //          join od in orderdetails on o.OrderID equals od.OrderID
                //          select new { o.OrderDate, od.OrderID, od.ProductID, od.UnitPrice, od.Quantity, od.Discount };
                //select  o.OrderDate;
                //取得O的bindingSource1.DataSource
                this.bindingSource1.DataSource = O.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;//把bindingSource1呈現在dataGridView1
                LoadDetailsSkillTake();
                LoadOrderDetails();
            }
            else { MessageBox.Show("請輸入年分"); }

          }

        void LoadOrderDetails()
        {
            DataRow currentod = (DataRow)this.bindingSource1.Current;
            string c = currentod["OrderID"].ToString();
            // MessageBox.Show(c);
            OD = from od in this.nwDataSet1.Order_Details
                 where od.OrderID.ToString()==c
                 select od;
            dataGridView2.DataSource = OD.ToList();
           
        }
      
private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            LoadOrderDetails();
        }
    }
}
//嚴重性 程式碼	說明	專案	檔案	行	隱藏項目狀態
//錯誤		
// 因為檔案 作業\Frm作業_1.resx 位於網際網路或是限制區域上，或是檔案上標有 Web 字樣
//    ，所以無法處理該檔案。
//    若希望處理這些檔案，請移除 Web 字樣。	LinqLabs