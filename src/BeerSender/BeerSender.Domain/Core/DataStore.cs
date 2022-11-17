using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerSender.Domain.Package;

namespace BeerSender.Domain.Core
{
    public class DataStore
    {
        public readonly Dictionary<Guid, BeerPackage> AggregateDictionary = new();
    }
}
