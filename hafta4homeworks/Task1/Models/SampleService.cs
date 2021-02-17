using System;
using Task1.Interfaces;

namespace Task1.Models
{
    public class SampleService : IScopedService, ITransientService, ISingletonService
    {
        Guid _id;

        public SampleService()
        {
            _id = Guid.NewGuid();
        }

        public Guid GetId()
        {
            return _id;
        }
    }
}
