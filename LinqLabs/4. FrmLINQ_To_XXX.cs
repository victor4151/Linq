using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskBand;
using System.Windows.Forms.DataVisualization.Charting;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ,11,12};
            //groupby產生 IGrouping ，輸入key的型態並分群
            IEnumerable<IGrouping<string, int>>q= from n in nums 
                                               group n by n % 2 ==0?"偶數":"奇數";           
            dataGridView1.DataSource=q.ToList();
            //========================
            //treeview
            foreach (var group in q)
            {
                //分奇數偶數 使用TreeNode
                TreeNode node =treeView1.Nodes.Add(group.Key);
                foreach (var item in group)
                {
                    //奇數偶數中數值依附在node上面
                    node.Nodes.Add(item.ToString());
                }
            }
            //================================
            //listview
            foreach (var group in q)
            {
                //要使用ListViewGroup將東西放進裡面
                ListViewGroup lvg = listView1.Groups.Add(group.Key, group.Key);//後面為標頭
                foreach (var item in group)
                {
                    //不需要再新增直接放到list鐘即可
                    listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            //groupby產生 IGrouping ，輸入key的型態並分群
            //將它暫時放入into g 之後再用select呈現 使用方法要誇號
            var q = from n in nums
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g
                    select new { MyKey = g.Key, MyCount = g.Count() ,MyAvg=g.Average(),MyGroup=g};
            //印出
            dataGridView1.DataSource = q.ToList();
            //======================================
            //treeview
            foreach (var group in q)
            {
                //使用自己設定的匿名note所以在跑group時要告知為跑甚麼資訊後面要.My甚麼的
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(s);
                foreach (var item in group.MyGroup)
                {

                    node.Nodes.Add(item.ToString());
                }
            }
            //================================
            //listview
            foreach (var group in q)
            {
                //header如上表
                string header = $"{group.MyKey} ({group.MyCount})";
                ListViewGroup lvg = listView1.Groups.Add(group.MyKey, header);//後面為標頭
                foreach (var item in group.MyGroup)
                {
                    //不需要再新增直接放到list鐘即可
                    listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
            //char
            chart1.DataSource= q.ToList();
            chart1.Series[0].XValueMember = "MyKey";
            chart1.Series[0].YValueMembers = "MyCount";
            chart1.Series[0].ChartType = SeriesChartType.Column;

            chart1.Series[1].XValueMember = "MyKey";
            chart1.Series[1].YValueMembers = "Myavg";
            chart1.Series[1].ChartType = SeriesChartType.Column;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            //groupby產生 IGrouping ，輸入key的型態並分群
            //將它暫時放入into g 之後再用select呈現 使用方法要誇號
            var q = from n in nums
                    group n by MyKey(n) into g
                    select new { MyKey = g.Key, MyCount = g.Count(), MyAvg = g.Average(), MyGroup = g };
            //印出
            dataGridView1.DataSource = q.ToList();
            //======================================
            //treeview
            foreach (var group in q)
            {
                //使用自己設定的匿名note所以在跑group時要告知為跑甚麼資訊後面要.My甚麼的
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(s);
                foreach (var item in group.MyGroup)
                {

                    node.Nodes.Add(item.ToString());
                }
            }
            //================================
            //listview
            foreach (var group in q)
            {
                //header如上表
                string header = $"{group.MyKey} ({group.MyCount})";
                ListViewGroup lvg = listView1.Groups.Add(group.MyKey, header);//後面為標頭
                foreach (var item in group.MyGroup)
                {
                    //不需要再新增直接放到list鐘即可
                    listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
            //char
            chart1.DataSource = q.ToList();
            chart1.Series[0].XValueMember = "MyKey";
            chart1.Series[0].YValueMembers = "MyCount";
            chart1.Series[0].ChartType = SeriesChartType.Column;

            chart1.Series[1].XValueMember = "MyKey";
            chart1.Series[1].YValueMembers = "Myavg";
            chart1.Series[1].ChartType = SeriesChartType.Column;
        }

        private string MyKey(int n)
        {
            if (n < 5)
                return "小";
            else if (n < 10)
                return "中";
            else
                return "大";
            
        }

        private void button38_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[]files = dir.GetFiles();
            dataGridView1.DataSource = files;
            var q = from f in files
                     group f by f.Extension into g
                     orderby g.Count() descending
                    select new
                     {
                         副檔名 = g.Key,
                         總數 = g.Count()
                         };
            dataGridView1.DataSource = q.ToList();
            //foreach (var group in f1)
            //{

            //    string s = $"{group.MYFileKey} ({group.MyCount})";
            //    TreeNode node = treeView1.Nodes.Add(s);
            //    foreach (var item in group.MyFileGroup)
            //    {

            //        node.Nodes.Add(item.ToString());
            //    }
            //}

        }

        private void button12_Click(object sender, EventArgs e)
        {
            ordersTableAdapter1.Fill(nwDataSet1.Orders);
            var q = from f in nwDataSet1.Orders
                    group f by f.OrderDate.Year into g
                    orderby g.Count() descending
                    select new
                    {
                        年分 = g.Key ,
                        總數=g.Count(),
                        MyGroup = g
                    };
            dataGridView1.DataSource = q.ToList();
            //treeview
            foreach (var group in q)
            {
                //使用自己設定的匿名note所以在跑group時要告知為跑甚麼資訊後面要.My甚麼的
                string s = $"{group.年分} ({group.總數})";
                TreeNode node = treeView1.Nodes.Add(s);
                foreach (var item in group.MyGroup)
                {

                    node.Nodes.Add(item.ToString());
                }
            }
            ////================================
            ////listview
            //foreach (var group in q)
            //{
            //    //header如上表
            //    string header = $"{group.MyKey} ({group.MyCount})";
            //    ListViewGroup lvg = listView1.Groups.Add(group.MyKey, header);//後面為標頭
            //    foreach (var item in group.MyGroup)
            //    {
            //        //不需要再新增直接放到list鐘即可
            //        listView1.Items.Add(item.ToString()).Group = lvg;
            //    }
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    let s = f.Extension//用let暫存要是名稱很長時可以做為區分用
                    where s==".log"
                    select f;
            dataGridView1.DataSource = q.ToList();
            MessageBox.Show("總數為"+q.Count());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] cahrs = { ',', ' ', '?' };//去除這些字以外的方式
            string s = "This is a pen, this is a apple, this is   banaba?";
            string[] words=s.Split(cahrs);//切割字
            var q = from w in words
                    where w != ""
                    group w by w.ToUpper() into g
                    select new { 
                        關鍵字=g.Key, 
                        總數 = g.Count()
                    };
            dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            categoriesTableAdapter1.Fill(nwDataSet1.Categories);
            productsTableAdapter1.Fill(nwDataSet1.Products);
            //var q = from p in nwDataSet1.Products
            //        group p by p.CategoryID into g
            //        select new
            //        {
            //            類別ID=g.Key,
            //            平均單價=g.Average(pr=>pr.UnitPrice)
            //        };
            var q = from c in nwDataSet1.Categories
                    join p in nwDataSet1.Products
                    on c.CategoryID equals p.CategoryID
                    group p by c.CategoryName into g //key 為c.CategoryName 依照這個分群
                    select new
                    {
                        類別名稱 = g.Key,
                        平均單價 = g.Average(pr => pr.UnitPrice)
                    };
            dataGridView1 .DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 2, 4, 56, 72, 11, 2 };

            int[] nums2 = { 2, 55, 88, 99, 11, 66 };
            //集合運算子 Distinct / Union / Intersect / Except
            IEnumerable<int> q = nums1.Intersect(nums2);//交集給變數Q
            q = nums1.Union(nums2);//聯集
            q = nums1.Distinct();//找唯一

            //切割運算子 Take / Skip
            q=nums1.Skip(1);

            //數量詞作業 : Any / All / Contains 回傳bool
            bool result = nums1.Any(n => n > 50);
            result = nums1.Any(n => n > 100);
            result = nums1.All(n => n > 1);

            //單一元素運算子 :     First / Last / Single / ElementAt  
            //FirstOrDefault / LastOrDefault / SingleOrDefault / ElementAtOrDefault
            int n1 = nums1.First();
            n1 = nums1.ElementAt(2);
            //n1 = nums1.ElementAt(12);//沒有值會超出索引報錯誤
            n1=nums1.ElementAtOrDefault(12);//即使沒有值會回傳預設0不會出錯
        }
    }
}
