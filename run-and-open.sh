#!/bin/bash

    # URL de l'application (ajustez si nécessaire)
    APP_URL="http://localhost:5199"

    # Lancer l'application en arrière-plan
    dotnet run --launch-profile http &

    # Attendre quelques secondes que le serveur démarre
    sleep 5

    # Ouvrir l'URL dans le navigateur par défaut
    xdg-open $APP_URL