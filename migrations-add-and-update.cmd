dotnet-ef database drop -f -c AccountsDbContext -p .\src\Accounts\Accounts.Infrastructure\ -s .\src\Web\

dotnet-ef migrations remove -c AccountsDbContext -p .\src\Accounts\Accounts.Infrastructure\ -s .\src\Web\


dotnet-ef migrations add init -c AccountsDbContext -p .\src\Accounts\Accounts.Infrastructure\ -s .\src\Web\

dotnet-ef database update -c AccountsDbContext -p .\src\Accounts\Accounts.Infrastructure\ -s .\src\Web\
