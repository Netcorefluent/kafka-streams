using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Netcorefluent.KafkaStreams
{
    public class KStream<K, V>
    {
        private readonly IObservable<KeyValuePair<K, V>> _observable;

        public KStream()
        {
        }

        public KStream(IObservable<KeyValuePair<K, V>> observable)
        {
            _observable = observable;
        }

        public KStream<K, V1> FlatMapValues<V1>(Func<V, IEnumerable<V1>> processor)
        {
            return new KStream<K, V1>(
                _observable.SelectMany(ys =>
                    processor(ys.Value)
                        .Select(v => new KeyValuePair<K, V1>(ys.Key, v))
                        .ToObservable()
                )
            );
        }

        public KStream<K1, V1> Map<K1, V1>(Func<K, V, KeyValuePair<K1, V1>> mapper)
        {
            return new KStream<K1, V1>(
                _observable.Select(kvp => mapper(kvp.Key, kvp.Value))
            );
        }

        public KTable<K, long> CountByKey(string name)
        {
            return new KTable<K, long>();
        }

        public IObservable<KeyValuePair<K, V>> ToObservable()
        {
            return _observable;
        }
    }
}
