using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnippets
{
    public class Snippets
    {
        public static void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            // 1. SELECT * of cars (SELECT ALL CARS)
            var carList = from car in cars select car;

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE car is Audi (SELECT AUDIs)
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }

        // Number Examples
        public static void LinQNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Each Number multiplied by 3
            // take all numbers, but 9
            // Order numbers by ascending value

            var processedNumberList = numbers
                .Select(num => num * 3) //{ 3, 6, 9, etc.}
                .Where(num => num != 9) // { all but the 9 }
                .OrderBy(num => num); //at the end, we order ascending
        }

        public static void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            // 1. Firts of all elements
            var firts = textList.First();

            // 2. Firts element that is "c"
            var cText = textList.First(text => text.Equals("c"));

            // 3. Firts element that contains "j"
            var jText = textList.First(text => text.Contains("j"));

            // 4. Firts element that contains "z" or default
            var firtsOrDefaultText = textList.FirstOrDefault(text => text.Contains("z"));//or element with "z"

            // 5. Last element that contains "z" or default
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z"));//or element with "z"

            // 6. Single Values
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherNumbers = { 0, 2, 6 };

            // Obtain {4, 8 }
            var myEvenNumbers = evenNumbers.Except(otherNumbers); // {4,8}
        }

        public static void MultipleSelects()
        {
            // SELECT MANY
            string[] myOpinions =
            {
                "Opinión 1, text 1",
                "Opinión 2, text 2",
                "Opinión 3, text 3"
            };

            var myOpinionSelection = myOpinions.SelectMany(o => o.Split(","));

            var enterprise = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Martín",
                            Email = "martin@imaginagroup.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Pepe",
                            Email = "pepe@imaginagroup.com",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Juanjo",
                            Email = "juanjo@imaginagroup.com",
                            Salary = 2000
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "Anna",
                            Email = "Anna@imaginagroup.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "María",
                            Email = "maria@imaginagroup.com",
                            Salary = 1500
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Marta",
                            Email = "marta@imaginagroup.com",
                            Salary = 4000
                        }
                    }
                }
            };

            // Obtain all Employees of all Enterprises
            var employeeList = enterprise.SelectMany(e => e.Employees);

            // Know if any list is empty
            bool hasEnterprise = enterprise.Any();

            bool hasEmployees = enterprise.Any(e => e.Employees.Any());

            // All enterprise at least has an employees with more than 1000 of salary
            bool employeeWithSalaryMoreThan1000 = 
                enterprise.Any(e => e.Employees.Any(employee => employee.Salary > 1000));
        }

        public static void LinqCollections()
        {
            var firstList = new List<string> { "a", "b", "c" };
            var secondList = new List<string> { "a", "b", "d" };

            // INNER JOIN
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element, secondElement) => new { element, secondElement }
                    );

            // OUTER JOIN - LEFT
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temmporalList
                                from temporalElement in temmporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };

            // OUTER JOIN - RIGHT
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            // UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        public static void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            //SKIP

            var skipTowFirstValues = myList.Skip(2); // {3,4,5,6,7,8,9,10}

            var skipTowLastValues = myList.SkipLast(2); //{1,2,3,4,5,6,7,8}

            var skipWhileSmallerTahn4 = myList.SkipWhile(num => num < 4); ////{4,5,6,7,8}

            //TAKE

            var takeFirstTowValues = myList.Take(2); //{1,2}

            var takeLastTowValues = myList.TakeLast(2); //{9,10}

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); //{1,2,3}
        }
    }
}