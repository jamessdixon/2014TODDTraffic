using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace ChickenSoftware.RoadAlert.Analysis.Tests
{
    [TestClass]
    public class MachineLearningEngineTests
    {
        [TestMethod]
        public void TestKKN_ReturnsExpected()
        {

            Tuple<int, int, int, int>[] inputs = { 
                new Tuple<int, int, int, int>(1, 0, 15, 1), 
                new Tuple<int,int,int,int>(1,0,11,1)};
            int[] outputs = { 1, 1 };

            var input = new Tuple<int, int, int, int>(1, 1, 1, 1);

            var output = MachineLearningEngine.RunKNN(inputs, outputs, input);

        }

        public void TestValidate_ReturnsExpected()
        {
            Tuple<int, int, int, int>[] inputs = { 
                new Tuple<int, int, int, int>(1, 0, 15, 1), 
                new Tuple<int,int,int,int>(1,0,11,1)};
            
            int[] outputs = { 1, 1 };

            Tuple<int, int, int, int, int>[] validations = { 
                new Tuple<int, int, int, int, int>(1, 0, 15, 1, 1), 
                new Tuple<int,int,int,int, int>(1,0,11,1, 1)};
        }
    }
}
