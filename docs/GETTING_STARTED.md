# Getting started

SiteKit is in active development. The repository contains the reusable core,
local Agent Host and a separate Umbraco Studio host. The hotel website and its
booking engine are not part of this project.

## Prerequisites

- .NET 10 SDK
- Git
- Optional: Docker Desktop

## Build the platform core

```powershell
dotnet restore SiteKit.sln
dotnet test SiteKit.sln --configuration Release
dotnet build SiteKit.sln --configuration Release
```

## Run the Studio host

Set a local administrator and secret before starting Studio. Do not use a
default password or commit these values:

```powershell
$env:SiteKit__AdminEmail = "you@example.com"
$env:SiteKit__AdminPassword = "use-a-long-random-local-secret"
$env:SITEKIT_DATA_ROOT = (Join-Path $PWD "data/sitekit")
dotnet run --project src/SiteKit.Studio
```

Open `http://localhost:5000/login` or the HTTPS address printed by ASP.NET Core.

The host uses Umbraco with a local SQLite database. For Docker, configure the
same two admin variables and run:

```powershell
docker compose up --build
```

Then open `http://localhost:5080/login`. The container build uses Debug because
the current Umbraco dependency graph includes ImageSharp, whose vendor requires
a license key for Release builds. The Studio extraction is still under active
verification; check the implementation ledger before using it with real data.

## Run the local Agent Host

```powershell
dotnet run --project src/SiteKit.AgentHost -- help
dotnet run --project src/SiteKit.AgentHost -- login
dotnet run --project src/SiteKit.AgentHost -- chat --project my-first-app
```

See [Agent Host](SITEKIT_AGENT_HOST.md) for local credential storage, plan
eligibility and tool boundaries.
