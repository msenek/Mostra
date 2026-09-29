# Mostra
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

### Core Features / Business Rules

**Merchant**
Manages their business through the system. Can:
- Create and manage their business profile
- Create categories
- Create products, with prices, stock, and photos
- Activate/deactivate products
- Generate a QR code for their business
- Share their catalog via a public link

**Customer**
No account needed:
1. Scan the QR code
2. Browse the catalog
3. See products and prices

### Architecture

Built with Clean Architecture principles (Domain / Application / Infrastructure / Api), using .NET, MediatR (CQRS-style handlers), PostgreSQL, and JWT-based authentication.
## Getting Started

### Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop/) and Docker Compose (bundled with Docker Desktop)

That's it — you don't need .NET, PostgreSQL, or Redis installed locally. Everything runs in containers.

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

4. The API will be available at `http://localhost:8080`. Database migrations run automatically on startup — no manual setup needed.

### Stopping the project

```bash
docker compose down
```

Data persists in a Docker volume, so your database survives restarts. To wipe everything (including data) and start fresh:

```bash
docker compose down -v
```

<img width="806" height="487" alt="image" src="https://github.com/user-attachments/assets/1205b840-1a58-4066-9d66-9f71f1dcd4cb" />
