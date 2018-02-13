using SuperFizzBuzz.Core;
using System;

namespace SuperFizzBuzz
{
    static class Program
    {
        static void Main(string[] args)
        {
            var fizz = new FizzBuzzOmatic();
            fizz.Add(3, "Fizz")
                .Add(7, "Buzz")
                .Add(38, "Bazz");

            var results = fizz.ProduceRange(-12, 145);

            Console.WriteLine("SuperFizzBuzz: from -12 to 145");
            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
        }
    }
}
