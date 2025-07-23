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
```
---

## ⚙️ GitHub Actions – CI/CD Workflow
To ensure code quality and stability, this project uses GitHub Actions for automated Continuous Integration (CI).

🧪 What It Does:

✅ The project is built using dotnet build

✅ All unit tests are executed using dotnet test

❌ If the build or tests fail, the action stops and marks the check as failed

✅ Successful builds help ensure merge safety and deploy readiness

🔁 Triggered on: push and pull_request

    
🔒 This helps catch errors early and ensures that only valid, tested code is merged into key branches.

---

## 📋 Swagger UI
Swagger is integrated for exploring and testing the API.

- Visit: `https://localhost:7286/swagger` (when running locally)
- Automatically generated based on controllers & models

---

## 🐳 Docker & Hosting

The backend is containerized using Docker and deployed on Render.

### 🧰 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://docs.docker.com/get-docker/) (for containerized runs)

> 📌 Docker is optional for local development but required for production deployment.

Run Locally with Docker
```bash
docker build -t portfolio-api:latest -f Portfolio.Api/Dockerfile .
docker run --env-file portfolio.api/.env -p 8080:8080 portfolio-api:latest
```
Then access the API at:
http://localhost:8080/swagger

---

## 🚀 Technologies Used
ASP.NET Core 8 (Web API)

Clean/Onion Architecture

xUnit & Mocking

Docker

Render.com for hosting

Vue (planned) for admin dashboard

---

## 📂 Project Structure

PortfolioBackend/

│

├── Portfolio.API/              # Web API (controllers, startup)

├── Portfolio.Core/             # Domain logic, interfaces, models

├── Portfolio.Infrastructure/   # Email service, future data access

└── Portfolio.Tests/            # Unit tests with mocking

---

## 🛡️ Security

Dependency Injection for all services

Input validation

Future authentication/authorization for admin API routes

---

## 📬 Contact
Want to know more or collaborate?

📧 Email: ralmanzo@gmail.com

🌐 Portfolio: https://ralmanzo.github.io/Portfolio/

🐙 GitHub: https://github.com/RAlmanzo
