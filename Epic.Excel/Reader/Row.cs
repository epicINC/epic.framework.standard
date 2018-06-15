using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Excel
{
    public class RowReader
    {
        internal RowReader(IRow row)
        {
            this.Row = row;
            this.CellReader = new CellReader(null);
        }

        internal CellReader CellReader
        {
            get;
            private set;
        }

        internal IRow Row
        {
            get;
            private set;
        }


        public CellReader this[int index]
        {
            get
            {
                this.CellReader.Cell = this.Row?.GetCell(index);
                return this.CellReader;
            }
        }

    }
}
