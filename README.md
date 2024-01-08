# ConfitecAPI - C# .NET API


## Prerequisites
- .NET SDK
- Docker

## Setup and Configuration
1. **Configure SQL Server Connection:**
   - Locate the `appsettings.json` file in the project directory.
   - Replace the "DefaultConnection" value with your SQL Server connection string.

2. **Setting Up SQL Server with Docker:**
   - Ensure Docker is installed and running on your system.
   - Navigate to the directory containing `docker-compose.yml`.
   - Run the following command to setup and start the SQL Server container:
     ```
     docker-compose up
     ```
