using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DevTest.JSON
{

    public class Config
    {

        /// <summary>
        /// 主机地址 Ur
        /// </summary>s
        [JsonPropertyName("host")]
        public string Host { get; set; }


        /// <summary>
        /// 证件打印机名称
        /// </summary>
        [JsonPropertyName("cardPrinter.name")]
        public string CardPrinter { get; set; }

        /// <summary>
        /// RFID 设备端口
        /// </summary>
        [JsonPropertyName("rfid.port")]
        public string RFIDPort { get; set; }

        /// <summary>
        /// RFID 卡加密运算 Key，同闸机 PDAs
        /// </summary>
        [JsonPropertyName("rfid.secret")]
        public string RFIDSecret { get; set; }

    }


    public class JSONTester
    {

        public static void LoadFromFile()
        {
            var file = "./config.json";
            var data = Epic.JSON.Read<Config>(file);

        }
    }
}
