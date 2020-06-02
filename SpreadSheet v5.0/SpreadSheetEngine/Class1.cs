using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace CptS321
{
    public abstract class Cell : INotifyPropertyChanged
    {
        //Variables
        protected int rowIndex;
        protected int columnIndex;
        protected string text;
        protected string value;
        public int flag; //flag = 1, Cell has changed. flag = 0, Cell has not changed

        //Events
        public event PropertyChangedEventHandler PropertyChanged; 

        //Getters and Setters
        public int RowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        public int ColumnIndex
        {
            get
            {
                return columnIndex;
            }
            set
            {
                columnIndex = value;
            }
        }
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value != text)
                {
                    text = value;
                    flag = 1;
                    PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }

            }
        }
        public string Value
        {
            get
            {
                return this.value; //need to use this.value instead of just value to avoid confusion between keyword and variable
            }
        }

        //Constructor
        public Cell(int row, int column)
        {
            rowIndex = row;
            columnIndex = column;
            value = 0.ToString();
            flag = 0;
        }

    }

    class Cell2D : Cell
    {
        //Variables
        public string location;

        //Constructor
        public Cell2D(int row, int column) : base(row, column)
        {
            location = Spreadsheet.getLocation(row, column);
        }

        public void setValue(string newValue)
        {
            this.value = newValue;
        }

    }

    public class Spreadsheet
    {
        //Variables
        private Cell2D[,] sheet;
        private int numRows;
        private int numColumns;

        //Event handler
        public event PropertyChangedEventHandler CellPropertyChanged = delegate { };

        //If a Cell Property Changed we call this function
        private void CellChanged(object sender, PropertyChangedEventArgs e)
        {
            var updatedCell = sender as Cell2D;
            if (updatedCell.Text[0] == '=') //If the cell text is =A35 we go and find the text of cell A35
            {
                string eq = updatedCell.Text.Substring(1); //Get rid of the = in front
                updatedCell.setValue(solver(eq));
            }
            CellPropertyChanged((sender as Cell2D), new PropertyChangedEventArgs("Text"));
        }

        private string solver(string equation)
        {
            ExpTree mytree = new ExpTree(equation);
            findCells(mytree);
            return mytree.Eval().ToString();
        }

        private void findCells(ExpTree tree)
        {
            foreach (var item in tree.variables.ToList())
            {
                tree.variables[item.Key] = float.Parse(cellLocation(item.Key).Value);
                tree.setVar(item.Key, float.Parse(cellLocation(item.Key).Value));
            }
        }

        //Each Cell's Location will be known with this function
        public static string getLocation(int row, int column) //must be static otherwise C# cries
        {
            string stringrow = (row + 1).ToString(); //Add 1 since we dont start at 0 on the spreadsheet
            string stringcolumn = ((char)(column + 65)).ToString(); //ASCII A = 65 so if column = 0 we are at 'A'
            return stringcolumn + stringrow; //Result should be A1, B5, C27, things like that

        }


        //This has been modified for use in finding varNodes for this project
        private Cell2D cellLocation(string location)
        {
            if (!char.IsLetter(location[0])) //If the 2nd char in the string isnt a letter it must not exist
            {
                return null;
            }
            int column = (int)((location[0]) - 65); //Reverse the ascii char back into an integer
            string temp = location.Substring(1); //Remove the first chars from the stirng so that "A35" now equals "35"
            int row = Int32.Parse(temp); //Make the string "35" an int now its just 35
            row--; //Our spreadsheet starts at 1 but our 2D array doesn't therefore we need to offset by 1

            //Check if columns and rows are out of range
            if ((column > numColumns) || (column < 0))
            {
                return null;
            }
            if ((row > numRows) || (row < 0))
            {
                return null;
            }
            return sheet[row, column];

        }

        //return a Cell from the sheet based on its location
        public Cell getCell(int row, int column)
        {
            return sheet[row, column];
        }

        //Constructor
        public Spreadsheet(int rows, int columns)
        {
            numRows = rows;
            numColumns = columns;

            sheet = new Cell2D[rows, columns];

            //Fill up the sheet
            for(int rowCount = 0; rowCount < numRows; rowCount++)
            {
                for (int columnCount = 0; columnCount < numColumns; columnCount++)
                {
                    sheet[rowCount, columnCount] = new Cell2D(rowCount, columnCount);
                    sheet[rowCount, columnCount].PropertyChanged += CellChanged;
                }
            }
        }
        public void SaveXML(Stream xmls)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(xmls, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                foreach (var cell in sheet)
                {
                    if (cell.Text != null)
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteElementString("column", cell.ColumnIndex.ToString());
                        writer.WriteElementString("row", cell.RowIndex.ToString());
                        writer.WriteElementString("text", cell.Text);

                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Dispose();
            }
        }

        public void LoadXML(Stream xmls)
        {
            XmlReader reader = XmlReader.Create(xmls);

            while (reader.Read())
            {
                if (reader.Name == "cell")
                {
                    string CellText = null;
                    int row = 0;
                    int col = 0;
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "column":
                                    reader.Read();
                                    col = Convert.ToInt32(reader.Value);
                                    break;
                                case "row":
                                    reader.Read();
                                    row = Convert.ToInt32(reader.Value);
                                    break;
                                case "text":
                                    reader.Read();
                                    CellText = reader.Value;
                                    break;
                            }
                        }
                        else if (reader.Name == "cell")
                        {
                            break;
                        }
                    }
                    if (CellText != null)
                    {
                        this.sheet[row, col].Text = CellText;
                    }
                }
            }
            reader.Dispose();
        }
    }

}

