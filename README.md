# 3-Tier Architecture Console Application

## Overview

This project demonstrates a 3-Tier Architecture implemented in a C# Console Application. It separates concerns into three distinct layers:

- **Presentation Layer**: Handles user interactions via the console.
- **Business Logic Layer (BLL)**: Contains the core application logic and rules.
- **Data Access Layer (DAL)**: Manages data persistence and retrieval from the database.

## Features

- Add, update, delete, and retrieve country records.
- Structured codebase promoting maintainability and scalability.
- Error handling and input validation mechanisms.

## Technologies Used

- C# (.NET Framework)
- SQL Server
- ADO.NET for database operations

## Getting Started

### Prerequisites

- Visual Studio or any C# compatible IDE.
- SQL Server instance.
- .NET Framework installed.

### Setup Instructions

1. **Clone the Repository**

   ```bash
   git clone https://github.com/Hazemyahea/3TierArchitecture--Console-App.git
   ```

2. **Configure the Database**

   - Create a SQL Server database named `ContactsDB`.
   - Execute the provided SQL script to create the `Countries` table:

     ```sql
     CREATE TABLE Countries (
         CountryID INT PRIMARY KEY IDENTITY(1,1),
         CountryName NVARCHAR(100) NOT NULL,
         Code NVARCHAR(10),
         PhoneCode NVARCHAR(10)
     );
     ```

3. **Update Connection String**

   - Navigate to the `ConnectionDB` class.
   - Update the `ConnectionString` property with your database connection details.

4. **Build and Run**

   - Open the solution in Visual Studio.
   - Build the project to restore dependencies.
   - Run the application.

## Usage

Upon running the application, follow the on-screen prompts to perform operations such as adding a new country, updating existing records, deleting, or viewing country information.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request for any enhancements or bug fixes.

## License

This project is licensed under the MIT License.
