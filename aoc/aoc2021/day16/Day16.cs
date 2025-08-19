using Helper;
using System.Numerics;
using System.Text;

public class Day16
{
    private static BigInteger ParsePacket(string binaryInput, ref int position)
    {
        if (position >= binaryInput.Length - 7) 
            return 0;

        int version = Convert.ToInt32(binaryInput.Substring(position, 3), 2);
        position += 3;

        int typeId = Convert.ToInt32(binaryInput.Substring(position, 3), 2);
        position += 3;

        if (typeId == 4) 
        {
            BigInteger literalValue = 0;
            while (binaryInput[position] == '1')
            {
                position++; 
                literalValue = (literalValue << 4) | Convert.ToInt32(binaryInput.Substring(position, 4), 2);
                position += 4;
            }
            position++; 
            literalValue = (literalValue << 4) | Convert.ToInt32(binaryInput.Substring(position, 4), 2);
            position += 4;
            return literalValue;
        }

        List<BigInteger> subPacketValues = new List<BigInteger>();
        if (binaryInput[position] == '0')
        {
            int totalLength = Convert.ToInt32(binaryInput.Substring(position + 1, 15), 2);
            position += 16;
            int endPosition = position + totalLength;

            while (position < endPosition)
            {
                subPacketValues.Add(ParsePacket(binaryInput, ref position));
            }
        }
        else
        {
            int numSubPackets = Convert.ToInt32(binaryInput.Substring(position + 1, 11), 2);
            position += 12;

            for (int i = 0; i < numSubPackets; i++)
            {
                subPacketValues.Add(ParsePacket(binaryInput, ref position));
            }
        }

        return typeId switch
        {
            0 => subPacketValues.Aggregate(BigInteger.Add),              // Sum
            1 => subPacketValues.Aggregate(BigInteger.One, BigInteger.Multiply), // Product
            2 => subPacketValues.Min(),                                  // Minimum
            3 => subPacketValues.Max(),                                  // Maximum
            5 => subPacketValues[0] > subPacketValues[1] ? 1 : 0,        // Greater than
            6 => subPacketValues[0] < subPacketValues[1] ? 1 : 0,        // Less than
            7 => subPacketValues[0] == subPacketValues[1] ? 1 : 0,       // Equal to
            _ => throw new InvalidOperationException("Unknown type ID")
        };
    }

    private static string HexToBinary(string hex)
    {
        return string.Concat(hex.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
    }

    public static void Run()
    {
        var text = new ListOfStrings();
        var bps = new List<(Blueprint, int)>
        {
            (text, -1),
        };
        new Parser("p.in", bps, new Symbols()
        {

        });
        var hexInput = text.lists.First().list.First();
        string binaryInput = HexToBinary(hexInput);

        int position = 0;
        var packetValue = ParsePacket(binaryInput, ref position);

        Console.WriteLine(packetValue);
    }
}

