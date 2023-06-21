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
using System.Windows.Forms.DataVisualization.Charting;


namespace LinqLabs.作業
{
    public partial class Frm作業_3 : Form
    {

        List<Student> students_scores = null;
        public Frm作業_3()
        {
            InitializeComponent();

            students_scores = new List<Student>()//載入資料
                          {
                            new Student{Id=1, Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                            new Student{Id=2, Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                            new Student{Id=3, Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                            new Student{Id=4, Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                            new Student{Id=5, Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                            new Student{Id=6, Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                           };
            var q2 = from n in students_scores
                     select n;
            List<Student> list1 = q2.ToList();
            dataGridView1.DataSource = list1;
            IEnumerable<Point> q = from n in students_scores
                                   //where
                                   select new Point (n.Eng,n.Chi);
            List<Point> list = q.ToList();
            //this.dataGridView1.DataSource = list;
            this.chart2.DataSource = list;
            this.chart2.Series[0].XValueMember = "X";
            this.chart2.Series[0].YValueMembers = "Y";
            this.chart2.Series[0].ChartType = SeriesChartType.Column;

        }
        public class Student
        { 
            public int Id { get; set; }
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            var q = from n in students_scores
                   // where 
                    select n;
            List<Student> list = q.ToList();
            listBox2.Items.Clear();
            listBox2.Items.Add ($"總共有{ list.Count()}個學員成績");

            this.chart2.DataSource = list;

            // 共幾個 學員成績 ?						

            // 找出 前面三個 的學員所有科目成績		

            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績		
            // NOTE 匿名型別


            // 找出學員 'bbb' 的成績	                       

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	!=

            //數學不及格...是誰
        }

        private void button37_Click(object sender, EventArgs e)
        {
            var q = from n in students_scores
                        //where
                    select new {
                        n.Id,
                        n.Name,
                        n.Class,
                        n.Chi,
                        n.Eng,
                        n.Math,
                        n.Gender,
                        平均成績 = (n.Math + n.Chi  + n.Eng) / 3,
                        總分= new int[] { n.Math, n.Chi, n.Eng }.Sum(),
                        最高分 = new int[] { n.Math, n.Chi, n.Eng }.Max(),
                        最低分 = new int[] { n.Math, n.Chi, n.Eng }.Min(),
                        全重加權國文=  n.Chi*2
                    };
            dataGridView1.DataSource=q.ToList();
            // 統計 每個學生個人成績
            // Rank by 三科成績加總 並排序
            // 國文權重加倍
            // 依平均分計算 Grade & Pass 

            // NOTE Rank
            //this.lblMaster.Text = "Rank";
            //this.lblDetails.Text = "";

            //int rank = 0;
            //var q = from s in students_scores....
            //        select new
            //        {
            //            s.Name,
            //            s.Gender,
            //            s.Class,
            //            s.Chi,
            //            s.Eng,
            //            s.Math,
            //            Min...
            //            Max....
            //            Avg = ...
            //            Sum =
            //            Weight = ...,
            //            ...
            //            Pass = ..Grade
            //            Rank = ++rank,
            //        };
        }

        private void button33_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                Random random = new Random();
                Student stu = new Student
                {
                    Id = (i + 1),
                    Name = (i + 1).ToString(),
                    Class = random.Next(0, 100) > 50 ? "CS_101" : "CS_102",
                    Chi = random.Next(0, 100),
                    Eng = random.Next(0, 100),
                    Math = random.Next(0, 100),
                    Gender = random.Next(0, 100) > 50 ? "Male" : "Female"
                };
                students_scores.Add(stu);

            }
            dataGridView1.DataSource= students_scores;
        }

        private void button1_Click(object sender, EventArgs e)//取前三名資訊
        {
            var q1 = students_scores.OrderBy(n => n.Id).Take(3);
            List<Student> list = q1.ToList();
            dataGridView1.DataSource= list;
        }

        private void button2_Click(object sender, EventArgs e)//取後面2名資訊
        {
            var q = (from n in students_scores
                     orderby n.Id descending
                     select n).Take(2);
            List<Student> list = q.ToList();
            dataGridView1.DataSource = list;
        }

        private void button3_Click(object sender, EventArgs e)//搜尋名子
        {
            dataGridView1.DataSource = students_scores.Where(n => n.Name == textBox1.Text).ToList();
            //List<Student> list = w.ToList();
           // dataGridView1.DataSource = list;
        }

        private void button4_Click(object sender, EventArgs e)//不數學及格
        {
            var f = students_scores.Where(n => n.Math < 60);
            List<Student> list = f.ToList();
            dataGridView1.DataSource = list;
        }

        private void button5_Click(object sender, EventArgs e)//除了誰以外都查
        {
            var w = students_scores.Where(n => n.Name != textBox1.Text);
            List<Student> list = w.ToList();
            dataGridView1.DataSource = list;
        }
    }
}
