using BeerSender.Domain.Package;

namespace BeerSender.Tests;

public class PackageTests : BeerSenderTestBase
{
    [Fact]
    public void CreatePackageSucceeds()
    {
        var packageId = Guid.NewGuid();
        
        Given();

        When(
            new CreatePackage.Command(packageId)
            );

        Expect(
            new PackageCreated(packageId)
            );
    }

    [Fact]
    public void AddLabelSucceeds()
    {
        var packageId = Guid.NewGuid();

        Given();

        var label = new ShippingLabel("abc123", Carrier.DHL);
        When(
            new AddShippingLabel.Command(packageId, label)
        );

        Expect(
            new ShippingLabelAdded(packageId, label)
        );
    }

    [Fact]
    public void AddLabelFails()
    {
        var packageId = Guid.NewGuid();

        Given();

        var label = new ShippingLabel(string.Empty, Carrier.DHL);
        When(
            new AddShippingLabel.Command(packageId, label)
        );

        Expect(
            new ShippingLabelFailedToAdd(packageId, label, AddShippingLabelFailureReason.MissingTrackingCode)
        );
    }
}