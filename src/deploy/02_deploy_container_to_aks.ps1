#push deployment to aks
#this command set the default kubeconfig 
az aks get-credentials -g rg-egssis-course -n egssis-course-cluster
#now deploy your application using a seperate file

kubectl apply -f barmanager_deployment.yml

#now deploy the service
kubectl apply -f barmanager_service.yml

kubectl describe deployments bartender-dev
