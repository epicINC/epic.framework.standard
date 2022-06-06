using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Framework.Test.Jsons
{
    /*
    public class Config
    {
        [JsonPropertyName("stringValue")]
        public string StringValue { get; set; }


        [JsonPropertyName("boolValue")]
        public bool BoolValue { get; set; }

        [JsonPropertyName("intValue")]
        public int IntValue { get; set; }

        [JsonPropertyName("dateValue")]
        public DateTime DateValue { get; set; }


        [JsonPropertyName("stringArray")]
        public string[] StringArray { get; set; }

        [JsonPropertyName("stringList")]
        public string[] StringList { get; set; }




        [JsonIgnore]
        public string Ignore { get; set; }
    }

    [TestClass]
    public class JsonTest
    {

        [TestMethod]
        public void Stringify()
        {
            var expected = "{\"stringValue\":\"test\",\"intValue\":0}";
            var tester = new Config() { StringValue = "test" };
            var actual = Epic.JSON.Stringify(tester);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Parse()
        {
            var expected = new Config() { StringValue = "test" };
            var actual = Epic.JSON.Parse<Config>("{\"stringValue\":\"test\",\"intValue\":0}");

            Assert.AreEqual(expected.StringValue, actual.StringValue);
        }


    }
    */
}
