1. On local machine run dotnet publish -p:CompressionEnabled=false in DessertRate.
1. On local machine keep .git folder but remove files from wannabegood.github.io project (sibling folder to DessertRate)
1. On local machine copy files in bin\Release\net8.0\publish\wwwroot\* to wannabegood.github.io project.
1. rename _framework folder to scripts
1. update scripts/blazor.webassembly.js; replace all "_framework" with "scripts"
1. update index.html replace all "_framework" with "scripts"
1. Push to wannabegood.githup.io project (main branch)

*** IMPORTANT ***
Use dl.dropboxusercontent.com in image names!!!!

https://dash.cloudflare.com/d72297d0edde80b39909290b703bfa75/workers/d1
Update ImageUrl column after uploading images to dropbox.  Need to share to get link.

Example URL:
https://dl.dropboxusercontent.com/scl/fi/gbc91ksxf6qnnh59djuyd/Tart-09.jpg?rlkey=ji92txlfpush6cf1eravz0ou6&st=bxkx88xi&raw=1

Change www to dl
Change dropbox.com to dropboxusercontent.com
Replace &dl=0 with &raw=1

Github url:  https://github.com/wannabegood/DessertRate

Application URL:  wannabegood.github.io

After submissions are made, use Postman to access results.
Copy/paste array of results in to a json file.
Open json file in LibreOffice and easily sort results.

=============================================================
dessert-results

cd to dessert-results folder

Add Secret
npx wrangler pages secret put API_TOKEN --project-name=dessert-results

Preview deployment
npx wrangler pages deploy . --project-name=dessert-results   
wrangler will give you a URL


Production deployment
npx wrangler pages deploy . --project-name=dessert-results --branch=main
https://dessert-results.pages.dev/
or
https://c3532166.dessert-results.pages.dev/

===============================================================
dessert-image-urls

cd dessert-image-urls

Deploy index.html
npx wrangler deploy

Open
https://dessert-image-urls.gtfullton.workers.dev