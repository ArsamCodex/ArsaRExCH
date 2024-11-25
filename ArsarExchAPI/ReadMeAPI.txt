Ars WebAPI
The Ars WebAPI project is designed as a standalone API, providing secure authentication and authorization using JWT (JSON Web Tokens).
This API interacts with the same database as the associated project but maintains its independence by defining its own model classes.

Key Features
Separate Model Classes
The WebAPI defines its own models rather than referencing models from the other project. While these models may have similar structures, this approach ensures:

Clear separation of concerns.
Flexibility for future modifications tailored to the API's needs.
JWT Authentication and Authorization

Implements JWT for secure user authentication and authorization.
Ensures token-based access control for API endpoints.
Shared Database
The API uses the same database as the other project, leveraging Microsoft Identity Core for managing user authentication and authorization.

Design Philosophy
Independence: The WebAPI is self-contained and does not rely on direct project references to shared models, allowing for modular and decoupled design.
Security: With JWT and Microsoft Identity Core, the API ensures robust and secure authentication mechanisms.
Scalability: The architecture supports future enhancements without tight coupling to other projects.
