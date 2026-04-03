# Decentralized Inventory Management System

## Background

A growing tech retail company, **VoltGear Systems**, is facing challenges managing its laptop inventory.

Current issues:

- Manual spreadsheets  
- Data duplication  
- Loss of records  
- No real-time visibility  

To modernize, the company wants a **web-based solution** that is:

- Fast  
- Scalable  
- Built using a **NoSQL database (MongoDB)**  
- Flexible to handle varying hardware specifications  

---

# Objective

Build a web application that:

- Captures laptop inventory data  
- Stores it in **MongoDB**  
- Uses the **Repository Pattern** for clean architecture  
- Implements **ASP.NET Core MVC** with Razor Views for UI  

---

# Part 1: Environment Setup


## 1. Project Setup

Create a project:

```
ASP.NET Core Web App (Model-View-Controller)
```

---

## 2. Database Setup

- Install and configure:
  - Local MongoDB instance  
  - MongoDB Atlas (cloud)

Create:

- Database:  
  ```
  InventoryDB
  ```

- Collection:  
  ```
  Laptops
  ```

---

## 3. Required Packages

Install via NuGet:

```
MongoDB.Driver
```

---

# Part 2: Model & Configuration


## 1. Define the Model

Create a class:

```
Laptop
```

### Required Fields

- Id (mapped to MongoDB `_id`)  
- Model Name  
- Serial Number  
- Price  

Note:

- Use BSON attributes to correctly map `Id` to MongoDB ObjectId.

---

## 2. App Configuration

Add MongoDB settings in:

```
appsettings.json
```

Example:

```
"DatabaseSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "InventoryDB",
  "CollectionName": "Laptops"
}
```

Important:

- Do not hardcode connection details in code.

---

# Part 3: Data Access Layer


## LaptopService

Create a service:

```
LaptopService.cs
```

### Responsibilities

- Handle all database operations  
- Follow Repository Pattern principles  

---

## Required Methods

### CreateAsync

```
CreateAsync(Laptop newLaptop)
```

- Inserts a new laptop document into MongoDB  

---

### GetAsync

```
GetAsync()
```

- Retrieves all laptops from the collection  

---

## Dependency Injection

- Register the service in DI container as:

```
Scoped
```

Reason:

- Ensures a fresh instance per request  
- Maintains proper connection handling  

---

# The Challenge

Develop a **Proof of Concept (PoC)** application that:

- Captures laptop details via web interface  
- Stores them in MongoDB  
- Displays stored data in real-time  

The design must follow:

- Clean architecture  
- Dependency Injection  
- Repository Pattern  

---

# Functional Requirements


## 1. Data Capture

Create a **"New Equipment" form** that collects:

- Model Name  
- Serial Number  
- Price  

---

## 2. NoSQL Integration

- Connect the application to MongoDB  
- Store records as **BSON documents**  

---

## 3. Real-Time Listing

Create a dashboard that:

- Fetches all laptop records  
- Displays them in an HTML table  

---

## 4. State Management

Provide user feedback using:

- TempData  
  OR  
- ViewBag  

Example message:

```
Laptop successfully saved to MongoDB
```

---

# Technical Constraints


## Architecture

- Use **Model-View-Controller (MVC)** pattern  

---

## Service Lifetime

- Register MongoDB service as:

```
Scoped
```

---

## Data Mapping

- Use MongoDB.Driver  
- Map:

```
Id → _id (ObjectId)
```

using BSON attributes  

---

## Configuration

- Retrieve connection details from:

```
appsettings.json
```

- Do not hardcode values  

---

# Validation Requirements

- Prevent empty input fields  
- Prevent negative price values  
- Display validation messages on UI  

---

# UI Requirements

- Use Bootstrap for styling  
- Ensure:

  - Responsive form  
  - Clean table layout  
  - Professional UI appearance  

---

# Success Criteria


## 1. Data Persistence

- Data must persist even after server restart  
- Verify using:
  - MongoDB Compass  
  - MongoDB Shell  

---

## 2. Validation

- System must prevent invalid input  
- Display proper validation messages  

---

## 3. Functionality

- User can add new laptops  
- User can view all laptops in dashboard  

---

## 4. Clean UI

- Form and table must be well-structured  
- Use Bootstrap components effectively  

---

# Concepts Covered

- ASP.NET Core MVC  
- MongoDB integration  
- NoSQL data modeling  
- Repository Pattern  
- Dependency Injection (Scoped lifetime)  
- Configuration management  
- Razor Views  
- Form validation  
- Bootstrap UI design  
- Real-time data display  
