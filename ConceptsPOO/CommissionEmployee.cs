namespace ConceptsPOO;
public class CommissionEmployee : Employee
{
    public float CommissionPercentaje { get; set; }

    public decimal Sales { get; set; }

    public override decimal GetValueToPay()
    {
        return (decimal)CommissionPercentaje * Sales;
    }

    public override string ToString()
    {
        return $"{base.ToString()}"+
        $"\n\tCommission percentaje: {$"{CommissionPercentaje:P2}", 15}"+
        $"\n\tSale: {$"{Sales:C2}", 15}\n\tValue to pay: {$"{GetValueToPay():C2}", 15}";
    }
}
