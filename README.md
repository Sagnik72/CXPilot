# CXPilot
CXPilot is an AI-powered customer experience platform for managing support tickets. It features role-based access (admin, agent, user), real-time communication, and AI-driven assistance with feedback analysis. Built with React, ASP.NET Core, Azure Functions, Azure SQL, Docker, and Kubernetes-ready architecture.

# 🚀 CXPilot

<p align="center">
  <b>AI-powered Customer Experience Platform</b><br/>
  Streamlining ticket management, collaboration, and support workflows
</p>

---

## 📌 Overview

**CXPilot** is a full-stack, cloud-ready customer experience platform designed to manage support tickets efficiently. It enables seamless collaboration between clients, agents, and administrators, enhanced with AI-powered assistance and feedback analysis.

---

## ✨ Key Features

- 🔐 **Role-Based Access Control (RBAC)**  
  - Admin, Agent, and User roles with secure JWT authentication  

- 🎫 **Ticket Management System**  
  - Create, track, and manage tickets with priority levels  
  - Status lifecycle: `Open → In Progress → Resolved`  

- 💬 **Real-Time Communication**  
  - Comment-based interaction between users and agents  

- 🤖 **AI Integration**  
  - Context-aware chatbot for assistance  
  - Feedback sentiment analysis for insights  

- 👑 **Admin Control Panel**  
  - User approval system  
  - Ticket assignment & monitoring  
  - Performance tracking  

- ☁️ **Cloud-Native Architecture**  
  - Serverless processing with Azure Functions  
  - Scalable storage using Azure SQL Database  

- 🐳 **DevOps Ready**  
  - Docker containerization  
  - Kubernetes deployment-ready  

---

## 🧱 Tech Stack

| Layer        | Technology |
|-------------|-----------|
| Frontend    | React.js, JavaScript, HTML5, CSS3 |
| Backend     | ASP.NET Core (C#), REST APIs |
| Auth        | JWT (JSON Web Token) |
| Database    | Azure SQL Database |
| Cloud       | Azure Functions |
| DevOps      | Docker, Kubernetes |

---

## 🔐 Authentication & Roles

| Role   | Responsibilities |
|--------|----------------|
| 👤 User (Client) | Create & track tickets |
| 👨‍💼 Agent        | Resolve tickets & communicate |
| 👑 Admin         | Approve users, assign tasks, monitor system |

---

## 🔁 System Workflow

```text
User Signup → Admin Approval → Login (JWT)
   ↓
Create Ticket
   ↓
Admin Assigns Agent
   ↓
Agent Works (In Progress)
   ↓
Client ↔ Agent Communication
   ↓
AI Assistance
   ↓
Ticket Resolved
   ↓
User Feedback → AI Analysis → Admin Insights
---
## 📁 Project Structure (High Level)
cxpilot/
│
├── frontend/        # React application
├── backend/         # ASP.NET Core APIs
├── azure-functions/ # AI & background processing
├── docker/          # Docker configs
└── k8s/             # Kubernetes manifests
---

## ⚙️ Setup Instructions

### Prerequisites
- Node.js  
- .NET SDK  
- Docker (optional)  
- Azure account  

---

### Run Frontend
```bash
cd frontend
npm install
npm start

###Run Backend
cd backend
dotnet run
