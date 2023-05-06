BookHub 📚


BookHub is a sleek and modern ASP.NET Core MVC web application designed for efficient book library management. It offers essential features for book management, ensuring a seamless experience for users.

🌟 Features


List all books
Add new books
Edit existing books
Delete books
Search books by Title/Author


🚀 Getting Started


Follow these instructions to set up the project on your local machine for development and testing purposes.

Prerequisites


.NET 6.0 SDK or later: Download

SQL Server (LocalDB) or another compatible SQL Server instance


🛠 Installation


1.Clone the repository:

git clone https://github.com/BalaRobertValentin/BookHub.git

2.Navigate to the project folder:

cd BookHub

3.Install the required NuGet packages:

dotnet restore

4.Update the connection string in the appsettings.json file to match your SQL Server instance.

5.Apply the database migrations to create the necessary tables:

dotnet ef database update

6.Run the application:

dotnet run

Open your browser and navigate to https://localhost:5001 (or the appropriate port) to view the application.
