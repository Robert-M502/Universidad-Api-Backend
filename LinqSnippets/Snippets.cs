using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinQ ()
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
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9};
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
                    (element, secondElement) => new { element, secondElement}
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

        // TODO:

        // Variables

        // ZIP

        // Repeat

        // ALL

        // Aggregate

        // Disctinct

        // GroupBy
    }
}