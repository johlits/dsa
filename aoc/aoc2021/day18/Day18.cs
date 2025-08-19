using Helper;

public class Day18
{
    public class SnailfishNumber
    {
        public SnailfishNumber Left { get; set; }
        public SnailfishNumber Right { get; set; }
        public int? Value { get; set; }
        public SnailfishNumber Parent { get; set; }

        public bool IsPair() => Left != null && Right != null;

        public SnailfishNumber(int value)
        {
            this.Value = value;
        }

        public SnailfishNumber(SnailfishNumber left, SnailfishNumber right)
        {
            this.Left = left;
            this.Right = right;
            this.Left.Parent = this;
            this.Right.Parent = this;
        }

        public void Explode()
        {
            var leftNumber = FindAdjacentNumber(this, true);
            if (leftNumber != null)
            {
                leftNumber.Value += this.Left.Value;
            }

            var rightNumber = FindAdjacentNumber(this, false);
            if (rightNumber != null)
            {
                rightNumber.Value += this.Right.Value;
            }

            this.Value = 0;
            this.Left = null;
            this.Right = null;
        }

        private SnailfishNumber FindAdjacentNumber(SnailfishNumber node, bool findLeft)
        {
            SnailfishNumber current = node;
            while (current.Parent != null)
            {
                SnailfishNumber parent = current.Parent;
                if (findLeft && parent.Left != current || !findLeft && parent.Right != current)
                {
                    current = findLeft ? parent.Left : parent.Right;
                    while (current.IsPair())
                    {
                        current = findLeft ? current.Right : current.Left;
                    }
                    return current;
                }
                current = parent;
            }
            return null;
        }

        public void Split()
        {
            this.Left = new SnailfishNumber(this.Value.Value / 2);
            this.Right = new SnailfishNumber((this.Value.Value + 1) / 2);
            this.Left.Parent = this;
            this.Right.Parent = this;
            this.Value = null;
        }

        public override string ToString()
        {
            if (this.IsPair())
            {
                return $"[{Left},{Right}]";
            }
            else
            {
                return this.Value.ToString();
            }
        }

        public SnailfishNumber Clone()
        {
            if (this.IsPair())
            {
                return new SnailfishNumber(this.Left.Clone(), this.Right.Clone());
            }
            else
            {
                return new SnailfishNumber(this.Value.Value);
            }
        }
    }

    public class SnailfishCalculator
    {
        public SnailfishNumber Parse(string input)
        {
            return Parse(ref input);
        }

        private SnailfishNumber Parse(ref string input)
        {
            if (input[0] == '[')
            {
                input = input.Substring(1); // consume '['
                SnailfishNumber left = Parse(ref input);
                input = input.Substring(1); // consume ','
                SnailfishNumber right = Parse(ref input);
                input = input.Substring(1); // consume ']'
                return new SnailfishNumber(left, right);
            }
            else
            {
                int end = input.IndexOfAny(new char[] { ',', ']' });
                int value = int.Parse(input.Substring(0, end));
                input = input.Substring(end);
                return new SnailfishNumber(value);
            }
        }

        public SnailfishNumber Add(SnailfishNumber a, SnailfishNumber b)
        {
            return new SnailfishNumber(a, b);
        }

        public void Reduce(SnailfishNumber number)
        {
            while (true)
            {
                if (Explode(number, 0)) continue;
                if (Split(number)) continue;
                break;
            }
        }

        private bool Explode(SnailfishNumber number, int depth)
        {
            if (number.IsPair())
            {
                if (depth == 4)
                {
                    number.Explode();
                    return true;
                }
                return Explode(number.Left, depth + 1) || Explode(number.Right, depth + 1);
            }
            return false;
        }

        private bool Split(SnailfishNumber number)
        {
            if (number.IsPair())
            {
                return Split(number.Left) || Split(number.Right);
            }
            else
            {
                if (number.Value >= 10)
                {
                    number.Split();
                    return true;
                }
            }
            return false;
        }

        public int Magnitude(SnailfishNumber number)
        {
            if (number.IsPair())
            {
                return 3 * Magnitude(number.Left) + 2 * Magnitude(number.Right);
            }
            else
            {
                return number.Value.Value;
            }
        }

        public SnailfishNumber AddAndReduce(SnailfishNumber a, SnailfishNumber b)
        {
            var sum = Add(a, b);
            Reduce(sum);
            return sum;
        }
    }

    public static void Run()
    {
        var calculator = new SnailfishCalculator();
        var lines = System.IO.File.ReadAllLines("p.in"); 
        var numbers = lines.Select(line => calculator.Parse(line)).ToList();

        int largestMagnitude = 0;

        for (int i = 0; i < numbers.Count; i++)
        {
            for (int j = 0; j < numbers.Count; j++)
            {
                if (i == j) continue; 

                var magnitude1 = calculator.Magnitude(calculator.AddAndReduce(numbers[i].Clone(), numbers[j].Clone()));
                var magnitude2 = calculator.Magnitude(calculator.AddAndReduce(numbers[j].Clone(), numbers[i].Clone()));

                largestMagnitude = new[] { largestMagnitude, magnitude1, magnitude2 }.Max();
            }
        }

        Console.WriteLine($"The largest magnitude of any sum of two different snailfish numbers is: {largestMagnitude}");
    }
}

