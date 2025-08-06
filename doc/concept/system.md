```mermaid
architecture-beta
    group backend(cloud)[Backend]

    service db(database)[PostgreSQL] in backend
    service appService(server)[App Service] in backend

    appService:R -- L:db
```
