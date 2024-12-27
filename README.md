# 🩸 Blood Donation App

<!-- Centered Core Technologies Badges -->
<div align="center">

![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)

</div>


## 🚀 Table of Contents

- [🌟 Features](#-features)
- [🛠️ Technologies Used](#️-technologies-used)
- [📈 Architecture](#architecture)
- [🔧 Setup & Installation](#-setup--installation)
- [🧑‍💻 Usage](#-usage)
- [📸 Screenshots](#-screenshots)
- [🤝 Contributing](#-contributing)
- [📄 License](#-license)
- [📫 Contact](#-contact)

## 🌟 Features

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

## 🛠️ Technologies Used

### **Core Technologies**
<div align="center">

![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor&logoColor=white)
    ![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
    ![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
    ![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)
    
</div>

### **Technologies Used**
<div align="center">

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)
    ![Entity Framework Core](https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)
    ![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
    ![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=json-web-tokens&logoColor=white)
    ![BCrypt](https://img.shields.io/badge/Bcrypt-4F5D95?style=for-the-badge&logo=security&logoColor=white)
    ![MailKit](https://img.shields.io/badge/MailKit-6C757D?style=for-the-badge&logo=mailkit&logoColor=white)
    ![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)
    ![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=white)
    
</div>

## 📈 Architecture

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

## 🔧 Setup & Installation

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

## 🧑‍💻 Usage

1. **Register a New User**

    - Navigate to the registration page.
    - Fill in the required details.
    - If you have an admin code, enter it to register as an admin.

2. **Login**

    - Use your credentials to log in.
    - Admin users have access to additional features.

3. **Manage Events**

    - **Admins** can create, update, and delete events.
    - **Users** can view events and register for them.

4. **View Reports**

    - **Admins** can view detailed reports of events, including participation and blood units collected.

5. **Profile Management**

    - Users can view and update their profiles.
    - Change passwords and update contact information as needed.

6. **Advanced Admin Functions**

    - **User Management**: Administrators can view all registered users, update user information, and delete users as necessary.
    - **Event Report Management**: Create comprehensive reports for each event, including participation numbers and blood variant collections.
    - **Aggregated Statistics**: Access summaries of blood variants collected across all events and overall participation metrics.
    - **Role Management**: Assign or change user roles to control access levels within the application.

## 📸 Screenshots

<!-- Replace the image URLs with actual screenshots of your application -->

### **Home Page**

![Web capture_27-12-2024_111348_localhost](https://github.com/user-attachments/assets/6da78f9e-5053-44c5-a61e-870b476406cb)

### **Event Management**

![Web capture_27-12-2024_111436_localhost](https://github.com/user-attachments/assets/0c81f7de-d003-4242-a559-362f2f20c02e)

### **Event Report Managemen**

![Web capture_27-12-2024_111450_localhost](https://github.com/user-attachments/assets/a47a5930-7090-483f-ac04-55e796efc72e)

### **Event Registration Managemen**

![Web capture_27-12-2024_11154_localhost](https://github.com/user-attachments/assets/64e39dab-c53b-40fa-b594-2230581e880f)

### **All Events**

![Web capture_27-12-2024_111516_localhost](https://github.com/user-attachments/assets/1624efdc-11bf-4e69-a496-cffd3b767afb)


## 🤝 Contributing

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

## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.

## 📫 Contact


- **Email**: [dilandilruksha0@gmail.com](mailto:dilandilruksha0@gmail.com)
