using LinqLabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using System.Windows.Forms.DataVisualization.Charting;//圖表的命名空間

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {


        public FrmHelloLinq()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //         public interface IEnumerable<T>
            //    System.Collections.Generic 的成員

            //摘要:
            //公開支援指定類型集合上簡單反覆運算的列舉值。

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int n in nums)
            {
                this.listBox1.Items.Add(n);
            }
            listBox1.Items.Add("===================");

            //Collections介面和類別，定義各種集合的物件，例如清單、佇列、位元陣列、雜湊表和字典。
            //IEnumerator 支援非泛型集合上的簡單反覆運算。
            System.Collections.IEnumerator en = nums.GetEnumerator();
            while (en.MoveNext())
            //Current取得集合中位於列舉值目前位置的元素。屬於 IEnumerator.Current 屬性
            {
                this.listBox1.Items.Add(en.Current);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>() { 1,2,3,4,5,6,7,8,9,10,11,111};
            foreach (int n in list)
            {
               listBox1.Items.Add((int)n);
            }
            //================
            listBox1.Items.Add("===================");
            //GetEnumerator() 目的傳回在 List<T> 中逐一查看的列舉值。
            List<int>.Enumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                listBox1.Items.Add(en.Current);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //int 它的類型必須包括一個公共無參數GetEnumerator方法，
            //其返回類型是類、結構或接口類型。
            //需要有IEnumerable<T>才能使用char是很多數字的集合所以可以使用
            //int i = 1;
            //foreach (int n in i)
            //{
            //    listBox1.Items.Add((int)n);
            //}

            foreach (char s in "abcd")
            {
                listBox1.Items.Add(s);
            }
        }
        //1.定義資料來源
        //2.定義LINQ語法
        //3.執行Query查詢
        private void button2_Click(object sender, EventArgs e)
        {
            //1.定義資料來源
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //2.定義LINQ語法
            //var好用
            //define query  (IEnumerable<int> q 是一個  Iterator 物件, 如陣列集合一般 (陣列集合也是一個  Iterator 物件
            IEnumerable<int> q = from n in nums
                                 where n % 2 == 0 && n  >= 5&&n<=10
                                 select n;
            //3.執行Query查詢
            //execute query (執行 iterator - 逐一查看集合的item)
            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
                                    
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //當linq語法定義時看到有方法不會直接跑會到執行時才會call方法
            //下中斷點看執行步驟當真才回傳並寫入不為真重新執行
            IEnumerable<int> q = from n in nums
                                 where IsEvent(n)
                                 select n;
            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private bool IsEvent(int n)
        {
            return n % 2 == 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //可以任意型別IEnumerable<'這裡可以改'>
            IEnumerable<Point> q = from n in nums
                                 where IsEvent(n)
                                 select new Point (n,n*n);
            foreach (Point n in q)
            {
                listBox1.Items.Add(n);
            }
            //execute query - ToXXX()
            //將結果以q.tolist方法存入list//foreach (.... in q)  { .......   list.Add(...)}   return list
            List<Point> list =q.ToList();
            this.dataGridView1.DataSource = list;//這邊要
            this.chart1.DataSource = list;//這邊要細節不然圖表不會呈現,犯傻1次(chart1.DataSource要先拿到資料)
            this.chart1.Series[0].XValueMember = "X";
            this.chart1.Series[0].YValueMembers = "Y";
            this.chart1.Series[0].ChartType = SeriesChartType.Column;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] words = { "xxx", "Apple", "yyApple", "xxx", "xxApple", "yyy", "pineapple" };
            IEnumerable<string> q = from w in words
                                    where w.ToLower().Contains("apple") && w.Length >5//回傳搜尋字彙&&字串長度
                                    select w;
            listBox1.Items.Clear();
            foreach (string n in q)
            { 
                listBox1.Items.Add(n);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        { 

            //也可以用var要是不知道IEnumerable<NWDataSet.ProductsRow>是甚麼的話
            IEnumerable < NWDataSet.ProductsRow > q = from p in this.nwDataSet1.Products
                                                      where (p.UnitPrice > 30 && p.UnitPrice <= 100) && p.ProductName.StartsWith("P")//StartsWith 搜尋字大小寫有分別SQL的like
                                                      select p;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            IEnumerable<NWDataSet.OrdersRow> y= from o in this.nwDataSet1.Orders
                                                where (o.OrderDate.Year ==1997)
                                                select o;
            this.dataGridView1.DataSource = y.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //參考where F12了解底下有甚麼甚麼命名空間
            //System.Linq
            int[] nums = { 1, 2, 3 };
          //  nums.Where
        }
    }
}
