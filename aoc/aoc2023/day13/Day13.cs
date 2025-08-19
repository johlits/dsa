
using System.Runtime.CompilerServices;

public class Day13
{
    public static bool AreStringsOffByOne(string str1, string str2)
    {
        int diffCount = 0;

        for (int i = 0; i < str1.Length; i++)
        {
            if (str1[i] != str2[i])
            {
                diffCount++;
                if (diffCount > 1)
                {
                    return false;  
                }
            }
        }

        return diffCount == 1;  
    }

    public static void Run()
    {
        using (StreamReader file = new StreamReader("day13/p.in"))
        {
            var cnt = 0;
            var maps = new List<List<string>>();
            List<string> map = new List<string>();
            string? ln;
            while ((ln = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(ln))
                {
                    maps.Add(map);
                    map = new List<string>();
                }
                else
                {
                    map.Add(ln);
                }
            }
            maps.Add(map);

            foreach (var m in maps)
            {
                // row by row
                var rscore = 0;
                var foundr = false;

                for (var i = 1; i < m.Count; i++)
                {
                    
                    if (m[i] == m[i-1] || AreStringsOffByOne(m[i], m[i-1]))
                    {
                        var rscore_temp = i;
                        var u = i - 1;
                        var d = i;
                        rscore = rscore_temp;

                        var eq = true;
                        var offs = AreStringsOffByOne(m[i], m[i - 1]) ? 1 : 0;
                        while (u > 0 && d < m.Count - 1)
                        {

                            u--;
                            d++;

                            if (m[u] != m[d])
                            {
                                if (AreStringsOffByOne(m[u], m[d]))
                                {
                                    offs++;
                                }
                                else
                                {
                                    eq = false;
                                    break;
                                }
                                if (offs > 1)
                                {
                                    eq = false;
                                    break;
                                }
                                
                            }
                        }
                        if (eq && offs == 1)
                        {
                            foundr = true;
                            break;
                        }
                    }
                }

                // col by col
                var cscore = 0;
                var foundc = false;

                var len = m[0].Length;
                for (var i = 1; i < len; i++)
                {
                    var eq = true;
                    var o = 0;
                    for (var j = 0; j < m.Count; j++)
                    {
                        if (m[j][i] != m[j][i - 1])
                        {
                            o++;
                            if (o > 1)
                            {
                                eq = false;
                                break;
                            }
                        }
                    }

                    if (eq)
                    {
                        var cscore_temp = i;
                        var l = i - 1;
                        var r = i;
                        cscore = cscore_temp;

                        var offs = o;
                        while (l > 0 && r < len - 1)
                        {

                            l--;
                            r++;

                            eq = true;
                            for (var j = 0; j < m.Count; j++)
                            {
                                if (m[j][r] != m[j][l])
                                {
                                    offs++;
                                    if (offs > 1)
                                    {
                                        eq = false;
                                        break;
                                    }
                                }
                            }
                            if (eq == false)
                            {
                                break;
                            }
                        }

                        if (eq && offs == 1)
                        {
                            foundc = true;
                            break;
                        }
                    }
                }
                if (foundr)
                {
                    cnt += rscore * 100;
                }
                if (foundc)
                {
                    cnt += cscore;
                }
                if (!foundr && !foundc)
                {
                    throw new Exception();
                }
                if (foundr && foundc)
                {
                    throw new Exception();
                }
            }

            Console.WriteLine(cnt);
            file.Close();
        }
    }
}

