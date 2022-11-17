using BeerSender.Domain.Package;

namespace BeerSender.Domain.Core
{
    public class DataStore
    {
        public readonly Dictionary<Guid, BeerPackage> AggregateDictionary = new();
    }
}
