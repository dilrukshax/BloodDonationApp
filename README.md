# 🩸 Blood Donation App

<!-- Centered Core Technologies Badges -->
<div align="center">

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)
    ![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
    ![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=json-web-tokens&logoColor=white)
    ![MailKit](https://img.shields.io/badge/MailKit-6C757D?style=for-the-badge&logo=mailkit&logoColor=white)
    ![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=white)
    ![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor&logoColor=white)

</div>

## Features

- **User Registration & Authentication**: Secure user sign-up and login with role-based access (admin and user).
- **Event Management**: Create, update, delete, and view blood donation events.
- **Registration for Events**: Users can register for events and manage their registrations.
- **Admin Dashboard**: Comprehensive administrative controls to manage users, events, and view reports.
- **Email Notifications**: Automated emails for registration, event updates, and profile changes.
- **Profile Management**: Users can view and update their profiles securely.
- **Security**: Implements JWT for secure API access and role-based authorization.
- **Advanced Admin Functions**:
  - **User Management**: Administrators can view all registered users, update user information, and delete users as necessary.
  - **Event Report Management**: Create comprehensive reports for each event, including participation numbers and blood variant collections.
  - **Aggregated Statistics**: Access summaries of blood variants collected across all events and overall participation metrics.
  - **Role Management**: Assign or change user roles to control access levels within the application.


### **Technologies Used**


## Architecture

The Blood Donation App follows a **Client-Server Architecture** with a clear separation between the frontend and backend. Here's an overview:

### **Backend (API)**
- **Controllers**: Handle HTTP requests and define API endpoints.
- **Services**: Contain business logic (e.g., EmailService).
- **Repositories**: Manage data access using Entity Framework Core.
- **Models**: Define data structures and DTOs (Data Transfer Objects).
- **Authentication & Authorization**: Secure endpoints using JWT and role-based access.

### **Frontend (Blazor)**
- **Components**: Reusable UI elements for different parts of the application.
- **Pages**: Represent different views/routes in the application.
- **Services**: Handle communication with the backend API.
- **State Management**: Manage user state and application data.

### **Database**
- **SQL Server**: Stores user data, event details, registrations, and reports.
- **Migrations**: Managed via Entity Framework Core to handle database schema changes.

## Setup & Installation

Follow these steps to set up the project locally.

### **Clone the Repository**

```bash
git clone https://github.com/your-username/blood-donation-app.git
cd blood-donation-app
```

### **Backend Setup**

1. **Navigate to the Backend Directory**

    ```bash
    cd BloodDonationAPI
    ```

2. **Configure Environment Variables**

    Create a `appsettings.json` file with the following structure:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER;Database=BloodDonationDB;Trusted_Connection=True;"
      },
      "Jwt": {
        "Key": "YourSecureKeyHere",
        "Issuer": "YourApp",
        "Audience": "YourAppUsers",
        "DurationInMinutes": "60"
      },
      "AdminRegistration": {
        "Code": "AdminSecretCode"
      },
      "EmailSettings": {
        "SmtpServer": "smtp.your-email.com",
        "Port": 587,
        "SenderName": "Blood Donation App",
        "SenderEmail": "no-reply@blooddonationapp.com",
        "Username": "your-email-username",
        "Password": "your-email-password"
      }
    }
    ```

3. **Apply Migrations and Update Database**

    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

4. **Run the Backend Server**

    ```bash
    dotnet run
    ```

    The API should now be running at `https://localhost:5001` or `http://localhost:5000`.

### **Frontend Setup**

1. **Navigate to the Frontend Directory**

    ```bash
    cd ../BloodDonationClient
    ```

2. **Configure Environment Variables**

    Update the `appsettings.json` or relevant configuration files with the backend API URL.

3. **Run the Frontend Application**

    ```bash
    dotnet run
    ```

    The frontend should now be accessible at `https://localhost:5001` or `http://localhost:5000`.


##  Screenshots

<!-- Replace the image URLs with actual screenshots of your application -->

#### **Home Page**

![Web capture_27-12-2024_111348_localhost](https://github.com/user-attachments/assets/6da78f9e-5053-44c5-a61e-870b476406cb)

#### **Event Management**

![Web capture_27-12-2024_111436_localhost](https://github.com/user-attachments/assets/0c81f7de-d003-4242-a559-362f2f20c02e)

#### **Event Report Managemen**

![Web capture_27-12-2024_111450_localhost](https://github.com/user-attachments/assets/a47a5930-7090-483f-ac04-55e796efc72e)

#### **Event Registration Managemen**

![Web capture_30-12-2024_91036_localhost](https://github.com/user-attachments/assets/1d4ca675-66e5-4232-9e70-ea81f855e4a4)

#### **All Events**

![Web capture_27-12-2024_111516_localhost](https://github.com/user-attachments/assets/1624efdc-11bf-4e69-a496-cffd3b767afb)


## Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. **Fork the Project**
2. **Create your Feature Branch**

    ```bash
    git checkout -b feature/AmazingFeature
    ```

3. **Commit your Changes**

    ```bash
    git commit -m 'Add some AmazingFeature'
    ```

4. **Push to the Branch**

    ```bash
    git push origin feature/AmazingFeature
    ```

5. **Open a Pull Request**


## Contact


- **Email**: [dilandilruksha0@gmail.com](mailto:dilandilruksha0@gmail.com)
