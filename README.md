Project: Simple-User-Management-API (https://github.com/beannguyen/Simple-User-Management-API)

Description:
- This project is divided from big project called Simple-User-Management
- This project focuses on Back-End (C# - .NET core 3.0/3.1) and Database (MSSQL)
- Front-End of big project can be refered from https://github.com/beannguyen/Simple-User-Management-Dashboard

Feature:
- Use .NET core 3.0/3.1 and Entity framework to build API system for project (UI described in FE project mentioned above)
- Use code first to generate Database from system model
- Provide Authentication method:
    - Authentication and authorization for SPAs (https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-3.1)
    - Provide API to authenticate with google
- Implement UserController for user management - add, delete, update (apply Repository Pattern (1) & UnitOfWork (2))
- Provide Roles, Permissions (3) for future extension
- Build BE for realtime chat (suggest using SignalR or other code base support, Ex: (4))

References:
- (1): https://codewithshadman.com/repository-pattern-csharp/
- (2): https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
- (3): https://www.thereformedprogrammer.net/a-better-way-to-handle-authorization-in-asp-net-core/
- (4): https://medium.com/@kent_19698/build-a-real-time-chat-app-from-scratch-using-vue-js-and-c-in-5-minutes-599387bdccbb
