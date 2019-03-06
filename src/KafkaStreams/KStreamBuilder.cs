namespace Netcorefluent.KafkaStreams
{
    public class KStreamBuilder
    {
        public KStream<K, V> Stream<K, V>(string stream)
        {
            return new KStream<K, V>();
        }
    }
}
