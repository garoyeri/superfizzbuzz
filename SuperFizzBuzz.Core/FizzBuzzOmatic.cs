using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperFizzBuzz.Core
{
    /// <summary>
    /// Fizz Buzz machine to generate arbitrary FizzBuzz sequences.
    /// </summary>
    public class FizzBuzzOmatic
    {
        List<ConfigurationItem> _configuration = new List<ConfigurationItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FizzBuzzOmatic"/> class.
        /// </summary>
        public FizzBuzzOmatic()
        {
        }

        /// <summary>
        /// Add an index multiple and the desired message to display when it is encountered.
        /// </summary>
        /// <param name="multiple">The index multiple to handle.</param>
        /// <param name="message">The message to be displayed, cannot be null.</param>
        /// <returns>This.</returns>
        /// <exception cref="ArgumentNullException">Thrown if message is null.</exception>
        /// <remarks>
        /// If the same multiple is specificed, only the latest multiple will be used.
        /// </remarks>
        public FizzBuzzOmatic Add(int multiple, string message)
        {
            if (message == null) throw new ArgumentNullException("message", "Message cannot be null");

            _configuration.RemoveAll(c => c.Multiple == multiple);
            _configuration.Add(new ConfigurationItem(multiple, message));

            return this;
        }

        /// <summary>
        /// Product a sequence of FizzBuzz messages based on the configuration.
        /// </summary>
        /// <param name="start">The starting index.</param>
        /// <param name="end">The ending index.</param>
        /// <returns>The correct FizzBuzz sequence.</returns>
        public IEnumerable<string> ProduceRange(int start, int end)
        {
            var index = start;

            var count = Math.Abs((start < end) ? (end - start + 1) : (start - end + 1));
            var direction = (start < end) ? 1 : -1;

            return ProduceSequence(Enumerable.Range(0, count).Select(n => start + n * direction));
        }

        /// <summary>
        /// Produce a sequence of FizzBuzz messages based on the configuration.
        /// </summary>
        /// <param name="indices">The specific sequence of indices to use.</param>
        /// <returns>The correct FizzBuzz sequence.</returns>
        public IEnumerable<string> ProduceSequence(params int[] indices)
        {
            return ProduceSequence(indices.AsEnumerable());
        }

        /// <summary>
        /// Produce a sequence of FizzBuzz messages based on the configuration.
        /// </summary>
        /// <param name="indices">The specific sequence of indices to use.</param>
        /// <returns>The correct FizzBuzz sequence.</returns>
        public IEnumerable<string> ProduceSequence(IEnumerable<int> indices)
        {
            var configSnapshot = _configuration.ToArray();

            return indices.Select(index =>
            {
                var messages = configSnapshot.Select(c => c.Process(index)).Where(m => m != null);
                if (messages.Any())
                {
                    return string.Join("", messages);
                }
                else
                {
                    return index.ToString();
                }
            });
        }

        /// <summary>
        /// Internal configuration item for each FizzBuzz multiple.
        /// </summary>
        class ConfigurationItem
        {
            public ConfigurationItem(int multiple, string message)
            {
                this.Multiple = multiple;
                this.Message = message;
            }

            public int Multiple { get; private set; }
            public string Message { get; private set; }

            public string Process(int index)
            {
                if (index % this.Multiple == 0) return Message;

                return null;
            }
        }
    }
}
