
public class Day11
{
    public class Item
    {
        public int[] wheels = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] sizes = new int[8] { 17, 7, 13, 2, 19, 3, 5, 11 };

        public Item(int start)
        {
            for (var i = 0; i < 8; i++)
            {
                wheels[i] = start % sizes[i];
            }
        }
        public void IncreaseWheels(int op)
        {
            for (var i = 0; i < 8; i++)
            {
                switch (op)
                {
                    case 0: wheels[i] = (wheels[i] * 5) % sizes[i]; break;
                    case 1: wheels[i] = (wheels[i] + 3) % sizes[i]; break;
                    case 2: wheels[i] = (wheels[i] + 7) % sizes[i]; break;
                    case 3: wheels[i] = (wheels[i] + 5) % sizes[i]; break;
                    case 4: wheels[i] = (wheels[i] + 2) % sizes[i]; break;
                    case 5: wheels[i] = (wheels[i] * 19) % sizes[i]; break;
                    case 6: wheels[i] = (wheels[i] * wheels[i]) % sizes[i]; break;
                    case 7: wheels[i] = (wheels[i] + 4) % sizes[i]; break;
                    default: throw new Exception("No such monkey");
                }
            }
        }

        public int ThrowTo(int id)
        {
            var isDivisible = wheels[id] == 0;
            switch (id)
            {
                case 0: return isDivisible ? 4 : 7;
                case 1: return isDivisible ? 3 : 2;
                case 2: return isDivisible ? 0 : 7;
                case 3: return isDivisible ? 0 : 2;
                case 4: return isDivisible ? 6 : 5;
                case 5: return isDivisible ? 6 : 1;
                case 6: return isDivisible ? 3 : 1;
                case 7: return isDivisible ? 5 : 4;
                default: throw new Exception("No such monkey");
            }
            
        }
    }

    private class Monkey
    {
        public int Id;
        public ulong Inspections = 0;
        public List<Item> Items;

        public Monkey()
        {
            Items = new List<Item>();
        }

        public void Operation(Item item)
        {
            item.IncreaseWheels(Id);
        }

        public int Test (Item item)
        {
            return item.ThrowTo(Id);
        }
    }

    public static void Run()
    {
        var monkeys = new List<Monkey>
        {
            new Monkey()
            {
                Id = 0,
                Items = new List<Item>() { new Item(89), new Item(74) }
            },
            new Monkey()
            {
                Id = 1,
                Items = new List<Item>() { new Item(75), new Item(69), new Item(87), new Item(57), new Item(84), new Item(90), new Item(66), new Item(50) }
            },
            new Monkey()
            {
                Id = 2,
                Items = new List<Item>() { new Item(55) }
            },
            new Monkey()
            {
                Id = 3,
                Items = new List<Item>() { new Item(69), new Item(82), new Item(69), new Item(56), new Item(68) }
            },
            new Monkey()
            {
                Id = 4,
                Items = new List<Item>() { new Item(72), new Item(97), new Item(50) }
            },
            new Monkey()
            {
                Id = 5,
                Items = new List<Item>() { new Item(90), new Item(84), new Item(56), new Item(92), new Item(91), new Item(91) }
            },
            new Monkey()
            {
                Id = 6,
                Items = new List<Item>() { new Item(63), new Item(93), new Item(55), new Item(53) }
            },
            new Monkey()
            {
                Id = 7,
                Items = new List<Item>() { new Item(50), new Item(61), new Item(52), new Item(58), new Item(86), new Item(68), new Item(97) }
            }
        };

        for (var i = 0; i < 10000; i++)
        {
            foreach(var monkey in monkeys)
            {
                for (var j = 0; j < monkey.Items.Count; j++)
                {
                    monkey.Inspections++;
                    var item = monkey.Items[j];
                    monkey.Operation(item);
                    monkeys[monkey.Test(item)].Items.Add(item);
                }
                monkey.Items.Clear();
            }
        }

        var sorted = monkeys.OrderBy(x => x.Inspections).Reverse().ToList();
        Console.WriteLine(sorted[0].Inspections * sorted[1].Inspections);
    }
}

