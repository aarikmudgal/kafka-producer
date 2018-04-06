using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace kafkaProducer
{
    class SimpleProducer : ISimpleProducer
    {
        public void Produce(string message)
        {
            var config = new Dictionary<string, object>
            {
                {"bootstrap.servers", "35.200.218.194:9092" }
            };

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                producer.ProduceAsync("simpletest", null, message).GetAwaiter().GetResult();
                producer.Flush(100);
            };
        }
    }
}
