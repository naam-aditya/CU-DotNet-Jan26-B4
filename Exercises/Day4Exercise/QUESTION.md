# Problem Statement: ASP.NET Core MVC – Training Portal

## Overview

You have been assigned the task of developing a basic web application using **ASP.NET Core MVC** for a college training program.

The purpose of this application is to help new learners understand the fundamental building blocks of the MVC architecture:

- Controllers  
- Action Methods  
- Views  
- Navigation  

---

## Business Scenario

A college wants a simple **Training Portal** to display information related to its training offerings.

The portal should allow users to navigate between different sections such as:

- Home  
- Courses  
- Contact  

Each section must be handled following proper **MVC conventions**.

---

## Functional Requirements

- Create an ASP.NET Core MVC application named **`TrainingPortal`**.
- Implement a controller to manage training-related pages.
- Create action methods for:
  - `Home`
  - `Courses`
  - `Contact`
- Design separate views for each action method.
- Configure navigation links to move between pages **without using hard-coded URLs**.
- Ensure proper routing so that URLs map correctly to controller actions.

---

## Learning Objectives

By completing this exercise, learners should be able to:

- Understand the role of controllers and action methods
- Create and organize views based on MVC conventions
- Use Tag Helpers for navigation
- Trace the request flow from browser → controller → view

---

## Expected Outcome

At the end of this exercise:

- The **Training Portal** should open on a **Home** page.
- Users should be able to smoothly navigate to:
  - Courses  
  - Contact  
- Navigation should work using the application menu.
- The application should follow proper **MVC structure and routing conventions**.