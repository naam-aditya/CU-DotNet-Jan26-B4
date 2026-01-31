namespace BillingEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            HospitalBilling billing = new();
            billing.AddPatient(new Patient() { Name = "Aditya", BaseFee = 600 });
            billing.AddPatient(new Inpatient() { Name = "Aditya", BaseFee = 600, DaysStayed = 8, DailyRate = 400 });
            billing.AddPatient(new OutPatient() { Name = "Aditya", BaseFee = 600, ProcedureFee = 900 });
            billing.AddPatient(new EmergencyPatient() { Name = "Aditya", BaseFee = 600, SecurityLevel = 4 });

            billing.GenerateDailyReport();
        }
    }

    class Patient
    {
        public required string? Name { get; set; }
        public decimal BaseFee { get; set; }
        public virtual decimal CalculateFinalBill() =>
            BaseFee;
    }

    class Inpatient : Patient
    {
        public int DaysStayed { get; set; }
        public decimal DailyRate { get; set; }

        public override decimal CalculateFinalBill() =>
            BaseFee + DaysStayed * DailyRate;
    }

    class OutPatient : Patient
    {
        public decimal ProcedureFee { get; set; }
        public override decimal CalculateFinalBill() =>
            BaseFee + ProcedureFee;
    }

    class EmergencyPatient : Patient
    {
        private int securityLevel;
        public int SecurityLevel
        {
            get { return securityLevel; }
            set
            {
                if (value >= 1 && value <= 5)
                    securityLevel = value;
                else
                    securityLevel = 1;
            }
        }

        public override decimal CalculateFinalBill() =>
            BaseFee * SecurityLevel;
    }

    class HospitalBilling
    {
        readonly List<Patient> patients = [];

        public void AddPatient(Patient patient) =>
            patients.Add(patient);
        
        public void GenerateDailyReport()
        {
            foreach (var patient in patients)
                Console.WriteLine($"{patient.Name}: {patient.CalculateFinalBill():C2}");
        }

        public decimal CalculateTotalRevenue()
        {
            decimal total = 0;
            foreach (var patient in patients)
                total += patient.CalculateFinalBill();
            return total;
        }

        public int GetInpatientCount()
        {
            int count = 0;
            foreach (var patient in patients)
                if (patient is Inpatient)
                    count++;
            return count;
        }
    }
}