dotnet publish -p:CompressionEnabled=false

remove files from wannabegood.github.io project.
copy files in bin\Release\net8.0\publish\wwwroot\* to wannabegood.github.io project.
rename _framework folder to scripts
update scripts/blazor.webassembly.js; replace all "_framework" with "scripts"
update index.html replace all "_framework" with "scripts"

wait for github pages new deployment

URL:  wannabegood.github.io