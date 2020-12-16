rd ..\maintainpublish /S /Q
dotnet publish ..\Maintain.Api\IIMes.Services.Maintain.Api.csproj -c release --framework netcoreapp3.1 -o ..\maintainpublish -r win81-x64 --self-contained true
copy .\install-maintainservice.bat ..\maintainpublish
copy .\uninstall-maintainservice.bat ..\maintainpublish

rd ..\runtimepublish /S /Q
dotnet publish ..\Runtime.Api\IIMes.Services.Runtime.Api.csproj -c release --framework netcoreapp3.1 -o ..\runtimepublish -r win81-x64 --self-contained true
copy .\install-runtimeservice.bat ..\runtimepublish
copy .\uninstall-runtimeservice.bat ..\runtimepublish

rd ..\dcconsumerpublish /S /Q
dotnet publish ..\DCConsumer.Service\DCConsumer.Service.csproj -c release --framework netcoreapp3.1 -o ..\dcconsumerpublish -r win81-x64 --self-contained true
copy .\install-dcconsumerservice.bat ..\dcconsumerpublish
copy .\uninstall-dcconsumerservice.bat ..\dcconsumerpublish

rd ..\privilegepublish /S /Q
dotnet publish ..\IIMes.Infra.Privilege.Api\IIMes.Infra.Privilege.Api.csproj -c release --framework netcoreapp3.1 -o ..\privilegepublish -r win81-x64 --self-contained true
copy .\install-privilegeservice.bat ..\privilegepublish
copy .\uninstall-privilegeservice.bat ..\privilegepublish
