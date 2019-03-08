using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Netcorefluent.KafkaStreams
{
    /// <summary>
    /// <see cref="KStream"/> is an abstration of a record stream of key-value pairs.
    /// <para>
    /// A <see cref="KStream"/> is either defined from one or multiple Kafka topics that are consumed message by message or
    /// the result of a <see cref="KStream"/> transformation. A <see cref="KTable"/> can also be converted into a <see cref="KStream"/>.
    /// </para>
    /// <para>
    /// A <see cref="KStream"/> can be transformed record by record, joined with another <see cref="KStream"/> or <see cref="KTable"/>, or
    /// can be aggregated into a <see cref="KTable"/>.
    /// </para>
    /// <para>
    /// <seealso cref="KTable"/>
    /// </para>
    /// </summary>
    /// <typeparam name="K">Type of keys</typeparam>
    /// <typeparam name="V">Type of values</typeparam>
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

        /// <summary>
        /// Create a new instance of <see cref="KStream"/> that consists of all elements of this stream which satisfy a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>a <see cref="KStream"/> that contains only those records that satisfy the given predicate</returns>
        public KStream<K, V> Filter(Func<K, V, bool> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new instance of <see cref="KStream"/> that consists of all elements of this stream which do not satisfy a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>a <see cref="KStream"/> that contains only those records that do not satisfy the given predicate</returns>
        public KStream<K, V> FilterNot(Func<K, V, bool> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new key from the current key and value
        /// </summary>
        /// <param name="mapper"></param>
        /// <typeparam name="K1">the new key type on the stream</typeparam>
        /// <returns>a <see cref="KStream"/> that contains records with different key type and same value type</returns>
        public KStream<K1, V> SelectKey<K1>(Func<K, V, K1> mapper)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new instance of <see cref="KStream"/> by transforming each element in this stream into a different element in the new stream.
        /// </summary>
        /// <param name="mapper"></param>
        /// <typeparam name="K1">the key type of the new stream</typeparam>
        /// <typeparam name="V1">the value type of the new stream</typeparam>
        /// <returns>a <see cref="KStream"/> that contains records with new key and value type</returns>
        public KStream<K1, V1> Map<K1, V1>(Func<K, V, KeyValuePair<K1, V1>> mapper)
        {
            return new KStream<K1, V1>(
                _observable.Select(kvp => mapper(kvp.Key, kvp.Value))
            );
        }

        /// <summary>
        /// Create a new instance of <see cref="KStream"/> by transforming the value of each element in this stream into a new value in the new stream.
        /// </summary>
        /// <param name="mapper"></param>
        /// <typeparam name="V1">the value type of the new stream</typeparam>
        /// <returns></returns>
        public KStream<K, V1> MapValues<V1>(Func<V, V1> mapper)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prints the elements of the stream.
        /// </summary>
        public void Print()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Write the elements of this stream to a file at the given path.
        /// </summary>
        /// <param name="filePath">name of file to write to</param>
        public void WriteAsText(String filePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new instance of <see cref="KStream"/> by transforming each element in this stream into zero or more elements in the new stream.
        /// </summary>
        /// <param name="mapper"></param>
        /// <typeparam name="K1">the key type of the new stream</typeparam>
        /// <typeparam name="V1">the value type of the new stream</typeparam>
        /// <returns>a <see cref="KStream"/> that contains more or less records with new key and value type</returns>
        public KStream<K1, V1> FlatMap<K1, V1>(Func<K, V, IEnumerable<KeyValuePair<K1, V1>>> mapper)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new instance of <see cref="KStream"/> by transforming the value of each element in this stream into zero or more values with the same key in the new stream.
        /// </summary>
        /// <param name="processor"></param>
        /// <typeparam name="V1">the value type of the new stream</typeparam>
        /// <returns>a <see cref="KStream"/> that contains more or less records with unmodified keys and new values of different type</returns>
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

        /// <summary>
        /// Creates an array of <see cref="KStream"/> from this stream by branching the elements in the original stream based on the supplied predicates.
        /// Each element is evaluated against the supplied predicates, and predicates are evaluated in order. Each stream in the result array
        /// corresponds position-wise (index) to the predicate in the supplied predicates. The branching happens on first-match: An element
        /// in the original stream is assigned to the corresponding result stream for the first predicate that evaluates to true, and
        /// assigned to this stream only. An element will be dropped if none of the predicates evaluate to true.
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns>multiple distinct substreams of this <see cref="KStream"/></returns>
        public KStream<K, V>[] Branch(Func<K, V, bool>[] predicates)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Materialize this stream to a topic, also creates a new instance of a <see cref="KStream"/> from the topic.
        /// </summary>
        /// <param name="topic">the topic name</param>
        /// <returns>a <see cref="KStream"/> that contains the exact same records as this <see cref="KStream"/></returns>
        public KStream<K, V> Through(string topic)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Perform an action on each element of <see cref="KStream"/>.
        /// Note that this is a terminal operation that returns void.
        /// </summary>
        /// <param name="action"></param>
        public void Foreach(Func<K, V> action)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Materialize this stream to a topic.
        /// </summary>
        /// <param name="topic">the topic name</param>
        public void To(string topic)
        {
            throw new NotImplementedException();
        }

        public KTable<K, long> CountByKey(string name)
        {
            return new KTable<K, long>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IObservable<KeyValuePair<K, V>> ToObservable()
        {
            return _observable;
        }

        // TODO: implement the not implemented methods above
        // TODO: add the stateful transformations once we've figured out how to do those.
        // DSL is based off of https://github.com/apache/kafka/blob/0.10.0/streams/src/main/java/org/apache/kafka/streams/kstream/KStream.java
    }
}
