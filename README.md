# Projet mwsoc: Biking

## Réalisations
Les éléments suivants ont été réalisés pour ce projet :
- Un client lourd JAVA permettant de réaliser une demande d'itinéraire entre deux adresses pour ensuite afficher les différentes étapes du trajet (récupérées depuis ActiveMQ) et afficher une carte montrant les points de départ et d'arrivée, ainsi que les stations de JCDecaux si disponibles.
- Un serveur SOAP en C# permettant de calculer un itinéraire entre 2 points. Il récupère les informations de JCDecaux depuis un serveur cache et les informations de navigation depuis l'api REST **OpenRouteService**.
- Un serveur Proxy-Cache en C# réalisant les requêtes REST vers l'api de **JCDecaux** et stockant les informations dans un cache avec une durée de validité de **2 minutes**.

## Hypothèse de travail
Il a été considéré un partenariat avec JCDecaux permettant aux utilisateurs de rendre un vélo dans un contract différent de celui d'emprunt de ce dernier. L'objectif étant de favoriser les déplacements *écologiques* entre différents lieux plus ou moins éloignés. 

## Membres du projet
- TRAMIER Yaël
- GRAFFAN Jérémy

Groupe: SI4 **FISA**

## Procédure de lancement

> **Warning**
> Il est obligatoire de lancer en **administrateur** les différents serveurs car nous utilisons des ports et urls différents entre le serveur et le serveur de cache, or cela est permis par **Windaube** seulement en administrateur.

### Automatique
Lancer un Terminal dans le dossier du projet.
```powershell
#Lancement ActiveMQ
.\ActiveMQ_start.bat
#Lancement des serveurs et client
.\biking_start.bat
```
### Manuelle
1. Lancer activeMQ (activemq:tcp://localhost:61616).
2. Executer le proxy cache.
3. Executer le serveur.
4. Executer le client.
5. Profiter !

### Accès
Il sera possible d'accèder au serveur cache via l'url http://localhost:8733/BikingCache/Service et au serveur de base via http://localhost:8733/BikingServer/Service.

## Sources
Les sources du projets sont séparées en deux parties:
- Le client lourd Java situé dans le dossier `BikingClient` et utilise **maven** pour la compilation et les dépendances.
- Le serveur C# dans le dossier `BikingServer`.
  - Pour ouvrir les sources, il est recommandé d'ouvrir le fichier `BikingServer.sln` avec VisualStudio.
  - A l'intérieur se trouve trois sous-projets :
    - BikingServer : Serveur SOAP permettant de calculer un chemin entre deux adresses.
    - BikingServerCache : Serveur Proxy-Cache auquel se connectera le `BikingServer` afin de recupérer les informations provenant de l'API de JCDecaux.
    - RestLib : Petite bibliothèque réalisée afin de simplifier les requêtes vers une API REST et l'utilisation de clé d'API. Elle est utilisée à la fois dans `BikingServer` (pour les requêtes à l'API d'OpenStreetRoute) et `BikingServerCache` (pour les requêtes à l'API de JCDecaux).

## Fonctionnement
Le champ Start comprend l'adresse de départ.
La champ end comprend l'adresse d'arrivée.

Si le logo vélo apparaît, le trajet est réalisable avec celui-ci en particulier.
![client_step_frame](/doc/image-steps.png)
![client_step_frame](/doc/image-client.png)
