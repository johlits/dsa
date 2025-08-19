using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day22
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

        public (Queue<int>, Queue<int>, int) Game(Queue<int> p1, Queue<int> p2)
        {
            /*
Before either player deals a card, if there was a previous round in this game that had exactly the same cards in the same order in the same players' decks, the game instantly ends in a win for player 1. Previous rounds from other games are not considered. (This prevents infinite games of Recursive Combat, which everyone agrees is a bad idea.)
Otherwise, this round's cards must be in a new configuration; the players begin the round by each drawing the top card of their deck as normal.
If both players have at least as many cards remaining in their deck as the value of the card they just drew, the winner of the round is determined by playing a new game of Recursive Combat (see below).
Otherwise, at least one player must not have enough cards left in their deck to recurse; the winner of the round is the player with the higher-value card.
             */
            var history = new List<string>();
            var round = 1;

            while (p1.Count > 0 && p2.Count > 0)
            {
                var s = "";
                var remaining1 = 0;
                var remaining2 = 0;
                foreach (int value in p1)
                {
                    s += value + ",";
                    remaining1++;
                }
                s += ".";
                foreach (int value in p2)
                {
                    s += value + ",";
                    remaining2++;
                }
                if (history.Contains(s))
                {
                    return (p1, p2, 1);
                }
                else {
                    history.Add(s);
                }

                var c1 = p1.Dequeue();
                var c2 = p2.Dequeue();
                var subwinner = 0;
                if (p1.Count >= c1 && p2.Count >= c2)
                {
                    var sub1 = new Queue<int>();
                    var sub2 = new Queue<int>();
                    var i = c1;
                    foreach (var s1 in p1)
                    {
                        sub1.Enqueue(s1);
                        i--;
                        if (i == 0)
                            break;
                    }
                    i = c2;
                    foreach (var s2 in p2)
                    {
                        sub2.Enqueue(s2);
                        i--;
                        if (i == 0)
                            break;
                    }

                    var sub = Game(new Queue<int>(sub1), new Queue<int>(sub2));
                    Console.WriteLine("BACK TO PREVIOUS");
                    if (sub.Item3 != 0)
                    {
                        //return (p1, p2, sub.Item3);
                        subwinner = sub.Item3;
                    }
                }

                if (subwinner == 0)
                {
                    if (c1 > c2)
                    {
                        subwinner = 1;
                    }
                    else
                    {
                        subwinner = 2;
                    }
                }

                if (subwinner == 1)
                {
                    Console.WriteLine("p1 wins round " + round);
                    foreach (var val in p1) Console.Write(val + " ");
                    Console.WriteLine();
                    foreach (var val in p2) Console.Write(val + " ");
                    Console.WriteLine();
                    p1.Enqueue(c1);
                    p1.Enqueue(c2);
                }
                else if (subwinner == 2)
                {
                    Console.WriteLine("p2 wins round " + round);
                    foreach (var val in p1) Console.Write(val + " ");
                    Console.WriteLine();
                    foreach (var val in p2) Console.Write(val + " ");
                    Console.WriteLine();
                    p2.Enqueue(c2);
                    p2.Enqueue(c1);
                }
                else
                {
                    throw new Exception();
                }

                round++;

            }

            if (p1.Count == 0)
            {
                return (p1, p2, 2);
            }
            if (p2.Count == 0)
            {
                return (p1, p2, 1);
            }
            return (p1, p2, 0);
        }


        public void Run()
        {
            var lines = R.ReadAllLines();
            var player = 0;

            var p1 = new Queue<int>();
            var p2 = new Queue<int>();

            foreach(var line in lines)
            {
                if(line.EndsWith(":"))
                {
                    player++;
                }
                else if (line == "")
                {

                }
                else
                {
                    if (player == 1)
                    {
                        p1.Enqueue(R.ToInt(line));
                    }
                    if (player == 2)
                    {
                        p2.Enqueue(R.ToInt(line));
                    }
                }
            }

            

            var result = Game(new Queue<int>(p1), new Queue<int>(p2));
            p1 = result.Item1;
            p2 = result.Item2;
            var winner = result.Item3;

            ulong tot = 0;

            if (winner == 1)
            {
                var num = p1.Count();
                while (p1.Count > 0)
                {
                    var temp = p1.Dequeue();
                    tot += (ulong)(temp * num);
                    num--;
                }
                Console.WriteLine(tot);
            }
            else
            {
                var num = p2.Count();
                while (p2.Count > 0)
                {
                    var temp = p2.Dequeue();
                    tot += (ulong)(temp * num);
                    num--;
                }
                Console.WriteLine(tot);
            }

            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}