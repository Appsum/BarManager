#login to acr
az acr login --name crbarmanagerjan
# docker build
docker build -f ../bartender/DockerFile -t crbarmanagerjan.azurecr.io/barmanager:1.0.2 ../../
# docker push
docker push crbarmanagerjan.azurecr.io/barmanager:1.0.2