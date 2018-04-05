using System;
using System.Collections.Generic;
using System.Text;

namespace kafkaProducer
{
    interface ISimpleProducer
    {
        void Produce(string message);
    }
}
