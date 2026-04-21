# Task Tracker API

A RESTful task management API built with ASP.NET Core and C#.

## Features
- CRUD operations for tasks
- Filter tasks by status and priority
- Mark tasks as completed
- Entity Framework Core with SQLite
- Layered architecture using controllers, services, and data access
- Swagger documentation
- Unit tests with xUnit

## Tech Stack
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- xUnit

## Run Locally
1. Install the .NET 8 SDK.
2. Restore dependencies with `dotnet restore`.
3. Start the API with `dotnet run`.
4. Open `/swagger` in your browser.

## Endpoints
- `GET /api/tasks`
- `GET /api/tasks/{id}`
- `POST /api/tasks`
- `PUT /api/tasks/{id}`
- `PATCH /api/tasks/{id}/complete`
- `DELETE /api/tasks/{id}`

## Filters
- `GET /api/tasks?status=completed`
- `GET /api/tasks?status=pending`
- `GET /api/tasks?priority=High`
