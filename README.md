# BrixtaOS Backend Architecture Guide

## Current Project Status

### Phase 1 - API Foundation

✅ Complete

* ASP.NET API
* Endpoints
* Contracts
* Dependency Injection
* Program.cs setup

---

### Phase 2 - Workflow Engine

✅ Complete

* Workflow Definitions
* Workflow Instances
* Event Submission
* State Transitions
* Workflow Runtime

Verified flow:

```txt
LeadCreated
    ↓ MailSent
FollowUp
```

---

### Phase 3 - Organizations Foundation

✅ Complete

* Organization Entity
* OrganizationId relationships
* Domain models updated

---

### Phase 4 - PostgreSQL + Neon + EF Core

✅ Complete

* Neon Database
* EF Core
* DbContext
* Migrations
* Database Tables

Current tables:

```txt
Organizations
Users
Roles
WorkflowDefinitions
WorkflowInstances
EventDefinitions
EventInstances
```

---

# Overall Architecture

```txt
API
 ↓
Application
 ↓
Domain
 ↓
Infrastructure
 ↓
PostgreSQL (Neon)
```

---

# Request Flow

Example:

```http
POST /api/workflows
```

Request path:

```txt
Client
 ↓
WorkflowsEndpoints.cs
 ↓
WorkflowRuntime.cs
 ↓
DbContext / InMemoryDataStore
 ↓
Database
```

Response path:

```txt
Database
 ↑
DbContext
 ↑
Runtime
 ↑
Endpoint
 ↑
Client
```

---

# Layer Responsibilities

## Domain Layer

Location:

```txt
BrixtaOS.Domain
```

Contains:

```txt
Organization
User
Role
WorkflowDefinition
WorkflowInstance
EventDefinition
EventInstance
```

Purpose:

```txt
Business Objects
```

Rules:

```txt
NO ASP.NET
NO DATABASE CODE
NO HTTP
```

Example:

```csharp
public class Organization
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
```

---

## Application Layer

Location:

```txt
BrixtaOS.Application
```

Contains:

```txt
WorkflowRuntime
Validation Logic
Business Logic
```

Purpose:

```txt
How the business behaves
```

Examples:

```csharp
ApplyEvent()
CanApplyEvent()
CreateWorkflow()
```

Rules:

```txt
Business Rules Only
No Database Logic
```

---

## Infrastructure Layer

Location:

```txt
BrixtaOS.Infrastructure
```

Contains:

```txt
BrixtaDbContext
InMemoryDataStore
```

Purpose:

```txt
How data is stored
```

Examples:

```txt
PostgreSQL
Redis
File System
Blob Storage
```

Rules:

```txt
Storage Only
```

---

## API Layer

Location:

```txt
BrixtaOS.Api
```

Contains:

```txt
Endpoints
Contracts
Program.cs
```

Purpose:

```txt
HTTP In
HTTP Out
```

Rules:

```txt
Receive Request
Call Business Logic
Return Response
```

---

# Folder Structure

```txt
BrixtaOS.Api
│
├── Contracts
├── Endpoints
└── Program.cs

BrixtaOS.Application
│
└── Runtime

BrixtaOS.Domain
│
├── Organizations
├── Users
├── Roles
├── Workflows
└── Events

BrixtaOS.Infrastructure
│
├── Persistence
│   └── BrixtaDbContext.cs
│
└── Storage
    └── InMemoryDataStore.cs
```

---

# Adding A New Endpoint

Example:

```txt
POST /api/organizations
```

---

## Step 1

Create Contract

Location:

```txt
Contracts/Organizations/
```

File:

```txt
CreateOrganizationRequest.cs
```

Example:

```csharp
public sealed record CreateOrganizationRequest
{
    public required string Name { get; init; }
}
```

---

## Step 2

Create Endpoint

Location:

```txt
Endpoints/
```

File:

```txt
OrganizationsEndpoints.cs
```

Example:

```csharp
app.MapPost("/api/organizations", ...);
```

---

## Step 3

Register Endpoint

Program.cs

```csharp
OrganizationsEndpoints.Map(app);
```

Done.

---

# Adding A New Domain Entity

Example:

```txt
Department
```

Create:

```txt
BrixtaOS.Domain/Departments/Department.cs
```

```csharp
public class Department
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
```

---

# Adding A New Database Table

## Step 1

Create Domain Entity

Example:

```csharp
Department.cs
```

---

## Step 2

Add DbSet

File:

```txt
BrixtaDbContext.cs
```

Add:

```csharp
public DbSet<Department> Departments { get; set; }
```

---

## Step 3

Create Migration

```bash
dotnet ef migrations add AddDepartments \
--project BrixtaOS.Infrastructure/BrixtaOS.Infrastructure.csproj \
--startup-project BrixtaOS.Api/BrixtaOS.Api.csproj
```

---

## Step 4

Apply Migration

```bash
dotnet ef database update \
--project BrixtaOS.Infrastructure/BrixtaOS.Infrastructure.csproj \
--startup-project BrixtaOS.Api/BrixtaOS.Api.csproj
```

Done.

Table exists.

---

# Adding Business Logic

DO NOT put business logic inside endpoints.

Bad:

```csharp
app.MapPost(...)
{
    // 200 lines of logic
}
```

Good:

```txt
Application/
```

Example:

```txt
LeaveRuntime.cs
```

```csharp
CanApproveLeave()

ApproveLeave()

RejectLeave()
```

Endpoint should call runtime.

Runtime should contain logic.

---

# Typical Feature Implementation

Example:

```txt
Department Feature
```

Files touched:

```txt
1. Domain
---------
Department.cs

2. Infrastructure
-----------------
BrixtaDbContext.cs

3. Contracts
------------
CreateDepartmentRequest.cs

4. Endpoints
------------
DepartmentsEndpoints.cs

5. Program
----------
Program.cs
```

---

# Database Change Rule

Whenever a Domain Model changes:

Example:

```csharp
public string Description { get; set; }
```

added to:

```csharp
Organization.cs
```

You must create migration.

Commands:

```bash
dotnet ef migrations add AddOrganizationDescription \
--project BrixtaOS.Infrastructure/BrixtaOS.Infrastructure.csproj \
--startup-project BrixtaOS.Api/BrixtaOS.Api.csproj
```

Then:

```bash
dotnet ef database update \
--project BrixtaOS.Infrastructure/BrixtaOS.Infrastructure.csproj \
--startup-project BrixtaOS.Api/BrixtaOS.Api.csproj
```

Rule:

```txt
Domain Changed
=
Migration Required
```

---

# Current Workflow Runtime Flow

Current implementation:

```txt
POST /api/events
 ↓
EventsEndpoints.cs
 ↓
WorkflowRuntime.ApplyEvent()
 ↓
WorkflowInstance.CurrentState changes
 ↓
Response returned
```

This is the core BrixtaOS engine.

---

# Debugging Guide

When something breaks ask:

```txt
API?
Application?
Infrastructure?
Database?
```

Not all at once.

---

## HTTP Errors

Examples:

```txt
400
404
415
```

Usually:

```txt
Contracts
Endpoints
```

---

## Workflow Errors

Examples:

```txt
Wrong state transition
Invalid event
```

Usually:

```txt
WorkflowRuntime
```

---

## Persistence Errors

Examples:

```txt
Data missing
Data disappears
Data not saved
```

Usually:

```txt
DbContext
Database
```

---

# Roadmap

## Phase 5

Persist Organizations

```txt
POST /api/organizations
GET /api/organizations
GET /api/organizations/{id}
```

Use:

```txt
DbContext
```

not:

```txt
InMemoryDataStore
```

---

## Phase 6

Persist Users

Replace InMemoryDataStore with EF Core.

---

## Phase 7

Persist Roles

Replace InMemoryDataStore with EF Core.

---

## Phase 8

Persist Workflow Definitions

Replace InMemoryDataStore with EF Core.

Design proper persistence for:

```txt
WorkflowState
WorkflowTransition
```

---

## Phase 9

Permissions

Examples:

```txt
CEO
Manager
Executive
Intern
```

Role-based event permissions.

---

## Phase 10

Authentication

```txt
JWT
Refresh Tokens
Organization Membership
```

---

## Phase 11

Orleans

Only after:

```txt
Organizations
Users
Roles
Permissions
Persistence
Authentication
```

are complete.

Use Orleans for:

```txt
Long Running Workflows
Timers
Reminders
Escalations
Distributed Processing
```

Example:

```txt
Lead Created
 ↓
Wait 3 Days
 ↓
No Response
 ↓
Send Reminder
 ↓
Wait 7 Days
 ↓
Escalate
```

---

# Golden Rule

Always build in this order:

```txt
Organizations
 ↓
Users
 ↓
Roles
 ↓
Permissions
 ↓
Workflow Persistence
 ↓
JWT
 ↓
Orleans
```

Never:

```txt
Orleans
 ↓
More Orleans
 ↓
Even More Orleans
```

Build boring things first.

Scale them later.

That is how BrixtaOS reaches production.
