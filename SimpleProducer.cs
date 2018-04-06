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
                {"bootstrap.servers", "35.200.241.185:9092" }
            };

            try
           {
               using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
               {
                   //producer.ProduceAsync("simpletest", null, message).GetAwaiter().GetResult();
                   //producer.Flush(100);
                   var dr = producer.ProduceAsync("simpletest", null, message).Result;
                   Console.WriteLine($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset}");
                   //producer.Flush(100);
               };
           }
           catch (Exception ex)
           {

               Console.WriteLine(ex.Message + ". Inner Exception - " + ex.InnerException.Message);
           }
        }
    }
}
