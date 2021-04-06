using System;
using System.Security.Cryptography;

namespace RPSgame
{
    class Program
    {
        static int Main(string[] arg)
        {
            if (arg.Length % 2 == 0 || arg.Length == 1)
            {
                Console.WriteLine("Need to enter ood number >= 3");
                return 0;
            }
            Random r = new Random();
            int compChoice = r.Next(1, arg.Length + 1);
            byte[] key = new byte[16];
            byte[] bytesComp = BitConverter.GetBytes(compChoice);
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(key);
            rng.GetBytes(bytesComp);
            var hash = new HMACSHA256(key);
            Console.WriteLine("HMAC: " + Convert.ToBase64String(hash.ComputeHash(key)) + Convert.ToBase64String(hash.ComputeHash(bytesComp)));
            int manChoice;
            while (true)
            {
                Console.WriteLine("Available moves:");
                int counter = 1;
                foreach (var a in arg)
                {
                    Console.WriteLine(counter + " " + a);
                    counter++;
                }

                Console.WriteLine("Choose yours: ");
                manChoice = Convert.ToInt32(Console.ReadLine());
                if (manChoice > arg.Length || manChoice < 0)
                {
                    Console.WriteLine($"Enter number from 1 to {arg.Length}");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Your move: " + arg[manChoice - 1]);
            Console.WriteLine("Computer move:" + arg[compChoice - 1]);
            if (manChoice + arg.Length / 2 > arg.Length)
            {
                if (manChoice == compChoice)
                {
                    Console.WriteLine("Draw!");
                }
                else if (manChoice - 1 - arg.Length / 2 <= compChoice - 1 && compChoice < manChoice)
                {
                    Console.WriteLine("Computer won!");
                }
                else
                {
                    Console.WriteLine("You won!");
                }
            }
            else
            {
                if (manChoice == compChoice)
                {
                    Console.WriteLine("Draw!");
                }
                else if (manChoice - 1 + arg.Length / 2 >= compChoice - 1 && compChoice > manChoice)
                {
                    Console.WriteLine("You won!");
                }
                else
                {
                    Console.WriteLine("Computer won!");
                }
            }
            Console.WriteLine("HMAC key: " + Convert.ToBase64String(key));

            return 0;
        }

    }
}
