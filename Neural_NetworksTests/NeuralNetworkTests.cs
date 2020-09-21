using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neural_Networks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Networks.Tests
{
    [TestClass()]
    public class NeuralNetworkTests
    {
        [TestMethod()]
        public void FeedForwardTest()
        {
            var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
            var inputs = new double[,]
            {
                //Результат - болеет - 1
                //            здоров - 0
                //Неправильная температура T
                //Хороший возраст A
                //Курит S
                //Правильно питается F
                // T A S F
                { 0,0,0,0 },
                { 0,0,0,1 },
                { 0,0,1,0 },
                { 0,0,1,1 },
                { 0,1,0,0 },
                { 0,1,0,1 },
                { 0,1,1,0 },
                { 0,1,1,1 },
                { 1,0,0,0 },
                { 1,0,0,1 },
                { 1,0,1,0 },
                { 1,0,0,1 },
                { 1,0,1,0 },
                { 1,1,0,1 },
                { 1,1,1,0 },
                { 1,1,1,1 }
            };
            var topology = new Topology(4, 1, 0.1, 2);
            var neuralNetwork = new NeuralNetwork(topology);
            var difference = neuralNetwork.Learn(outputs, inputs, 10000000);//обучение 1000 прогон

            var results = new List<double>();
            for (int i = 0; i < outputs.Length; i++)
            {
                var row = NeuralNetwork.GetRow(inputs, i);
                var res = neuralNetwork.Predict(row).Output;//использование
                results.Add(res);
            }

            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);
                Assert.AreEqual(expected, actual);
            }
        }

        #region DataSetTest
        //    [TestMethod()] 

        //    public void DataSetTests()
        //    {
        //        var outputs = new List<double>();
        //        var inputs = new List<double[]>();
        //        using (var sr = new StreamReader("C:\\Users\\aser\\source\\repos\\Neural_Networks\\Neural_NetworksTests\\heart.csv"))
        //        {
        //            var header = sr.ReadLine();
        //            while (!sr.EndOfStream)
        //            {
        //                var row = sr.ReadLine();
        //                var temp = row.Split(',');
        //                var values = temp.Select(v => Convert.ToDouble(v.Replace(".",","))).ToList();
        //                var output = values.Last();
        //                var input = values.Take(values.Count - 1).ToArray();
        //                outputs.Add(output);
        //                inputs.Add(input);
        //            }
        //        }
        //        var inputSignals = new double[inputs.Count, inputs[0].Length];
        //        for (int i = 0; i < inputSignals.GetLength(0); i++)
        //        {
        //            for (int j = 0; j < inputSignals.GetLength(1); j++)
        //            {
        //                inputSignals[i, j] = inputs[i][j];
        //            }
        //        }

        //        var topology = new Topology(outputs.Count, 1, 1, outputs.Count/2);
        //        var neuralNetwork = new NeuralNetwork(topology);
        //        var difference = neuralNetwork.Learn(outputs.ToArray(), inputSignals, 100);//обучение 1000 прогон

        //        var results = new List<double>();
        //        for (int i = 0; i < outputs.Count; i++)
        //        {
        //            var res = neuralNetwork.FeedForward(inputs[i]).Output;//использование
        //            results.Add(res);
        //        }

        //        for (int i = 0; i < results.Count; i++)
        //        {
        //            var expected = Math.Round(outputs[i], 2);
        //            var actual = Math.Round(results[i], 2);
        //            Assert.AreEqual(expected, actual);
        //        }
        //    }
        //}
        #endregion 


        [TestMethod()]

        public void RecognizeImageTest()
        {
            var size = 1000;
            var parasitizedPath = @"D:\Dlya C# foto\cell_images\cell_images\Parasitized";
            var unParasitizedPath = @"D:\Dlya C# foto\cell_images\cell_images\Uninfected";
            var converter = new PictureConverter();
            var testParazitizedImageInput = converter.Convert(@"C:\Users\aser\source\repos\Neural_Networks\Neural_NetworksTests\images\Parasitized.png");
            var testUnParazitizedImageInput = converter.Convert(@"C:\Users\aser\source\repos\Neural_Networks\Neural_NetworksTests\images\Unparasitized.png");
            var topology = new Topology(testParazitizedImageInput.Count, 1, 0.1, testParazitizedImageInput.Count / 2);
            var neuralNetwork = new NeuralNetwork(topology);

            double[,] parazitizedInputs = GetData(parasitizedPath, converter, testParazitizedImageInput,size);
            neuralNetwork.Learn(new double[] { 1 }, parazitizedInputs, 1);

            double[,] unparazitizedInputs = GetData(unParasitizedPath, converter, testParazitizedImageInput,size);
            neuralNetwork.Learn(new double[] { 0 }, unparazitizedInputs, 1);

            var par = neuralNetwork.Predict(testParazitizedImageInput.Select(t => (double)t).ToArray());
            var unPar =  neuralNetwork.Predict(testUnParazitizedImageInput.Select(t => (double)t).ToArray());

            Assert.AreEqual(1, Math.Round(par.Output, 2));
            Assert.AreEqual(0, Math.Round(unPar.Output, 2));
        }

        private static double[,] GetData(string parasitizedPath, PictureConverter converter, List<int> testImageInput, int size)
        {
            var images = Directory.GetFiles(parasitizedPath);
            var result = new double[size, testImageInput.Count];
            for (int i = 0; i < size; i++)
            {
                var image = converter.Convert(images[i]);
                for (int j = 0; j < image.Count; j++)
                {
                    result[i, j] = image[j];
                }
            }
            return result;
        }
    }
}