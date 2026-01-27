namespace LoanInheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            Loan[] loan =
            [
                new HomeLoan("101", "Aditya01", 10000, 2),
                new HomeLoan("102", "Aditya02", 10000, 3),
                new CarLoan("103", "Aditya03", 10000, 2),
                new CarLoan("104", "Aditya04", 10000, 3)
            ];

            for (int i = 0; i < loan.Length; i++)
            {
                Console.WriteLine(loan[i].CalculateEMI().ToString("N2"));
            }
        }
    }

    class Loan
    {
        public string LoanNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal PrincipalAmount { get; set; }
        public int TenureInYears { get; set; }

        public Loan()
        {
            LoanNumber = string.Empty;
            CustomerName = string.Empty;
            PrincipalAmount = 0;
            TenureInYears = 0;
        }

        public Loan(string loanNumber, string customerName, decimal principalAmount, int tenureInYears)
        {
            LoanNumber = loanNumber;
            CustomerName = customerName;
            PrincipalAmount = principalAmount;
            TenureInYears = tenureInYears;
        }

        public double CalculateEMI()
        {
            return Convert.ToDouble(PrincipalAmount) * 0.1 * TenureInYears; 
        }
    }

    class HomeLoan(
        string loanNumber, 
        string customerName, 
        decimal principalAmount, 
        int tenureInYears
    ) : Loan(loanNumber, customerName, principalAmount, tenureInYears)
    {
        public double CalculateOneTimeProcessingFee()
        {
            return Convert.ToDouble(PrincipalAmount) * 0.01;
        }

        public new double CalculateEMI()
        {
            return Convert.ToDouble(PrincipalAmount)* 0.08 * TenureInYears + CalculateOneTimeProcessingFee();
        }
    }

    class CarLoan(
        string loanNumber, 
        string customerName, 
        decimal principalAmount, 
        int tenureInYears
    ) : Loan(loanNumber, customerName, principalAmount, tenureInYears)
    {
        public new double CalculateEMI()
        {
            return Convert.ToDouble(PrincipalAmount + 15000) * 0.09 * TenureInYears;
        }
    }
}