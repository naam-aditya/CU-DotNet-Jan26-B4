namespace Day15Exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Height person1 = new(5, 6.5m);
            Height person2 = new(67.5m);

            Console.WriteLine(person1.ToString()); 
            Console.WriteLine(person2.ToString()); 
            Console.WriteLine(person1.AddHeights(person2).ToString());
        }
    }

    class Height
    {
        public int Feet { get; set; }
        public decimal Inches { get; set; }

        public Height()
        {
            Feet = 0;
            Inches = 0;
        }

        public Height(decimal inches)
        {
            Feet = (int) inches / 12;
            Inches = inches % 12;
        }

        public Height(int feet, decimal inches)
        {
            Feet = feet + ((int) (inches / 12));
            Inches = inches % 12;
        }

        public override string ToString()
        {
            return $"Height - {Feet} feet {Inches:F1} inches";
        }

        public Height AddHeights(Height height)
        {
            Height tmp = new()
            {
                Inches = Inches + height.Inches
            };

            tmp.Feet = Feet + height.Feet + ((int) tmp.Inches / 12);
            tmp.Inches %= 12;

            return tmp;
        }
    }
}