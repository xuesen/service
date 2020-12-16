rd ..\maintainpublish-ubuntu /S /Q
dotnet publish ..\Maintain.Api\IIMes.Services.Maintain.Api.csproj -c release --framework netcoreapp3.1 -o ..\maintainpublish-ubuntu -r ubuntu.20.04-x64 --self-contained true
copy .\start-maintainservice.bat ..\maintainpublish-ubuntu

rd ..\runtimepublish-ubuntu /S /Q
dotnet publish ..\Runtime.Api\IIMes.Services.Runtime.Api.csproj -c release --framework netcoreapp3.1 -o ..\runtimepublish-ubuntu -r ubuntu.20.04-x64 --self-contained true
copy .\start-runtimeservice.bat ..\runtimepublish-ubuntu

rd ..\dcconsumerpublish-ubuntu /S /Q
dotnet publish ..\DCConsumer.Service\DCConsumer.Service.csproj -c release --framework netcoreapp3.1 -o ..\dcconsumerpublish-ubuntu -r ubuntu.20.04-x64 --self-contained true
copy .\start-dcconsumerservice.bat ..\dcconsumerpublish-ubuntu

