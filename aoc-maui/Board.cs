using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_maui
{
    public class Board
    {
        public char[,] Cells;
        public int Columns;
        public int Rows;

        public Board(string fileName)
        {
            using (StreamReader file = new StreamReader(fileName))
            {
                string ln;
                var i = -1;
                while ((ln = file.ReadLine()) != null)
                {
                    if (i == -1)
                    {
                        var words = ln.Split(',');
                        Columns = int.Parse(words[0]);
                        Rows = int.Parse(words[1]);
                        Cells = new char[Columns, Rows];
                        i++;
                        continue;
                    }
                    for (var j = 0; j < ln.Length; j++)
                    {
                        Cells[j, i] = ln[j];
                    }
                    i++;
                }
            }
        }

        public Board(int columns, int rows)
        {
            Cells = new char[columns, rows];
            Columns = columns;
            Rows = rows;
        }
    }
}
