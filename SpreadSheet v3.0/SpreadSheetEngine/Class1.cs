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
        readonly int RowIndex;
        readonly int ColumnIndex;
        protected string Text;
        protected string Value;

        public Cell(int Column, int Row)
        {
            RowIndex = Row;
            ColumnIndex = Column;
            Text = "";
            Value = "";
        }
        public event PropertyChangedEventHandler ValPropertyChanged;
        public void Val_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Value");
            if (ValPropertyChanged != null)
            {
                ValPropertyChanged(this, e);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/get
        public int getRowIndex
        {
            get
            {
                return this.RowIndex;
            }
        }
        public int getColumnIndex
        {
            get
            {
                return this.ColumnIndex;
            }
        }
        public string getText
        {
            get
            {
                return this.Text;
            }
            set
            {
                if (Text != value)
                {
                    Text = value;
                    OnPropertyChanged("Text");

                }
            }
        }

        public string getValue
        {
            get
            {
                return Value;
            }
        }

    }

    public class spreadsheet
    {
        private class Cell2D : Cell
        {
            public Cell2D(int column, int row):base(column, row)
            {
                //inherited class
            }

            public static Cell2D Factory(int column, int row)
            {
                return new Cell2D(column, row);
            }

            public void setValue(string newValue)
            {
                this.Value = newValue;
            }
        }
        private int ColumnCount = 0;
        private int RowCount = 0;

        private Cell2D[,] Cellset;

        private int getColumnCount
        {
            get
            {
                return ColumnCount;
            }
        }

        private int getRowCount
        {
            get
            {
                return RowCount;
            }
        }

        public spreadsheet(int column, int row)
        {
            Cellset = new Cell2D[column, row];
            int i = 0;
            int j = 0;
            while (i < column)
            {
                while (j < row)
                {
                    Cellset[i, j] = new Cell2D(i, j);
                    j++;
                }
                i++;
            }
            ColumnCount = column;
            RowCount = row;
        }

        public Cell getCell(int column, int row)
        {
            if (column > ColumnCount || column < 0)
            {
                return Cellset[0,0];
            }
            if (row > RowCount || row < 0)
            {
                return Cellset[0,0];
            }
            return Cellset[column, row];
        }

        public event PropertyChangedEventHandler CellPropertyChanged;
        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var thing = sender as Cell2D;
            // Sets value 
            thing.setValue(thing.getText);
            // Sends property changed 
            if (CellPropertyChanged != null)
            {
                CellPropertyChanged(sender, e);
            }
        }
    }
}

