using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Xunit;

namespace Netcorefluent.KafkaStreams.Test
{
    public class KStreamTest
    {
        private readonly KeyValuePair<Unit, Unit> DEFAULT_KEY_VALUE_PAIR = KeyValuePair.Create(Unit.Default, Unit.Default);
        [Fact]
        public void KStream_ConstructFromObservable()
        {
            var observable = Observable.Return(DEFAULT_KEY_VALUE_PAIR);

            var testObject = new KStream<Unit, Unit>(observable);
        }

        [Fact]
        public async void ToObservable()
        {
            var observable = Observable.Return(DEFAULT_KEY_VALUE_PAIR);

            var testObject = new KStream<Unit, Unit>(observable);

            var actual = testObject.ToObservable();

            Assert.Equal(await actual.SingleAsync(), DEFAULT_KEY_VALUE_PAIR);
        }

        [Fact]
        public void FlatMapValues_ReturnsNewKStream()
        {
            var observable = Observable.Return(DEFAULT_KEY_VALUE_PAIR);
            var testObject = new KStream<Unit, Unit>(observable);
            var actual = testObject.FlatMapValues(x => new[] { x });
            Assert.NotSame(testObject, actual);
        }

        [Fact]
        public async void FlatMapValues_AppliesProcessorToStream()
        {
            var observable = Observable.Return(KeyValuePair.Create(Unit.Default, "3"));
            var testObject = new KStream<Unit, string>(observable);

            var expected = new[] {
                KeyValuePair.Create(Unit.Default, 1),
                KeyValuePair.Create(Unit.Default, 2),
                KeyValuePair.Create(Unit.Default, 3)
            };

            var actual = await testObject
                .FlatMapValues(x => Enumerable.Range(1, int.Parse(x)))
                .ToObservable()
                .ToList().SingleAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Map_ReturnsNewKStream()
        {
            var observable = Observable.Return(DEFAULT_KEY_VALUE_PAIR);
            var testObject = new KStream<Unit, Unit>(observable);
            var actual = testObject.Map((k, v) => KeyValuePair.Create(k, v));
            Assert.NotSame(testObject, actual);
        }

        [Fact]
        public async void Map_AppliesMapperToStream()
        {
            var observable = new[] {
                KeyValuePair.Create("hello", "WORLD"),
                KeyValuePair.Create("it's", "ME")
            }.ToObservable();
            var testObject = new KStream<string, string>(observable);

            var expected = new[] {
                KeyValuePair.Create("HELLO", "world"),
                KeyValuePair.Create("IT'S", "me")
            };

            var actual = await testObject
                .Map((k, v) =>
                    KeyValuePair.Create(k.ToUpper(), v.ToLower())
                )
                .ToObservable()
                .ToList()
                .SingleAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void Map_AppliesMapperToStream_WithTypesDifferentFromInput()
        {
            var observable = new[] {
                KeyValuePair.Create("hello", "WORLD"),
                KeyValuePair.Create("it's", "ME")
            }.ToObservable();
            var testObject = new KStream<string, string>(observable);

            var expected = new[] {
                KeyValuePair.Create(5, 5),
                KeyValuePair.Create(4, 2)
            };

            var actual = await testObject
                .Map((k, v) =>
                    KeyValuePair.Create<int, int>(k.Length, v.Length)
                )
                .ToObservable()
                .ToList()
                .SingleAsync();

            Assert.Equal(expected, actual);
        }
    }
}
