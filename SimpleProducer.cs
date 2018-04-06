﻿using System;
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
            Console.WriteLine("Inside Produce method");
            var config = new Dictionary<string, object>
            {
                {"bootstrap.servers", "35.200.241.185:9092" }
            };

            Console.WriteLine("Config set");
            try
           {
               using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
               {
                    Console.WriteLine($"Config {config["bootstrap.servers"].ToString()}");
                   //producer.ProduceAsync("simpletest", null, message).GetAwaiter().GetResult();
                   //producer.Flush(100);
                   
                   //var dr = producer.ProduceAsync("simpletest", null, message).Result;
                   var deliveryReport = producer.ProduceAsync("simpletest", null, message);
                    deliveryReport.ContinueWith(task =>
                    {
                        Console.WriteLine($"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}, Error: {task.Result.Error}");
                    });
                   Console.WriteLine($"ProduceAsync called...{deliveryReport}");
                   //Console.WriteLine($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset}");
                   producer.Flush(TimeSpan.FromSeconds(10));
               };
           }
           catch (Exception ex)
           {

               Console.WriteLine(ex.Message + ". Inner Exception - " + ex.InnerException.Message);
           }
        }
    }
}
