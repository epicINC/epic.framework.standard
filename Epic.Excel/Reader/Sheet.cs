using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Epic.Excel
{
    public class SheetReader
    {
        internal SheetReader(Reader reader, ISheet sheet)
        {
            this.Reader = reader;
            this.Sheet = sheet;
            this.RowReader = new RowReader(this.Row);
            
        }




        public Reader Reader
        {
            get;
            private set;
        }

        internal RowReader RowReader
        {
            get;
            private set;
        }

        internal ISheet Sheet
        {
            get;
            private set;
        }



        internal IRow Row
        {
            get;
            private set;
        }

        internal int Offset
        {
            get;
            private set;
        }

        public bool Read()
        {
            return (this.Row = this.Sheet.GetRow(this.Offset++)) != null;
        }

        public CellReader this[int index]
        {
            get { return this.RowReader[index]; }
        }

    }
}
