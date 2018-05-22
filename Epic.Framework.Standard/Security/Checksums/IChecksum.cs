using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Security.Checksums
{
    public interface IChecksum
    {
        void Update(int value);
        void Update(byte[] buffer);
        void Update(byte[] buffer, int offset, int count);


        long Value
        {
            get;
        }
        void Reset();
    }
}
