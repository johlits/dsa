using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day13
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

        public void Run()
        {
            var lines = R.ReadAllLines();
            var p = new Dictionary<ulong, ulong>();
            ulong timestamp = 0;
            ulong largestBus = 0;
            ulong largestT = 0;
            ulong firstBus = 0;
            bool first = true;
            
            foreach (var val in lines[1].Split(','))
            {
                if (val != "x")
                {
                    if (first)
                    {
                        firstBus = R.ToULong(val);
                        first = false;
                    }

                        p.Add(timestamp, R.ToULong(val));
                        R.Out("Adding " + timestamp + " " + val);
                        


                    if (R.ToULong(val) > largestBus)
                    {
                        largestBus = R.ToULong(val);
                        largestT = timestamp;
                    }
                }
                timestamp++;
            }

            R.Out("LARGEST " + largestBus + " " + largestT);
            //LARGEST 503 54
            //LARGEST 59 4


            ulong time = 261286278953586;// 26970356359627;// 4904020578;// 2461;
            ulong ti;
            bool ok;
            ulong lasttime = 0;

            while (time < 1000000000000000)
            {
                ti = time;


                    //Console.WriteLine(ti);

                    ok = true;
                    foreach (var bus in p)
                    {
                        
                        if ((ti + bus.Key) % bus.Value != 0)
                        {
                            ok = false;
                            break;
                        }
                        else if (bus.Value == 29)
                    {
                        
                            
                        Console.WriteLine(time + ": " + (time - lasttime));
                        lasttime = time;
                        
                            

                    }
                    }
                    if (ok)
                    {
                        R.Out(ti);
                        return;
                    }


                time += 1516548631;// largestBus;// 2361684091;// 181668007;// 474329;// 11569;

                //R.Out(time);
                /*if (time % 503000000 == 0)
                {
                    R.Out(time);
                }*/
            }

            R.Out("END");
            Console.ReadLine();

            // 350


            /*
            ulong t = 0;
            while (true)
            {
                if (t % 10000000 == 0)
                {
                    R.Out(t);
                }

                var ok = true;
                foreach (var bus in p)
                {
                    if ((t + bus.Key) % bus.Value != 0)
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    R.Out(t);
                    break;
                }
                t += ti;
            }
            */


        }
    }
}