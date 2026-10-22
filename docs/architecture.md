# SiteKit architecture

SiteKit separates editorial publishing from software application state.

- **Umbraco CMS** owns editorial content, media, and publishing workflows.
- **SiteKit.Core** owns validated project, component, entity, screen, module,
  permission and import contracts, registries, and project generation.
- **SiteKit Studio** provides project, component, screen, entity and module
  authoring inside a standalone Umbraco host.
- **SiteKit Agent Host** runs on the user's machine, keeps authentication and
  agent history locally, and calls SiteKit through typed allowlisted tools.
- **Generated applications** are independent .NET projects. Business data
  belongs in operational data stores; it is not forced into the CMS content tree.

Component definitions are shared, versioned packages. A project places a
definition by reference and supplies independent content and appearance.
Declarative definitions render through validated primitives. Custom developer
code belongs in a controlled build and installation pipeline.

Industry-specific systems such as hotel bookings are optional modules outside
the platform core. See the [implementation ledger](SITEKIT_IMPLEMENTATION_PLAN.md)
for current integration and verification boundaries.
