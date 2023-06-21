using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//entity data model 特色

//1. App.config 連接字串
//2. Package 套件下載, 參考 EntityFramework.dll, EntityFramework.SqlServer.dll
//3. 導覽屬性 關聯
//4. DataSet model 需要處理 DBNull; Entity Model  不需要處理 DBNull (DBNull 會被 ignore)
//5. IQuerable<T> query 執行時會轉成 => T-SQL
namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
            //觀察DB寫入時的紀錄,執行結果用Tsql但系統已經協助處理完畢
            dbcontext.Database.Log=Console.Write;
           // Console.WriteLine(("xxx");
        }
         NorthwindEntities dbcontext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
           
            var q=from p in dbcontext.Products
                  where p.UnitPrice>30
                  select p;
            dataGridView1.DataSource = q.ToList();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var q =from p in dbcontext.Products
                   orderby p.UnitsInStock descending,p.ProductID descending //先依照庫存排序,要是一樣用產品ID排序
                   select p;
            dataGridView1.DataSource= q.ToList();
            //====================================
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //找Categories第一筆的所有產品
            dataGridView1.DataSource= dbcontext.Categories.First().Products.ToList();
            //Products的第一筆是哪個CategoryName
            MessageBox.Show(dbcontext.Products.First().Category.CategoryName);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from p in dbcontext.Products.AsEnumerable() //先在這轉成TSql指令AsEnumerable
                    group p by p.Category.CategoryName into g //join的概念
                    select new { 
                        類別名稱 = g.Key, 
                        平均單價 =$"{g.Average(p=>p.UnitPrice):C2}"//這邊的C2格式化時才不會報錯,設變數p p等於平均價格
                    };
            dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //bool? n; ?是指在DB中允許空值 所以會有找不到的狀態,這時先用value把值給找出來後再進行年份等等資訊確認
            //n = false;
            //n = true;
            //n = null;
            var q = from o in dbcontext.Orders
                    group o by o.OrderDate.Value.Year into g
                    select new { 年分 = g.Key, 加總 = g.Count() };
            dataGridView1.DataSource = q.ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
           // dbcontext.Products.Add(new Product { ProductName = "cccc", Discontinued = true });
            Product pro = new Product { ProductName = "cccc" ,Discontinued=true};
            dbcontext.Products.Add(pro);
            dbcontext.SaveChanges();
        }
    }
}
