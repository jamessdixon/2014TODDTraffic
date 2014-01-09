using ChickenSoftware.RoadAlert.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSoftware.RoadAlert.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            //RunTrafficStopsByMonth();
            //RunTrafficStopsByHour();
            //RunTrafficStopsByDay();

            GetSuccessPercentageOfValidations();
            Console.WriteLine("End");
            Console.ReadKey();
        }

        private static void GetSuccessPercentageOfValidations()
        {
            var output = MachineLearningEngine.GetSuccessPercentageOfValidations;
            Console.WriteLine(output.Item1.ToString() + ":" + output.Item2.ToString() + ":" + output.Item3.ToString());
        }

        private static void RunValidations()
        {
            var output = MachineLearningEngine.GetValidationsViaKKN;
            foreach (var tuple in output)
            {
                Console.WriteLine(tuple.Item1.ToString() + ":" + tuple.Item2.ToString());
            }
        }

        private static void RunSingleKKN()
        {
            var trainingInputClass = MachineLearningEngine.TrainingInputClass;
            var trainingOutputClass = MachineLearningEngine.TrainingOutputClass;
            var input = new Tuple<int, int, int, int>(1, 1, 1, 1);
            var output = MachineLearningEngine.RunKNN(trainingInputClass, trainingOutputClass, input);
            Console.WriteLine(output.ToString());
        }

        private static void RunTrafficStopsByMonth()
        {
            var output = AnalysisEngine.TrafficStopsByMonth;
            foreach (var tuple in output)
            {
                Console.WriteLine(tuple.Item1 + ":" + tuple.Item2 + ":" + tuple.Item3 + ":" + tuple.Item4 + ":" + tuple.Item5);
            }
        }

        private static void RunTrafficStopsByDay()
        {
            var output = AnalysisEngine.TrafficStopsByDay;
            foreach (var tuple in output)
            {
                Console.WriteLine(tuple.Item1 + ":" + tuple.Item2 + ":" + tuple.Item3 + ":" + tuple.Item4 + ":" + tuple.Item5);
            }
        }

        private static void RunTrafficStopsByHour()
        {
            var output = AnalysisEngine.TrafficStopsByHour;
            foreach (var tuple in output)
            {
                Console.WriteLine(tuple.Item1 + ":" + tuple.Item2 + ":" + tuple.Item3 + ":" + tuple.Item4 + ":" + tuple.Item5);
            }
        }


    }
}
