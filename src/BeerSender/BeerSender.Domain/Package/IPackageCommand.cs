using BeerSender.Domain.Core;

namespace BeerSender.Domain.Package;

public interface IPackageCommand : ICommand
{
    Guid PackageId { get; }
}