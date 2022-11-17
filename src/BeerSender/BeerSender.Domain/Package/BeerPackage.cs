using BeerSender.Domain.Core;

namespace BeerSender.Domain.Package;

public class BeerPackage 
{
    private Guid _packageId;
    private ShippingLabel _shippingLabel;

    public void Apply(object @event)
    {
        switch (@event)
        {
            case PackageCreated packageCreated:
                Created(packageCreated);
                return;
            case AddShippingLabel shippingLabelAdded:
                LabelAdded(shippingLabelAdded);
                return;
        }
    }

    private void LabelAdded(AddShippingLabel @event)
    {
        _shippingLabel = @event.Label;
    }

    private void Created(PackageCreated @event)
    {
        _packageId = @event.PackageId;
    }

}