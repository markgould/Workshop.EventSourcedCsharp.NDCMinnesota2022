using BeerSender.Domain.Core;

namespace BeerSender.Domain.Package;

public interface IPackageCommand : ICommand
{
    Guid PackageId { get; }
}

// Commands
public record CreatePackage(Guid PackageId) : IPackageCommand;
public record AddShippingLabel(Guid PackageId, ShippingLabel Label) : IPackageCommand;
public record AddBeerToPackage(Guid PackageId, BeerBottle Beer) : IPackageCommand;

// Events
public record PackageCreated(Guid PackageId);
public record BeerAdded(Guid PackageId, BeerBottle Beer);
public record BeerFailedToAdd(Guid PackageId, BeerBottle Beer, AddBeerFailureReason AddBeerFailureReason);
public record ShippingLabelAdded(Guid PackageId, ShippingLabel Label);
public record ShippingLabelFailedToAdd(Guid PackageId, ShippingLabel Label, AddShippingLabelFailureReason AddShippingLabelFailureReason);

public enum AddBeerFailureReason
{
    PackageFull
}

public enum AddShippingLabelFailureReason
{
    MissingTrackingCode,
    InvalidTrackingCode,
    InvalidShipper
}