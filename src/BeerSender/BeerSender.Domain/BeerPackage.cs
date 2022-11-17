namespace BeerSender.Domain;

public interface IHandleCommand<in TCommand> where TCommand : ICommand
{
    IEnumerable<object> Handle(TCommand command);
}

public class BeerPackage :
    IHandleCommand<CreatePackage>,
    IHandleCommand<AddShippingLabel>
{
    private Guid _packageId;
    private ShippingLabel _shippingLabel;

    public IEnumerable<object> Handle(CreatePackage command)
    {
        yield return new PackageCreated(command.PackageId);
    }

    public IEnumerable<object> Handle(AddShippingLabel command)
    {
        if (string.IsNullOrEmpty(command.Label.ShippingCode))
        {
            yield return new ShippingLabelFailedToAdd(command.PackageId, command.Label,
                AddShippingLabelFailureReason.MissingTrackingCode);
            yield break;
        }

        yield return new ShippingLabelAdded(command.PackageId, command.Label);
    }

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