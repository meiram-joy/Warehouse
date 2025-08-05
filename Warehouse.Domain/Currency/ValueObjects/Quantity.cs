using CSharpFunctionalExtensions;

namespace Warehouse.Domain.Currency.ValueObjects;

public class Quantity : ValueObject
{
    public decimal Value { get; }
    private Quantity(decimal value)
    {
        if (value < 0) throw new ArgumentException("Quantity cannot be negative");
        Value = value;
    }
    
    public static Quantity operator +(Quantity a, Quantity b) => new(a.Value + b.Value);
    
    public static Quantity operator -(Quantity a, Quantity b)
    {
        if (a.Value < b.Value) throw new InvalidOperationException("Insufficient quantity");
        return new(a.Value - b.Value);
    }
    
    public static Quantity Create(decimal value)
    {
        if (value < 0) throw new ArgumentException("Quantity cannot be negative");
        return new Quantity(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public override string ToString() => Value.ToString("0.##");
}