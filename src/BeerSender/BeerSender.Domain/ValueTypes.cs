namespace BeerSender.Domain;

public record ShippingLabel(string ShippingCode, Carrier Carrier)
{
    public bool IsValid()
    {
        return ShippingCode.Length > 10;
    }
}

public enum Carrier
{
    UPS,
    FedEx,
    DHL
}

public record BeerBottle(string Brewery, string Name);