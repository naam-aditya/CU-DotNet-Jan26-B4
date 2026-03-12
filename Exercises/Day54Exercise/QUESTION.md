# Assignment: Build a Simple Company Website Using ASP.NET MVC and Bootstrap

## Objective

Create an **ASP.NET MVC application** that demonstrates:

- Multiple controllers  
- Multiple views  
- Shared layout using `_Layout.cshtml`  
- Bootstrap navigation and UI components  

The focus is on **UI structure and MVC routing**, not backend logic.

---

# Step 1 – Create MVC Project

Create a project:

```
ASP.NET Core Web App (Model-View-Controller)
```

Project Name:

```
CompanyPortal
```

After creating the project:

- Run the application once
- Verify that the default MVC project runs successfully.

---

# Step 2 – Create Controllers

Create the following controllers.

---

## 1. HomeController

File:

```
Controllers/HomeController.cs
```

```csharp
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
}
```

---

## 2. ServicesController

```csharp
public class ServicesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Consulting()
    {
        return View();
    }

    public IActionResult Training()
    {
        return View();
    }
}
```

---

## 3. ProductsController

```csharp
public class ProductsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Software()
    {
        return View();
    }

    public IActionResult Tools()
    {
        return View();
    }
}
```

---

## 4. ContactController

```csharp
public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
```

---

# Step 3 – Create Views

Create the corresponding views for each controller action.

Example structure:

```
Views/Home/Index.cshtml
Views/Home/About.cshtml

Views/Services/Index.cshtml
Views/Services/Consulting.cshtml
Views/Services/Training.cshtml

Views/Products/Index.cshtml
Views/Products/Software.cshtml
Views/Products/Tools.cshtml

Views/Contact/Index.cshtml
```

Each page should contain **Bootstrap UI components**.

---

# Step 4 – Modify `_Layout.cshtml`

Modify the shared layout file.

File:

```
Views/Shared/_Layout.cshtml
```

Add a **Bootstrap Navbar**.

The navbar should contain **dropdown menus** for each controller.

Example navigation structure:

- Home
  - Index
  - About

- Services
  - Consulting
  - Training

- Products
  - Software
  - Tools

- Contact

Use MVC routing attributes:

```
asp-controller
asp-action
```

Example:

```
<a asp-controller="Home" asp-action="Index">Home</a>
```

---

# Step 5 – Use Bootstrap Components in Pages

Each page should demonstrate different Bootstrap UI elements.

---

## Home Page

Use:

- Jumbotron / Hero Section  
- Bootstrap Cards  
- Bootstrap Buttons  

Example components:

- Card component  
- Call-to-action buttons  

---

## About Page

Use:

- Bootstrap Alert  
- Bootstrap List Group  

Example use:

- Company mission statement  
- Feature highlights  

---

## Services Pages

Use:

- Bootstrap Accordion  
- Bootstrap Cards  

Example sections:

- Consulting services  
- Training programs  

---

## Products Pages

Use:

- Bootstrap Table  
- Bootstrap Badges  

Example display:

- Product list  
- Status badges  

---

## Contact Page

Use:

- Bootstrap Form  
- Input fields  
- Submit button  

Example inputs:

- Name  
- Email  
- Message  

Note:

- No backend form submission logic required.

---

# Step 6 – Routing Verification

Students must verify the following routes work correctly.

```
/Home/Index
/Home/About

/Services/Consulting
/Services/Training

/Products/Software
/Products/Tools

/Contact
```

Also verify navigation through the **layout navbar**.

---

# Expected Output

The final application should contain:

- 4 Controllers
- 8 or more Views
- Shared Layout
- Bootstrap Navbar
- Dropdown Navigation
- Bootstrap UI Components in each page
- Proper MVC Routing

---

# Learning Outcomes

Students will understand:

- ASP.NET MVC project structure  
- Controllers and Action methods  
- Razor Views (`.cshtml`)  
- Shared Layout (`_Layout.cshtml`)  
- Navigation using `asp-controller` and `asp-action`  
- Bootstrap UI integration  
- View routing in MVC  