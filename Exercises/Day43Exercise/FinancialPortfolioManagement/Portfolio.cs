namespace FinancialPortfolioManagement;

class Portfolio
{
    private List<FinancialInstrument> _listOfFinancialInstruments = [];
    private Dictionary<string, FinancialInstrument> _dictOfFinancialInstruments = [];

    public void AddInstrument(FinancialInstrument financialInstrument)
    {
        if (_dictOfFinancialInstruments.ContainsKey($"{financialInstrument.Id}"))
            throw new Exception("Duplicate Instrumnet Id.");
        
        _listOfFinancialInstruments.Add(financialInstrument);
        _dictOfFinancialInstruments[financialInstrument.Id] = financialInstrument;
    }

    public void RemoveInstrument(string id)
    {
        if (_dictOfFinancialInstruments.ContainsKey(id))
        {
            var tmp = _dictOfFinancialInstruments[id];
            _listOfFinancialInstruments.Remove(tmp);
            _dictOfFinancialInstruments.Remove(id);
        }
    }
    
    public decimal GetTotalPortfolioValue() =>
        _listOfFinancialInstruments.Sum(i => i.CalculateCurrentValue());
    
    public FinancialInstrument? GetInstrumentById(string id) =>
        _dictOfFinancialInstruments.ContainsKey(id) 
            ? _dictOfFinancialInstruments[id] : null;
    
    public IEnumerable<FinancialInstrument> GetInstrumentsByRisk(string risk)
        => _listOfFinancialInstruments
            .OfType<IRiskAssessable>()
            .Where(t => t.GetRiskCategory() == risk)
            .Cast<FinancialInstrument>();
    
    public List<FinancialInstrument> GetAllInstruments()
        => _listOfFinancialInstruments;
}