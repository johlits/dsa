using Helper;
using System.Collections.Immutable;

public class Day25
{
    public static Map RunSimulationStep(Map currentMap)
    {
        Map mapAfterMovingEast = MoveCucumbers(currentMap, Direction.East);
        Map finalMap = MoveCucumbers(mapAfterMovingEast, Direction.South);

        return finalMap;
    }

    private static Map MoveCucumbers(Map map, Direction moveDirection)
    {
        var mapBuilder = map.Data.ToBuilder();

        foreach (var cucumberPosition in map.GetCucumbersPositions(moveDirection))
        {
            var nextPosition = map.CalculateNextPosition(cucumberPosition);
            if (map.IsPositionEmpty(nextPosition))
            {
                var currentIndex = map.ConvertPositionToIndex(cucumberPosition);
                var nextIndex = map.ConvertPositionToIndex(nextPosition);
                // Swap positions of the cucumber and the empty space.
                (mapBuilder[currentIndex], mapBuilder[nextIndex]) = (mapBuilder[nextIndex], mapBuilder[currentIndex]);
            }
        }

        return map with { Data = mapBuilder.ToImmutable() };
    }

    public enum Direction { Empty, East, South }

    public record Map(ImmutableArray<Direction> Data, int Width)
    {
        public int Height => Data.Length / Width;

        public Direction this[(int x, int y) position] => Data[ConvertPositionToIndex(position)];

        public int ConvertPositionToIndex((int x, int y) position)
            => IsValidPosition(position) ? (position.y * Width) + position.x : throw new ArgumentOutOfRangeException();

        public (int x, int y) CalculateNextPosition((int x, int y) position)
            => this[position] switch
            {
                Direction.East => ((position.x + 1) % Width, position.y),
                Direction.South => (position.x, (position.y + 1) % Height),
                _ => throw new InvalidOperationException()
            };

        public IEnumerable<(int x, int y)> GetCucumbersPositions(Direction type)
            => from y in Enumerable.Range(0, Height)
                from x in Enumerable.Range(0, Width)
                let pos = (x, y)
                where this[pos] == type
                select pos;

        public bool IsPositionEmpty((int x, int y) position) => this[position] == Direction.Empty;

        private bool IsValidPosition((int x, int y) position)
            => position.x >= 0 && position.x < Width && position.y >= 0 && position.y < Height;

        public static Map Parse(IEnumerable<string> input)
        {
            var entries = ImmutableArray.CreateBuilder<Direction>();
            int? width = null;
            foreach (var line in input)
            {
                width = line.Length;
                entries.AddRange(line.Select(ch => ch switch
                {
                    '>' => Direction.East,
                    'v' => Direction.South,
                    _ => Direction.Empty
                }));
            }

            if (!width.HasValue)
                throw new FormatException("Invalid input format");

            return new Map(entries.ToImmutable(), width.Value);
        }
    }

    public static void Run()
    {
        var map = Map.Parse(File.ReadLines("p.in"));

        int stepCount = 0;
        var currentMap = map;
        while (true)
        {
            stepCount++;
            var nextMap = RunSimulationStep(currentMap);

            if (nextMap.Data.SequenceEqual(currentMap.Data))
                break;

            currentMap = nextMap;
        }

        Console.WriteLine($"Steps required for the cucumbers to stop moving: {stepCount}");
    }
}

