@echo off

copy Dockerfile PublicInfo.API

cd PublicInfo.API
cmd /C docker build -f ./Dockerfile --force-rm -t publicinfoimg:dev ..
del "Dockerfile"

cd ../
cmd /C heroku container:push -a public-info  web
cmd /C heroku container:release -a public-info web

docker image prune -a -f

pause


