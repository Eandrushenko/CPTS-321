﻿//Elijah Andrushenko
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
using System.IO;


namespace SpreadSheet
{
    public partial class Form1 : Form
    {
        private Spreadsheet SS = new Spreadsheet(50, 26);
        int storageRow;
        int storageColumn;
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
            dataGridView1.CellClick += dataGridView1_CellClick;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.SendToBack();
            richTextBox1.SendToBack();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = SS.getCell(e.RowIndex, e.ColumnIndex).Text;
            textBox2.Text = SS.getCell(e.RowIndex, e.ColumnIndex).Value;
            storageRow = e.RowIndex;
            storageColumn = e.ColumnIndex;
        }

        private void CheckEnterKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (dataGridView1.SelectedCells.Count>0)
                {
                    SS.getCell(storageRow, storageColumn).Text = textBox1.Text;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.KeyPress += new KeyPressEventHandler(CheckEnterKeyPress);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void LoadSheet(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "XML Files (*.xml)|*.xml";
            ofd.DefaultExt = "xml";
            ofd.AddExtension = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.ClearSelection();
                Stream fs = ofd.OpenFile();
                cleartable(50, 26);
                SS.LoadXML(fs);
            }
        }

        private void SaveSheet(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "XML Files (*.xml)|*.xml";
            sfd.DefaultExt = "xml";
            sfd.AddExtension = true;


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream fs = sfd.OpenFile();
                SS.SaveXML(fs);
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSheet(sender, e);
        }

        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoadSheet(sender, e);
        }

        private void cleartable(int rows, int columns)
        {
            for (int rowCount = 0; rowCount < rows; rowCount++)
            {
                for (int columnCount = 0; columnCount < columns; columnCount++)
                {
                    SS.getCell(rowCount, columnCount).Text = "=0";
                    SS.getCell(rowCount, columnCount).Text = " ";
                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cleartable(50, 26);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.SendToBack();
            richTextBox1.SendToBack();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BringToFront();
            button2.BringToFront();
        }
    }
}

