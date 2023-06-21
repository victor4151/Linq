using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            //非泛用集合無法使用<>中的內容物
            ArrayList arr = new ArrayList();
            arr.Add(10);
            arr.Add(100);
            //類別無法細節
            //var q=from n in arr.Cast<int>()
            //      select n;
            //屬性才能細節
            //IEnumerable<string> q = from n in arr.Cast<int>()
            //        where n > 3
            //        select n.ToString();
            //使用匿名型別
            var q = from n in arr.Cast<int>()
                    where n > 3
                    select new { //需new一個
                        N = n, 
                        S = n * n, 
                        T = n * n * n 
                    };   
            //foreach (string s in q)
            //{
            //    dataGridView1.Add(s);
            //}
            dataGridView1.DataSource = q.ToList();
            // dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //混合查詢
            productsTableAdapter1.Fill(nwDataSet1.Products);
            //先將需要的資料排序後,直接拿前五筆資訊就能取得TOP5
            var q = (from p in nwDataSet1.Products 
                     orderby p.UnitsInStock descending
                     select p).Take(5);
            dataGridView1.DataSource= q.ToList();     
                }

        private void button1_Click(object sender, EventArgs e)
        {
            //馬上執行的有下列 LinQ多半為延遲查詢
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 ,10};
            listBox1.Items.Add("Max="+ nums.Max());
            listBox1.Items.Add("Min=" + nums.Min());
            listBox1.Items.Add("Avg=" + nums.Average());
            listBox1.Items.Add("Sum=" + nums.Sum());
            listBox1.Items.Add("Count=" + nums.Count());
            //============================
            //搭配委派做查詢可以將資料先進行立即執行加總後再去委派來查看
            productsTableAdapter1.Fill(nwDataSet1.Products);
            listBox1.Items.Add("Max UnitsInStock =" + nwDataSet1.Products.Max(p => p.UnitsInStock));
            listBox1.Items.Add("Min UnitsInStock=" + nwDataSet1.Products.Min(p => p.UnitsInStock));
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}