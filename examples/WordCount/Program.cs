using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;

namespace Netcorefluent.KafkaStreams.Examples.WordCount
{
    /*
        Demonstrates, using the high-level KStream DSL, how to implement the WordCount program
        that computes a simple word occurrence histogram from an input text.

        In this example, the input stream reads from a topic named "streams-file-input", where the values of messages
        represent lines of text; and the histogram output is written to topic "streams-wordcount-output" where each record
        is an updated count of a single word.

        Before running this example you must create the source topic (e.g. via bin/kafka-topics.sh --create ...)
        and write some data to it (e.g. via bin-kafka-console-producer.sh). Otherwise you won't see any data arriving in the output topic.
    */
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Config();
            var builder = new KStreamBuilder();

            var source = builder.Stream<string, string>("streams-file-input");

            var counts = source
                .FlatMapValues(value => value.ToLower().Split(" "))
                .Map((key, value) => KeyValuePair.Create(key, value))
                .CountByKey("Counts");

            counts.To("streams-wordcount-output");

            var streams = new KafkaStreams(builder, config);
            streams.Start();

            Thread.Sleep(5000);

            streams.Close();
        }
    }
}
