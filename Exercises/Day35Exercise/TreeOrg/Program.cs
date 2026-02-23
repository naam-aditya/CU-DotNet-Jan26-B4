namespace TreeOrg;


class Program
{
    static void Main(string[] args)
    {
        var ceo = new EmployeeNode("Jordan Smith", "CEO");
        var cto = new EmployeeNode("Alex Chen", "CTO");
        var manager = new EmployeeNode("Sarah Vane", "Dev Manager");
        var dev1 = new EmployeeNode("Leo G.", "Senior Dev");
        var dev2 = new EmployeeNode("Maya R.", "Junior Dev");

        var company = new OrganizationTree(ceo);
        
        ceo.AddReport(cto);
        cto.AddReport(manager);
        manager.AddReport(dev1);
        manager.AddReport(dev2);

        company.Display();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}

public class EmployeeNode
{
    public string Name { get; set; }
    public string Position { get; set; }
    public List<EmployeeNode> Reports { get; set; }

    public EmployeeNode(string name, string position)
    {
        Name = name;
        Position = position;
        Reports = [];
    }

    public void AddReport(EmployeeNode employee)
    {
        Reports.Add(employee);
    }
}

public class OrganizationTree
{
    public EmployeeNode Root { get; set; }

    public OrganizationTree(EmployeeNode rootEmployee)
    {
        Root = rootEmployee;
    }

    public void Display()
    {
        if (Root == null) return;
        Console.WriteLine("ORGANIZATION STRUCTURE");
        Console.WriteLine("======================");
        PrintRecursive(Root, 0);
    }

    private static void PrintRecursive(EmployeeNode current, int depth)
    {
        string indent = new(' ', depth * 4);
        string connector = depth == 0 ? "" : "└── ";

        Console.WriteLine($"{indent}{connector}{current.Name} ({current.Position})");

        foreach (var report in current.Reports)
        {
            PrintRecursive(report, depth + 1);
        }
    }
}
