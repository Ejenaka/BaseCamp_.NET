using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCampTask1
{
    class Tasks
    {
        // 2) Дано число, проверить какого оно типа: нечётное, чётное
        public bool IsEven(int number) => number % 2 == 0;

        // 3) Дано 3 числа, найти число которое находится между A < B < C
        public int FindBetween(int a, int b, int c)
        {
            int[] nums = { a, b, c };
            int max = nums[0];
            int min = nums[0];

            foreach (int num in nums)
            {
                if (num < min)
                    min = num;

                if (num > max)
                    max = num;
            }

            if (a != min && a != max)
            {
                return a;
            }

            if (b != min && b != max)
            {
                return b;
            }

            else
            {
                return c;
            }
        }

        // 4) Дан произвольный массив чисел, найти уникальные числа в нём. Использовать только циклы, условные операторы.
        public IEnumerable<int> GetUniqueNumbers(int[] nums)
        {
            var uniqueNumbers = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int numberCount = 0;
                for (int j = 0; j < nums.Length; j++)
                {
                    if (nums[j] == nums[i])
                        numberCount++;
                }
                
                if (numberCount == 1)
                {
                    uniqueNumbers.Add(nums[i]);
                }
            }

            return uniqueNumbers.ToArray();
        }

        // Доп. LINQ реализация 4 задания
        public IEnumerable<int> GetUniqueNumbersLINQ(int[] nums) => nums.Distinct();


        // 5) Написать метод который сможет транспонировать матрицу.
        public int[,] TransposeMatrix(int[,] matrix)
        {
            int[,] transposed = new int[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    transposed[j, i] = matrix[i, j];
                }
            }

            return transposed;
        }

        public void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // 6) Написать метод который будет округлять число до N символов
        public double RoundToDigits(double num, int digits) => Math.Round(num, digits);

        // 7) Найти y ():
        // Y = 100 * tg(x) * √x / x
        // Y = 2 * sin(x) - 4
        public double Y1(int x) => 100 * Math.Tan(x) * Math.Sqrt(x) / x;
        public double Y2(int x) => 2 * Math.Sin(x) - 4;

        // 8) Дано произвольную строку, найти строку между указаным символом. Выводить только первое совпадение.
        public string FindString(string str, char delimetr)
        {
            int firstIndex = str.IndexOf(delimetr);
            int lastIndex = str.IndexOf(delimetr, firstIndex + 1);

            var stringBuilder = new StringBuilder();

            for (int i = firstIndex + 1; i < lastIndex; i++)
            {
                stringBuilder.Append(str[i]);
            }

            return stringBuilder.ToString();
        }

        // Доп. Реализация LINQ 8 задания
        public string FindStringLINQ(string str, char delimetr)
        {
            char[] result = str.SkipWhile(c => c == delimetr).TakeWhile(c => c == delimetr).ToArray();

            return new string(result);
        }

        // 9) Найти слово в произвольной строке и вывести индексы границ этого слова в строке.
        public int[] GetWordBounds(string str, string word)
        {
            int firstIndex = str.IndexOf(" " + word) + 1;
            int lastIndex = str.IndexOf(" ", firstIndex) - 1;

            return new[] { firstIndex, lastIndex };
        }

        // ДОП. Дан произвольный массив чисел, найти числа которые повторяются более 2-х раз. Использовать только циклы, условные операторы.
        public IEnumerable<int> GetNumbersRepeatsTwoTimes(int[] nums)
        {
            var result = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int numberCount = 0;
                for (int j = 0; j < nums.Length; j++)
                {
                    if (nums[j] == nums[i])
                        numberCount++;
                }

                if (numberCount > 2)
                {
                    if (!result.Contains(nums[i]))
                    {
                        result.Add(nums[i]);
                    }
                }
            }

            return result.ToArray();
        }

        // ДОП. Найти количество вхождений элементов в массиве
        public Dictionary<int, int> GetNumbersStatistics(int[] nums)
        {
            var result = new Dictionary<int, int>();

            Array.Sort(nums);

            for (int i = 0; i < nums.Length;)
            {
                int currentNum = nums[i];
                int numCount = 0;

                for (int j = i; j < nums.Length; j++)
                {
                    if (currentNum == nums[j])
                    {
                        numCount++;
                    }
                }

                result.Add(currentNum, numCount);

                i += numCount;
            }

            return result;
        }

    }
}
