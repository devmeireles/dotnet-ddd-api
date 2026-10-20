# ChefHero

## Requirements

- .NET SDK 10.0.302 (or a later .NET 10 feature band)
- Node.js and npm
- Docker Compose
- Entity Framework Core CLI (`dotnet-ef` 10.x) for database migrations
- Tailwind CSS v4 and the ShadcnBlazor package are configured in `src/ChefHero.Web`

## Build

Run from the repository root:

```sh
dotnet restore ChefHero.slnx
dotnet build ChefHero.slnx
```

## Local development

1. Create a root `.env` file for Docker Compose:

   ```dotenv
   POSTGRES_DB=chefhero
   POSTGRES_USER=chefhero
   POSTGRES_PASSWORD=<your-local-password>
   POSTGRES_PORT=5432
   ```

2. Configure the API connection string and JWT signing key with User Secrets:

   ```sh
   dotnet user-secrets set "ConnectionStrings:ChefHero" "Host=localhost;Port=5432;Database=chefhero;Username=chefhero;Password=<your-local-password>" --project src/ChefHero.API
   dotnet user-secrets set "Jwt:SecretKey" "replace-with-a-long-random-local-secret" --project src/ChefHero.API
   ```

3. Install the front-end development dependencies:

   ```sh
   npm install --prefix src/ChefHero.Web
   ```

4. Start PostgreSQL and apply migrations:

   ```sh
   npm --prefix src/ChefHero.Web run dev:setup
   ```

5. Run the API, Blazor front end, and Tailwind watcher together in one terminal:

   ```sh
   npm --prefix src/ChefHero.Web run dev
   ```

The front end runs at `http://localhost:5114`; the API runs at `http://localhost:5251`. In Development, OpenAPI and Scalar are available from the API at `/openapi/v1.json` and `/scalar`. `dotnet watch` reloads the API and Blazor app on code changes; Tailwind rebuilds its stylesheet when Razor or CSS sources change. Press Ctrl+C to stop all three processes.

The front end is organized using atomic-design layers: `Components/Atoms`, `Components/Molecules`, and `Components/Organisms/Showcases`. `Pages` compose the domain-specific showcase components; the home page uses `MenuShowcase`, which renders reusable `MenuCard` components and a ShadcnBlazor `Button`.

Rebuild the Tailwind stylesheet without starting the watchers:

```sh
npm --prefix src/ChefHero.Web run build:css
```

Stop the database container when finished:

```sh
docker compose down
```

## Docker

Start the PostgreSQL service in the background:

```sh
docker compose up -d postgres
```

View its logs or check its status:

```sh
docker compose logs -f postgres
docker compose ps
```

Stop the service:

```sh
docker compose down
```

The named `chefhero-postgres-data` volume keeps database data when the container is stopped. `docker compose down -v` also deletes that volume and its data.

## Database migrations

Install the EF Core CLI if it is not already available:

```sh
dotnet tool install --global dotnet-ef --version 10.0.12
```

Create a migration after changing the EF model:

```sh
dotnet ef migrations add <MigrationName> --project src/ChefHero.Infrastructure --startup-project src/ChefHero.API --output-dir Persistence/Migrations
```

Apply migrations to the configured database:

```sh
dotnet ef database update --project src/ChefHero.Infrastructure --startup-project src/ChefHero.API
```

List migrations:

```sh
dotnet ef migrations list --project src/ChefHero.Infrastructure --startup-project src/ChefHero.API
```

To roll back to a specific migration, replace `<MigrationName>` with its name:

```sh
dotnet ef database update <MigrationName> --project src/ChefHero.Infrastructure --startup-project src/ChefHero.API
```

To remove the most recent migration that has not been applied to the database:

```sh
dotnet ef migrations remove --project src/ChefHero.Infrastructure --startup-project src/ChefHero.API
```
