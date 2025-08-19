using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

public class Day20
{
    private static Image Enhance(Image image, ImmutableArray<bool> algorithm, int steps)
    {
        var enhancedImage = image;
        for (var i = 0; i < steps; i++)
        {
            enhancedImage = EnhanceOnce(enhancedImage, algorithm);
        }

        return enhancedImage;
    }

    private static Image EnhanceOnce(Image image, ImmutableArray<bool> algorithm)
    {
        var expandedBounds = image.Bounds.Expand();

        var enhancedPixels = expandedBounds.GetAllPoints()
                           .Where(point => algorithm[image.CalculateEnhancementValue(point)])
                           .ToImmutableHashSet();

        var infinitePixelValue = image.InfiniteValue ? algorithm.Last() : algorithm.First();

        return new Image(expandedBounds, enhancedPixels, infinitePixelValue);
    }

    private static (ImmutableArray<bool> algorithm, Image image) ParseInput(IEnumerable<string> inputLines)
    {
        var algorithm = inputLines.First().Select(character => character == '#').ToImmutableArray();

        var pixelData = ImmutableHashSet.CreateBuilder<Point>();
        foreach (var (line, y) in inputLines.Skip(2).Select((line, index) => (line, index)))
        {
            pixelData.UnionWith(line.Select((ch, x) => (x, ch == '#'))
                                   .Where(pixel => pixel.Item2)
                                   .Select(pixel => new Point(pixel.x, y)));
        }

        var imageBounds = new Rectangle(
            pixelData.Min(p => p.X),
            pixelData.Min(p => p.Y),
            pixelData.Max(p => p.X),
            pixelData.Max(p => p.Y));

        return (algorithm, new Image(imageBounds, pixelData.ToImmutable()));
    }

    record struct Point(int X, int Y);

    record Rectangle(int Left, int Top, int Right, int Bottom)
    {
        public bool Contains(Point point) => point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom;

        public Rectangle Expand() => new Rectangle(Left - 1, Top - 1, Right + 1, Bottom + 1);

        public IEnumerable<Point> GetAllPoints()
            => from y in Enumerable.Range(Top, Bottom - Top + 1)
               from x in Enumerable.Range(Left, Right - Left + 1)
               select new Point(x, y);
    }

    record Image(Rectangle Bounds, ImmutableHashSet<Point> Pixels, bool InfiniteValue = false)
    {
        public bool GetPixel(Point point) => Bounds.Contains(point) ? Pixels.Contains(point) : InfiniteValue;

        public int CalculateEnhancementValue(Point point)
        {
            var values = from deltaY in Enumerable.Range(-1, 3)
                         from deltaX in Enumerable.Range(-1, 3)
                         select GetPixel(new Point(point.X + deltaX, point.Y + deltaY)) ? 1 : 0;

            return values.Aggregate(0, (current, value) => (current << 1) | value);
        }

        public int CountEnhancedPixels() => Pixels.Count(Bounds.Contains);
    }

    public static void Run()
    {
        var (algorithm, image) = ParseInput(File.ReadAllLines("p.in"));

        var part1Result = Enhance(image, algorithm, 2);
        Console.WriteLine(part1Result.CountEnhancedPixels());

        var part2Result = Enhance(image, algorithm, 50);
        Console.WriteLine(part2Result.CountEnhancedPixels());
    }
}
