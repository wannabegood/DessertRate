1. dotnet publish -p:CompressionEnabled=false
1. remove files from wannabegood.github.io project.
1. copy files in bin\Release\net8.0\publish\wwwroot\* to wannabegood.github.io project.

That's good enough for testing locally.

You need this on the main branch for deployment 2025:
rename _framework folder to scripts
update scripts/blazor.webassembly.js; replace all "_framework" with "scripts"
update index.html replace all "_framework" with "scripts"

wait for github pages new deployment

URL:  wannabegood.github.io

https://formkeep.com/f/b7297b9b267f