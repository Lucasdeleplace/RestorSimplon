# RestorSimplonAPI

RestorSimplonAPI est une API d�velopp�e en .NET 9 pour g�rer des commandes de clients, cat�gories, items et commandes. Cette API utilise Entity Framework Core avec une base de donn�es SQLite.

## Pr�requis

- .NET 9 SDK
- Visual Studio 2022
- SQLite

## Installation
                      
1. Clonez le d�p�t :
```
git clone https://github.com/lucasdeleplace/restorsimplon.git
cd restorsimplon
```

2. Ouvrez le projet dans Visual Studio 2022.

3. Restaurez les packages NuGet :
```
dotnet restore
```


4. Mettez � jour la base de donn�es :
```
Add-Migration init
Update-Database
```

L'API sera accessible � l'adresse `https://localhost:7265` en d�veloppement.

## Endpoints

### Clients

- `GET /clients` : R�cup�re tous les clients.
- `GET /clients/{id}` : R�cup�re un client par ID.
- `POST /clients` : Cr�e un nouveau client.
- `PUT /clients/{id}` : Met � jour un client existant.
- `DELETE /clients/{id}` : Supprime un client.
- `GET /clients/{id}/with-order` : R�cup�re un client avec ses commandes.

### Cat�gories

- `GET /categories` : R�cup�re toutes les cat�gories.
- `GET /categories/{id}` : R�cup�re une cat�gorie par ID.
- `POST /categories` : Cr�e une nouvelle cat�gorie.
- `PUT /categories/{id}` : Met � jour une cat�gorie existante.
- `DELETE /categories/{id}` : Supprime une cat�gorie.
- `GET /categories/{id}/with-items` : R�cup�re une cat�gorie avec ses items.

### Items

- `GET /items` : R�cup�re tous les items.
- `GET /items/{id}` : R�cup�re un item par ID.
- `POST /items` : Cr�e un nouvel item.
- `PUT /items/{id}` : Met � jour un item existant.
- `DELETE /items/{id}` : Supprime un item.
- `GET /items/{id}/with-category` : R�cup�re un item avec sa cat�gorie.

### Commandes

- `GET /orders` : R�cup�re toutes les commandes.
- `GET /orders/{id}` : R�cup�re une commande par ID.
- `POST /orders` : Cr�e une nouvelle commande.
- `PUT /orders/{id}` : Met � jour une commande existante.
- `DELETE /orders/{id}` : Supprime une commande.

## Swagger

L'API utilise Swagger pour la documentation. En mode d�veloppement, vous pouvez acc�der � la documentation Swagger � l'adresse `https://localhost:7265/index.html`.

## Contribuer

Les contributions sont les bienvenues ! Veuillez soumettre une pull request ou ouvrir une issue pour discuter des changements que vous souhaitez apporter.
