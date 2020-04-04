## The Business Process
Below are the assumptions and requirements for the business.

- Types of **users**:
  - **Owner** - the owner of the kennel.
  - **Staff** - a staff member at the kennel.
  - **Customer** - a customer of the kennel.
- Owners have admin access over the **organization** and **locations**.
- Staff users are able to compl ete **tasks** related to daily business - varying levels or permissions.
- Customers may add their **pets** and manage personal and pet details.
- Customers may book a **kennel** or **grooming slot** *or both*.

### Data Dependencies
- User -> Organization (OwnerId) -> Location (OrganizationId)
- User -> Pet (OwnerId)
- User & Pet & Location ->  Booking (OwnerId, PetId, LocationId)


## Architecture
Described below is the software architecture type.

Essentially, the domain model is at the center, wrapped in an application layer which embeds use cases, adapts the application to implementation details, and all dependencies should pointer inward toward the domain.

#### Domain Layer
The domain layer consists of **entities** or **aggregates**.

From _Clean Architecture_, Uncle Bob said:
"An entity is an object within our computer system that embodies a small set of critical business rules operating on critical business data."


#### Application Layer
The application layer includes commands or queries that do not contain business logic