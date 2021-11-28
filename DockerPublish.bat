@echo off

cd PublicInfo.API
cmd /C docker build -f ./Dockerfile --force-rm -t publicinfoimg:dev ..

cd ../
cmd /C heroku container:push -a public-info  web
cmd /C heroku container:release -a public-info web

pause