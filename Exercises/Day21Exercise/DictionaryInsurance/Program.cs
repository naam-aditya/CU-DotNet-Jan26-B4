namespace DictionaryInsurance
{
    class Program
    {
        static void Main(string[] args)
        {
            PolicyTracker tracker = new();
            Policy policy1 = new() 
            { 
                HolderName = "Aditya1", 
                Premium = 10000, 
                RiskScore = 45, 
                RenewalDate = DateTime.Now.AddYears(-4) 
            };
            Policy policy2 = new() 
            { 
                HolderName = "Aditya2", 
                Premium = 20000, 
                RiskScore = 85, 
                RenewalDate = DateTime.Now.AddYears(-4) 
            };
            Policy policy3 = new() 
            { 
                HolderName = "Aditya3", 
                Premium = 30000, 
                RiskScore = 45, 
                RenewalDate = DateTime.Now.AddYears(-2) 
            };

            Console.WriteLine();
        }
    }

    class Policy
    {
        public string? HolderName { get; set; }
        public decimal Premium { get; set; }
        private int riskScore;
        public int RiskScore
        {
            get { return riskScore; }
            set {
                if (value >= 1 && value <= 100) 
                    riskScore = value;
                else
                    riskScore = 1; 
            }
        }
        
        public DateTime RenewalDate { get; set; }

        public override string ToString() =>
            $"Name: {HolderName}, Premium: {Premium}, RiskScore: {RiskScore}, RenewalDate: {RenewalDate}";
    }

    class PolicyTracker
    {
        readonly Dictionary<string, Policy> record = [];

        public int AdjustBulk()
        {
            int count = 0;
            foreach (var item in record.Values)
            {
                if (item.RiskScore > 75)
                {
                    item.Premium += item.Premium * 0.05m;
                    count++;
                }
            }
            return count;
        }

        public int CleanUpPolicies()
        {
            int count = 0;
            DateTime threeYearsAgo = DateTime.Now.AddYears(-3);
            foreach (var item in record.Keys)
            {
                if (record[item].RenewalDate < threeYearsAgo)
                {
                    record.Remove(item);
                    count++;
                }
            }
            return count;
        }

        public string GetPolicyDetails(string id)
        {
            if (record.TryGetValue(id, out Policy? policy))
                return policy.ToString();
            return $"Policy with Id: {id} Not Found";
        }
    }
}