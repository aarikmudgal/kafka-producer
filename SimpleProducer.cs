using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using System.Threading.Tasks;
using Confluent.Kafka.Serialization;

namespace kafkaProducer
{
    class SimpleProducer : ISimpleProducer
    {
        public void Produce(string message)
        {
            Console.WriteLine("Inside Produce method");
            var config = new Dictionary<string, object>
            {
                {"bootstrap.servers", "35.200.241.185:9092" },
                { "produce.offset.report", true }
            };

            Console.WriteLine("Config set");
             try
           {
               using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
               {
                   producer.OnLog += Loggers.ConsoleLogger;
                   producer.OnError += Producer_OnError;
                   var result = producer.ProduceAsync("simpletest", null, message).Result;
                   Console.WriteLine($"----- Delivered '{result.Value}' to: {result.TopicPartitionOffset}");
               };
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message + ". Inner Exception - " + ex.InnerException.Message);
           }
        }
        private void Producer_OnError(object sender, Error e)
        {
            Console.WriteLine($"ERROR ****** Error code: {e.Code}, Reason: {e.Reason} ***** ");
        }
    }
}
