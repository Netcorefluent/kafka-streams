using System;
using System.Collections.Generic;

namespace Netcorefluent.KafkaStreams
{
    public class KStream<K, V>
    {
        public KStream<K, V> FlatMapValues(Func<V, IEnumerable<V>> processor)
        {
            return new KStream<K, V>();
        }

        public KStream<K, V> Map(Func<K, V, KeyValuePair<K, V>> mapper)
        {
            return new KStream<K, V>();
        }

        public KTable<K, long> CountByKey(string name)
        {
            return new KTable<K, long>();
        }
    }
}
