# Tree Forum — ASP.NET Backend Web Application

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-purple)
![C#](https://img.shields.io/badge/C%23-.NET-blue)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-green)
![SQL](https://img.shields.io/badge/Database-SQL-lightgrey)
![Auth](https://img.shields.io/badge/Auth-ASP.NET%20Identity-orange)

Tree Forum is a full-stack web forum built with **ASP.NET Core and C#**, focused on **authentication, authorization, and relational data modeling**. The project demonstrates backend fundamentals such as secure user management, ownership-based CRUD operations, and persistent storage using a SQL database.

---

## Overview

The application allows users to register, log in, create discussion threads, comment on posts, and manage their profiles. Authorization rules ensure users can only modify their own content, while unauthenticated users have read-only access.


---

## Core Features

- User authentication and registration using ASP.NET Identity  
- Ownership-based authorization for creating, editing, and deleting threads  
- Commenting system linked to authenticated users  
- Public read access for unauthenticated users  
- User profile management, including avatar uploads  
- Relational data persistence using Entity Framework Core and SQL  

---

## Tech Stack

**Backend**
- C# / ASP.NET Core
- Razor Pages
- ASP.NET Identity
- Entity Framework Core
- SQL Database

**Frontend**
- Server-side rendered Razor Views
- HTML/CSS

---

## Security & Access Control

- Authentication handled via ASP.NET Identity
- Authorization rules enforce:
  - Authenticated-only posting and commenting
  - Edit/delete permissions restricted to content owners
  - Private user pages inaccessible to other users
- All data persisted in a relational SQL database

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

---
