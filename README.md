<h1>🎬 Movie Theatre App</h1>

<p><strong>Movie Theatre App</strong> is a full-stack web application built with <strong>React 19</strong> on the frontend and <strong>.NET 8 Web API</strong> on the backend. It replicates the core functionality of a modern cinema booking system—allowing users to browse movies, explore actor and theatre details, and manage content as an admin.</p>

<p>The project follows <strong>Clean Architecture</strong> and <strong>SOLID principles</strong> to ensure maintainability, scalability, and separation of concerns.</p>

<blockquote>⚠️ <em>Note: Working on Docker and CI/CD.</em></blockquote>

<hr />

<h2>🚀 Features</h2>
<ul>
  <li>🔐 <strong>User Authentication</strong> – Secure login and registration with JWT.</li>
  <li>🔎 <strong>Search & Filter</strong> – Easily search movies, actors, and theatres.</li>
  <li>✏️ <strong>Admin Editing</strong> – Admin users can edit all entities in the system.</li>
  <li>📱 <strong>Responsive UI</strong> – Fully responsive design for desktop and mobile devices.</li>
  <li>🗺️ <strong>Interactive Maps</strong> – Theatres displayed with Leaflet maps integration.</li>
</ul>

<hr />

<h2>🧰 Technology Stack</h2>

<h3>🖥️ Backend – .NET 8 Web API</h3>
<ul>
  <li><strong>ASP.NET Core</strong> – High-performance framework for modern APIs.</li>
  <li><strong>Entity Framework Core</strong> – ORM for PostgreSQL with code-first migrations.</li>
  <li><strong>JWT Authentication</strong> – Secure token-based authentication for protected routes.</li>
  <li><strong>PostgreSQL</strong> – Relational database for persistent, structured data.</li>
  <li><strong>Testing</strong> – <code>xUnit</code> for unit testing, with <code>FakeItEasy</code> for mocking.</li>
</ul>

<h3>🌐 Frontend – React 19 + TypeScript</h3>
<ul>
  <li><strong>React 19</strong> – Component-based architecture with Hooks and Context API.</li>
  <li><strong>React Hook Form</strong> – Performant and accessible form handling.</li>
  <li><strong>React Router DOM</strong> – Declarative routing for SPA navigation.</li>
  <li><strong>React Leaflet</strong> – Interactive map display using Leaflet in React.</li>
  <li><strong>Axios</strong> – Promise-based HTTP client for REST API integration.</li>
  <li><strong>Bootstrap 5</strong> – Responsive UI components and grid system.</li>
  <li><strong>Testing Tools:</strong>
    <ul>
      <li><code>@testing-library/react</code> – UI behavior and interaction testing.</li>
      <li><code>@testing-library/user-event</code> – Simulated user actions.</li>
      <li><code>@testing-library/jest-dom</code> – Enhanced assertions for DOM testing.</li>
      <li><code>msw</code> – API mocking with Mock Service Worker for integration tests.</li>
    </ul>
  </li>
</ul>

<h3>☁️ Cloud & DevOps</h3>
<ul>
  <li><strong>Azure DevOps (CI/CD)</strong> – Pipelines for automated testing and deployment.</li>
  <li><strong>Azure Container Apps</strong> – Serverless deployment for containerized backend.</li>
  <li><strong>Docker</strong> – Containerization of backend services for portability.</li>
  <li><strong>Cloudinary</strong> – Media management service for uploading and optimizing images.</li>
</ul>

<hr />

<h2>📦 Project Architecture</h2>

<h3>🖥️ Frontend (React 19 + TypeScript)</h3>
<pre>
src/
├── api/                  # Axios instance, interceptors, API services
│   └── services/
├── components/           # Reusable UI components (TextField, Toast, etc.)
├── contexts/             # Global contexts (AuthContext, ToastContext, etc.)
├── features/             # Domain-specific logic (actor, movie, genre, theatre)
│   └── movie/
├── hooks/                # Custom hooks (useAuth, useToast, etc.)
├── layouts/              # Shared layout components (MainLayout)
├── pages/                # Route-level UI views (Movies.tsx, Login.tsx, etc.)
├── routes/               # Route config and guards
├── types/                # Shared TypeScript models (DTOs, enums, etc.)
├── utils/                # Mappers, helpers, formatting utilities
├── styles/               # Global and component styles (Bootstrap, overrides)
├── setupTests.ts         # Testing environment setup
└── index.tsx             # App root entry point
</pre>

<h3>🧠 Backend (.NET 8 Web API + Clean Architecture)</h3>
<pre>
src/
├── API/                  # Entry point layer (Controllers, Middleware, DTOs)
│   ├── Controllers/
│   ├── Middleware/
│   └── Models/
├── Application/          # Business logic layer (Use Cases, DTOs, Interfaces)
│   ├── DTOs/
│   ├── Interfaces/
│   └── Mappings/
├── Domain/               # Core domain entities and logic
│   ├── Entities/
│   └── Interfaces/
├── Infrastructure/       # Implementation of infrastructure concerns
│   ├── Persistence/      # EF Core DbContext, Repositories
│   ├── Migrations/       # EF Core migration files
│   └── Services/         # External services like Cloudinary, Auth
</pre>
