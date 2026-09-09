using System;
using DataSerie;
using Esport;

var valorant = DataSeries<ValorantMatch>.FromCsv("data/valorant.csv", ParseValorant);
var cs2 = DataSeries<Cs2Match>.FromCsv("data/cs2.csv", ParseCs2);
var lol = DataSeries<LolMatch>.FromCsv("data/lol.csv", ParseLol);

var valorantMatch = new[]
{
    new DataPoint<ValorantMatch>(new DateTime(2024, 1, 15), new ValorantMatch("Léa", "Jett",  18, 6, 4, 8,  13, true)),
    new DataPoint<ValorantMatch>(new DateTime(2024, 1, 15), new ValorantMatch("Léa", "Reyna", 22, 8, 2, 11,  9, false)),
    new DataPoint<ValorantMatch>(new DateTime(2024, 1, 15), new ValorantMatch("Léa", "Neon",  20, 7, 5,  9, 13, true)),
};

var cs2Match = new[]
{
    new DataPoint<Cs2Match>(new DateTime(2024, 1, 15), new Cs2Match("Raphaël", "Mirage",  "CT", 21, 14, 5, 2, true)),
    new DataPoint<Cs2Match>(new DateTime(2024, 1, 15), new Cs2Match("Kiara",   "Dust2",   "T",  26, 11, 1, 4, true)),
    new DataPoint<Cs2Match>(new DateTime(2024, 1, 15), new Cs2Match("Raphaël", "Inferno", "T",  14, 16, 6, 1, false)),
};

var lolMatch = new[]
{
    new DataPoint<LolMatch>(new DateTime(2024, 1, 15), new LolMatch("Noé", "Thresh", 2, 4, 18, 42, 71, true)),
    new DataPoint<LolMatch>(new DateTime(2024, 1, 15), new LolMatch("Noé", "Thresh", 1, 6, 12, 35, 64, false)),
};
ValorantMatch ParseValorant(string[] cols) => new ValorantMatch(
    cols[1],              // player
    cols[2],              // agent
    int.Parse(cols[3]),   // kills
    int.Parse(cols[4]),   // deaths
    int.Parse(cols[5]),   // assists
    int.Parse(cols[6]),   // headshots
    int.Parse(cols[7]),   // roundsWon
    bool.Parse(cols[8])   // won
);

Cs2Match ParseCs2(string[] cols) => new Cs2Match(
    cols[1],              // player
    cols[2],              // map
    cols[3],              // startSide (côté joué en 1re mi-temps — CT ou T)
    int.Parse(cols[4]),   // kills
    int.Parse(cols[5]),   // deaths
    int.Parse(cols[6]),   // assists
    int.Parse(cols[7]),   // mvps
    bool.Parse(cols[8])   // won
);

LolMatch ParseLol(string[] cols) => new LolMatch(
    cols[1],              // player
    cols[2],              // champion
    int.Parse(cols[4]),   // kills
    int.Parse(cols[5]),   // deaths
    int.Parse(cols[6]),   // assists
    int.Parse(cols[7]),   // cs
    int.Parse(cols[8]),   // visionScore
    bool.Parse(cols[9])   // won
);

Console.WriteLine(valorant.Count);


Console.WriteLine(cs2.Count);


Console.WriteLine(lol.Count);

Console.ReadKey();