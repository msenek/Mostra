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



<img width="630" height="437" alt="image" src="https://github.com/user-attachments/assets/f6c358c2-36fc-4c92-bc45-6f0154fd4ffe" />
