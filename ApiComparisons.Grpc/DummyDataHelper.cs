using ApiComparisons.Shared;
using ApiComparisons.Shared.GRPC.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiComparisons.Grpc
{
    public interface IDataHelper
    {
        List<Person> People { get; }
    }

    public class DummyDataHelper : IDataHelper
    {
        private readonly InitializerSettings settings;

        public DummyDataHelper(IOptions<InitializerSettings> options)
        {
            this.settings = options.Value;
            People = Enumerable.Range(1, this.settings.Persons)
                .Select(o => new Person { Name = $"Person {o}", Created = Timestamp.FromDateTime(DateTime.UtcNow) })
                .ToList();
        }

        public List<Person> People { get; }
    }
}
