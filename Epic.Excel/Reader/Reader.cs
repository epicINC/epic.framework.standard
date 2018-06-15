using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Epic.Excel
{
    public class Reader
    {
        public Reader(string path)
        {
            this.Workbook = WorkbookFactory.Create(path);
        }

        public Reader(Stream value)
        {
            this.Workbook = WorkbookFactory.Create(value);
        }


        IWorkbook Workbook
        {
            get;
            set;
        }

        Dictionary<int, SheetReader> Data
        {
            get;
            set;
        } = new Dictionary<int, SheetReader>();

        SheetReader current;
        public SheetReader Current
        {
            get { return this.current ?? this[0]; }
        }

        public SheetReader this[int index]
        {
            get
            {
                if (this.Data.TryGetValue(index, out this.current)) return this.current;
                if (this.Workbook.NumberOfSheets > index) return this.current = this.Data[index] = new SheetReader(this, this.Workbook.GetSheetAt(index));
                return null;
            }
        }

        public SheetReader this[string name]
        {
            get
            {
                var index = this.Workbook.GetSheetIndex(name);
                if (index == -1) return null;
                return this[index];
            }
        }
    }
}
