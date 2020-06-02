using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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
                Cell temp = cellLocation(updatedCell.Text);
                updatedCell.Text = temp.Text;
            }
            CellPropertyChanged((sender as Cell2D), new PropertyChangedEventArgs("Text"));
        }

        //Each Cell's Location will be known with this function
        public static string getLocation(int row, int column) //must be static otherwise C# cries
        {
            string stringrow = (row + 1).ToString(); //Add 1 since we dont start at 0 on the spreadsheet
            string stringcolumn = ((char)(column + 65)).ToString(); //ASCII A = 65 so if column = 0 we are at 'A'
            return stringcolumn + stringrow; //Result should be A1, B5, C27, things like that

        }

        //Now we have the location we can make a Cell in that spot
        private Cell2D cellLocation(string location)
        {
            if (!char.IsLetter(location[1])) //If the 2nd char in the string isnt a letter it must not exist
            {
                return null;
            }
            int column = (int)((location[1]) - 65); //Reverse the ascii char back into an integer
            string temp = location.Substring(2); //Remove the first chars from the stirng so that "=A35" now equals "35"
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

    }
}

