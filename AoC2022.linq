<Query Kind="Program">
  <Namespace>System.Windows</Namespace>
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"
#load "aoc2022_inputs"

enum End { Lose = 0, Draw = 3, Win = 6 };
enum Hand { Rock = 1, Paper = 2, Sissors = 3 };

void Main()
{
	var elfs = new List<Elf>();

}
static class  Day_2
{
	private static string input => Inputs.Day_2;
	
	public static int A()
	{
		var score = 0;
		foreach (var r in Regex.Split(input, @"\s{2,}"))
		{
			Hand hand_a = ToHand(r[0]), hand_b = ToHand(r[2]);
			var end = HandVsHand(hand_a, hand_b);
			score += Score(end, hand_b);
		}
		return score;
	}
	public static int B()
	{
		var score = 0;
		foreach (var r in Regex.Split(input, @"\s{2,}"))
		{
			var hand_a = ToHand(r[0]);
			var end = ToEnd(r[2]);
			var hand_b = HandVsEnd(hand_a, end);
			score += Score(end, hand_b);
		}
		return score;
	}
	
	private static Hand ToHand(char input) => "AX".Contains(input) ? Hand.Rock : "BY".Contains(input) ? Hand.Paper : Hand.Sissors;
	private static End ToEnd(char input) => input == 'X' ? End.Lose : input == 'Y' ? End.Draw : End.Win;
	private static End HandVsHand(Hand a, Hand b) => (a, b) switch
	{
		(Hand.Rock, Hand.Paper) or (Hand.Paper, Hand.Sissors) or (Hand.Sissors, Hand.Rock) => End.Win,
		(Hand.Rock, Hand.Sissors) or (Hand.Paper, Hand.Rock) or (Hand.Sissors, Hand.Paper) => End.Lose,
		(_, _) => End.Draw
	};
	private static Hand HandVsEnd(Hand a, End b) => (a, b) switch
	{
		(Hand.Rock, End.Draw) or (Hand.Paper, End.Lose) or (Hand.Sissors, End.Win) => Hand.Rock,
		(Hand.Rock, End.Win) or (Hand.Paper, End.Draw) or (Hand.Sissors, End.Lose) => Hand.Paper,
		(Hand.Rock, End.Lose) or (Hand.Paper, End.Win) or (Hand.Sissors, End.Draw) => Hand.Sissors
	};
	private static int Score(End end, Hand hand) => (int)end + (int)hand;
}
static (List<Elf> elfs, (int a, int b) res) Day_1(List<Elf> elfs)
{
	foreach (var e in Regex.Split(Inputs.Day_1, @"\s{3,}"))
	{		
		var elf = new Elf();
		foreach (var str in Regex.Split(e, @"\s{2,}")) elf.Items.Add(Int32.Parse(str));
		elf.ItemSum = elf.Items.Sum();
		elfs.Add(elf);
	}
	elfs = elfs.OrderByDescending(e => e.ItemSum).ToList(); 
	var a = elfs.Select(e => e.ItemSum).FirstOrDefault();
	var b = elfs.Take(3).Sum(e => e.ItemSum);
	
	return (elfs, (a, b));
}

class Elf
{
	public List<int> Items { get; set; } = new List<int>();
	public int ItemSum { get; set; }
}


#region private::Tests
[Fact] void Day_1a() => Assert.True( Day_1(new List<Elf>()).res.a == 70613);
[Fact] void Day_1b() => Assert.True( Day_1(new List<Elf>()).res.b == 205805);
[Fact] void Day_2a() => Assert.True( Day_2.A() == 11449);
[Fact] void Day_2b() => Assert.True( Day_2.B() == 13187);
#endregion