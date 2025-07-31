# Branching

The development within this repository will follow the strategy described within this file.

## Branches

The `main` branch is continuously deployed to the **production** environment.
Contributions to `main` may only occur through pull requests from `dev`.

The `dev` branch is continuously deployed to the **test** environment.
Contributions to `dev` may only occur through pull requests from `feature`.

A `hotfix` branch may be used to directly contribute a hotfix to the `main` branch and thus the **production environment**.

A `feature` branch must be used whenever contributing a new feature. When the feature is ready, it should be merged into the `dev` via a pull request.

```mermaid
---
config:
  gitGraph:
    mainBranchOrder: 2
---
gitGraph
    commit
    branch dev order: 3
    commit

    branch feature/xyz order: 4
    commit
    commit
    checkout dev
    merge feature/xyz

    checkout main
    commit
    merge dev

    branch hotfix order: 1
    commit
    checkout main
    merge hotfix
```
