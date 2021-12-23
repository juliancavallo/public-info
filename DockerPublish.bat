@echo off

cmd /C docker build -t publicinfoimg ..

cmd /C heroku container:push -a public-info  web
cmd /C heroku container:release -a public-info web

docker image prune -a -f

pause


