var input = File.ReadAllLines("Input.txt");
var rows = input
    .Select(x => x.Split(" "))
    .Select(x => x.Select(int.Parse)
        .ToList())
    .ToList();

var sum = 0;

foreach (var row in rows)
{
    List<bool> isSafe = [];
    List<int> safeNumbers = [];
    
    for (int i = 0; i < row.Count; i++)
    {
        var i1 = i;
        var pairs = row.Where((_, n) => n != i1).Zip(row.Where((_, n) => n != i1).Skip(1), (first, second) => (first, second));
        var deviations = pairs.Select(x => x.first - x.second).ToList();
        isSafe.Add(IsSafe(deviations));
    }

    if (isSafe.Any(x => x))
    {
        sum += 1;
    }
    
    isSafe.Clear();
    safeNumbers.Clear();
}

Console.WriteLine(sum);
return;

bool IsSafe(List<int> numbers) =>
    numbers.All(x => x is >= 1 and <= 3) || numbers.All(x => x is >= -3 and <= -1);