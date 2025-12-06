using System;
using System.Drawing;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        private static double MinLength;
        private static int[] bestOrder;

        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            int size = checkpoints.Length;
            int[] bestOrder = new int[size]; 

            for (int i = 0; i < size; i++)
                bestOrder[i] = i;  

            if (size == 1) return bestOrder; 

            MinLength = double.MaxValue;  
            MakeTrivialPermutation(checkpoints, new int[size], 1, new double[size], ref bestOrder);  

            return bestOrder;
        }

        private static void MakeTrivialPermutation(Point[] checkpoints, int[] positions, int currentPosition, double[] lengths, ref int[] bestOrder)
        {
            if (currentPosition == checkpoints.Length) 
            {
                if (lengths[currentPosition - 1] < MinLength)
                {
                    MinLength = lengths[currentPosition - 1];
                    bestOrder = (int[])positions.Clone();  
                }
                return;
            }

            for (int i = 1; i < positions.Length; i++)
            {
                int index = Array.IndexOf(positions, i, 1, currentPosition - 1);
                if (index == -1)
                {
                    lengths[currentPosition] = lengths[currentPosition - 1] 
                        + GetDistanceBetween(checkpoints[positions[currentPosition - 1]], checkpoints[i]);
                    if (lengths[currentPosition] < MinLength)
                    {
                        positions[currentPosition] = i;
                        MakeTrivialPermutation(checkpoints, positions, currentPosition + 1, lengths, ref bestOrder);
                    }
                }
            }
        }

        public static double GetDistanceBetween(Point a, Point b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
