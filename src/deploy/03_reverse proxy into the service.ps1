#reverse proxy into the service
kubectl port-forward service/bartender-dev 8888:80