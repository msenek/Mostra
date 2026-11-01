# Mostra

![CI](https://github.com/msenek/Mostra/actions/workflows/ci.yml/badge.svg)

A backend API that lets small merchants publish a price catalog for their business and share it with customers through a QR code.

## About This Project

### The Problem

I got tired of walking into small businesses — food stalls, craft markets, local shops — and having no idea what anything costs. No prices displayed anywhere. And honestly, asking feels awkward, sometimes even embarrassing, especially when you have to ask about every single item.

More than once, my girlfriend and I walked away from a place without buying anything, simply because we didn't know the price and didn't want to ask. I'm sure this doesn't just happen to us — it's a small but real source of lost sales for businesses that never even realize it's happening.

### The Solution

Mostra makes it easy for merchants to list every product with its price, stock status, and photos — without friction, without a complicated setup.

Picture a craft market stall selling handmade bracelets. None of them are labeled with a price, so you end up asking "how much for this one?" over and over. Tiring, and a little annoying for everyone involved.

Now imagine a QR code printed large on the stall's front post, in plain sight. Anyone scans it and lands on a catalog page designed by the merchant, showing every product and its price — clear, simple, and immediate.

### Who Is It For

**Merchants** — anyone running a business, or just getting started, who wants an easy way to build a catalog that customers can browse on their own, without needing an employee to explain prices one by one.

**Customers** — no registration required. Scan the QR code, browse the catalog, see the prices. That's it.

## Features

**Merchant**
- Register and log in (JWT access tokens with refresh token rotation and logout)
- Create and manage businesses, categories, and products
- Set prices and stock status
- Upload, replace, and delete product photos
- Activate or deactivate products without deleting them
- Delete and restore products (soft delete)
- Generate a QR code for each business
- Share the catalog through a public link

**Customer**
No account needed:
1. Scan the QR code
2. Browse the catalog
3. See products and prices

**Platform**
- Resource-level authorization: a merchant can only touch their own data
- Redis cache on the public catalog, invalidated whenever the catalog changes
- Rate limiting on the public catalog (20 requests/min) and on merchant endpoints (100 requests/min)
- Automatic cleanup of products soft-deleted for more than 30 days, including their images
- Unit tests and a CI pipeline on every push and pull request

## Tech Stack

| Area | Technology |
|---|---|
| Framework | .NET 10 / ASP.NET Core Web API |
| Database | PostgreSQL with Entity Framework Core (code-first migrations, applied on startup) |
| Cache | Redis |
| Application pattern | MediatR (one handler per use case) + FluentValidation |
| Authentication | JWT + hashed, rotating refresh tokens, BCrypt for passwords |
| Image storage | Cloudinary |
| QR codes | QRCoder |
| Containers | Docker (multi-stage build) and Docker Compose |
| Testing | xUnit and Moq |
| CI | GitHub Actions |
| API docs | NSwag / Swagger UI (Development environment only) |

## Architecture

Built with Clean Architecture principles. The layers are organized as folders inside a single project, and dependencies point inward:

```
Api              → controllers, middleware, rate limiting, CORS
Application      → use cases (MediatR handlers), DTOs, validators, interfaces, pipeline behaviors
Domain           → entities and business rules
Infrastructure   → EF Core, repositories, Cloudinary, token service, cache invalidation, background jobs
```

`Application` only depends on interfaces (repositories, token service, image storage, cache invalidation). The concrete implementations live in `Infrastructure`, so any of them can be swapped without touching the use cases.

### Design Decisions

**Ownership validation as a pipeline behavior.** Every request that targets a business, category, or product implements a marker interface (`IBusinessOwnedRequest`, `ICategoryOwnedRequest`, `IProductOwnedRequest`). A single MediatR behavior intercepts those requests, resolves the owning business, and compares its merchant against the authenticated user. Handlers never have to remember to check ownership, which closes the class of IDOR bugs where one merchant edits another merchant's data.

**Refresh tokens.** Access tokens live 15 minutes and refresh tokens 7 days. Refresh tokens are random values stored only as SHA-256 hashes, so a leaked database does not leak usable tokens. Every refresh rotates the token: the old one is revoked and a new one is issued. Logout revokes the token immediately.

**Caching with explicit invalidation.** The public catalog is the most-read, least-written endpoint, so it is cached in Redis through a caching behavior (`ICacheableRequest`). A 5-minute TTL acts as a safety net, and every handler that changes products or categories also evicts the cached catalog of that business, so merchants see their changes right away.

**Soft delete with expiration.** Products are soft-deleted and can be restored. A background service runs daily and permanently removes products that have been deleted for more than 30 days, along with their images in Cloudinary.

**Domain exceptions.** Handlers throw typed exceptions (`NotFoundException`, `ForbiddenException`, `ConflictException`, `UnauthorizedException`) and a global middleware translates them into the right HTTP status codes.

## Getting Started

### Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop/) and Docker Compose (bundled with Docker Desktop)

You don't need .NET, PostgreSQL, or Redis installed locally. Everything runs in containers.

### Running the project

1. Clone the repository:
   ```bash
   git clone https://github.com/msenek/Mostra.git
   cd Mostra
   ```

2. Create a `.env` file in the project root with the following variables:
   ```
   POSTGRES_USER=your_db_user
   POSTGRES_PASSWORD=your_db_password
   POSTGRES_DB=mostra_db
   JWT_SECRET=your_jwt_secret
   CLOUDINARY_CLOUD_NAME=your_cloudinary_cloud_name
   CLOUDINARY_API_KEY=your_cloudinary_api_key
   CLOUDINARY_API_SECRET=your_cloudinary_api_secret
   PUBLIC_CATALOG_BASE_URL=http://localhost:8080
   ```
   > Cloudinary credentials are free — sign up at [cloudinary.com](https://cloudinary.com) to get yours.

3. Start everything (API + PostgreSQL + Redis):
   ```bash
   docker compose up -d
   ```

4. The API is available at `http://localhost:8080`. Database migrations run automatically on startup, so no manual setup is needed.

### Stopping the project

```bash
docker compose down
```

Data persists in a Docker volume, so your database survives restarts. To wipe everything (including data) and start fresh:

```bash
docker compose down -v
```

> Swagger UI is only enabled when the API runs in the `Development` environment. The Docker setup runs in `Production` mode.

## API Overview

All merchant endpoints require a `Bearer` token. The public catalog does not.

| Area | Method and route |
|---|---|
| Auth | `POST /api/auth/register`, `POST /api/auth/login`, `POST /api/auth/refresh`, `POST /api/auth/logout` |
| Businesses | `POST /api/businesses`, `GET /api/businesses`, `PUT /api/businesses/{id}`, `DELETE /api/businesses/{id}`, `GET /api/businesses/{id}/qrcode` |
| Categories | `POST /api/categories`, `GET /api/categories/business/{businessId}`, `PUT /api/categories/{id}`, `DELETE /api/categories/{id}` |
| Products | `POST /api/products`, `GET /api/products`, `GET /api/products/{id}`, `GET /api/products/business/{businessId}`, `PATCH /api/products/{id}`, `DELETE /api/products/{id}` |
| Product status | `PUT /api/products/{id}` (activate or deactivate), `PUT /api/products/{id}/restore` |
| Product images | `POST /api/products/{id}/image`, `PUT /api/products/{id}/image`, `DELETE /api/products/{id}/image` |
| Public catalog | `GET /api/catalog/{slug}` (no authentication) |

## Testing

```bash
dotnet test
```

There are 10 unit tests (xUnit and Moq) covering the ownership pipeline behavior and the refresh token flow, which are the security-critical parts of the project. The same tests run in GitHub Actions on every push and pull request.


## Screenshot

<img width="806" height="487" alt="image" src="https://github.com/user-attachments/assets/1205b840-1a58-4066-9d66-9f71f1dcd4cb" />