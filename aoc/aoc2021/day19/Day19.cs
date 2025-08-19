using Helper;
using System.Collections.Immutable;

public class Day19
{
    private static IList<Scanner> AlignAllScanners(IEnumerable<Scanner> scanners)
    {
        var unmatched = scanners.GroupBy(x => x.Id)
                                .ToDictionary(g => g.Key, g => g.ToImmutableList());

        var matched = new Dictionary<int, Scanner>();

        if (unmatched.TryGetValue(0, out var initialScanner))
        {
            matched.Add(0, initialScanner.First());
            unmatched.Remove(0);
        }
        else
        {
            throw new InvalidOperationException("No scanner with ID 0 found.");
        }

        var queue = new Queue<int>();
        queue.Enqueue(0);

        while (queue.Count > 0 && unmatched.Count > 0)
        {
            var currentId = queue.Dequeue();
            var currentScanner = matched[currentId];

            foreach (var scannerGroup in unmatched.Values)
            {
                var matches = AlignAnyScanners(currentScanner, new[] { scannerGroup });
                foreach (var m in matches)
                {
                    matched[m.Id] = m;
                    queue.Enqueue(m.Id);
                    unmatched.Remove(m.Id);
                    break; 
                }
            }
        }

        return matched.Values.ToList();
    }

    private static IEnumerable<Scanner> AlignAnyScanners(Scanner target, IEnumerable<IEnumerable<Scanner>> scanners)
    {
        foreach (var scannerGroup in scanners)
        {
            foreach (var scanner in scannerGroup)
            {
                foreach (var alignedScanner in AlignAnyOrientation(target, new[] { scanner }))
                {
                    yield return alignedScanner;
                }
            }
        }
    }

    private static IEnumerable<Scanner> AlignAnyOrientation(Scanner target, IEnumerable<Scanner> scanners)
    {
        foreach (var scanner in scanners)
        {
            foreach (var alignedScanner in AlignSingleOrientation(target, scanner))
            {
                yield return alignedScanner;
                yield break;
            }
        }
    }

    private static IEnumerable<Scanner> AlignSingleOrientation(Scanner target, Scanner scanner)
    {
        foreach (Vector t in target.AbsoluteBeacons)
        {
            foreach (Vector s in scanner.RelativeBeacons)
            {
                Vector offset = t.Subtract(s);
                Scanner moved = scanner with { Position = offset };
                if (target.AbsoluteBeacons.Intersect(moved.AbsoluteBeacons).Count() >= 12)
                {
                    yield return moved;
                    yield break; 
                }
            }
        }
    }

    private static ImmutableList<Scanner> Parse(IEnumerable<string> input)
    {
        var scannersBuilder = ImmutableList.CreateBuilder<Scanner>();
        var beacons = new List<Vector>();
        int id = -1;

        foreach (var line in input)
        {
            if (line.StartsWith("---"))
            {
                if (id >= 0 && beacons.Count > 0)
                {
                    scannersBuilder.AddRange(Scanner.CreateOrientations(id, beacons));
                    beacons.Clear();
                }
                id++; 
            }
            else if (id >= 0 && !string.IsNullOrWhiteSpace(line))
            {
                var coords = line.Split(',');
                beacons.Add(new Vector(
                    int.Parse(coords[0]),
                    int.Parse(coords[1]),
                    int.Parse(coords[2])
                ));
            }
        }

        if (id >= 0 && beacons.Count > 0)
        {
            scannersBuilder.AddRange(Scanner.CreateOrientations(id, beacons));
        }

        return scannersBuilder.ToImmutable();
    }

    record struct Vector(int X, int Y, int Z)
    {
        public Vector Subtract(Vector other)
            => new Vector(X - other.X, Y - other.Y, Z - other.Z);

        public Vector Add(Vector other)
            => new Vector(X + other.X, Y + other.Y, Z + other.Z);

        public int ManhattanDistance(Vector other)
            => Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z);

        public IEnumerable<Vector> EnumFacingDirections()
        {
            yield return this;
            yield return new Vector(-X, -Y, Z);
            var rotated = new Vector(Y, Z, X);
            yield return rotated;
            yield return new Vector(-rotated.X, -rotated.Y, rotated.Z);
            rotated = new Vector(rotated.Y, rotated.Z, rotated.X);
            yield return rotated;
            yield return new Vector(-rotated.X, -rotated.Y, rotated.Z);
        }

        public IEnumerable<Vector> EnumRotations()
        {
            var current = this;
            for (int i = 0; i < 4; i++)
            {
                yield return current;
                current = new Vector(current.X, -current.Z, current.Y);
            }
        }

        public IEnumerable<Vector> EnumOrientations()
            => EnumFacingDirections().SelectMany(direction => direction.EnumRotations());
    }

    record Scanner(int Id, ImmutableHashSet<Vector> RelativeBeacons, Vector Position = default)
    {
        public IEnumerable<Vector> AbsoluteBeacons
        {
            get
            {
                foreach (var beacon in RelativeBeacons)
                {
                    yield return beacon.Add(Position);
                }
            }
        }

        public static IEnumerable<Scanner> CreateOrientations(int id, IEnumerable<Vector> beacons)
        {
            var orientationGroups = new Dictionary<int, ImmutableHashSet<Vector>.Builder>();

            foreach (var beacon in beacons)
            {
                int index = 0;
                foreach (var orientation in beacon.EnumOrientations())
                {
                    if (!orientationGroups.ContainsKey(index))
                    {
                        orientationGroups[index] = ImmutableHashSet.CreateBuilder<Vector>();
                    }

                    orientationGroups[index].Add(orientation);
                    index++;
                }
            }

            foreach (var orientationGroup in orientationGroups)
            {
                yield return new Scanner(id, orientationGroup.Value.ToImmutable());
            }
        }
    }

    public static void Run()
    {
        var input = Parse(File.ReadLines("p.in"));
        var matched = AlignAllScanners(input);

        var uniquePoints = new HashSet<Vector>();
        foreach (var scanner in matched)
        {
            foreach (var beacon in scanner.AbsoluteBeacons)
            {
                uniquePoints.Add(beacon);
            }
        }
        Console.WriteLine($"Unique Points: {uniquePoints.Count}");

        var locations = matched.Select(scanner => scanner.Position).ToList();
        int maxDistance = 0;
        for (int i = 0; i < locations.Count; i++)
        {
            for (int j = i + 1; j < locations.Count; j++)
            {
                int distance = locations[i].ManhattanDistance(locations[j]);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }
        }
        Console.WriteLine($"Max Distance: {maxDistance}");
    }
}

