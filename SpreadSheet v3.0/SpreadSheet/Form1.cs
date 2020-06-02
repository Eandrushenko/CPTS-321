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
        spreadsheet SS;
        public Form1()
        {
            InitializeComponent();
        }


        private void SheetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            Cell thing = sender as Cell;

            dataGridView1.Rows[thing.getRowIndex].Cells[thing.getColumnIndex].Value = thing.getValue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            int i = 0;
            int j = 65; //ASCII 65 = 'A'
            int k = 0;
            while (j < 91)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = (Convert.ToChar(j)).ToString(), Name = i.ToString() });
                j++;
                k++;
            }
            while (i < 50)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                i++;
            }
            SS = new spreadsheet(k, i);
            SS.CellPropertyChanged += SheetPropertyChanged;
        }

        private void Demo()
        {
            int i = 0;
            Random Randy = new Random();
            while (i < 50)
            {
                int col = Randy.Next(26);
                int row = Randy.Next(50);
                SS.getCell(col, row).getText = "Hello World!";
                //dataGridView1.Rows[row].Cells[col].Value = "Hello World!";
                i++;
            }
            
            int B = 0;
            while (B < 50)
            {
                //SS.getCell(1, B)._Text = "This is Cell B" + (B + 1).ToString();
                dataGridView1.Rows[B].Cells[1].Value = "This is Cell B" + (B + 1).ToString();
                B++;
            }
            int j = 0;
            while(j < 50)
            {
                //SS.getCell(0, j)._Text = "=B" + (j + 1).ToString();
                //dataGridView1.Rows[j].Cells[0].Value = SS.getCell(0, j)._Value;
                j++;
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Demo();
        }
    }
}
