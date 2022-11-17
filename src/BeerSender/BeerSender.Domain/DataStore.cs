using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerSender.Domain
{
    public class DataStore
    {
        public readonly Dictionary<Guid, BeerPackage> AggregateDictionary = new();
    }
}
