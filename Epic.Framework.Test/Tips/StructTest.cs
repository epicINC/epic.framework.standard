using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Epic.Framework.Test.Tips
{
    [StructLayout(LayoutKind.Explicit)]
    public struct FloatBytes
    {
        [FieldOffset(0)]
        public byte Index0;
        [FieldOffset(1)]
        public byte Index1;
        [FieldOffset(2)]
        public byte Index2;
        [FieldOffset(3)]
        public byte Index3;

        [FieldOffset(0)]
        public int i;

        [FieldOffset(0)]
        public float Value;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct OSVersionInfo
    {
        public int OSVersionInfoSize;
        public int MajorVersion;
        public int MinorVersion;
        public int BuildNumber;
        public int PlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string VersionString;
    }

    [TestClass]
    public class StructTest
    {

        [DllImport("kernel32", EntryPoint = "GetVersionEx")]
        public static extern bool GetVersionEx2(ref OSVersionInfo osvi);

        [TestMethod]
        public void ConverterTest()
        {
            var result = new OSVersionInfo();
            GetVersionEx2(ref result);


        }



        [TestMethod]
        public void FloatBytes()
        {
            var result = new FloatBytes();
            result.Value = 0.4f;
            Assert.AreEqual(result.Index0, 205);
            Assert.AreEqual(result.Index1, 204);
            Assert.AreEqual(result.Index2, 204);
            Assert.AreEqual(result.Index3, 62);
        }
    }
}
