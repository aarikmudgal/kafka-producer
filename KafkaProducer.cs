using System;

namespace kafkaProducer
{
    class KafkaProducer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your message. Enter q to Quit");

            var message = default(string);

            while((message = Console.ReadLine()) != "q")
            {
                var producer = new SimpleProducer();
                producer.Produce(message);
            }
            

        }
    }
}
