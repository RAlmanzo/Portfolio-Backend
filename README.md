# 💼 Portfolio Backend – ASP.NET Core Web API

This repository contains the **backend for my personal portfolio**, built with **ASP.NET Core Web API** following the **Onion/Clean Architecture** pattern. It powers key functionalities such as contact form messaging, and soon, dynamic portfolio content management.

---

## 🧱 Architecture Overview

This solution follows a **Clean Architecture** approach and is split into multiple projects:

- `Portfolio.API` – The entry point (Web API)  
- `Portfolio.Core` – Domain entities, interfaces, and business logic  
- `Portfolio.Infrastructure` – Implementations (e.g., email service, data access)  
- `Portfolio.Tests` – Unit testing with **xUnit** and **mocking**

This modular structure ensures separation of concerns, testability, and long-term maintainability.

---

## ✉️ Features

### ✅ Email Service
Users can send messages via the contact form on the frontend.  
This is handled by an **email service** in the backend using SMTP configuration.

### 🛠️ Dynamic Portfolio API (In Progress)
A secure API is being built to manage:

- Personal information  
- Projects  
- Resume (Skills, Education, Experience)

This allows dynamic updates of portfolio content without redeploying the frontend.

> A future admin dashboard (see below) will interact with this API.

---

## 🖥️ Upcoming: Admin Dashboard (ASP.NET Core MVC + Vue)

Once the API is complete, I will build an **admin interface** using:

- **ASP.NET Core MVC**  
- **Vue.js** frontend

This dashboard will allow me to manage portfolio content through a user-friendly UI.

---

## 🧪 Testing

Unit tests are written using:

- **xUnit** – for test structure and assertions  
- **Moq** or similar mocking library – for dependency injection and isolation

To run the tests:

```bash
dotnet test
Tests cover core business logic and services, including the email service.

🐳 Docker & Hosting
The backend is containerized using Docker and deployed on Render.

Run Locally with Docker
bash
Kopiëren
Bewerken
docker build -t portfolio-backend .
docker run -p 5000:80 portfolio-backend
Then access the API at:
http://localhost:5000

🚀 Technologies Used
ASP.NET Core 8 (Web API)

Clean/Onion Architecture

xUnit & Mocking

Docker

Render.com for hosting

Vue (planned) for admin dashboard

📂 Project Structure
bash
Kopiëren
Bewerken
PortfolioBackend/
│
├── Portfolio.API/              # Web API (controllers, startup)
├── Portfolio.Core/             # Domain logic, interfaces, models
├── Portfolio.Infrastructure/   # Email service, future data access
└── Portfolio.Tests/            # Unit tests with mocking
🛡️ Security
Dependency Injection for all services

Input validation for message sending

Future authentication/authorization for admin API routes

📬 Contact
Want to know more or collaborate?

📧 Email: your.email@example.com

🌐 Portfolio: your-portfolio.com

🐙 GitHub: your-username
