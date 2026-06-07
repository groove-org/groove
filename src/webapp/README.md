# webapp

## Nswag Client Generation

*dotnet 9.x muss installiert sein.*

1. `dotnet tool install --global NSwag.ConsoleCore`
2. `nswag run nswag.json`
3. Client wird unter `lib/apiClient.ts` abgelegt.