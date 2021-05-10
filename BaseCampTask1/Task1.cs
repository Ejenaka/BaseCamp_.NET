using System;
using System.Linq;

namespace BaseCampTask1
{
    // По результатам семестра в 1-А классе 10 отличником, 14 хорошистов, 4 троечника. 
    // В 1-Б - 8 отличников, 12 хорошистов, 5 троечников. 1-В - 12 отличников, 7 хорошистов, 8 троечников.
    // Найти: 
    // a) Сколько отличников, хорошистов и троечников на всей параллели классов
    // b) % соотношение отличников, хорошистов и троечников в классах
    // c) % соотношение отличников, хорошистов и троечников на парллели

    class Task1
    {
        private double classAExcellent = 10;
        private double classAGood = 14;
        private double classANormal = 4;

        private double classBExcellent = 8;
        private double classBGood = 12;
        private double classBNormal = 8;

        private readonly double[] studentsFromA;
        private readonly double[] studentsFromB;

        public Task1()
        {
            studentsFromA = new[] {classAExcellent, classAGood, classANormal};
            studentsFromB = new[] {classBExcellent, classBGood, classBNormal};
        }

        public void PrintStudentsInfo()
        {
            var students = GetStudentsByTypes();

            Console.WriteLine($"Excellent students: {students[0]}, good students: {students[1]}, normal students: {students[2]}");
        }

        private double[] GetStudentsByTypes() 
        {
            return new double[] 
            { 
                classAExcellent + classBExcellent, 
                classAGood + classBGood, 
                classANormal + classBNormal 
            };
        }

        public void PrintStudentsRatioByClasses()
        {
            double studentsASum = studentsFromA.Sum();
            double studentsBSum = studentsFromB.Sum();

            double ratioAExcellent = classAExcellent / studentsASum * 100;
            double ratioAGood = classAGood / studentsASum * 100;
            double ratioANormal = classANormal / studentsASum * 100;

            double ratioBExcellent = classBExcellent / studentsBSum * 100;
            double ratioBGood = classBGood / studentsBSum * 100;
            double ratioBNormal = classBNormal / studentsBSum * 100;

            Console.WriteLine($"Students A: excellent: {ratioAExcellent}%, good: {ratioAGood}%, normal: {ratioANormal}%");
            Console.WriteLine($"Students B: excellent: {ratioBExcellent}%, good: {ratioBGood}%, normal: {ratioBNormal}%");
        }

        public void PrintStudentsRatio()
        {
            double studentsASum = studentsFromA.Sum();
            double studentsBSum = studentsFromB.Sum();
            double studentsSum = studentsASum + studentsBSum;

            double ratioExcellent = (classAExcellent + classBExcellent) / studentsSum * 100;
            double ratioGood = (classAGood + classBGood) / studentsSum * 100;
            double ratioNormal = (classANormal + classBNormal) / studentsSum * 100;

            Console.WriteLine($"All studetns: excellent: {ratioExcellent}%, good: {ratioGood}%, normal: {ratioNormal}%");
        }
    }
}
