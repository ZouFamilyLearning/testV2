using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testV2
{
    public partial class Ver2_1 : Form
    {
        // 所有資料的各自list
        public List<string> DealDate;
        public List<string> StockID;
        public List<string> StockName;
        public List<string> SecBrokerID;
        public List<string> SecBrokerName;
        public List<string> Price;
        public List<string> BuyQty;
        public List<string> CellQty;
        //

        public Ver2_1()
        {
            InitializeComponent();


        }

        public void newLists()
        {
            DealDate = new List<string>();
            StockID = new List<string>();
            StockName = new List<string>();
            SecBrokerID = new List<string>();
            SecBrokerName = new List<string>();
            Price = new List<string>();
            BuyQty = new List<string>();
            CellQty = new List<string>();
        }

        private void btnSetColumns_Click(object sender, EventArgs e)
        {
            // 資料讀取的視窗
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                this.txtFile.Text = file.SafeFileName;
                //
                // 新增所有List
                newLists();
                //
                //清除所有Column, Row
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                //
                // Add Column
                AddColumn("DealDate");
                AddColumn("StocksID");
                AddColumn("StockName");
                AddColumn("SecBrokerID");
                AddColumn("SecBrokerName");
                AddColumn("Price");
                AddColumn("BuyQty");
                AddColumn("CellQty");
                //
                // 有檔案就讀入資料
                if (File.Exists(txtFile.Text))
                {
                    // 存入陣列
                    string[] content = File.ReadAllLines(txtFile.Text, Encoding.GetEncoding("BIG5"));
                    //
                    // 開始計算資料讀取速度
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    //
                    // read all data
                    for (int i = 1; i < content.Length; i++)
                    {
                        string[] data = content[i].Split(',');
                        DealDate.Add(data[0]); // 分別存入DealDate
                        StockID.Add(data[1]); // 分別存入StockID
                        StockName.Add(data[2]); // 分別存入StockName
                        SecBrokerID.Add(data[3]); // 分別存入SecBrokerID
                        SecBrokerName.Add(data[4]); // 分別存入SecBrokerName
                        Price.Add(data[5]); // 分別存入Price
                        BuyQty.Add(data[6]); // 分別存入BuyQty
                        CellQty.Add(data[7]); // 分別存入CellQty

                        // dataGridView1.Rows.Add(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7]);//將Object Array填入DataRow
                    }
                    for (int i = 0; i < 50; i++)
                    {
                        dataGridView1.Rows.Add(DealDate[i], StockID[i], StockName[i], SecBrokerID[i], SecBrokerName[i], Price[i], BuyQty[i], CellQty[i]);//將Object Array填入DataRow
                    }
                    // dataGridView1.DataSource = content;
                    // 按照StockID改變順序
                    dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
                    //
                    //自動調整寬度
                    dataGridView1.AutoResizeColumns();
                    //
                    // 停止計算讀取速度
                    stopWatch.Stop();
                    //
                    // 顯示讀取速度在textBox1
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                    textBox1.Text = "讀取檔案: " + elapsedTime;
                    //
                }
                //
                comboBoxList();
            }

        }
        public void AddColumn(string strHeader)
        {
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = strHeader;
            dataGridView1.Columns.Add(column);
        }

        private void comboBoxList()
        {
            List<string> lis_DataList = new List<string>();
            lis_DataList.Add(StockID[1] + "-" + StockName[1]);
            lis_DataList.Add(StockID[2] + "-" + StockName[2]);

            comboBox1.DataSource = lis_DataList;
        }

    }

}
