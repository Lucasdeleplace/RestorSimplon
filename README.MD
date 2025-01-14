# RestorSimplonAPI

RestorSimplonAPI est une API développée en .NET 9 pour gérer des commandes de clients, catégories, items et commandes. Cette API utilise Entity Framework Core avec une base de données SQLite.

## Prérequis

- .NET 9 SDK
- Visual Studio 2022
- SQLite

## Installation
                      
1. Clonez le dépôt :
```
git clone https://github.com/lucasdeleplace/restorsimplon.git
cd restorsimplonapi
```

2. Ouvrez le projet dans Visual Studio 2022.

3. Restaurez les packages NuGet :
```
dotnet restore
```


4. Mettez à jour la base de données :
```
Add-Migration init
Update-Database
```

L'API sera accessible à l'adresse `https://localhost:7265` en développement.

## Endpoints

### Clients

- `GET /clients` : Récupère tous les clients.
- `GET /clients/{id}` : Récupère un client par ID.
- `POST /clients` : Crée un nouveau client.
- `PUT /clients/{id}` : Met à jour un client existant.
- `DELETE /clients/{id}` : Supprime un client.
- `GET /clients/{id}/with-order` : Récupère un client avec ses commandes.

### Catégories

- `GET /categories` : Récupère toutes les catégories.
- `GET /categories/{id}` : Récupère une catégorie par ID.
- `POST /categories` : Crée une nouvelle catégorie.
- `PUT /categories/{id}` : Met à jour une catégorie existante.
- `DELETE /categories/{id}` : Supprime une catégorie.
- `GET /categories/{id}/with-items` : Récupère une catégorie avec ses items.

### Items

- `GET /items` : Récupère tous les items.
- `GET /items/{id}` : Récupère un item par ID.
- `POST /items` : Crée un nouvel item.
- `PUT /items/{id}` : Met à jour un item existant.
- `DELETE /items/{id}` : Supprime un item.
- `GET /items/{id}/with-category` : Récupère un item avec sa catégorie.

### Commandes

- `GET /orders` : Récupère toutes les commandes.
- `GET /orders/{id}` : Récupère une commande par ID.
- `POST /orders` : Crée une nouvelle commande.
- `PUT /orders/{id}` : Met à jour une commande existante.
- `DELETE /orders/{id}` : Supprime une commande.

## Swagger

L'API utilise Swagger pour la documentation. En mode développement, vous pouvez accéder à la documentation Swagger à l'adresse `https://localhost:7265/index.html`.

## Contribuer

Les contributions sont les bienvenues ! Veuillez soumettre une pull request ou ouvrir une issue pour discuter des changements que vous souhaitez apporter.
