using System;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            int yesCounter = 1;
            int noCounter = 2;

            int total = yesCounter + noCounter;

            var yesPercent = yesCounter / total * 100.0;
            var noPercent = noCounter / total * 100.0;

            Console.WriteLine($"Yes Counts: {yesCounter}, Percentage: {yesPercent}%");
            Console.WriteLine($"No Counts: {noCounter}, Percentage: {noPercent}%");

            if (yesCounter > noCounter)
            {
                Console.WriteLine($"Yes Won");
            }
            else if (noCounter > yesCounter)
            {
                Console.WriteLine($"No Won");
            }
            else
            {
                Console.WriteLine($"Draw");
            }
        }
    }
}