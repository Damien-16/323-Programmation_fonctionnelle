using DataSerie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esport
{
    public static class MatchGenerator
    {
        public static DataSeries<Cs2Match> GenerateCs2(string player, int count, int seed = 42)
        {
            var rng = new Random(seed);
            var maps = new[] { "Dust2", "Mirage", "Inferno", "Nuke", "Ancient" };
            var sides = new[] { "CT", "T" };
            var start = new DateTime(2023, 9, 1);

            return DataSeries<Cs2Match>.From(
                Enumerable.Range(1, count).Select(i => new DataPoint<Cs2Match>(
                    start.AddDays(i),
                    new Cs2Match(player, maps[rng.Next(maps.Length)], sides[rng.Next(2)], rng.Next(10, 28), rng.Next(6, 18), rng.Next(0, 8), rng.Next(0, 5), rng.Next(2) == 0)
                    )
                )
            );
        }
    }
}
