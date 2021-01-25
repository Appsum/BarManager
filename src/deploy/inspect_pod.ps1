#get interactive pod session and do a curl
kubectl get pods
#get pod bartender
kubectl exec --stdin --tty bartender-dev-7dbbb98f6d-qlxfc /bin/bash
#install curl
apt-get update; apt-get install curl
curl http://10.244.0.11/healthz