using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VM Golf",
                "VM Clifornia",
                "Audi A3",
                "Audi A5",
                "Failt Punto",
                "Seat Ibiza",
                "Seat león"
            };

            // 1. SELECT * OF cars (SELECT ALL CARS)
            var carList = from car in cars select car;
            foreach (var car in carList) {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE car is Audi (SELECT AUDIs)
            var audiList = from car in cars where car.Contains("Audi") select car;
            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }

        // Ejemplos con números
        static public void LinqNumbers() {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //Cada numero multiplicado por 3 
            // Obtenemos todos los números, menos el 9
            // Ordenamos de manera acendente

            var processeNumberList =
                numbers
                    .Select(num => num * 3) // {3, 6, 9, etc}
                    .Where(num => num != 9) // {menos el 9}
                    .OrderBy(num => num); // {lo ordenamos acendente}
        }

        static public void SearchExamples() {
            List<string> textList = new List<string> {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            // 1. Encontrar los primeros elementos 
            var first = textList.First();

            // 2. Econtrar el primer elemento que tenga la letra "c"
            var cText = textList.First(text => text.Equals("c"));

            // 3. Encontrar el primer elemento que contenga una letra "j"
            var jText = textList.First(text => text.Contains("j"));

            // 4. Encotrar el primer elemento que contenga la z o un valor por defecto
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z")); // "" o "el primer elementos que contengan la z"

            // 5. Encotrar el ultimo elemento que contenga la z o un valor por defecto
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z")); // "" o "el utimo elementos que contengan la z"

            // 6. Obtener un elemento unico
            var uniqueText = textList.Single();
            var uniqueDefaultText = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // 7. Compración entre listas {4, 8} los que no se repiten
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers); // {4, 8}
        }
        static public void MultipleSelect() {
            // SELECT MANY
            string[] myOpinions = {
                "Opinion 1, texto 1",
                "Opinion 2, texto 2",
                "Opinion 3, texto 3"
            };

            // Separa los elementos por la coma
            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[] {
                new Enterprise() {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new [] {
                        new Employee
                        {
                            Id = 1,
                            Name = "Martin",
                            Email = "martin@mail.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Pepe",
                            Email = "pep@mail.com",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Juan",
                            Email = "Juan@mail.com",
                            Salary = 2000
                        }
                    }
                },
                new Enterprise() {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new [] {
                        new Employee
                        {
                            Id = 4,
                            Name = "Ana",
                            Email = "ana@mail.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "Maria",
                            Email = "maria@mail.com",
                            Salary = 1500
                        },
                        new Employee
                        {
                            Id = 6,
                            Name = "Marta",
                            Email = "marta@mail.com",
                            Salary = 4000
                        }
                    }
                }
            };

            // Obtener todos los empleados de todas las empresas
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Saber si tenemos una lista vacia 
            bool hasEnterprises = enterprises.Any();
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // Todos empleados que reciben mas de 1000 en salario 
            bool hasEmployeeWithSalaryMoraThan1000 =
                enterprises.Any(enterprise =>
                    enterprise.Employees.Any(employee => employee.Salary >= 1000)
                );
        }
        static public void linqCollections() {
            var firstList = new List<string> { "a", "b", "c" };
            var secondList = new List<string> { "a", "c", "d" };

            // INNER JOIN
            var commonResult = from element in firstList
                               join seconElement in secondList
                               on element equals seconElement
                               select new { element, seconElement };
            // INNER JOIN
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
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
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
        static public void SkipTakeLinq() {
            var myList = new[] {
            1,2,3,4,5,6,7,8,9,10
            };
            // SKIP
            var skipTwoFirstValues = myList.Skip(2); // {3,4,5,6,7,8,9,10}
            var skipLastTwo = myList.SkipLast(2); // {1,2,3,4,5,6,7,8}
            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); // {4,5,6,7,8,9,10}
            // TAKE
            var takeFistTwoValues = myList.Take(2); // {1,2}
            var takeLastTwo = myList.TakeLast(2); // {9,10}
            var takeWhileSmallerThan4 = myList.SkipWhile(num => num < 4); // {1,2,3}
        }


        // Paging con Skip y Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage) {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        // Variables
        static public void linqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;
            Console.WriteLine("Avegare: {0}", numbers.Average());
            foreach (int number in aboveAverage) {
                Console.Write("Query: Number: {0} Square: {1}", number, Math.Pow(number, 2));
            }
        }

        // ZIP
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };
            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word);
            // {"1=one", "2=two",...}
        }

        // Repeat & range
        static public void repeatRangeLinq() {
            // Generar una coleccion de valores de 1 - 1000 --> Rango
            IEnumerable<int> first1000 = Enumerable.Range(0, 1000);
            // Repetir un valor N tiempos
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5);  // "X","X","X","X","X"

        }

        // 
        static public void studentsLinq() {
            var classRoom = new[] {
                new Student {
                    Id = 1,
                    Name = "Martín",
                    Grade = 90,
                    Certified = true
                },
                new Student {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false
                },
                new Student {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true
                },
                new Student {
                    Id = 4,
                    Name = "Alvaro",
                    Grade = 10,
                    Certified = false
                },
                new Student {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true
                },
                new Student {
                    Id = 6,
                    Name = "Robert",
                    Grade = 80,
                    Certified = true
                }
            };

            // Alumnos certificados en la clase
            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;
            // Alumnos no certificados
            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == false
                                       select student;
            // Alumnos con notas aprobados
            var approveStudents = from student in classRoom
                                  where student.Grade >= 50 && student.Certified == true
                                  select student.Name; // Solo los nombres de los estudiantes

                                  
        }

        // ALL
        static public void AllLinq() {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };
            bool allAreSmallerThan10 = numbers.All(x => x < 10); // True
            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // False

            var emptyList = new List<int>(); 
            bool allNumbersAreGreaterThan0 = numbers.All(x => x >= 0); // True
        }

        // Aggregate -> secuencia acumalitava de funciones
        static public void aggregateQueries() {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            // Sumar todos los numeros
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            // 0, 1 =>
            // 1, 2  => 3
            // 3, 4 => 7
            // etc.

            string[] words = { "hello, ", "my", "name", "is", "Rober" }; // hello, my name is Robert
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);
            // "", "hello, " => hello,
            // "hello, ", "my" => hello, my
            // "hello, my", "name" => hello, my name
            // etc.

        }

        // Disctinc
        static public void distinctValues() {
            int[] numbers = { 1, 2, 3, 4, 5, 4, 3, 2, 1 };
            // obtener los valores sin repeteción 
            IEnumerable<int> distinctValues = numbers.Distinct();
        }

        // GroupBy
        static public void groupByExamples() {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // Obtener solo los valores pares y agregarlos en un grupo
            var grouped = numbers.GroupBy(x => x % 2 == 0);
            // Vamos a tener dos grupos 
            // 1. El grupo que no cumple la condicion (números impares)
            // 2. El grupo que si cumple la condicion (números pares)
            foreach (var group in grouped) {
                foreach (var value in group) {
                    Console.WriteLine(value); // 1,3,5,7,9... 2,4,6,8 (primero los que no lo cumplen y luego los que lo cumplen)
                }
            }

            // Otro ejemplo
            var classRoom = new[] {
                new Student {
                    Id = 1,
                    Name = "Martín",
                    Grade = 90,
                    Certified = true
                },
                new Student {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false
                },
                new Student {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true
                },
                new Student {
                    Id = 4,
                    Name = "Alvaro",
                    Grade = 10,
                    Certified = false
                },
                new Student {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true
                },
                new Student {
                    Id = 6,
                    Name = "Robert",
                    Grade = 80,
                    Certified = true
                }
            };
            // Grupo de estudiantes que estan certificado
            var certifiedQuery = classRoom.GroupBy(student => student.Certified);
            // Obtenderemos dos grupos 
            // 1. No certificados
            // 2. Certificados
            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("--------{0}", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name); 
                }
            }
        }
        static public void relationsLinq() {
            List<Post> posts = new List<Post>() {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment> ()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My content "
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My other content "
                        }
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment> ()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My other comment",
                            Content = "My new content "
                        },
                        new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My other new comment",
                            Content = "My new content "
                        }
                    }
                }
            };
            
            var commentsContent = posts.SelectMany
                (post => post.Comments, 
                (post, comment) => new { PostId = post.Id, CommentContent = comment.Content});
        }
    }
}