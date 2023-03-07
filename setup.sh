## Creat react bunlde files.
## Note: they'll be stored under `wwwroot\js\react\wordpress-reader` - See webpack.config.js
cd react
npm i
npx webpack
cd ..
cd reactWebsocket
npm i
npx webpack
cd ..

## Build Docker image
docker build . -t thekey_image
docker container create --name thekey -i -t -p 1000:80 -p 1001:443 thekey_image

## Run container
docker compose up -d