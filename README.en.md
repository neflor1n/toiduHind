# ToiduHind.ee

[🇪🇪 Eesti](README.md) | [🇷🇺 Русский](README.ru.md) | [🇬🇧 English](README.en.md)

## TALLINN INDUSTRIAL EDUCATION CENTER

### Toiduhind.ee website

[Toiduhind.ee](https://toiduhind.vercel.app/)

### Project Documentation

- **Supervisors**: Irina Merkulova & Marina Oleinik
- **Developers**: Vsevolod Tsarev & Bogdan Sergachev
- **Course**: TARpv23
- **Location**: TALLINN
- **Year**: 2025

---

## Table of Contents

1. [Purpose](#purpose)
2. [Target Audience](#target-audience)
3. [Project Verbal Definition](#project-verbal-definition)
4. [Software Functionality Requirements](#software-functionality-requirements)
5. [Data Management Requirements](#data-management-requirements)

---

## Purpose

The purpose of **ToiduHind.ee** project is to create a **user-friendly and functional application** that allows users to compare food prices across different stores. The application's features help save both **time** and **money**:

- **Real-time price comparison**: ability to see prices from different stores and track their changes.
- **Shopping list management**: ability to create shopping lists and share them with others.
- **Price notifications**: receive alerts about discounts and price drops.
- **Delivery service integration**: the application makes the shopping process even easier.

---

## Target Audience

The ToiduHind.ee service is designed for three main user groups:

| **User Type** | **Description** | **Goals** |
|---------------|----------------|-----------|
| **Administrator** | Application manager with full system access. | User and product management, ensuring security. |
| **Regular User** | Unregistered user who compares prices and creates shopping lists. | Price comparison, shopping list management, receiving notifications. |
| **Verified User** | User who has registered and confirmed their email. | Advanced features: price notifications, shared lists, personal preferences. |

---

## Project Verbal Definition

The application includes the following **main objects** and their properties:

| **Object** | **Description** |
|------------|----------------|
| **User** | User who searches and compares food prices. |
| **Verified User** | Registered user with access to additional features. |
| **Administrator** | Responsible for database and system management. |
| **Product** | Specific item whose prices are compared. |
| **Store** | Physical store or e-store whose prices are represented in the system. |
| **Price Comparison** | Function that displays product prices across different stores. |
| **Shopping List** | User-created list of desired products. |
| **Discount** | Sale or special offer related to a specific product. |

---

## Software Functionality Requirements

**Interface Requirements**:
- Clear search function for quick product finding.
- Real-time price comparison without page refresh.

**Core Functions**:
1. Real-time product price comparison.
2. Discount monitoring and notifications.
3. Shopping list creation and sharing.
4. Administrator tools (management, data addition and updates).

---

## Data Management Requirements

- **Database Structure**:
  - Users, Products, Store Information (Stores), Prices, Shopping Lists (Wishlists), Notifications.

- **Data Validation and Security**:
  - Password encryption.
  - Role-based access control (RBAC).
  - HTTPS connections.

- **Relationships**:
  - **Users ↔ Wishlists**: each user can have one shopping list.
  - **Products ↔ Prices ↔ Stores**: each product can be available in multiple stores at different prices.

---

## Additional Information

The **ToiduHind.ee** application is created to provide transparent and quick access to food prices and help users make **more informed purchasing decisions**.

Long-term project goals:
- Expand the database with local and international store prices.
- Integrate more features, such as personalized offers and AI-powered recommendations.

---
