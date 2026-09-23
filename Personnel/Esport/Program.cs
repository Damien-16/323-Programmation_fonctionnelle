using System;
using System.Runtime.Intrinsics.Arm;
using System.Linq;
using DataSerie;
using Esport;

var valorant = DataSeries<ValorantMatch>.FromCsv("data/valorant.csv", ParseValorant);
var cs2 = DataSeries<Cs2Match>.FromCsv("data/cs2.csv", ParseCs2);
var lol = DataSeries<LolMatch>.FromCsv("data/lol.csv", ParseLol);

ValorantMatch ParseValorant(string[] cols) => new ValorantMatch(
    cols[1],              // player
    cols[2],              // agent
    int.Parse(cols[3]),   // kills
    int.Parse(cols[4]),   // deaths
    int.Parse(cols[5]),   // assists
    int.Parse(cols[6]),   // headshots
    int.Parse(cols[7]),   // roundsWon
    bool.Parse(cols[8]),   // won
    DateTime.Parse(cols[0])
);

Cs2Match ParseCs2(string[] cols) => new Cs2Match(
    cols[1],              // player
    cols[2],              // map
    cols[3],              // startSide (côté joué en 1re mi-temps — CT ou T)
    int.Parse(cols[4]),   // kills
    int.Parse(cols[5]),   // deaths
    int.Parse(cols[6]),   // assists
    int.Parse(cols[7]),   // mvps
    bool.Parse(cols[8]),  // won
    DateTime.Parse(cols[0])
);

LolMatch ParseLol(string[] cols) => new LolMatch(
    cols[1],              // player
    cols[2],              // champion
    int.Parse(cols[4]),   // kills
    int.Parse(cols[5]),   // deaths
    int.Parse(cols[6]),   // assists
    int.Parse(cols[7]),   // cs
    int.Parse(cols[8]),   // visionScore
    bool.Parse(cols[9]),   // won
    DateTime.Parse(cols[0])
);

var baaad = valorant.Outliers(m => m.Kills < 0);
Console.WriteLine($"Total matchs : {valorant.Count}");
Console.WriteLine($"Anomalies trouvées : {baaad.Count}");

var cleanValorant = valorant.Sanitize(m =>
    m.Kills < 0 || m.Kills > 50 ||
    m.Deaths < 0 || m.Deaths > 30 ||
    m.Assists < 0
);
var cleanCs2 = cs2.Sanitize(m =>
    m.Kills + m.Assists > 50 ||
    m.Deaths < 0
);
var cleanLol = lol.Sanitize(m =>
    m.Kills > 10 ||
    m.Deaths < 1 ||
    m.Assists < 0 ||
    m.Cs < 0
);
Console.WriteLine($"Valorant original : {valorant.Count}"); // 25
var clean = valorant.Sanitize(m => m.Kills < 0);
Console.WriteLine($"Valorant clean    : {clean.Count}");    // 25

Func<Cs2Match, bool> isValid = m =>
    m.Kills + m.Assists <= 50 &&
    m.Deaths >= 1;

if (args.Contains("--generate"))
{
    var target = args[Array.IndexOf(args, "--generate") + 1];
    var players = target == "all"
        ? new[] { "Raphaël", "Kiara", "Dylan", "Noé" }
        : new[] { target };

    foreach (var player in players)
    {
        var series = MatchGenerator.GenerateCs2(player, 20);
        ExportCs2(series.Filter(isValid), $"{player.ToLower()}_generated.csv");
        Console.WriteLine($"{player} : données générées et exportées");
    }
    return;
}

void ExportCs2(DataSeries<Cs2Match> matches, string path)
{
    var header = "date,player,map,start_side,kills,deaths,assists,mvps,won";
    string[] lines = matches.Values.Select(dp =>
        $"{dp.Timestamp:yyyy-MM-dd},{dp.Player},{dp.Map},{dp.StartSide}," +
        $"{dp.Kills},{dp.Deaths},{dp.Assists},{dp.Mvps},{dp.Won.ToString().ToLower()}"
        ).ToArray();
    File.WriteAllLines(path, lines.Prepend(header));
}

Console.WriteLine($"Valorant : {valorant.Count} matchs");
Console.WriteLine($"CS2      : {cs2.Count} matchs");
Console.WriteLine($"LoL      : {lol.Count} matchs");



var raphaelGenerated = MatchGenerator.GenerateCs2("Raphaël", 20);
Console.WriteLine(raphaelGenerated.Count); // 20

//Func<Cs2Match, bool> isValid = m =>
//    m.Kills + m.Assists <= 50 &&
//    m.Deaths >= 1;

//var raphaelValid = DataSeries<Cs2Match>.From(raphaelGenerated.Values.Where(isValid));
//Console.WriteLine($"Avant : {raphaelGenerated.Count}, après : {raphaelValid.Count}");

ExportCs2(raphaelGenerated, "raphael_generated.csv");

Console.ReadKey();