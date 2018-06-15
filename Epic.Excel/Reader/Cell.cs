using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Excel
{
    public class CellReader
    {
        internal CellReader(ICell cell)
        {

        }

        internal ICell Cell
        {
            get;
            set;
        }
    }
}
