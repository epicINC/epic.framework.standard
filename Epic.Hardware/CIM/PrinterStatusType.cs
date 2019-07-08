using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Hardware.CIM
{

    public enum PrinterStatusType : ushort
    {
        Other = 0x1,
        Unknown = 0x2,
        Idle = 0x3,
        Printing = 0x4,
        WarmingUp = 0x5,
        StoppedPrinting = 0x6,
        Offline = 0x7
    }


    enum PrinterStatusXType
    {
        Other = 1,
        Unknown = 2,
        Idle = 3,
        Printing = 4,
        Warmup = 5,
        StoppedPrinting = 6,
        Offline = 7,
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-rprn/1625e9d9-29e4-48f4-b83d-3bd0fdaea787
    /// </summary>
    [Flags]
    enum PrinterStatusBType
    {

        /// <summary>
        /// 准备就绪
        /// </summary>
        Ready = 0,

        /// <summary>
        /// 忙
        /// </summary>
        Busy = 0x00000200,

        /// <summary>
        /// 被打开
        /// </summary>
        DoorOpen = 0x00400000,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 0x00000002,

        /// <summary>
        /// 初始化
        /// </summary>
        Initializing = 0x0008000,

        /// <summary>
        /// 正在输入,输出
        /// </summary>
        IOActive = 0x00000100,

        /// <summary>
        /// 手工送纸
        /// </summary>
        ManualFeed = 0x00000020,

        /// <summary>
        /// 无墨粉
        /// </summary>
        NoToner = 0x00040000,

        /// <summary>
        /// 不可用
        /// </summary>
        NotAvailable = 0x00001000,

        /// <summary>
        /// 离线
        /// </summary>
        Offline = 0x00000080,

        /// <summary>
        /// 内存溢出
        /// </summary>
        OutofMemory = 0x00200000,

        /// <summary>
        /// 输出口已满
        /// </summary>
        OutputBinFull = 0x00000800,

        /// <summary>
        /// 当前页无法打印
        /// </summary>
        PagePunt = 0x00080000,

        /// <summary>
        /// 塞纸
        /// </summary>
        PaperJam = 0x00000008,

        /// <summary>
        /// 打印纸用完
        /// </summary>
        PaperOut = 0x00000010,

        /// <summary>
        /// 纸张问题
        /// </summary>
        PageProblem = 0x00000040,

        /// <summary>
        /// 暂停
        /// </summary>
        Paused = 0x00000001,

        /// <summary>
        /// 正在删除
        /// </summary>
        PendingDeletion = 0x00000004,

        /// <summary>
        /// 正在打印
        /// </summary>
        Printing = 0x00000400,

        /// <summary>
        /// 正在处理
        /// </summary>
        Processing = 0x00004000,

        /// <summary>
        /// 墨粉不足
        /// </summary>
        TonerLow = 0x00020000,

        /// <summary>
        /// 需要用户干预
        /// </summary>
        UserIntervention = 0x00100000,

        /// <summary>
        /// 等待
        /// </summary>
        Waiting = 0x20000000,

        /// <summary>
        /// 热机中
        /// </summary>
        WarmingUp = 0x00010000,

        /// <summary>
        /// 未知状态
        /// </summary>
        UnknownStatus = -1

    }

}
