using System.Text;

var part1Input = File.ReadAllLines("Input.txt", Encoding.UTF8);

var firstList = part1Input
    .Select(x =>
    {
        var first = x.Split(" ").FirstOrDefault();
        return int.Parse(first!);
    }).ToList();

var secondList = part1Input
    .Select(x =>
    {
        var second = x.Split(" ").LastOrDefault();
        return int.Parse(second!);
    }).ToList();

firstList = firstList.OrderBy(x => x).ToList();
secondList = secondList.OrderBy(x => x).ToList();
var sum = 0;

for (var i = 0; i < firstList.Count; i++)
{
    var result = Math.Abs(firstList[i] - secondList[i]);
    sum += result;
}

Console.WriteLine($"Sum: {sum}");
var similaritySum = 0;

foreach (var number in firstList)
{
    var count = secondList.Count(x => x == number);
    if (count > 0)
    {
        similaritySum += number * count;
    }
}

Console.WriteLine($"Similarity sum: {similaritySum}");