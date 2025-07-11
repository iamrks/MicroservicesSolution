
# 🧩 Microservices Demo Project - .NET Core, SQL Server, RabbitMQ, Ocelot

This is a hands-on microservices architecture built using **.NET Core**, **Entity Framework Core**, **RabbitMQ with MassTransit**, **Ocelot API Gateway**, and **JWT-based Authentication** using **ASP.NET Core Identity**.


## 🛠️ Tech Stack

- [.NET 9 Web API](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [MassTransit](https://masstransit.io/)
- [Ocelot API Gateway](https://ocelot.readthedocs.io/)
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)
- [JWT Authentication](https://jwt.io/)
- [Docker (optional)](https://www.docker.com/)

---


### Prerequisites

- [.NET 9 SDK or later](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [RabbitMQ](https://www.rabbitmq.com/download.html) (run locally or via Docker)

### 🐳 Optional: Run RabbitMQ via Docker

```bash
docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```


docker build -t orderservice -f Services/OrderService/Dockerfile .

docker run -d -p 5300:5300 --name orderservice -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_URLS=http://+:5300 -e ConnectionStrings__DefaultConnection="Server=host.docker.internal;Database=OrderDb;User Id=sa;Password=password123;TrustServerCertificate=True;" -e RabbitMq__Host=host.docker.internal orderservice





docker build -t apigateway -f Services/ApiGateway/Dockerfile .