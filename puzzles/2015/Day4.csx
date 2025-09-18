// https://adventofcode.com/2015/day/4

using System.Security.Cryptography;
using System.Text;

const string PUZZLE_INPUT = "ckczppom";
using (var hashingAlgorithm = MD5.Create())
{
    for (int i = 0; i < int.MaxValue; i++)
    {
        string input = $"{PUZZLE_INPUT}{i}";
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = hashingAlgorithm.ComputeHash(inputBytes);
        string hash = Convert.ToHexString(hashBytes).ToLowerInvariant();
        if (hash.StartsWith("00000")) // Part 1: "00000", Part 2: "000000"
        {
            Console.WriteLine(i);
            break;
        }
    }
}

Console.WriteLine("Done.");