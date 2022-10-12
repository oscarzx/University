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


        //Paging with skip & take
        public static IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        //Variables
        public static void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()  //let sirve para hacer variables locales de la propia consulta
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;

            Console.WriteLine($"Average: {numbers.Average()}");

            foreach (int number in aboveAverage)
            {
                Console.WriteLine($"Query: {number} Square: {Math.Pow(number, 2)}");
            }
        }

        //ZIP
        public static void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word);
            // { "1=one", "2=two", "3=three" }

        }


        //Repeat & Range
        public static void RepeatRangeLinq()
        {
            // Generate collection from 1 - 1000 --> RANGE
            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);

            // Repeat a value N times
            IEnumerable<string> fiveXs = Enumerable.Repeat("x", 5); //{ "X", "X", "X", "X", "X" }
        }

        public static void StudentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id=1,
                    Name="Martín",
                    Grade=90,
                    Certified=true
                },
                new Student
                {
                    Id=2,
                    Name="Juan",
                    Grade=50,
                    Certified=false
                },
                new Student
                {
                    Id=3,
                    Name="Ana",
                    Grade=96,
                    Certified=true
                },
                new Student
                {
                    Id=4,
                    Name="Álvaro",
                    Grade=10,
                    Certified=false
                },
                new Student
                {
                    Id=5,
                    Name="Pedro",
                    Grade=50,
                    Certified=true
                }
            };

            var cartifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudent = from student in classRoom
                                      where student.Certified == false
                                      select student;

            var appovedStudentsNames = from student in classRoom
                                  where student.Grade >= 50 && student.Certified == true
                                  select student.Name;
        }

        
        // ALL
        public static void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };
            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true
            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // false

            var emptyList = new List<int>();
            bool allNumbersAreGreaterThan0 = numbers.All(x => x > 10); // true
        }

        // Aggregate
        public static void AggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Sum all numbers
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);

            // 0, 1 => 1
            // 1, 2 => 3
            // 3, 4 => 7
            // etc...

            string[] words = { "Hello,", "My", "name", "is", "Oscar" };
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);

            // "", "Hello," => Hello,
            // "Hello,", "My" => Hello, My
            // "Hello, My", "name" => Hello, My name
        }

        //Disctinct
        public static void DistinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> duistincValues = numbers.Distinct();
        }

        // GroupBy
        public static void GroupByExamples()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Obtain only even numbers and generate two groups
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            // We will have two groups:
            // 1. The group that doesnt fit the condition (odd numbers)
            // 2. The group that fits the condition (even numbers)

            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value);// 1,3,5,7,9.....2,4,6,8,10(first the odds then the even)
                }
            }

            //Another example
            var classRoom = new[]
            {
                new Student
                {
                    Id=1,
                    Name="Martín",
                    Grade=90,
                    Certified=true
                },
                new Student
                {
                    Id=2,
                    Name="Juan",
                    Grade=50,
                    Certified=false
                },
                new Student
                {
                    Id=3,
                    Name="Ana",
                    Grade=96,
                    Certified=true
                },
                new Student
                {
                    Id=4,
                    Name="Álvaro",
                    Grade=10,
                    Certified=false
                },
                new Student
                {
                    Id=5,
                    Name="Pedro",
                    Grade=50,
                    Certified=true
                }
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified && student.Grade > 50);

            //We obtain two groups
            // 1. NOt certified students
            // 2. Certified Students

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("------------ {0} ---------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }

        public static void RelationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My other content"
                        }
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My other comment",
                            Content = "My new content"
                        },
                        new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My other new comment",
                            Content = "My new content"
                        }
                    }
                }
            };

            var commentsContent = posts.SelectMany(
                post => post.Comments, (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });
        }
    }
}