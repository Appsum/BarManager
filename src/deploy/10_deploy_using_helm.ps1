#deploy using helm
helm upgrade --namespace default --install --values ../bartender/charts/bartender-development/values.yaml --set image.tag=1.0.2 --wait bartender-dev ../bartender/charts/bartender-development