using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent
{
    class Day25
    {
        static class R
        {
            static object o;
            public static void Set(object result)
            {
                o = result;
            }
            public static void Out(object result = null)
            {
                object output = o;
                if (result != null)
                {
                    output = result;
                }
                Console.WriteLine(DateTime.Now + " Result -> " + output.ToString());
            }
            public static List<string> ReadAllLines(string from = @"in.txt")
            {
                string dataIn = System.IO.File.ReadAllText(from);
                var lines = dataIn.Split('\n');
                var ret = new List<string>();
                foreach (var line in lines)
                {
                    ret.Add(line.Trim());
                }
                return ret;
            }
            public static int ToInt(string s)
            {
                return Convert.ToInt32(s);
            }
            public static ulong ToULong(string s)
            {
                return Convert.ToUInt64(s);
            }
        }

        public static Dictionary<(ulong, ulong), ulong> mem = new Dictionary<(ulong, ulong), ulong>();

        public ulong TransformSubjectNumber(ulong loopSize, ulong subjectNumber)
        {
            ulong val = 1;
            ulong start = 0;

            if (mem.ContainsKey((loopSize - 1, subjectNumber)))
            {
                start = loopSize - 1;
                val = mem[(loopSize - 1, subjectNumber)];
            }

            for (ulong i = start; i < loopSize; i++)
            {
                val = val * subjectNumber;
                val = val % 20201227;
            }

            if (!mem.ContainsKey((loopSize, subjectNumber)))
            {
                mem.Add((loopSize, subjectNumber), val);
            }

            return val;
        }

        public (ulong, ulong) CryptographicHandshake(ulong cardLoopSize, ulong doorLoopSize)
        {

            var cardsPublicKey = TransformSubjectNumber(cardLoopSize, 7);
            var doorsPublicKey = TransformSubjectNumber(doorLoopSize, 7);

            var encryptionKey1 = TransformSubjectNumber(cardLoopSize, doorsPublicKey);
            var encryptionKey2 = TransformSubjectNumber(doorLoopSize, cardsPublicKey);

            return (encryptionKey1, encryptionKey2);
        }

        public void Run()
        {

            // 15335876
            // 15086442

            ulong cardsPublicKey = 15335876;
            ulong doorsPublicKey = 15086442;

            ulong cardLoopSize = 0;
            ulong doorLoopSize = 0;

            Console.WriteLine("Card public key: " + cardsPublicKey);
            Console.WriteLine("Door public key: " + doorsPublicKey);

            for (cardLoopSize = 0; cardLoopSize < ulong.MaxValue; cardLoopSize++)
            {
                if (TransformSubjectNumber(cardLoopSize, 7) == cardsPublicKey)
                {
                    break;
                }
            }

            Console.WriteLine("Card loop size: " + cardLoopSize);

            for (doorLoopSize = 0; doorLoopSize < ulong.MaxValue; doorLoopSize++)
            {
                if (TransformSubjectNumber(doorLoopSize, 7) == doorsPublicKey)
                {
                    break;
                }
            }

            Console.WriteLine("Door loop size: " + doorLoopSize);

            var encryptionKeys = CryptographicHandshake(cardLoopSize, doorLoopSize);
            
            Console.WriteLine("Encryption key 1: " + encryptionKeys.Item1);
            Console.WriteLine("Encryption key 2: " + encryptionKeys.Item2);

            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}