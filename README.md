# CXPilot

AI-powered customer experience platform for ticket management, collaboration, and sentiment-driven support insights.

## 📌 Overview

`CXPilot` is a cloud-ready support platform built with React, ASP.NET Core APIs, Azure Functions, and Azure SQL. It supports role-based workflows (Admin, Agent, User), end-to-end ticket lifecycle handling, and AI-assisted feedback analysis.

## 🧠 System Architecture

```text
      +----------------------+
      |      Frontend        |
      |      (React)         |
      +----------+-----------+
                 |
                 |
    -------------------------------
    |             |               |

+---------------+ +---------------+ +----------------------+
| Auth Service  | | Ticket Service| | AI Analysis Service |
|   (.NET API)  | |   (.NET API)  | | (.NET / Azure Func) |
+-------+-------+ +-------+-------+ +----------+-----------+
        |                 |                    |
        |                 |                    |
        ---------------------------------------
                         |
                 +---------------+
                 | Azure SQL DB  |
                 +---------------+
```

---

## 🔗 API Endpoints

### 🔐 Auth Service

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/register` | Register new user |
| POST | `/login` | Login and get JWT token |
| GET | `/users/pending` | Get users awaiting approval |
| POST | `/approve-user` | Approve user and assign role |

---

### 🎫 Ticket Service

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/tickets` | Create new ticket |
| GET | `/tickets` | Get all tickets |
| GET | `/tickets/{id}` | Get ticket by ID |
| PUT | `/tickets/{id}/status` | Update ticket status |
| POST | `/tickets/{id}/assign` | Assign ticket to agent |
| POST | `/tickets/{id}/comment` | Add comment |
| POST | `/tickets/{id}/feedback` | Submit feedback |

---

### 🤖 AI Analysis Service

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/analyze-feedback` | Analyze sentiment of feedback |

---

## 📊 Sample AI Response

```json
{
  "sentiment": "Negative",
  "keywords": ["slow", "delay"]
}
```

## 🧱 Database Design (High Level)

- `Users`: `id, name, email, password, role, isApproved`
- `Tickets`: `id, title, description, status, priority, createdBy, assignedTo`
- `Comments`: `id, ticketId, userId, message, timestamp`
- `Feedback`: `id, ticketId, message, sentiment`

## 🔁 Data Flow Example

1. User creates ticket
2. Ticket stored in database
3. Admin assigns agent
4. Agent resolves issue
5. User submits feedback
6. Feedback sent to AI service
7. Sentiment stored in database
8. Admin views insights

## 🐳 Deployment Architecture

- Each service runs in its own container (Docker)
- Services are orchestrated using Kubernetes
- AI processing can be deployed via Azure Functions
- Database hosted on Azure SQL Database

## 🚀 Future Enhancements

- Auto ticket assignment using AI
- Real-time notifications
- Advanced analytics dashboard
- Multi-tenant support

---

## 🔥 What This README Now Shows

- ✅ Architecture thinking
- ✅ API design
- ✅ Data flow clarity
- ✅ Cloud readiness
