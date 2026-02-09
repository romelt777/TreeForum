# Tree Forum — ASP.NET Web Forum Application

Tree Forum is a full-stack web forum built with **C# and ASP.NET Core**, featuring user authentication, role-based access control, and persistent data storage using a relational SQL database. The project demonstrates core web application concepts including authentication, authorization, CRUD operations, and server-side rendering with Razor Pages.

---

## 📌 Project Overview

Tree Forum allows users to create discussion threads, comment on posts, and manage personal profiles within a secure, authenticated environment. The system enforces ownership-based access control, ensuring users can only modify their own content.

This project was built to demonstrate:
- Secure user authentication and authorization
- Relational data modeling for forum-style applications
- Full CRUD workflows with access restrictions
- Server-rendered UI using Razor Pages

---

## ✨ Features

- User registration and login using ASP.NET Identity  
- Create, edit, and delete discussion threads (owner-only)  
- Commenting system with user attribution  
- Public thread browsing for unauthenticated users  
- User profile pages displaying account details and authored threads  
- User account management (profile updates, avatar upload)  
- Dynamic navigation based on authentication state  

---

## 🛠️ Technology Stack

### Backend
- C# / ASP.NET Core  
- Razor Pages  
- ASP.NET Identity  
- Entity Framework Core  
- SQL Database  

### Frontend
- Razor Views  
- Server-side rendered HTML  
- CSS  

---

## 🔐 Authentication & Authorization

- Authentication handled via ASP.NET Identity  
- Authorization rules enforce:
  - Only authenticated users can create posts or comments
  - Users may edit or delete only their own discussions
  - Private pages (e.g., **My Threads**) are inaccessible to other users
- User data is securely persisted in a relational database

---

## 📸 Screenshots

### Public Forum (Unauthenticated User)
![Home Page - No User](Screenshots/home_page_no_user.png)

---

### Thread Discussion and Comments
![Discussion Page](Screenshots/discussion.png)  
![Comments on Post](Screenshots/comments_on_post.png)

---

### Authentication
![Login](Screenshots/log_in.png)  
![Register](Screenshots/register.png)

---

### Authenticated User Experience
![Home Page - Logged In](Screenshots/home_page_user_logged_in.png)  
![My Threads](Screenshots/user_threads2.png)

---

### User Profile & Management
![User Profile](Screenshots/user_profile.png)  
![Manage User](Screenshots/user_manage.png)  
![Change Profile Picture](Screenshots/changed_photo.png)

---

### Content Management
![Edit Discussion](Screenshots/edit_discussion.png)  
![Delete Discussion](Screenshots/delete_discussion.png)

---

## 📚 What This Project Demonstrates

- Full-stack web development with ASP.NET Core  
- Secure authentication and authorization workflows  
- Relational database design with Entity Framework  
- Server-side rendering using Razor Pages  
- Ownership-based access control for user-generated content  

---

## 🚀 Future Improvements

- Pagination for large discussion lists  
- Search and filtering functionality  
- Role-based moderation tools  
- REST API layer for a future SPA frontend  

---
