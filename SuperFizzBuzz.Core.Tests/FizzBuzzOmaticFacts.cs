using System;
using System.IO;
using System.Linq;
using Xunit;

namespace SuperFizzBuzz.Core.Tests
{
    public class FizzBuzzOmaticFacts
    {
        /// <summary>
        /// Baseline test to make sure the constructor works.
        /// </summary>
        [Fact]
        public void can_create()
        {
            var fizzer = new FizzBuzzOmatic();
        }

        /// <summary>
        /// Regular fizzbuzz output is typically described as: Write a program that prints the numbers from 1 to 100.
        /// But for multiples of 3, print "Fizz" instead of the number.
        /// For multiples of 5, print "Buzz".
        /// For Multiples of 3 and 5, print "FizzBuzz".         
        /// </summary>
        [Fact]
        public void can_solve_classic_fizzbuzz()
        {
            var fizzer = new FizzBuzzOmatic();
            fizzer.Add(3, "Fizz");
            fizzer.Add(5, "Buzz");

            var result = fizzer.ProduceRange(1, 100);

            var expected = FizzBuzzCsvFile.Read<FizzBuzzCsvFile.FizzBuzzDataRecord>(Path.Combine("Data", "FizzBuzzData.csv"));

            Assert.Equal(expected.ToArray(), result.ToArray());
        }

        /// <summary>
        /// Test the classic FizzBuzz but run indices backwards.
        /// </summary>
        [Fact]
        public void can_solve_classic_fizzbuzz_backwards()
        {

            var fizzer = new FizzBuzzOmatic();
            fizzer.Add(3, "Fizz");
            fizzer.Add(5, "Buzz");

            var result = fizzer.ProduceRange(100, 1);

            var expected = FizzBuzzCsvFile.Read<FizzBuzzCsvFile.FizzBuzzDataRecord>(Path.Combine("Data", "FizzBuzzData.csv")).Reverse();

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Print the numbers from -12 through 145.
        /// For multiples of 3, print "Fizz"
        /// For Multiples of 7, print "Buzz"
        /// For Multiples of 38, print "Bazz"
        /// Print the appropriate combination of tokens for any number that matches more than one of those rules.
        /// </summary>
        [Fact]
        public void can_solve_super_fizzbuzz()
        {
            var fizzer = new FizzBuzzOmatic();
            fizzer.Add(3, "Fizz");
            fizzer.Add(7, "Buzz");
            fizzer.Add(38, "Bazz");

            var result = fizzer.ProduceRange(-12, 145);

            var expected = FizzBuzzCsvFile.Read<FizzBuzzCsvFile.SuperFizzBizzDataRecord>(Path.Combine("Data", "SuperFizzBuzzData.csv"));

            Assert.Equal(expected.ToArray(), result.ToArray());
        }

        /// <summary>
        /// Configurations can be provided in an alternative order, so the messages should be printed in that priority order.
        /// Can generate tokens other than "Fizz" and "Buzz" and can evaluate division by numbers other than 3 and 5.
        /// Maybe a user wants to test division by 4, 13, and 9, and output "Frog", "Duck", and "Chicken" for them
        /// (e.g., in this case, 52 would ouput "FrogDuck", 36 would output "FrogChicken", 468 would output "FrogDuckChicken", etc.)
        /// </summary>
        [Fact]
        public void can_handle_unsorted_messages()
        {
            var fizzer = new FizzBuzzOmatic();
            fizzer.Add(4, "Frog");
            fizzer.Add(13, "Duck");
            fizzer.Add(9, "Chicken");

            var resultFrogDuckChicken = fizzer.ProduceRange(468, 468);
            var resultFrogDuck = fizzer.ProduceRange(52, 52);
            var resultFrogChicken = fizzer.ProduceRange(36, 36);
            var resultDuckChicken = fizzer.ProduceRange(13*9, 13*9);

            Assert.Equal("FrogDuckChicken", resultFrogDuckChicken.FirstOrDefault());
            Assert.Equal("FrogDuck", resultFrogDuck.FirstOrDefault());
            Assert.Equal("FrogChicken", resultFrogChicken.FirstOrDefault());
            Assert.Equal("DuckChicken", resultDuckChicken.FirstOrDefault());
        }

        /// <summary>
        /// Can produce output for a user supplied set of integers, even if they’re not sequential.
        /// </summary>
        [Fact]
        public void can_handle_arbitrary_indices()
        {
            var fizzer = new FizzBuzzOmatic();
            fizzer.Add(3, "Fizz")
                .Add(5, "Buzz");

            var results = fizzer.ProduceSequence(3,5,15,1,2);

            var expected = new[]
            {
                "Fizz", "Buzz", "FizzBuzz", "1", "2"
            };

            Assert.Equal(expected, results);
        }

        /// <summary>
        /// Can produce output for a user supplied set of integers, even if they’re not sequential.
        /// </summary>
        [Fact]
        public void can_handle_enumerable_sequence()
        {
            var fizzer = new FizzBuzzOmatic();
            fizzer.Add(3, "Fizz")
                .Add(5, "Buzz");

            var results = fizzer.ProduceSequence(Enumerable.Range(1, 5));

            var expected = new[]
            {
                "1", "2", "Fizz", "4", "Buzz"
            };

            Assert.Equal(expected, results);
        }

        /// <summary>
        /// Can override the multiples in the configuration and only produce the latest message for each multiple.
        /// </summary>
        [Fact]
        public void can_override_multiples()
        {
            var fizzer = new FizzBuzzOmatic();
            fizzer.Add(7, "Fizz")
                .Add(12, "Buzz")
                .Add(3, "Fizz")
                .Add(5, "Buzz");

            var results = fizzer.ProduceSequence(Enumerable.Range(1, 5));

            var expected = new[]
            {
                "1", "2", "Fizz", "4", "Buzz"
            };

            Assert.Equal(expected, results);
        }
    }
}
