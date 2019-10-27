using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg2
{
    static class DataSource
    {
        public static float[][] FillMatrixWithFloatData()
        {
            var matrix = new[]
            {
                //new float[] { 2, 1, 1, 7 } , new float[] { 0, 2, 1, 4 } , new float[] { 1, 1, 2, 6 }
                // new float[] { 1, 1/2f , 1/3f , 32 } , new float[] { 1/2f, 1/3f, 1/4f, 22 } , new float[] { 1/3f, 1/4f, 1/5f, 17 }
                new float[] { 1.00f, 0.5f , 0.33f , 32 } , new float[] { 0.5f, 0.33f, 0.25f, 22 } , new float[] { 0.33f, 0.25f, 0.20f, 17 }

                //new float[] { 4,-2,4,-2,8 }, new float[] {3,1,4,2,7 },new float[] {2,4,2,1,10},new float[] {2,-2,4,2,2}
            };

            return matrix;
        }

        public static double[][] FillMatrixWithDoubleData()
        {
            var matrix = new[]
            {
                //new double[] { 2, 1, 1, 7 } , new double[] { 0, 2, 1, 4 } , new double[] { 1, 1, 2, 6 }
                // new double[] { 1, 1/2 , 1/3 , 32 } , new double[] { 1/2, 1/3, 1/4, 22 } , new float[] { 1/3, 1/4, 1/5, 17 }
                new double[] { 1.00, 0.5 , 0.33 , 32 } , new double[] { 0.5, 0.33, 0.25, 22 } , new double[] { 0.33, 0.25, 0.20, 17 }

                //new double[] { 4,-2,4,-2,8 }, new double[] {3,1,4,2,7 },new double[] {2,4,2,1,10},new double[] {2,-2,4,2,2}
            };

            return matrix;
        }
    }
}
