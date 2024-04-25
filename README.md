# 🏦 Bankable 

## Lancer l'exécutable Bankable

- Mac : Il y a un .zip avec l'exécutable de base.
- Linux : `.deb` pour installer sur une distribution Debian/Ubuntu based.
- Windows : `.exe` pour Windows.

## Lancer le projet de développement

### Les prérequis

Il vous faudra la **version 8 de .NET**. Pour installer voici le lien : [Download .NET 0.8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Rider possède un support built-in pour Avalonia, il ne devrait rien avoir à installer. Sinon il vous faudra regarder la [documentation](https://docs.avaloniaui.net/docs/get-started/set-up-an-editor) pour installer l'extension correspondante.

> :bulb: Si jamais il y a un problème avec Avalonia, voici la documentation pour potentiellement setup Avalonia, etc... [lien](https://docs.avaloniaui.net/docs/get-started/install).

### Avec Rider

- Récupérer le code source (`.zip` via le repository ou en clonant si c'est possible)
- L'ouvrir avec Rider
- Lancer le projet en utilisant le bouton en haut à droite

> :bulb: Normalement les migrations devraient s'effectuer automatiquement car elles sont gérées dans le code (car nous utilisons du SQLite et donc la base de donnée est stocké directement dans les `ApplicationData`) 

### Avec Visual Studio Code (lancer en ligne de commande)

- Récupérer le code source (`.zip` via le repository ou en clonant si c'est possible)
- L'ouvrir dans Visual Studio Code
- Faire dans le terminal la commande :
    ```bash
    dotnet run
    ```