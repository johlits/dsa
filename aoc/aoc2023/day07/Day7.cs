using System.Threading.Tasks.Sources;

public class Day7
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day07/p.in"))
        {

            List<Tuple<ulong, ulong, string>> sets = new List<Tuple<ulong, ulong, string>>();
            string? ln;
            while ((ln = file.ReadLine()) != null)
            {
                var parts = ln.Split(" ");
                var hand = parts[0];
                ulong bid = ulong.Parse(parts[1]);

                var ace_tot = 0;
                var king_tot = 0;
                var queen_tot = 0;
                var knight_tot = 0;
                var ten_tot = 0;
                var nine_tot = 0;
                var eight_tot = 0;
                var seven_tot = 0;
                var six_tot = 0;
                var five_tot = 0;
                var four_tot = 0;
                var three_tot = 0;
                var two_tot = 0;

                var ace = 0;
                var king = 0;
                var queen = 0;
                var knight = 0;
                var ten = 0;
                var nine = 0;
                var eight = 0;
                var seven = 0;
                var six = 0;
                var five = 0;
                var four = 0;
                var three = 0;
                var two = 0;

                var twoofakind = 0;
                var threeofakind = 0;

                for (var i = 0; i < hand.Length; i++)
                {
                    if (hand[i] == 'A') ace++;
                    else if (hand[i] == 'K') king++;
                    else if (hand[i] == 'Q') queen++;
                    else if (hand[i] == 'J') knight++;
                    else if (hand[i] == 'T') ten++;
                    else if (hand[i] == '9') nine++;
                    else if (hand[i] == '8') eight++;
                    else if (hand[i] == '7') seven++;
                    else if (hand[i] == '6') six++;
                    else if (hand[i] == '5') five++;
                    else if (hand[i] == '4') four++;
                    else if (hand[i] == '3') three++;
                    else if (hand[i] == '2') two++;

                    if (hand[i] == 'A') ace_tot++;
                    else if (hand[i] == 'K') king_tot++;
                    else if (hand[i] == 'Q') queen_tot++;
                    else if (hand[i] == 'J') knight_tot++;
                    else if (hand[i] == 'T') ten_tot++;
                    else if (hand[i] == '9') nine_tot++;
                    else if (hand[i] == '8') eight_tot++;
                    else if (hand[i] == '7') seven_tot++;
                    else if (hand[i] == '6') six_tot++;
                    else if (hand[i] == '5') five_tot++;
                    else if (hand[i] == '4') four_tot++;
                    else if (hand[i] == '3') three_tot++;
                    else if (hand[i] == '2') two_tot++;
                }

                for (var i = 0; i < 5; i++) {
                    if (ace + knight >= 3 && threeofakind <= 1) { threeofakind++; ace -= 3; if (ace < 0) { knight -= -ace; ace = 0; } }
                    else if (king + knight >= 3 && threeofakind <= 1) { threeofakind++; king -= 3; if (king < 0) { knight -= -king; king = 0; } }
                    else if (queen + knight >= 3 && threeofakind <= 1) { threeofakind++; queen -= 3; if (queen < 0) { knight -= -queen; queen = 0; } }
                    else if (ten + knight >= 3 && threeofakind <= 1) { threeofakind++; ten -= 3; if (ten < 0) { knight -= -ten; ten = 0; } }
                    else if (nine + knight >= 3 && threeofakind <= 1) { threeofakind++; nine -= 3; if (nine < 0) { knight -= -nine; nine = 0; } }
                    else if (eight + knight >= 3 && threeofakind <= 1) { threeofakind++; eight -= 3; if (eight < 0) { knight -= -eight; eight = 0; } }
                    else if (seven + knight >= 3 && threeofakind <= 1) { threeofakind++; seven -= 3; if (seven < 0) { knight -= -seven; seven = 0; } }
                    else if (six + knight >= 3 && threeofakind <= 1) { threeofakind++; six -= 3; if (six < 0) { knight -= -six; six = 0; } }
                    else if (five + knight >= 3 && threeofakind <= 1) { threeofakind++; five -= 3; if (five < 0) { knight -= -five; five = 0; } }
                    else if (four + knight >= 3 && threeofakind <= 1) { threeofakind++; four -= 3; if (four < 0) { knight -= -four; four = 0; } }
                    else if (three + knight >= 3 && threeofakind <= 1) { threeofakind++; three -= 3; if (three < 0) { knight -= -three; three = 0; } }
                    else if (two + knight >= 3 && threeofakind <= 1) { threeofakind++; two -= 3; if (two < 0) { knight -= -two; two = 0; } }
                    //else if (knight == 3) { threeofakind++; knight -= 3; }

                    else if (ace + knight >= 2 && twoofakind <= 2) { twoofakind++; ace -= 2; if (ace < 0) { knight -= -ace; ace = 0; } }
                    else if (king + knight >= 2 && twoofakind <= 2) { twoofakind++; king -= 2; if (king < 0) { knight -= -king; king = 0; } }
                    else if (queen + knight >= 2 && twoofakind <= 2) { twoofakind++; queen -= 2; if (queen < 0) { knight -= -queen; queen = 0; } }
                    else if (ten + knight >= 2 && twoofakind <= 2) { twoofakind++; ten -= 2; if (ten < 0) { knight -= -ten; ten = 0; } }
                    else if (nine + knight >= 2 && twoofakind <= 2) { twoofakind++; nine -= 2; if (nine < 0) { knight -= -nine; nine = 0; } }
                    else if (eight + knight >= 2 && twoofakind <= 2) { twoofakind++; eight -= 2; if (eight < 0) { knight -= -eight; eight = 0; } }
                    else if (seven + knight >= 2 && twoofakind <= 2) { twoofakind++; seven -= 2; if (seven < 0) { knight -= -seven; seven = 0; } }
                    else if (six + knight >= 2 && twoofakind <= 2) { twoofakind++; six -= 2; if (six < 0) { knight -= -six; six = 0; } }
                    else if (five + knight >= 2 && twoofakind <= 2) { twoofakind++; five -= 2; if (five < 0) { knight -= -five; five = 0; } }
                    else if (four + knight >= 2 && twoofakind <= 2) { twoofakind++; four -= 2; if (four < 0) { knight -= -four; four = 0; } }
                    else if (three + knight >= 2 && twoofakind <= 2) { twoofakind++; three -= 2; if (three < 0) { knight -= -three; three = 0; } }
                    else if (two + knight >= 2 && twoofakind <= 2) { twoofakind++; two -= 2; if (two < 0) { knight -= -two; two = 0; } }
                    //else if (knight == 2) { twoofakind++; knight -= 2; }
                }


                ulong score = 0;

                if (ace_tot + knight_tot >= 5)               score = 100000000000;
                else if (king_tot + knight_tot >= 5)         score = 100000000000;
                else if (queen_tot + knight_tot >= 5)        score = 100000000000;
                else if (knight_tot >= 5)                    score = 100000000000;
                else if (ten_tot + knight_tot >= 5)          score = 100000000000;
                else if (nine_tot + knight_tot >= 5)         score = 100000000000;
                else if (eight_tot + knight_tot >= 5)        score = 100000000000;
                else if (seven_tot + knight_tot >= 5)        score = 100000000000;
                else if (six_tot + knight_tot >= 5)          score = 100000000000;
                else if (five_tot + knight_tot >= 5)         score = 100000000000;
                else if (four_tot + knight_tot >= 5)         score = 100000000000;
                else if (three_tot + knight_tot >= 5)        score = 100000000000;
                else if (two_tot + knight_tot >= 5)          score = 100000000000;

                else if (ace_tot + knight_tot >= 4)          score = 90000000000;
                else if (king_tot + knight_tot >= 4)         score = 90000000000;
                else if (queen_tot + knight_tot >= 4)        score = 90000000000;
                else if (knight_tot >= 4)                    score = 90000000000;
                else if (ten_tot + knight_tot >= 4)          score = 90000000000;
                else if (nine_tot + knight_tot >= 4)         score = 90000000000;
                else if (eight_tot + knight_tot >= 4)        score = 90000000000;
                else if (seven_tot + knight_tot >= 4)        score = 90000000000;
                else if (six_tot + knight_tot >= 4)          score = 90000000000;
                else if (five_tot + knight_tot >= 4)         score = 90000000000;
                else if (four_tot + knight_tot >= 4)         score = 90000000000;
                else if (three_tot + knight_tot >= 4)        score = 90000000000;
                else if (two_tot + knight_tot >= 4)          score = 90000000000;

                else if (threeofakind >= 1 && twoofakind >= 1)
                {
                                                score = 80000000000;
                }
                else if (threeofakind >= 1)
                {
                                                score = 70000000000;
                }
                else if (twoofakind >= 2)
                {
                                                score = 60000000000;
                }
                else if (twoofakind >= 1)
                {
                                                score = 50000000000;
                }
                else
                {
                    //if (ace_tot >= 1)           score = 47000000000;
                    //else if (king_tot >= 1)     score = 44000000000;
                    //else if (queen_tot >= 1)    score = 41000000000;
                    //else if (knight_tot >= 1)   score = 38000000000;
                    //else if (ten_tot >= 1)      score = 35000000000;
                    //else if (nine_tot >= 1)     score = 32000000000;
                    //else if (eight_tot >= 1)    score = 29000000000;
                    //else if (seven_tot >= 1)    score = 26000000000;
                    //else if (six_tot >= 1)      score = 23000000000;
                    //else if (five_tot >= 1)     score = 20000000000;
                    //else if (four_tot >= 1)     score = 17000000000;
                    //else if (three_tot >= 1)    score = 14000000000;
                    //else if (two_tot >= 1)      score = 11000000000;
                }


                if (hand[0] == 'A')             score += 1300000000;
                else if (hand[0] == 'K')        score += 1200000000;
                else if (hand[0] == 'Q')        score += 1100000000;
                else if (hand[0] == 'T')        score += 1000000000;
                else if (hand[0] == '9')        score += 900000000;
                else if (hand[0] == '8')        score += 800000000;
                else if (hand[0] == '7')        score += 700000000;
                else if (hand[0] == '6')        score += 600000000;
                else if (hand[0] == '5')        score += 500000000;
                else if (hand[0] == '4')        score += 400000000;
                else if (hand[0] == '3')        score += 300000000;
                else if (hand[0] == '2')        score += 200000000;
                else if (hand[0] == 'J')        score += 100000000;

                if (hand[1] == 'A')             score += 13000000;
                else if (hand[1] == 'K')        score += 12000000;
                else if (hand[1] == 'Q')        score += 11000000;
                else if (hand[1] == 'T')        score += 10000000;
                else if (hand[1] == '9')        score += 9000000;
                else if (hand[1] == '8')        score += 8000000;
                else if (hand[1] == '7')        score += 7000000;
                else if (hand[1] == '6')        score += 6000000;
                else if (hand[1] == '5')        score += 5000000;
                else if (hand[1] == '4')        score += 4000000;
                else if (hand[1] == '3')        score += 3000000;
                else if (hand[1] == '2')        score += 2000000;
                else if (hand[1] == 'J')        score += 1000000;

                if (hand[2] == 'A')             score += 130000;
                else if (hand[2] == 'K')        score += 120000;
                else if (hand[2] == 'Q')        score += 110000;
                else if (hand[2] == 'T')        score += 100000;
                else if (hand[2] == '9')        score += 90000;
                else if (hand[2] == '8')        score += 80000;
                else if (hand[2] == '7')        score += 70000;
                else if (hand[2] == '6')        score += 60000;
                else if (hand[2] == '5')        score += 50000;
                else if (hand[2] == '4')        score += 40000;
                else if (hand[2] == '3')        score += 30000;
                else if (hand[2] == '2')        score += 20000;
                else if (hand[2] == 'J')        score += 10000;

                if (hand[3] == 'A')             score += 1300;
                else if (hand[3] == 'K')        score += 1200;
                else if (hand[3] == 'Q')        score += 1100;
                else if (hand[3] == 'T')        score += 1000;
                else if (hand[3] == '9')        score += 900;
                else if (hand[3] == '8')        score += 800;
                else if (hand[3] == '7')        score += 700;
                else if (hand[3] == '6')        score += 600;
                else if (hand[3] == '5')        score += 500;
                else if (hand[3] == '4')        score += 400;
                else if (hand[3] == '3')        score += 300;
                else if (hand[3] == '2')        score += 200;
                else if (hand[3] == 'J')        score += 100;

                if (hand[4] == 'A')             score += 13;
                else if (hand[4] == 'K')        score += 12;
                else if (hand[4] == 'Q')        score += 11;
                else if (hand[4] == 'T')        score += 10;
                else if (hand[4] == '9')        score += 9;
                else if (hand[4] == '8')        score += 8;
                else if (hand[4] == '7')        score += 7;
                else if (hand[4] == '6')        score += 6;
                else if (hand[4] == '5')        score += 5;
                else if (hand[4] == '4')        score += 4;
                else if (hand[4] == '3')        score += 3;
                else if (hand[4] == '2')        score += 2;
                else if (hand[4] == 'J')        score += 1;



                sets.Add(new Tuple<ulong, ulong, string>(score, bid, hand));
            }

            ulong sum = 0;
            ulong idx = 1;
            sets = sets.OrderBy(t => t.Item1).ToList();
            foreach (var tuple in sets)
            {
                Console.WriteLine($"({tuple.Item3}, {tuple.Item2}, {tuple.Item1})");
                sum += idx * (ulong)tuple.Item2;
                idx++;
            }
            Console.WriteLine(sum);

            file.Close();
        }
    }
}

