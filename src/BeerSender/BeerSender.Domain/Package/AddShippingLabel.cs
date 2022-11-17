using BeerSender.Domain.Core;

namespace BeerSender.Domain.Package
{
    public record ShippingLabelAdded(Guid PackageId, ShippingLabel Label);
    public record ShippingLabelFailedToAdd(Guid PackageId, ShippingLabel Label, AddShippingLabelFailureReason AddShippingLabelFailureReason);
    public enum AddShippingLabelFailureReason
    {
        MissingTrackingCode,
        InvalidTrackingCode,
        InvalidShipper
    }

    public class AddShippingLabel
    {
        public record Command(Guid PackageId, ShippingLabel Label) : IPackageCommand;

        internal class AddShippingLabelHandler : IHandle<Command>
        {
            public IEnumerable<object> Handle(Command command)
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
}
