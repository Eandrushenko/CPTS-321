//Elijah Andrushenko
//011476324
//Form1 
//The Code written for the NotepadApp can be found here, and all things stream related 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NotepadApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Open Textbox
        private void LoadText(TextReader sr)
        {
            textBox1.Clear(); 
            try
            {
                textBox1.AppendText(sr.ReadToEnd());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Load contents of a file
        private void loadFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Load File",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)    
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))     
                {
                    LoadText(sr);         
                }
            }
        }

        private void load50(object sender, EventArgs e)
        {
            using (FibonacciTextReader ftr = new FibonacciTextReader(50))
            {
                LoadText(ftr);
            }
        }

        private void load100(object sender, EventArgs e)
        {
            using (FibonacciTextReader ftr = new FibonacciTextReader(100))
            {
                LoadText(ftr);
            }
        }

        //Save contents of a file
        private void saveFile(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Save As",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)        
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))      
                {
                    try
                    {
                        sw.Write(textBox1.Text);         
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}