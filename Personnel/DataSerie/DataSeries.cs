using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSerie
{
    public class DataSeries<T>
    {
        private readonly IEnumerable<DataPoint<T>> _data;

        private DataSeries(IEnumerable<DataPoint<T>> data) => _data = data;

        public static DataSeries<T> From(IEnumerable<DataPoint<T>> source) => new DataSeries<T>(source);

        public int Count => _data.Count();
        public IEnumerable<DataPoint<T>> Values => _data;
    }
}
