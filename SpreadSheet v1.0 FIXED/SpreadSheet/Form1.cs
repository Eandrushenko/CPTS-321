//Elijah Andrushenko
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CptS321;


namespace SpreadSheet
{
    public partial class Form1 : Form
    {
        private Spreadsheet SS = new Spreadsheet(50, 26);

        //Event for the creation of Spreadsheet
        private void SheetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var updatedCell = sender as Cell;

            if(e.PropertyName == "Value")
            {
                dataGridView1[updatedCell.ColumnIndex, updatedCell.RowIndex].Value = updatedCell.Value;
            }
            else if (e.PropertyName == "Text")
            {
                dataGridView1[updatedCell.ColumnIndex, updatedCell.RowIndex].Value = updatedCell.Text;
            }
        }

        public Form1()
        {
            InitializeComponent();
            SS.CellPropertyChanged += SheetPropertyChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //dataGridView1.Columns.Clear();
            //dataGridView1.Rows.Clear();

            //Make 26 Columns, A-Z
            dataGridView1.ColumnCount = 26;
            int mychar = 65;
            int count = 0;
            while (mychar <= 90)
            {
                dataGridView1.Columns[count].Name = ((char)mychar).ToString();
                mychar++;
                count++;
            }
            //Make 50 Rows, 1-50
            count = 1;
            while(count <= 50)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[count - 1].HeaderCell.Value = count.ToString();
                count++;
            }
            //The Rows numbers wouldnt show the entire number needed this call
            dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.datagridview.cellbeginedit?view=netframework-4.7.2
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridView1[e.ColumnIndex, e.RowIndex].Value = SS.getCell(e.RowIndex, e.ColumnIndex).Text;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string item = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
            if (item != null)
            {
                Cell myCell = SS.getCell(e.RowIndex, e.ColumnIndex);
                myCell.Text = item;
            }
        }

        public void Demo1()
        {
            //Hello World in 50 Places
            Random randy = new Random();
            for (int i = 0; i < 50; i++)
            {
                SS.getCell(randy.Next(0, 49), randy.Next(0, 25)).Text = "Hello World!";

            }

            //Label Column B
            string Bcells = "This is cell B";
            for (int i = 0; i < 50; i++)
            {
                SS.getCell(i, 1).Text = Bcells + ((i + 1).ToString());
            }

            //Copy Text of Column B into Column A
            Bcells = "=B";
            for (int i = 0; i < 50; i++)
            {
                SS.getCell(i, 0).Text = Bcells + ((i + 1).ToString());
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Demo1();
        }
    }
}
