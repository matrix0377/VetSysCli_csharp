# VetSysCli v1.0

Sistema de gestão para clínica veterinária.

## Stack
- Backend: C# / .NET 8.0.422
- Banco de dados: PostgreSQL 17
- Frontend: Blazor WebAssembly
- ORM: EF Core (Npgsql)
- Autenticação: JWT (planejada)

## Requisitos
- .NET SDK 8.0.422
- Docker (para Postgres)
- PostgreSQL 17 (ou usar docker-compose)

## Como rodar
1. Configure `src/VetSysCli.Api/appsettings.Development.json` com a connection string.
2. Suba Postgres:
   `docker-compose up -d`
3. Instale dotnet-ef se necessário:
   `dotnet tool install --global dotnet-ef --version 8.*`
4. Crie e aplique migrations:
   `dotnet ef migrations add InitialCreate --project src/VetSysCli.Infrastructure --startup-project src/VetSysCli.Api`
   `dotnet ef database update --project src/VetSysCli.Infrastructure --startup-project src/VetSysCli.Api`
5. Rode a API:
   `dotnet run --project src/VetSysCli.Api`
6. Rode o frontend:
   `dotnet run --project src/VetSysCli.Web`

## Observações
- Versão do C# usada: **C# / .NET 8.0.422**
- Formatos de foto aceitos: jpg, jpeg, png, gif, tiff, tif, webp
- Design: paleta em degradê, poucas cores, foco em legibilidade
