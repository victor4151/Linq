using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


//Notes: LINQ 主要參考 
//組件 System.Core.dll,
//namespace {}  System.Linq
//public static class Enumerable
//
//public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);

//1. 泛型 (泛用方法)                                                                         (ex.  void SwapAnyType<T>(ref T a, ref T b)
//2. 委派參數 Lambda Expression (匿名方法簡潔版)               (ex.  MyWhere(nums, n => n %2==0);
//3. Iterator                                                                                      (ex.  MyIterator)
//4. 擴充方法   

namespace Starter
{
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1 = 100, n2 = 200;
            MessageBox.Show(n1+" "+n2);
            Swap(ref n1, ref n2);//方法為傳址給的變數也必須要ref
            MessageBox.Show(n1 + " " + n2);

            string s1 = "aaa", s2 = "bbb";
            MessageBox.Show(s1+" "+s2);
            Swap(ref s1, ref s2);
            MessageBox.Show(s1+" "+s2); 

        }
        public void Swap(ref int n1, ref int n2)//ref為傳址必須要加 由於前面沒加導致無法回傳
        //可以多個方法同名 此為多載
        {
            int temp = n1;
            n1 = n2;
            n2= temp;
        }
        public void Swap(ref string n1, ref string n2)//ref為傳址必須要加 由於前面沒加導致無法回傳
        {
            string temp = n1;
            n1 = n2;
            n2 = temp;
        }
        public void Swap(ref Point n1, ref Point n2)//ref為傳址必須要加 由於前面沒加導致無法回傳
        {
            Point temp = n1;
            n1 = n2;
            n2 = temp;
        }
        public static void SwapAnyType<T>(ref T n1, ref T n2)//加上public static使他變得更完善
        {
            T temp = n1;
            n1 = n2;
            n2 = temp;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1 = 100, n2 = 200;
            MessageBox.Show(n1 + " " + n2);
            SwapAnyType<int>(ref n1, ref n2);//方法為傳址給的變數也必須要ref
            MessageBox.Show(n1 + " " + n2);

            string s1 = "aaa", s2 = "bbb";
            MessageBox.Show(s1 + " " + s2);
            SwapAnyType(ref s1, ref s2);//沒加<string>會自動推斷型別
            MessageBox.Show(s1 + " " + s2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s1 = "aaa", s2 = "bbb";
            MessageBox.Show(s1 + " " + s2);
           // Swapobject(ref object s1, ref s2);
            MessageBox.Show(s1 + " " + s2);
        }
        void Swapobject(ref object n1, ref object n2)
        {
            //物件需要做型別轉換
            object temp = n1;
            n1 = n2;
            n2 = temp;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //            嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
            //錯誤  CS0123  'aaa' 沒有任何多載符合委派 'EventHandler' LinqLabs C:\LINQ\LinqLabs(StartUp)\LinqLabs\2.FrmLangForLINQ.cs    90  作用中

            //            this.buttonX.Click += aaa;
            //1.0具名方法
            this.buttonX.Click += bbb;//new EventHandler(bbb);
            //2.0匿名方法
            this.buttonX.Click += delegate (object sender1, EventArgs e1)
                                 {
                                     MessageBox.Show("2.0匿名方法");
                                 };
            //3.0匿名方法
            this.buttonX.Click += (object sender1, EventArgs e1) =>
            {
                MessageBox.Show("3.0匿名方法");
            };

        }
        void aaa()
        { 
        
        }
        void bbb(object sender, EventArgs e)//給2個變數一個object 另一個為EventArgs
        {
            MessageBox.Show("bbb");
        }
        //step 1: create delegate class
        //step 2: create delegate object
        //step 3: call method
        public delegate bool MyDelegate(int n);//create delegate class
        private void button9_Click(object sender, EventArgs e)
        {
            bool result = Test(6);
            MessageBox.Show("result= " + result);
            //==================================
            //C# 1.0 具名方法
            MyDelegate delegateObj = new MyDelegate(Test);//create delegate object 並將委派方法放進去
            result = delegateObj(5);//也能用 delegateObj.Invoke(5);//call method
            MessageBox.Show("result= " + result);
            //=============================
            delegateObj = IsEvent;//方法
            result = delegateObj(8);
            MessageBox.Show("result= " + result);
            //=============================
            //2.0匿名方法
            delegateObj = delegate (int n)
            {
                return n > 5;
            };
            result = delegateObj(7);
            MessageBox.Show("result= " + result);
            //==============================
            //3.0匿名方法
            delegateObj = n => n > 5;//省略int return
            result = delegateObj(3);
            MessageBox.Show("result= " + result);


        }
        bool Test(int n)
        {
            return n > 5;
        }
        bool IsEvent(int n)
        { 
            return n %2 == 0;
        }
        
        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5,6 ,7,8,9,10};
            List<int> list = MyWhere(nums,Test);//具名方法

            List<int>IsEvent= MyWhere(nums, n => n % 2 == 0);//匿名方法
            List<int>OddEvent = MyWhere(nums, n => n % 2 == 1);

            foreach (int n in IsEvent)
            { 
                this.listBox1.Items.Add(n);
            }
            foreach (int n in list)
            {
                this.listBox2.Items.Add(n);
            }
        }

        List<int> MyWhere(int[] nums, MyDelegate delegateObj)
        {
            List<int>list=new List<int>();
            foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                    list.Add(n);
                }
            }
            return list;
        }
        IEnumerable<int> MyIterator(int[] nums, MyDelegate delegateObj)
        {
            List<int> list = new List<int>();
            foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                    yield return n;
                }
            }
            
        }
        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            IEnumerable<int> q = MyIterator(nums, n => n % 2 == 0);

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<Point> q = nums.Where( n => n >5).Select(n=>new Point (n,n*n));//委派中放方法,先用where取出大於5的數值,然後再用select將大於5的值錯其他運算

            foreach (Point n in q)
            {
                this.listBox1.Items.Add(n);
            }
            //=======================================
            string[] words = { "aaa", "bbbb", "ccccc" };
            IEnumerable<string> q2 = words.Where<string>(s => s.Length > 3);
            foreach (string s in q2)
            { 
                this.listBox2.Items.Add(s); 
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            var n = 100;
            var s = "aaa";
            s.ToLower();
            var p=new Point (100, 200);
            listBox1.Items.Add(p.X);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Font font1 = new Font("arial", 15); // ()=> { ....}

            //類別多種參數,建構子會有多種對應變數多載的應用

            MyPoint pt0 = new MyPoint();
            MyPoint pt1 = new MyPoint(100);
            MyPoint pt2 = new MyPoint(10, 10);

            List<MyPoint> list = new List<MyPoint>();
            list.Add(pt0);
            list.Add(pt1);
            list.Add(pt2);
            //物件初始化{}
            list.Add(new MyPoint { Field1 = "1111", P1 = 111, X = 11, Y = 11111 });
            list.Add(new MyPoint { Field1 = "999", P1 = 9999, X = 999 });
            
            this.dataGridView1.DataSource = list;//類別變數不能被細節只有屬性可以
        }

        private void button43_Click(object sender, EventArgs e)
        {
            //匿名型別建立方式
            var pt1 = new { name = "xxx", p1 = 1111, x = 555, y = 888 };
            var pt2 = new { P1 = 200, P2 = 300 };
            this.listBox1.Items.Add(pt1.GetType());
            this.listBox1.Items.Add(pt2.GetType());

            var pt3 = new { X = 200, Y = 300, Z = 999, Name = "xxx" };
            this.listBox1.Items.Add(pt3.GetType());
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var q=from n in nums
            //      where n>5
            //      select new { N=n, Square = n*n, Cube = n*n*n};

            var q = nums.Where(n => n > 5).Select(n => new { N = n, Square = n * n, Cube = n * n * n });
           this.dataGridView1.DataSource= q.ToList();
            //====================================================
            //productsTableAdapter1.Fill(nwDataSet1.Products);
            //var q2 = from p in nwDataSet1.Products
            //         where p.UnitPrice > 30
            //         select new
            //         {
            //             N = p.UnitPrice,
            //             p.ProductID,
            //             p.UnitPrice,
            //             p.UnitsInStock,
            //             總價=p.UnitPrice*p.UnitsInStock                   
            //         };
            //dataGridView2.DataSource = q2.ToList();
            //==========================================
            productsTableAdapter1.Fill(nwDataSet1.Products);
            var q3 = from p in nwDataSet1.Products
                     where p.UnitPrice > 30
                     select new
                     {
                         N = p.UnitPrice,
                         p.ProductID,
                         p.UnitPrice,
                         p.UnitsInStock,
                         總價 = $"{p.UnitPrice * p.UnitsInStock:C2}"
                     };
            dataGridView2.DataSource = q3.ToList();
        }

        private void button40_Click(object sender, EventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            //新增擴充方法 WordCount()
            string s =  "abcd"  ;
            int count = s.WordCount();
            MessageBox.Show("count= " + count);
            //=====================
            string s1 = "123456789";
            count = s1.WordCount();
            MessageBox.Show("count= " + count);
            //=============
            char ch = s1.Cchar(2);
            MessageBox.Show("count= " + ch);
        }
    }
    //    嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
    //錯誤  CS0509	'MyString': 無法衍生自密封類型 'string'	LinqLabs C:\shared\LINQ\LinqLabs(Solution)\LinqLabs\2. FrmLangForLINQ.cs	444	作用中
    //無法用繼承的方式
    //    class MyString :String
    //    {
    //      //  int WordCount(.....)
    //    }

    public class MyPoint
    {
        public MyPoint() 
        {
        }
        public MyPoint(int p1)
        { 
            P1 = p1;//P1為屬性
        }
        public MyPoint(int x,int y)
        {
            X = x;//大寫的為屬性
            Y = y;
        }
        public string Field1 = "aaaa", Filed2 = "bbbb";
        private int m_P1;
        public int P1
        {
            get
            {
                return m_P1;
            }
            set
            {
                m_P1 = value;
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
public static class MyStingExtend //靜態類別
{
    public static int WordCount(this string s)//靜態方法 傳入變數要使用this
    {
        return s.Length;
    }
    public static char Cchar(this string s,int index)
    {
        return s[index];//字串自帶索引回傳        
    }
}
