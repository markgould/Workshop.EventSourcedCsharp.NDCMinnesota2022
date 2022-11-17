using BeerSender.Domain.Core;

namespace BeerSender.Domain.Package.Handlers
{
    internal class AddShippingLabelHandler : IHandle<AddShippingLabel>
    {
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
    }
}
