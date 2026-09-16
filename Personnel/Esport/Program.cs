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