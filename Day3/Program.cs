using System.Text.RegularExpressions;

var input = File.ReadAllText("Input.txt");

// Part 1
var p1Matches =
    new Regex(@"mul\((?<x>\d+),(?<y>\d+)\)")
        .Matches(input)
        .Select(x => (int.Parse(x.Groups["x"].Value), int.Parse(x.Groups["y"].Value)))
        .Sum(x => x.Item1 * x.Item2);

Console.WriteLine($"Part 1 result: {p1Matches}");

// Part 2
var regex = new Regex(@"mul\((?<x>\d+),(?<y>\d+)\)|(?<do>do\(\))|(?<dont>don't\(\))");
var p2Matches = regex.Matches(input)
    .Select<Match, Instruction>(x =>
    {
        if (x.Groups["do"].Success)
            return new Do();
        if (x.Groups["dont"].Success)
            return new Dont();
        return new Mul(int.Parse(x.Groups["x"].Value), int.Parse(x.Groups["y"].Value));
    });

var result = p2Matches.Aggregate((Sum: 0, Pass: true), (acc, ins) =>
{
    return ins switch
    {
        Mul m when acc.Pass => (acc.Sum + m.X * m.Z, acc.Pass),
        Do => (acc.Sum, true),
        Dont => (acc.Sum, false),
        _ => acc
    };
}).Sum;

Console.WriteLine($"Part 2 result: {result}");

internal abstract record Instruction;
internal record Mul(int X, int Z) : Instruction;
internal record Do() : Instruction;
internal record Dont() : Instruction;