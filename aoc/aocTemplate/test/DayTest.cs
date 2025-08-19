using Helper;

public class DayTest
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("test/p.in"))
        {
            string? ln;
            while ((ln = file.ReadLine()) != null)
            {
                Console.WriteLine(ln);
            }

            file.Close();
        }
    }
}

