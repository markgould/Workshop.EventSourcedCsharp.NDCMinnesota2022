namespace BeerSender.Domain.Package;

// Commands

public record AddBeerToPackage(Guid PackageId, BeerBottle Beer) : IPackageCommand;

// Events

public record BeerAdded(Guid PackageId, BeerBottle Beer);
public record BeerFailedToAdd(Guid PackageId, BeerBottle Beer, AddBeerFailureReason AddBeerFailureReason);

public enum AddBeerFailureReason
{
    PackageFull
}

