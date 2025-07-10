# WebPortal - Votre Portail Web Multifonctionnel

Bienvenue sur WebPortal, une application web ASP.NET Core MVC modulaire et responsive, conçue pour offrir une variété de services via l'intégration d'APIs externes. Ce projet a été développé dans le cadre d'un projet final de développement web.

## Fonctionnalités

- **Générateur de Blagues :** Intègre l'API [Official Joke API](https://official-joke-api.appspot.com/) pour fournir des blagues aléatoires ou par catégorie (général, programmation, toc-toc, papa).
- **Design Responsive :** Utilise Bootstrap 5 pour une expérience utilisateur optimale sur tous les appareils (ordinateurs de bureau, tablettes, mobiles).
- **Architecture MVC :** Suivi strict du modèle Model-View-Controller pour une séparation claire des préoccupations et une maintenance facilitée.
- **Gestion d'Erreurs Robuste :** Gestion des erreurs côté client et serveur, avec des messages informatifs pour l'utilisateur.
- **Optimisations de Performance :** Utilisation de la compression de réponse pour des temps de chargement plus rapides.

## Technologies Utilisées

- **Backend :** ASP.NET Core 8 MVC (C#)
- **Frontend :** HTML5, CSS3 (Bootstrap 5), JavaScript
- **Base de Données :** Aucune base de données n'est utilisée pour ce projet simple, les données proviennent des APIs.
- **API Externe :** Official Joke API
- **Outils :** Visual Studio Code, .NET CLI, Git, GitHub

## Configuration et Exécution Locale

Pour exécuter ce projet localement, suivez les étapes ci-dessous :

### Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installé sur votre système.
- [Visual Studio Code](https://code.visualstudio.com/) avec les extensions C# (C# Dev Kit, C#).

### Étapes

1.  **Cloner le dépôt :**

    ```bash
    git clone https://github.com/YOUR_GITHUB_USERNAME/YOUR_REPOSITORY_NAME.git
    cd YOUR_REPOSITORY_NAME/WebPortal
    ```

    _(Remplacez `YOUR_GITHUB_USERNAME` et `YOUR_REPOSITORY_NAME` par vos informations réelles)_

2.  **Restaurer les dépendances :**

    ```bash
    dotnet restore
    ```

3.  **Exécuter l'application :**

    ```bash
    dotnet run
    ```

    L'application démarrera et sera accessible via `http://localhost:5199` (ou un autre port si 5199 est déjà utilisé).

## Structure du Projet

```
WebPortal/
├── Controllers/       # Gère les requêtes utilisateur et interagit avec les modèles et les vues
├── Models/            # Représente les données de l'application (ex: Joke.cs)
├── Services/          # Contient la logique métier et les appels aux APIs externes (ex: JokeService.cs)
├── Views/             # Fichiers Razor pour l'interface utilisateur
│   ├── Home/
│   ├── Joke/
│   └── Shared/
├── wwwroot/           # Fichiers statiques (CSS, JS, images)
├── appsettings.json   # Fichiers de configuration de l'application
├── Program.cs         # Point d'entrée de l'application et configuration des services/middlewares
└── WebPortal.csproj   # Fichier projet C#
```

## Contribution

Les contributions sont les bienvenues ! Si vous souhaitez améliorer ce projet, n'hésitez pas à soumettre des Pull Requests.

## Licence

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.
