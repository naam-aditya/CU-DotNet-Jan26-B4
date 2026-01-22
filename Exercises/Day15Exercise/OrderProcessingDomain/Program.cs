namespace OrderProcessingDomain
{
    class Program
    {
        static void Main(string[] args)
        {
            Order tmp = new();
            Order order = new(101, "Aditya");

            order.AddItem(500);
            order.AddItem(300);
            order.ApplyDiscount(10);

            Console.WriteLine(order.GetOrderSummary());
        }
    }

    class Order
    {
        bool isDiscountApplied;
        DateTime orderDate;
        string? status;

        public int OrderId { get; private set; }
        private string? customerName;
        public string? CustomerName
        {
            get { return customerName; }
            set {
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                    customerName = value;
                else
                    customerName = "INVALID CUSTOMER NAME";
            }
        }
        public decimal TotalAmount { get; private set; }
        
        public Order()
        {
            OrderId = 0;
            CustomerName = "";
            TotalAmount = 0;
            orderDate = DateTime.Today;
            status = "NEW";
            isDiscountApplied = false;
        }

        public Order(int orderId, string customerName)
        {
            OrderId = orderId;
            CustomerName = customerName;
            TotalAmount = 0;
            orderDate = DateTime.Today;
            status = "NEW";
            isDiscountApplied = false;
        }

        public DateTime GetOrderDate()
        {
            return orderDate;
        }

        public void AddItem(decimal price)
        {
            if (price >= 0)
                TotalAmount += price;
        }

        public void ApplyDiscount(decimal percentage)
        {
            if (!isDiscountApplied && percentage >= 1 && percentage <= 30)
            {
                TotalAmount -= TotalAmount * percentage / 100;
                isDiscountApplied = true;
            }
            else if (percentage < 1 || percentage > 30)
            {
                Console.WriteLine("INVALID DISCOUNT AMOUNT");
            }
            else
            {
                Console.WriteLine("DISCOUNT ALREADY APPLIED");
            }
        }

        public string GetOrderSummary()
        {
            return $"Order Id: {OrderId}\n" +
                $"Customer: {CustomerName}\n" +
                $"Total Amount: {TotalAmount}\n" +
                $"Status: {status}";
        }
    }
}
