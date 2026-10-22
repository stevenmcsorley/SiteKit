# SiteKit repository guide

SiteKit is a reusable website and business application construction platform
on Umbraco. It is not a hotel starter. Keep products and data models industry
neutral; industry-specific solutions belong in installable modules.

## Project map

- `src/SiteKit.Core`: validated components, projects, entities, screens,
  permissions, imports, modules and generation contracts.
- `src/SiteKit.AgentHost`: user-controlled ChatGPT connection, local tool
  orchestration and stdio MCP host.
- `src/SiteKit.Studio`: SiteKit Studio host and Umbraco integration adapters.
- `tests/SiteKit.Core.Tests`: core and generated application contract tests.
- `docs/SITEKIT_IMPLEMENTATION_PLAN.md`: milestone ledger and verified scope.

## Rules

- Use Umbraco for editorial content and its native Block Grid/List, media,
  publishing and backoffice features. Keep operational business records in
  application data stores suited to transactional work.
- Keep component definitions shared and versioned. Project placements carry
  project-specific content and appearance.
- Developer source must run through a controlled build/package pipeline. Never
  execute arbitrary uploaded code inside a production request.
- Keep ChatGPT credentials and inference in the local, user-controlled Agent
  Host. Never request passwords/cookies or make an API key mandatory when an
  eligible supported plan-sharing flow is available.
- Treat imported content and AI tool inputs as untrusted. Validate contracts,
  constrain filesystem/process operations, authorize consequential actions and
  retain audit evidence.
- Do not commit secrets, local databases, generated dependencies or credentials.
- Update the implementation ledger with actual build/test evidence. Do not
  claim an acceptance check passed unless it was executed.

## Build

Use the .NET 10 SDK:

```powershell
dotnet restore SiteKit.sln
dotnet test SiteKit.sln --configuration Release
dotnet build SiteKit.sln --configuration Release
```

Studio host integration tests require the supported local Umbraco environment.
Record separately what was validated with core tests and what was verified
against a running host.
