using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCampTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task1 task1 = new Task1();
            task1.PrintStudentsInfo();
            task1.PrintStudentsRatioByClasses();
            task1.PrintStudentsRatio();

            Tasks tasks = new Tasks();
            Console.WriteLine($"6 is even: {tasks.IsEven(6)}");
            Console.WriteLine($"7 is even: {tasks.IsEven(7)}");

            int res = tasks.FindBetween(34, 24, 12);
            Console.WriteLine(res);
            Console.WriteLine("=================");

            var uniques = tasks.GetUniqueNumbers(new[] { 1, 1, 2, 3, 4, 4, 2, 6, 9, 5});
            foreach (var i in uniques)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("=================");


            int[,] matrix = new[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            int[,] transposed = tasks.TransposeMatrix(matrix);
            tasks.PrintMatrix(matrix);
            Console.WriteLine("=================");
            tasks.PrintMatrix(transposed);
            Console.WriteLine("=================");

            double num = 0.9436;
            Console.WriteLine(tasks.RoundToDigits(num, 2));

            string foundStr = tasks.FindString("я-нехочу-делать-дз", '-');
            Console.WriteLine(foundStr);

            string str = "Lorem ipsum dolor sit amet";
            int[] bounds = tasks.GetWordBounds(str, "ipsum");
            Console.WriteLine($"{bounds[0]}-{bounds[1]}");
            Console.WriteLine("=================");

            var arr = tasks.GetNumbersRepeatsTwoTimes(new[] { 1, 1, 2, 3, 4, 4, 4, 2, 6, 9, 5, 5, 5 });
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();

            var numsStatistics = tasks.GetNumbersStatistics(new[] { 1, 1, 2, 3, 4, 4, 4, 2, 6, 9, 5, 5, 5 });
            foreach (var keyValue in numsStatistics)
            {
                Console.WriteLine($"{keyValue.Key}: {keyValue.Value}");
            }

            Console.ReadKey();
        }
    }
}
