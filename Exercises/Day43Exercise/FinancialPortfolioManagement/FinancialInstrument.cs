namespace FinancialPortfolioManagement;

abstract class FinancialInstrument
{
    private decimal _units;
    private decimal _purchasePrice;
    private decimal _marketPrice;
    private string _currency;

    public string Id { get; set; }
    public string Name { get; set; }

    public string Currency 
    {
        get => _currency; 
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 3)
                throw new InvalidFinancialDataException("Currency must be 3-letter code.");
            _currency = value.ToUpper();   
        }
    }

    public DateTime PurchaseDate { get; set; }

    public decimal Units
    {
        get => _units;
        set 
        {
            if (value < 0)
                throw new InvalidFinancialDataException("Quantity cannot be negative.");
            _units = value; 
        }
    }

    public decimal PurchasePrice
    {
        get => _purchasePrice;
        set 
        {
            if (value < 0)
                throw new InvalidFinancialDataException();
            _purchasePrice = value; 
        }
    }

    public decimal MarketPrice
    {
        get => _marketPrice;
        set 
        {
            if (value < 0)
                throw new InvalidFinancialDataException(); 
            _marketPrice = value;
        }
    }

    private FinancialInstrument() 
    { 
        Id = string.Empty; 
        Name = string.Empty; 
        _currency = string.Empty; 
    }

    protected FinancialInstrument(
        string id, 
        string name, 
        string currency, 
        DateTime purchaseDate, 
        decimal units, 
        decimal purchasePrice, 
        decimal marketPrice
    ) : this()
    {
        Id = id;
        Name = name;
        Currency = currency;
        PurchaseDate = purchaseDate;
        Units = units;
        PurchasePrice = purchasePrice;
        MarketPrice = marketPrice;
    }

    public abstract decimal CalculateCurrentValue();

    public virtual string GetInstrumentSummary() =>
        string.Concat(
            $"Id: {Id};",
            $"Name: {Name};",
            $"Currency: {Currency};",
            $"Purchase Date: {PurchaseDate}",
            $"Units: {Units};",
            $"Purchase Price: {PurchaseDate:C2}",
            $"Market Price: {MarketPrice:C2}"
        );
}

class Equity(
    string id, 
    string name, 
    string currency,
    DateTime purchaseDate,
    decimal units,
    decimal purchasePrice,
    decimal marketPrice
) : FinancialInstrument(id, name, currency, purchaseDate, units, purchasePrice, marketPrice),
    IRiskAssessable,
    IReportable
{
    public override decimal CalculateCurrentValue()
        => Units * MarketPrice;

    public string GenerateReportLine()
        => $"[Equity] {GetInstrumentSummary()}";

    public string GetRiskCategory() => "HIGH";
}

class Bond(
    string id,
    string name,
    string currency,
    DateTime purchaseDate,
    decimal units,
    decimal purchasePrice,
    decimal marketPrice
) : FinancialInstrument(id, name, currency, purchaseDate, units, purchasePrice, marketPrice)
{
    public override decimal CalculateCurrentValue()
        => Units * MarketPrice;
}

class FixedDeposit(
    string id,
    string name,
    string currency,
    DateTime purchaseDate,
    decimal units,
    decimal purchasePrice,
    decimal marketPrice
) : FinancialInstrument(id, name, currency, purchaseDate, units, purchasePrice, marketPrice)
{
    public override decimal CalculateCurrentValue()
        => Units * MarketPrice;
}

class MutualFund(
    string id,
    string name,
    string currency,
    DateTime purchaseDate,
    decimal units,
    decimal purchasePrice,
    decimal marketPrice
) : FinancialInstrument(id, name, currency, purchaseDate, units, purchasePrice, marketPrice),
    IRiskAssessable,
    IReportable
{
    public override decimal CalculateCurrentValue()
        => Units * MarketPrice;

    public string GenerateReportLine()
        => $"[Mutual Fund] {GetInstrumentSummary()}";

    public string GetRiskCategory() => "MEDIUM";
}
