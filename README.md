# Library Management MVC

A library management web application built with **ASP.NET Core MVC (.NET 10)**, **EF Core**, and **SQL Server**.

## Features
- Book and category management (CRUD)
- User registration and login
- Borrowing and returning books with late return penalties
- Wishlist and book reviews with admin approval
- Admin dashboard and responsive Persian UI

## Technologies
C#, ASP.NET Core MVC, EF Core, SQL Server, LINQ, Repository Pattern, Service Layer, Fluent API.

## How to Run
1. Open `Library.slnx` in Visual Studio.
2. Set `Library.Web` as the Startup Project.
3. Update the connection string in `appsettings.json`.
4. Run `Update-Database` in Package Manager Console.
5. Run the project.

## Demo Accounts
- **Admin:** `admin` / `123456`
- **User:** `amir` / `123456`

## Project Structure
`Domain` | `Application` | `Infrastructure` | `Web`

Based on my previous [Library Management Console Project](https://github.com/amir-fayazi/week12-library-management).
