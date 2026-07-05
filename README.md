# Prompt Processing System

An asynchronous AI prompt processing system built with **.NET 8**, **React**, **PostgreSQL**, **Docker** and **Ollama**.

The application demonstrates a production-like architecture where prompt processing is performed asynchronously by a background worker communicating with a local Large Language Model.

---

# Features

- Submit prompts through a React UI
- Store prompt jobs in PostgreSQL
- Background processing using .NET Worker Service
- AI integration using Ollama
- Automatic status updates
- Dockerized environment
- Automatic database migrations
- Automatic Ollama model initialization

---

# Technologies

Backend

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL

Frontend

- React
- Vite
- Axios

Infrastructure

- Docker
- Docker Compose
- Ollama
- llama3.2

---

# Running the project

## Requirements

- Docker Desktop
- Docker Compose

No local installation of .NET, Node.js or PostgreSQL is required.

---

## Clone repository

```bash
git clone https://github.com/kamilue/PromptProcessing.git

cd PromptProcessing
```

---

## Start application

```bash
docker compose up --build
```

During the first startup Docker will:

- build all images
- create PostgreSQL database
- apply Entity Framework migrations
- start Ollama
- download the llama3.2 model
- start the Worker
- start the Frontend

---

## First startup

The very first launch may take several minutes.

Ollama needs to download the language model (~2GB), and the first inference may also take noticeably longer because the model has to be loaded into memory.

Subsequent requests will be significantly faster.

---

# Services

| Service     | URL                           |
| ----------- | ----------------------------- |
| Frontend    | http://localhost:5173         |
| Backend API | http://localhost:7267         |
| Swagger     | http://localhost:7267/swagger |
| Ollama      | http://localhost:11434        |

---

# Project structure

```
Prompt.Domain
Prompt.Application
Prompt.Infrastructure
Prompt.Api
Prompt.Worker

frontend/
```

---

# Example workflow

1. Submit a prompt.

```
What is 2 + 2?
```

2. The API stores the request in PostgreSQL.

3. The Worker detects a pending job.

4. The Worker sends the prompt to Ollama.

5. Ollama generates a response.

6. The Worker updates the database.

7. The frontend automatically displays the completed response.

---

# Notes

The Worker communicates with Ollama through its REST API.

Prompt processing is intentionally asynchronous to simulate a production-style architecture where long-running tasks are handled outside the HTTP request pipeline.

---
