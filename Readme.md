# Real-Time Chat Application

A production-grade, real-time chat application built with ASP.NET Core, demonstrating modern architectural patterns and distributed systems concepts.

## Overview

This project showcases expertise in building scalable, maintainable applications using Clean Architecture, CQRS pattern, Domain-Driven Design with Value Objects, real-time communication, and event-driven design with message queues.

## Tech Stack

### Backend

- **ASP.NET Core** - Web API framework
- **SignalR** - Real-time WebSocket communication
- **MongoDB** - NoSQL document database
- **RabbitMQ** - Message broker for asynchronous processing
- **MediatR** - CQRS implementation (Commands/Queries)


### Libraries \& Tools

- **FluentValidation** - Input validation
- **JWT Bearer Authentication** - Secure authentication
- **Swagger/OpenAPI** - API documentation
- **xUnit + Moq** - Unit testing
- **VS Code + .NET CLI** - Development environment


## Architecture

### Clean Architecture Layers

The project follows Clean Architecture principles with strict separation of concerns:

- **ChatApp.Domain** - Core business entities and value objects
- **ChatApp.Application** - Business logic, CQRS handlers, DTOs
- **ChatApp.Infrastructure** - Data access, external services
- **ChatApp.Api** - Controllers, SignalR hubs, middleware
- **ChatApp.Tests** - Unit tests


### Key Patterns

- **Clean Architecture** - Separation of concerns with dependency inversion
- **CQRS** - Command/Query separation using MediatR
- **Repository Pattern** - Data access abstraction
- **Domain-Driven Design (DDD)** - Value Objects for type-safe domain modeling
- **Event-Driven Architecture** - Async processing with RabbitMQ
- **Dependency Injection** - Built-in DI container


### Domain-Driven Design Value Objects

The domain layer uses **Value Objects** to enforce business rules at the type level, making invalid states unrepresentable

**Benefits:**

- **Type Safety**: The compiler prevents invalid data from entering the domain
- **Self-Documenting**: `NonEmptyString` immediately communicates validation requirements
- **Single Source of Truth**: Validation logic lives in the domain, not scattered across layers
- **Smart Constructors**:
    - `Mk()` - Safe construction returning `null` for invalid data
    - `MkUnsafe()` - For trusted sources (e.g., database reads)
    - Constructor - Throws for invalid data

This approach is used throughout the domain entities (User, ChatRoom, Message) to ensure data integrity at the lowest level.
 
## Architecture Flow

### Real-Time Message Flow

```
User sends message
       ↓
   ChatHub (SignalR)
       ↓
       ├─→ [Instant] Broadcast to room members
       ↓
       └─→ Publish "MessageSent" event to RabbitMQ
              ↓
              ├─→ Worker #1: Save to MongoDB (async)
              └─→ Worker #2: Push notification to offline users
```


### Key Benefits

- **Decoupling**: Real-time delivery is independent of database writes
- **Resilience**: Messages queue if database is temporarily down
- **Scalability**: Multiple workers process messages concurrently
- **Fan-Out**: One event triggers multiple independent actions
- **Type Safety**: Value Objects prevent invalid data at compile time

  

**Access Swagger UI**
```
https://localhost:5001/swagger
```

## License

MIT License

 