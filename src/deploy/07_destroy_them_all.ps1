#destroy them allow
kubectl delete -f barmanager_ingress.yml
kubectl delete -f barmanager_service.yml
kubectl delete -f barmanager_deployment.yml