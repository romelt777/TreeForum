# Tree Forum — ASP.NET Core Web Application

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-purple)
![C#](https://img.shields.io/badge/C%23-.NET-blue)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-green)
![SQL](https://img.shields.io/badge/Database-SQL-lightgrey)
![Auth](https://img.shields.io/badge/Auth-ASP.NET%20Identity-orange)

Tree Forum is a full-stack web forum built with **ASP.NET Core and C#**.  
It focuses on authentication, authorization, and relational data modeling using **ASP.NET Identity and Entity Framework Core**.

---

## Overview

The application allows users to:

- Register and log in
- Create discussion threads
- Comment on posts
- Edit or delete their own content
- Manage their user profile

Unauthenticated users have read-only access to public discussions.

Authorization rules ensure users can only modify content they own.

---

## Architecture & Implementation

- Authentication implemented with **ASP.NET Identity**
- Ownership-based authorization enforced at the application level
- Relational data modeled with **Entity Framework Core**
- Data persisted in a **SQL database**
- Server-side rendering using **Razor Pages**

The system enforces role and ownership constraints to protect user content and restrict access to private profile data.

---

## Tech Stack

**Backend**
- C# / ASP.NET Core 8
- Razor Pages
- ASP.NET Identity
- Entity Framework Core
- SQL Database

**Frontend**
- Server-rendered Razor Views
- HTML / CSS

---

## Screenshots

### Public & Authenticated Views
![Home Page - No User](Screenshots/home_page_no_user.png)
![Home Page - Logged In](Screenshots/home_page_user_logged_in.png)

### Discussions & Comments
![Discussion Page](Screenshots/discussion.png)
![Comments](Screenshots/comments_on_post.png)

### User Management
![User Profile](Screenshots/user_profile.png)
![Manage User](Screenshots/user_manage.png)
