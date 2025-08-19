using System;
using System.Collections.Generic;

namespace Advent
{
    class Day11
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

            var w = lines[0].Length;
            var h = lines.Count;

            var arr = new char[w, h];

            for (var i = 0; i < h; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    arr[j, i] = lines[i][j];
                    Console.Write(arr[j, i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("");
            Console.WriteLine("---");

            var simulate = true;
            while (simulate)
            {
                simulate = false;

                

                var newArr = new char[w, h];

                for (var i = 0; i < h; i++)
                {
                    for (var j = 0; j < w; j++)
                    {
                        newArr[j, i] = '.';
                    }
                }


                for (var i = 0; i < h; i++)
                {
                    for (var j = 0; j < w; j++)
                    {

                        var aCnt = 0;
                        var bCnt = 0;

                        // left
                        if (j == 0)
                        {

                        }
                        else if (arr[j - 1, i] != '#')
                        {
                            bCnt++;
                        }

                            var tmpj = j;
                            var tmpi = i;
                            while (tmpj > 0)
                            {
                                tmpj--;
                            if (arr[tmpj, tmpi] == 'L')
                            {
                                break;
                            }
                                if (arr[tmpj, tmpi] == '#')
                                {
                                    aCnt++;
                                    break;
                                }
                            }


                        // right
                        if (j == w - 1)
                        {

                        }
                        else if (arr[j +1, i] != '#')
                        {
                            bCnt++;
                        }

                             tmpj = j;
                             tmpi = i;
                            while (tmpj < w-1)
                            {
                                tmpj++;
                            if (arr[tmpj, tmpi] == 'L')
                            {
                                break;
                            }
                                if (arr[tmpj, tmpi] == '#')
                                {
                                    aCnt++;
                                    break;
                                }
                            }


                        // up
                        if (i == 0)
                        {

                        }
                        else if (arr[j, i-1] != '#')
                        {
                            bCnt++;
                        }

 
                             tmpj = j;
                             tmpi = i;
                            while (tmpi > 0)
                            {
                                tmpi--;
                            if (arr[tmpj, tmpi] == 'L')
                            {
                                break;
                            }
                                if (arr[tmpj, tmpi] == '#')
                                {
                                    aCnt++;
                                    break;
                                }
                            }


                        // down
                        if (i == h - 1)
                        {

                        }
                        else if (arr[j, i + 1] != '#')
                        {
                            bCnt++;
                        }


                             tmpj = j;
                             tmpi = i;
                            while (tmpi < h-1)
                            {
                                tmpi++;
                            if (arr[tmpj, tmpi] == 'L')
                            {
                                break;
                            }
                                if (arr[tmpj, tmpi] == '#')
                                {
                                    aCnt++;
                                    break;
                                }
                            }


                        // left up
                        if ((j == 0 || i == 0))
                        {

                        }
                        else if (arr[j - 1, i - 1] != '#')
                        {
                            bCnt++;
                        }


                             tmpj = j;
                             tmpi = i;
                            while (tmpj > 0 && tmpi > 0)
                            {
                                tmpj--;
                                tmpi--;
                            if (arr[tmpj, tmpi] == 'L')
                            {

                                break;
                            }
                            if (arr[tmpj, tmpi] == '#')
                                {
                                    aCnt++;
                                    break;
                                }
                            }

                        // right up
                        if ((j == w - 1 || i == 0))
                        {

                        }
                        else if (arr[j +1, i - 1] != '#')
                        {
                            bCnt++;
                        }

                             tmpj = j;
                             tmpi = i;
                            while (tmpj < w-1 && tmpi > 0)
                            {
                                tmpj++;
                                tmpi--;
                            if (arr[tmpj, tmpi] == 'L')
                            {

                                break;
                            }
                            if (arr[tmpj, tmpi] == '#')
                                {
                                    aCnt++;
                                    break;
                                }
                            }


                        // left down
                        if ((j == 0 || i == h - 1))
                        {

                        }
                        else if (arr[j - 1, i + 1] != '#')
                        {
                            bCnt++;
                        }

                             tmpj = j;
                             tmpi = i;
                            while (tmpj > 0 && tmpi < h-1)
                            {
                                tmpj--;
                                tmpi++;
                            if (arr[tmpj, tmpi] == 'L')
                            {

                                break;
                            }
                            if (arr[tmpj, tmpi] == '#')
                                {
                                    aCnt++;
                                    break;
                                }
                            }


                        // right down
                        if ((j == w - 1 || i == h - 1))
                        {
                            
                        }
                        else if (arr[j + 1, i + 1] != '#')
                        {
                            bCnt++;
                        }


                             tmpj = j;
                             tmpi = i;
                            while (tmpj < w-1 && tmpi < h - 1)
                            {
                                tmpj++;
                                tmpi++;
                            if (arr[tmpj, tmpi] == 'L')
                            {

                                break;
                            }
                            if (arr[tmpj, tmpi] == '#')
                                {
                                    aCnt++;
                                    break;
                                }
                            }



                        newArr[j, i] = arr[j, i];

                        if (arr[j, i] == 'L' && aCnt == 0)
                        {
                            newArr[j, i] = '#';
                            simulate = true;
                        }
                        else if (arr[j, i] == '#' && aCnt >= 5)
                        {
                            newArr[j, i] = 'L';
                            simulate = true;
                        }




                    }
                }

                var occupied = 0;
                for (var ii = 0; ii < h; ii++)
                {
                    for (var jj = 0; jj < w; jj++)
                    {
                        arr[jj, ii] = newArr[jj, ii];
                        if (arr[jj, ii] == '#')
                        {
                            occupied++;
                        }
                        Console.Write(arr[jj, ii]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("");
                Console.Write(occupied);
                Console.WriteLine("---");



            }
            Console.WriteLine("Done!");


        }
    }
}