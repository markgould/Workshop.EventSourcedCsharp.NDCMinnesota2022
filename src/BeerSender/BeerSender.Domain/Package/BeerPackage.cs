namespace BeerSender.Domain.Package;

public class BeerPackage
{
    private const int Capacity = 10;

    private Guid _packageId;
    private ShippingLabel _shippingLabel;
    private List<BeerBottle> _bottles = new();

    public int RemainingCapacity => Capacity - _bottles.Count;


    public void Apply(object @event)
    {
        switch (@event)
        {
            case PackageCreated packageCreated:
                Created(packageCreated);
                return;
            case ShippingLabelAdded shippingLabelAdded:
                LabelAdded(shippingLabelAdded);
                return;
        }
    }

    private void LabelAdded(ShippingLabelAdded @event)
    {
        _shippingLabel = @event.Label;
    }

    private void Created(PackageCreated @event)
    {
        _packageId = @event.PackageId;
    }

}