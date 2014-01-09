using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccordLocal
{
    [Serializable]
    public class KNearestNeighbors<T>
    {
        private int k;

        private T[] inputs;
        private int[] outputs;

        private int classCount;

        private Func<T, T, double> distance;

        private double[] distances;


        /// <summary>
        ///   Creates a new <see cref="KNearestNeighbors"/>.
        /// </summary>
        /// 
        /// <param name="k">The number of nearest neighbors to be used in the decision.</param>
        /// 
        /// <param name="inputs">The input data points.</param>
        /// <param name="outputs">The associated labels for the input points.</param>
        /// <param name="distance">The distance measure to use in the decision.</param>
        /// 
        public KNearestNeighbors(int k, T[] inputs, int[] outputs, Func<T, T, double> distance)
        {
            checkArgs(k, null, inputs, outputs, distance);

            int classCount = outputs.Distinct().Count();

            initialize(k, classCount, inputs, outputs, distance);
        }

        /// <summary>
        ///   Creates a new <see cref="KNearestNeighbors"/>.
        /// </summary>
        /// 
        /// <param name="k">The number of nearest neighbors to be used in the decision.</param>
        /// <param name="classes">The number of classes in the classification problem.</param>
        /// 
        /// <param name="inputs">The input data points.</param>
        /// <param name="outputs">The associated labels for the input points.</param>
        /// <param name="distance">The distance measure to use in the decision.</param>
        /// 
        public KNearestNeighbors(int k, int classes, T[] inputs, int[] outputs, Func<T, T, double> distance)
        {
            checkArgs(k, classes, inputs, outputs, distance);

            initialize(k, classes, inputs, outputs, distance);
        }

        private void initialize(int k, int classes, T[] inputs, int[] outputs, Func<T, T, double> distance)
        {
            this.inputs = inputs;
            this.outputs = outputs;

            this.k = k;
            this.classCount = classes;

            this.distance = distance;
            this.distances = new double[inputs.Length];
        }


        /// <summary>
        ///   Gets the set of points given
        ///   as input of the algorithm.
        /// </summary>
        /// 
        /// <value>The input points.</value>
        /// 
        public T[] Inputs
        {
            get { return inputs; }
        }

        /// <summary>
        ///   Gets the set of labels associated
        ///   with each <see cref="Inputs"/> point.
        /// </summary>
        /// 
        public int[] Outputs
        {
            get { return outputs; }
        }

        /// <summary>
        ///   Gets the number of class labels
        ///   handled by this classifier.
        /// </summary>
        /// 
        public int ClassCount
        {
            get { return classCount; }
        }

        /// <summary>
        ///   Gets or sets the distance function used
        ///   as a distance metric between data points.
        /// </summary>
        /// 
        public Func<T, T, double> Distance
        {
            get { return distance; }
            set { distance = value; }
        }


        /// <summary>
        ///   Gets or sets the number of nearest 
        ///   neighbors to be used in the decision.
        /// </summary>
        /// 
        /// <value>The number of neighbors.</value>
        /// 
        public int K
        {
            get { return k; }
            set
            {
                if (value <= 0 || value > inputs.Length)
                    throw new ArgumentOutOfRangeException("value",
                        "The value for k should be greater than zero and less than total number of input points.");

                k = value;
            }
        }

        /// <summary>
        ///   Computes the most likely label of a new given point.
        /// </summary>
        /// 
        /// <param name="input">A point to be classified.</param>
        /// 
        /// <returns>The most likely label for the given point.</returns>
        /// 
        public int Compute(T input)
        {
            double[] scores;
            return Compute(input, out scores);
        }

        /// <summary>
        ///   Computes the most likely label of a new given point.
        /// </summary>
        /// 
        /// <param name="input">A point to be classified.</param>
        /// <param name="response">A value between 0 and 1 giving 
        /// the strength of the classification in relation to the
        /// other classes.</param>
        /// 
        /// <returns>The most likely label for the given point.</returns>
        /// 
        public int Compute(T input, out double response)
        {
            double[] scores;
            int result = Compute(input, out scores);
            response = scores[result] / scores.Sum();

            return result;
        }

        /// <summary>
        ///   Computes the most likely label of a new given point.
        /// </summary>
        /// 
        /// <param name="input">A point to be classified.</param>
        /// <param name="scores">The distance score for each possible class.</param>
        /// 
        /// <returns>The most likely label for the given point.</returns>
        /// 
        public virtual int Compute(T input, out double[] scores)
        {
            //// Compute all distances
            for (int i = 0; i < inputs.Length; i++)
                distances[i] = distance(input, inputs[i]);

            //int[] idx = distances.Bottom(k, inPlace: true);
            int[] idx = { 0, 1 };

            scores = new double[classCount];

            for (int i = 0; i < idx.Length; i++)
            {
                int j = idx[i];

                int label = outputs[j];
                double d = distances[i];

                // Convert to similarity measure
                scores[label] += 1.0 / (1.0 + d);
            }

            //// Get the maximum weighted score
            //int result; scores.Max(out result);

            //return result;

            return 1;
        }






        private static void checkArgs(int k, int? classes, T[] inputs, int[] outputs, Func<T, T, double> distance)
        {
            if (k <= 0)
                throw new ArgumentOutOfRangeException("k", "Number of neighbors should be greater than zero.");

            if (classes != null && classes <= 0)
                throw new ArgumentOutOfRangeException("k", "Number of classes should be greater than zero.");

            if (inputs == null)
                throw new ArgumentNullException("inputs");

            if (outputs == null)
                throw new ArgumentNullException("inputs");

            if (inputs.Length != outputs.Length)
                throw new ArgumentException("outputs",
                    "The number of input vectors should match the number of corresponding output labels");

            if (distance == null)
                throw new ArgumentNullException("distance");
        }
    }
}


