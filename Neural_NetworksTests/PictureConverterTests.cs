using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neural_Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Networks.Tests
{
    [TestClass()]
    public class PictureConverterTests
    {
        [TestMethod()]
        public void ConvertTest()
        {
            var converter = new PictureConverter();
            var inputs = converter.Convert(@"C:\Users\aser\source\repos\Neural_Networks\Neural_NetworksTests\images\Parasitized.png");
            converter.Save(@"D:\image.png", inputs);
        }
    }
}