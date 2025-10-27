# FinanTechSolutionsSample (Final)

Proyecto demo .NET 8 listo para Visual Studio Code.

## Instrucciones

1. Asegúrate de tener .NET 8 SDK instalado.
2. Abre la carpeta `FinanTechSolutionsSample_net8_final` en VS Code.
3. En la terminal:
   ```
   dotnet restore
   dotnet build
   dotnet run
   ```
4. Abre en el navegador `http://localhost:5000` (o el puerto que muestre dotnet run).
5. Usa las cuentas de ejemplo: `acct-1001`, `acct-1002`.

Notas:
- La base de datos SQLite se crea en `FinanTech.db`.
- SignalR hub en `/hubs/notifications`.
