<Query Kind="Program">
  <Namespace>System.Windows</Namespace>
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"
#load "aoc2022_inputs"

void Main()
{
	var elfs = new List<Elf>();
}

static ( List<Elf> elfs, (int a, int b) res) Day_1(List<Elf> elfs)
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
#endregion