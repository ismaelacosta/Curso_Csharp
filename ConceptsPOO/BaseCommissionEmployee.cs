namespace ConceptsPOO;
public class BaseCommissionEmployee : CommissionEmployee
{
    public decimal Base { get; set; }

    public override decimal GetValueToPay()
    {
        return base.GetValueToPay() + Base;
    }

    public override string ToString()
    {
        return $"{base.ToString()}\n\t Base : {$"{Base:C2}", 15}" ;
    }
}
