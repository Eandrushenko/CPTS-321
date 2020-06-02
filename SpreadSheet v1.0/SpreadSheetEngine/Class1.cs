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

        public event PropertyChangedEventHandler PropertyChanged;
        //https://docs.microsoft.com/en-us/dotnet/framework/wpf/data/how-to-implement-property-change-notification
        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/get
public int _RowIndex
        {
            get
            {
                return RowIndex;
            }
        }
        public int _ColumnIndex
        {
            get
            {
                return ColumnIndex;
            }
        }
        public string _Text
        {
            get
            {
                return Text;
            }
            set
            {
                if (Text != value)
                {
                    Text = value;
                    OnPropertyChanged(Text);

                }
            }
        }

        public string _Value
        {
            get
            {
                return Value;
            }
        }

    }
    public class Spreadsheet
    {
        private class Cell2D : Cell
        {
            public Cell2D(int column, int row) : base(column, row)
            {
                //use inherited Cell Constructor
            }
            public static Cell2D Factory(int Column, int Row)
            {
                return new Cell2D(Column, Row);
            }
            public void SetValue(string newval)
            {
                this.Value = newval;
            }
        }
        private int numColumns;
        private int numRows;
        
        public int _numColumns
        {
            get
            {
                return numColumns;
            }
        }
        public int _numRows
        {
            get
            {
                return numRows;
            }
        }
        public Cell getCell(int Column, int Row)
        {
            if (Row > numRows || Column > numColumns || Row < 0 || Column < 0)
            {
                Console.WriteLine("Out of Range");
                return null;
            }
            else
            {
                return new Cell2D(Column, Row);
            }
        }
    }
}

