# SiteKit implementation ledger

This ledger describes the clean SiteKit repository, not the earlier hotel proof
of concept. Only results executed against this repository are treated as current
verification.

## Product boundary

The repository includes a reusable core, local Agent Host, independent Umbraco
Studio host, shared component editor, project/screen/import/module editors,
generated runtime and contract tests. It deliberately excludes hotel routes,
booking, inventory, payment integration, hotel import logic and hotel demo data.
Vertical features belong in optional modules.

## Milestones

| Milestone | Scope | Clean repository status |
| --- | --- | --- |
| M0 — Security and baseline | Clean build, secure Studio access and isolated storage | Partial: clean .NET 10 Debug build; SiteKit-only configured administrator session; Umbraco first boot/database install still failing verification; production hardening pending |
| M1 — Universal foundation | Project manifests, registries and module contracts | Partial: project/component/entity/screen/import/module contracts and registries extracted; Studio host adapter boundary still needs review |
| M2 — Dynamic Component Studio | Visual, schema-driven reusable components | Partial: schema and component UI extracted; live host authoring, export and cross-project reuse must be reverified here |
| M3 — ChatGPT integration | Official Sign in with ChatGPT and local orchestration | Partial: local Agent Host and typed tools compile; live account authorization and eligible inference unverified |
| M4 — Project generation | Independent sites and app projects | Partial: website/application/combined/API generator extracted; host integration and clean-repo acceptance remain |
| M5 — Application Studio | Entities, permissions, CRUD and screens | Partial: JSON data store and generated SQLite CRUD contracts are tested; Studio UI CRUD and durable operational database adapter require verification |
| M6 — Workflows and modules | Umbraco automation and installable packages | Partial: declarative module package services extracted; Automate adapter intentionally left for an explicit integration boundary |
| M7 — AI application generation | Create and modify projects through tools | Partial: local typed tool orchestration and generation code extracted; live end-to-end flow unverified |
| M8 — Production readiness | Accessibility, security, reliability and performance | Pending beyond core contract verification |

## Verified in this repository

| Check | Result |
| --- | --- |
| .NET 10 Debug solution build | Passed, 0 errors; warnings include upstream package advisories and ImageSharp licensing notice |
| .NET 10 Release core and generated runtime suite | Passed, 29/29 tests |
| Standalone Studio runtime | Failed initial boot: Umbraco reports SQLite `unable to open database file`; do not treat the host as ready yet |
| ChatGPT account/inference | Not attempted; no account authorization performed |
| Docker image | Not verified after extraction |

Generated-app tests initially failed because this workstation's default `dotnet`
command resolves to .NET 8/9; they passed after running with `F:\dotnet10-sitekit`
at the front of `PATH` and `DOTNET_ROOT` set accordingly. CI installs .NET 10.

## Architecture decisions

1. Use Umbraco for editorial content, assets, publishing and native CMS
   editing. Use application stores for operational records.
2. Keep components shared and versioned. Projects reference definitions and
   provide independent content and appearance.
3. Keep simple components declarative and validated. Custom developer code
   belongs in a controlled build/package pipeline, never in a production
   request interpreter.
4. Keep ChatGPT credentials and inference in the user-controlled Agent Host.
   Eligibility depends on OpenAI's current program rules.
5. Keep vertical solutions as modules. The hotel POC is outside this repository.
6. Verify every Umbraco adapter against the supported package version and a
   clean installed instance before marking its milestone complete.

## Immediate follow-up

- Fix local SQLite path resolution and complete the Umbraco install/login smoke.
- Add clean-repo tests for Studio authentication, entity CRUD, component
  authoring and per-project reuse.
- Verify Studio visual assets and API routes against this host.
- Review transitive dependency advisories and retain only narrow, evidenced
  exceptions.
- Add generated project persistence/migration and backup verification.
- Verify Docker from this repository, then capture new screenshots from the
  clean host. Existing screenshots are historical captures from the POC host.
