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
        private double _classAExcellent = 10;
        private double _classAGood = 14;
        private double _classANormal = 4;

        private double _classBExcellent = 8;
        private double _classBGood = 12;
        private double _classBNormal = 8;

        private readonly double[] _studentsFromA;
        private readonly double[] _studentsFromB;

        public Task1()
        {
            _studentsFromA = new[] {_classAExcellent, _classAGood, _classANormal};
            _studentsFromB = new[] {_classBExcellent, _classBGood, _classBNormal};
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
                _classAExcellent + _classBExcellent, 
                _classAGood + _classBGood, 
                _classANormal + _classBNormal 
            };
        }

        public void PrintStudentsRatioByClasses()
        {
            double studentsASum = _studentsFromA.Sum();
            double studentsBSum = _studentsFromB.Sum();

            double ratioAExcellent = _classAExcellent / studentsASum * 100;
            double ratioAGood = _classAGood / studentsASum * 100;
            double ratioANormal = _classANormal / studentsASum * 100;

            double ratioBExcellent = _classBExcellent / studentsBSum * 100;
            double ratioBGood = _classBGood / studentsBSum * 100;
            double ratioBNormal = _classBNormal / studentsBSum * 100;

            Console.WriteLine($"Students A: excellent: {ratioAExcellent}%, good: {ratioAGood}%, normal: {ratioANormal}%");
            Console.WriteLine($"Students B: excellent: {ratioBExcellent}%, good: {ratioBGood}%, normal: {ratioBNormal}%");
        }

        public void PrintStudentsRatio()
        {
            double studentsASum = _studentsFromA.Sum();
            double studentsBSum = _studentsFromB.Sum();
            double studentsSum = studentsASum + studentsBSum;

            double ratioExcellent = (_classAExcellent + _classBExcellent) / studentsSum * 100;
            double ratioGood = (_classAGood + _classBGood) / studentsSum * 100;
            double ratioNormal = (_classANormal + _classBNormal) / studentsSum * 100;

            Console.WriteLine($"All studetns: excellent: {ratioExcellent}%, good: {ratioGood}%, normal: {ratioNormal}%");
        }
    }
}
