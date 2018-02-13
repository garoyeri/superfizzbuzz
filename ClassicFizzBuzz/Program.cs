using SuperFizzBuzz.Core;
using System;

namespace ClassicFizzBuzz
{
    static class Program
    {
        static void Main(string[] args)
        {
            var fizz = new FizzBuzzOmatic();
            fizz.Add(3, "Fizz")
                .Add(5, "Buzz");

            var results = fizz.ProduceRange(1, 100);

            Console.WriteLine("ClassicFizzBuzz: from 1 to 100");
            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
        }
    }
}
