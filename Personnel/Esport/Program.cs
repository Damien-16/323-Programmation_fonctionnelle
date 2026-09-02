using System;
using DataSerie;
using Esport;

var valorantMatches = new[]
{
    new DataPoint<ValorantMatch>(new DateTime(2024, 1, 15), new ValorantMatch("Léa", "Jett",  18, 6, 4, 8,  13, true)),
    new DataPoint<ValorantMatch>(new DateTime(2024, 1, 15), new ValorantMatch("Léa", "Reyna", 22, 8, 2, 11,  9, false)),
    new DataPoint<ValorantMatch>(new DateTime(2024, 1, 15), new ValorantMatch("Léa", "Neon",  20, 7, 5,  9, 13, true)),
};

var cs2Matches = new[]
{
    new DataPoint<Cs2Match>(new DateTime(2024, 1, 15), new Cs2Match("Raphaël", "Mirage",  "CT", 21, 14, 5, 2, true)),
    new DataPoint<Cs2Match>(new DateTime(2024, 1, 15), new Cs2Match("Kiara",   "Dust2",   "T",  26, 11, 1, 4, true)),
    new DataPoint<Cs2Match>(new DateTime(2024, 1, 15), new Cs2Match("Raphaël", "Inferno", "T",  14, 16, 6, 1, false)),
};

var lolMatches = new[]
{
    new DataPoint<LolMatch>(new DateTime(2024, 1, 15), new LolMatch("Noé", "Thresh", 2, 4, 18, 42, 71, true)),
    new DataPoint<LolMatch>(new DateTime(2024, 1, 15), new LolMatch("Noé", "Thresh", 1, 6, 12, 35, 64, false)),
};

var valorant = DataSeries<ValorantMatch>.From(valorantMatches);
Console.WriteLine(valorant.Count);

var cs2 = DataSeries<Cs2Match>.From(cs2Matches);
Console.WriteLine(cs2.Count);

var lol = DataSeries<LolMatch>.From(lolMatches);
Console.WriteLine(lol.Count);

Console.ReadKey();