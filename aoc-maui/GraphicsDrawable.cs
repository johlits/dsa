using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Maui.Graphics.Color;

namespace aoc_maui
{
    public class GraphicsDrawable : IDrawable
    {
        public Board board;
        private Dictionary<char, Color> colors = new Dictionary<char, Color>()
        {
            { '\0', Color.FromArgb("#ffffff") },
            { '.', Color.FromArgb("#1c395b") },
            { 'o', Color.FromArgb("#7eabe0") },
            { '#', Color.FromArgb("#000000") },
        };

        public GraphicsDrawable()
        {
            this.board = new Board(colors.Count, 1);
            var i = 0;
            foreach (KeyValuePair<char, Color> color in colors)
            {
                this.board.Cells[i++, 0] = color.Key;
            }
        }

        public void UpdateBoard(Board board)
        {
            this.board = board;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = colors['.'];
            canvas.FillRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);
            canvas.FillColor = colors['o'];

            float cellWidthPx = dirtyRect.Width / board.Columns;
            float cellHeightPx = dirtyRect.Height / board.Rows;

            float borderFrac = .1f;
            float xPad = borderFrac * cellWidthPx;
            float yPad = borderFrac * cellHeightPx;

            for (int rowIndex = 0; rowIndex < board.Rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < board.Columns; columnIndex++)
                {
                    canvas.FillColor = colors[board.Cells[columnIndex, rowIndex]];
                    canvas.FillRectangle(
                        columnIndex * cellWidthPx + xPad,
                        rowIndex * cellHeightPx + yPad,
                        cellWidthPx - xPad * 2,
                        cellHeightPx - yPad * 2);
                }
            }
        }
    }
}
