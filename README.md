
# PeopleDirectory - REST API and Angular Frontend

## Introduction

This project showcases a full-stack application: a RESTful API named PeopleDirectory built with ASP.NET Core (.NET) and a user interface (UI) developed using Angular. The API leverages an in-memory database (no SQL Server Management Studio required) to manage people's data.

## Prerequisites

.NET SDK 6.0 or later: Download and install from https://dotnet.microsoft.com/download
Node.js and npm (or yarn): Download and install from https://nodejs.org/
Code editor or IDE: Visual Studio, Visual Studio Code, or any preferred editor
Setting Up the Project

## Clone the Repository:

Bash
git clone https://your-repository-url.git
Use code with caution.
Replace https://your-repository-url.git with the actual URL of your project repository (if applicable).

Restore NuGet Packages (for .NET project):
Open a terminal or command prompt in the project's root directory and run:

Bash
dotnet restore
Use code with caution.
Install Node.js Packages (for Angular project):
Navigate to the Angular project directory (typically PeopleDirectory.Client or similar) and run:

Bash
npm install
Use code with caution.
(or yarn install if using yarn)

Running the Application

Start the .NET API:
Open a terminal in the .NET project directory (usually PeopleDirectory.Server or similar) and execute:

Bash
dotnet run
Use code with caution.
This will start the API, typically listening on port 5000 by default (you can check the output for the exact port number).

Start the Angular Development Server:
In a separate terminal, navigate to the Angular project directory and run:

Bash
ng serve
Use code with caution.
This will start the Angular development server, usually accessible at http://localhost:4200 (check the output for the exact URL).

## API Usage (Optional)

The API provides RESTful endpoints for CRUD (Create, Read, Update, Delete) operations on people's data. You can use tools like Postman or browser developer tools to send requests:

GET /api/client: Retrieve all people
GET /api/client/{id}: Get a specific person by ID
POST /api/client: Create a new person (provide data in the request body)
PUT /api/client/{id}: Update an existing person (provide data in the request body)
DELETE /api/client/{id}: Delete a person
Additional Notes

This project uses in-memory data storage, meaning data won't persist after the application restarts.
For production environments, consider using a persistent database like SQL Server or a cloud-based database service.