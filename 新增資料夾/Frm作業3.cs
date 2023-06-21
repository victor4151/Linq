using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            // 共幾個 學員成績 ?						

            // 找出 前面三個 的學員所有科目成績		

            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績		
            // NOTE 匿名型別


            // 找出學員 'bbb' 的成績	                       
            ShowStudents(q, "學員 'bbb' 的成績");

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	!=

            //數學不及格...是誰
        }

        private void button37_Click(object sender, EventArgs e)
        {
            // 統計 每個學生個人成績
            // Rank by 三科成績加總 並排序
            // 國文權重加倍
            // 依平均分計算 Grade & Pass 

            // NOTE Rank
            this.lblMaster.Text = "Rank";
            this.lblDetails.Text = "";

            int rank = 0;
            var q = from s in students_scores....
                    select new
                    {
                        s.Name,
                        s.Gender,
                        s.Class,
                        s.Chi,
                        s.Eng,
                        s.Math,
                        Min...
                        Max....Avg = ...
                        Sum =
                        Weight = ...,
                        ...
                        Pass = ..Grade
                        Rank = ++rank,
                    };
        }
    }
}
