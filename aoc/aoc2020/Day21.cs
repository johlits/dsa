using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day21
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
            var dishes = new List<(List<string>, List<string>)>();
            var dic = new Dictionary<string, List<string>>();
            var allAllegrens = new List<string>();
            var res = 0;


            var resDic = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                var food = line.Split(' ');
                var step = 0;
                var ingredients = new List<string>();
                var allegrens = new List<string>();
                
                foreach (var f in food)
                {
                    if (f == "(contains")
                    {
                        step = 1;
                    }
                    else if (step == 0)
                    {
                        ingredients.Add(f);
                    }
                    else if (step == 1)
                    {
                        var alle = f.Remove(f.Count() - 1);
                        allegrens.Add(alle);
                        if (!allAllegrens.Contains(alle)) {
                            allAllegrens.Add(alle);
                        }
                    }
                }
                dishes.Add((ingredients, allegrens));
            }

            var _frkmp = "";
            var _qhqs = "";
            var _ntflq = "";
            var _qnhjhn = "";
            var _dhsnxr = "";
            var _rzrktx = "";
            var _xncgqbcp = "";
            var _lgnhmx = "";
            var y = 12345678;
            while (y++ < 99999999)
            {
                var ok = true;
                var s = "" + y;
                if (s.Contains("0") || s.Contains("9"))
                    continue;
                var l = new List<char>();
                foreach (var c in s)
                {
                    if (l.Contains(c))
                    {
                        ok = false;
                        break;
                    }
                    else
                    {
                        l.Add(c);
                    }
                }

                if (ok == false)
                    continue;

                var n1 = Int32.Parse("" + s[0]) - 1;
                var n2 = Int32.Parse("" + s[1]) - 1;
                var n3 = Int32.Parse("" + s[2]) - 1;
                var n4 = Int32.Parse("" + s[3]) - 1;
                var n5 = Int32.Parse("" + s[4]) - 1;
                var n6 = Int32.Parse("" + s[5]) - 1;
                var n7 = Int32.Parse("" + s[6]) - 1;
                var n8 = Int32.Parse("" + s[7]) - 1;

                


                _frkmp = allAllegrens[n1];
                _qhqs = allAllegrens[n2];
                _ntflq = allAllegrens[n3];
                _qnhjhn = allAllegrens[n4];
                _dhsnxr = allAllegrens[n5];
                _rzrktx = allAllegrens[n6];
                _xncgqbcp = allAllegrens[n7];
                _lgnhmx = allAllegrens[n8];


                Console.WriteLine(_frkmp + " " + _qhqs + " " + _ntflq + " " + _qnhjhn + " " + _dhsnxr + " " + _rzrktx + " " + _xncgqbcp + " " + _lgnhmx);



                foreach (var dish in dishes)
                {
                    if (!dish.Item1.Contains("frkmp") && dish.Item2.Contains(_frkmp))
                    {
                        ok = false;
                        break;
                    }
                    if (!dish.Item1.Contains("qhqs") && dish.Item2.Contains(_qhqs))
                    {
                        ok = false;
                        break;
                    }
                    if (!dish.Item1.Contains("ntflq") && dish.Item2.Contains(_ntflq))
                    {
                        ok = false;
                        break;
                    }
                    if (!dish.Item1.Contains("qnhjhn") && dish.Item2.Contains(_qnhjhn))
                    {
                        ok = false;
                        break;
                    }
                    if (!dish.Item1.Contains("dhsnxr") && dish.Item2.Contains(_dhsnxr))
                    {
                        ok = false;
                        break;
                    }
                    if (!dish.Item1.Contains("rzrktx") && dish.Item2.Contains(_rzrktx))
                    {
                        ok = false;
                        break;
                    }
                    if (!dish.Item1.Contains("xncgqbcp") && dish.Item2.Contains(_xncgqbcp))
                    {
                        ok = false;
                        break;
                    }
                    if (!dish.Item1.Contains("lgnhmx") && dish.Item2.Contains(_lgnhmx))
                    {
                        ok = false;
                        break;
                    }
                    
                }
                if (ok == true)
                {
                    Console.WriteLine("OK");
                    Console.WriteLine("frkmp = " + _frkmp);
                    Console.WriteLine("qhqs =  " + _qhqs);
                    Console.WriteLine("ntflq =  " + _ntflq);
                    Console.WriteLine("qnhjhn =  " + _qnhjhn);
                    Console.WriteLine("dhsnxr =  " + _dhsnxr);
                    Console.WriteLine("rzrktx =  " + _rzrktx);
                    Console.WriteLine("xncgqbcp = " + _xncgqbcp);
                    Console.WriteLine("lgnhmx = " + _lgnhmx);
                    Console.ReadLine();
                }
            }
            

            foreach (var dish in dishes)
            {
                var ingredients = dish.Item1;
                var allegrens = dish.Item2;
                foreach (var ingr in ingredients)
                {
                    if (!dic.ContainsKey(ingr))
                    {
                        dic.Add(ingr, new List<string>());
                    }
                    foreach (var alle in allegrens)
                    {
                        dic[ingr].Add(alle);
                    }
                }
            }

            while (true)
            {
                var tra = new List<string>();
                foreach (var alle in allAllegrens)
                {
                    var best = -1;
                    var bestIngredient = "";
                    foreach (var kvp in dic)
                    {
                        var cnt = 0;
                        foreach (var allegren in kvp.Value)
                        {
                            if (allegren == alle)
                            {
                                cnt++;
                            }
                        }
                        if (cnt > best)
                        {
                            best = cnt;
                            bestIngredient = kvp.Key;
                        }
                        else if (cnt == best)
                        {
                            best = -1;
                        }
                    }

                    if (best > 0)
                    {
                        Console.WriteLine(alle + " is " + bestIngredient);
                        
                        resDic.Add(alle, bestIngredient);
                        
                        var toRemove = new List<string>();
                        foreach (var kvp in dic)
                        {
                            //if (kvp.Key != bestIngredient)
                            //{
                                while (kvp.Value.Contains(alle))
                                {
                                    kvp.Value.Remove(alle);
                                }
                                if (kvp.Value.Count <= 1)
                                {
                                    if (kvp.Key != bestIngredient) 
                                    toRemove.Add(kvp.Key);
                                }
                            //}
                        }

                        /*Console.WriteLine(bestIngredient + " could also be ");
                        foreach (var x in dic[bestIngredient])
                        {
                            Console.WriteLine(x);
                        }
                        Console.WriteLine();*/

                        dic.Remove(bestIngredient);

                        foreach (var tr in toRemove)
                        {
                            //Console.Write(tr + " is clean ");
                            var cnt = 0;
                            foreach (var dish in dishes)
                            {
                                if (dish.Item1.Contains(tr))
                                {
                                    cnt++;
                                }
                            }
                            res += cnt;
                            //Console.WriteLine(cnt);
                            dic.Remove(tr);
                        }
                        tra.Add(alle);

                    }

                }

                foreach (var tr in tra)
                {
                    allAllegrens.Remove(tr);
                }

                if (dic.Count == 0)
                {
                    break;
                }
                if (allAllegrens.Count == 0)
                    break;
            }
            Console.WriteLine(res);

            var list = resDic.Keys.ToList();
            list.Sort();

            foreach (var val in list)
            {
                Console.Write(resDic[val] + ",");
            }
            Console.WriteLine();

            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}