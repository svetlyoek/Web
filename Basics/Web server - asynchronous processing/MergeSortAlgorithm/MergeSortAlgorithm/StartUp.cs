namespace MergeSortAlgorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<int> unsortedNumbers = new List<int>();
            List<int> sortedNumbers;
            Random random = new Random();

            Console.WriteLine("Unsorted numbers:");

            for (int i = 0; i < 10; i++)
            {
                unsortedNumbers.Add(random.Next(10, 100));
                Console.Write(unsortedNumbers[i] + " ");
            }
            Console.WriteLine();

            sortedNumbers = MergeSort(unsortedNumbers);

            Console.WriteLine("Sorted numbers:");

            foreach (var sorted in sortedNumbers)
            {
                Console.Write(sorted + " ");
            }
            Console.WriteLine();
        }

        private static List<int> MergeSort(List<int> unsortedNumbers)
        {
            if (unsortedNumbers.Count <= 1)
            {
                return unsortedNumbers;
            }

            List<int> leftHalf = new List<int>();
            List<int> rightHalf = new List<int>();

            int middle = unsortedNumbers.Count / 2;

            for (int i = 0; i < middle; i++)
            {
                leftHalf.Add(unsortedNumbers[i]);
            }
            for (int j = middle; j <= unsortedNumbers.Count - 1; j++)
            {
                rightHalf.Add(unsortedNumbers[j]);
            }

            leftHalf = MergeSort(leftHalf);
            rightHalf = MergeSort(rightHalf);

            return MergeAsync(leftHalf, rightHalf);
        }

        private static List<int> MergeAsync(List<int> leftHalf, List<int> rightHalf)
        {
            List<int> result = new List<int>();

            while (leftHalf.Any() || rightHalf.Any())
            {
                if (leftHalf.Any() && rightHalf.Any())
                {
                    if (leftHalf.First() <= rightHalf.First())
                    {
                        result.Add(leftHalf.First());
                        leftHalf.Remove(leftHalf.First());
                    }
                    else
                    {
                        result.Add(rightHalf.First());
                        rightHalf.Remove(rightHalf.First());
                    }
                }
                else if (leftHalf.Any())
                {
                    result.Add(leftHalf.First());
                    leftHalf.Remove(leftHalf.First());
                }
                else if (rightHalf.Any())
                {
                    result.Add(rightHalf.First());
                    rightHalf.Remove(rightHalf.First());
                }
            }
            return result;
        }
    }
}

