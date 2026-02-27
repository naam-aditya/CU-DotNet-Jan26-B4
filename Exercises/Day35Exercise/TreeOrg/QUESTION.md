# Hierarchical Data Structures in C#

## Topic: Implementing and Traversing a General Tree  
**Complexity:** Intermediate  

---

## 1. Objective

To understand the practical application of **Tree Data Structures** by modeling a real-world scenario (an Organizational Chart).

You will practice:

- Object-oriented design  
- List management  
- Recursive algorithms  

---

## 2. Scenario

You have been hired by a startup to build a lightweight HR tool.

The tool must represent the company’s reporting structure, where:

- One manager can have multiple direct reports.
- The structure must visually reflect hierarchy.

---

## 3. Technical Requirements

Your solution must include the following components:

---

### A. The Node Class

Create a generic class:

```
TreeNode<T>
```

It must:

- Store a `Data` property of type `T`
- Maintain a collection of children  
  (e.g., `List<TreeNode<T>>`)
- Include a method:
  ```
  AddChild(TreeNode<T> child)
  ```
  to build the hierarchy

---

### B. Tree Traversal

Implement a **Recursive function** to display the tree.

Requirements:

- Use indentation (spaces or connectors like `└──`)
- Visually represent the depth of each employee
- Recursively traverse all child nodes

---

### C. Data Population

Populate the tree with at least **three levels**, for example:

```
CEO
 └── Director
      └── Manager
           └── Employee
```

---

# Provided Implementation (Employee-Based Tree)

Below is a concrete implementation using `EmployeeNode` instead of a generic `TreeNode<T>`.

---

## EmployeeNode Class

```csharp
public class EmployeeNode
{
    public string Name { get; set; }
    public string Position { get; set; }
    public List<EmployeeNode> Reports { get; set; }

    public EmployeeNode(string name, string position)
    {
        Name = name;
        Position = position;
        Reports = new List<EmployeeNode>();
    }

    public void AddReport(EmployeeNode employee)
    {
        Reports.Add(employee);
    }
}
```

---

## OrganizationTree Class

```csharp
public class OrganizationTree
{
    public EmployeeNode Root { get; set; }

    public OrganizationTree(EmployeeNode rootEmployee)
    {
        Root = rootEmployee;
    }

    public void Display() { }

    private void PrintRecursive(EmployeeNode current, int depth) { }
}
```

---

## Program Execution

```csharp
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
```

---

## Expected Output Structure

```
ORGANIZATION STRUCTURE
======================
Jordan Smith (CEO)
    └── Alex Chen (CTO)
        └── Sarah Vane (Dev Manager)
            └── Leo G. (Senior Dev)
            └── Maya R. (Junior Dev)
```

---

## Concepts Covered

- General Tree Data Structure  
- Parent–Child Relationships  
- Recursive Tree Traversal  
- Depth-based indentation  
- Hierarchical modeling  
- Object composition  
- Real-world tree representation  

---

## Key Learning Insight

This structure represents a **General Tree (N-ary Tree)**:

- One root node  
- Each node can have multiple children  
- Traversal is naturally implemented using recursion  
- Depth parameter controls visualization  

This mirrors many real-world systems:

- Organizational charts  
- File systems  
- Category hierarchies  
- Menu structures  