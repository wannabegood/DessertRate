1. On local machine run dotnet publish -p:CompressionEnabled=false in DessertRate.
1. On local machine keep .git folder but remove files from wannabegood.github.io project (sibling folder to DessertRate)
1. On local machine copy files in bin\Release\net8.0\publish\wwwroot\* to wannabegood.github.io project.
2. Push to wannabegood.githup.io project.

That's good enough for testing locally.

You need this on the main branch for deployment 2026:
rename _framework folder to scripts
update scripts/blazor.webassembly.js; replace all "_framework" with "scripts"
update index.html replace all "_framework" with "scripts"

Use dl.dropboxusercontent.com in image names

wait for github pages new deployment

URL:  wannabegood.github.io

https://formkeep.com/f/b7297b9b267f