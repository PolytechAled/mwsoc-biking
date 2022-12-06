# Projet mwsoc: Biking

## Réalisations
Les éléments suivant ont été réalisés pour ce projet :
- Un client lourd JAVA permettant de réaliser une demande d'itinéraire entre deux addresses pour ensuite afficher les différentes étapes du trajet (récupérées depuis ActiveMQ) et afficher une carte montrant les points de départ et d'arrivé, ainsi que les stations de JCDecaux si disponibles
- Un serveur SOAP en C# permettant de calculé un itinéraire entre 2 points. Il récupère les informations de JCDecaux depuis un serveur cache et les informations de navigation depuis l'api REST **OpenRouteService**
- Un serveur Proxy-Cache en C# realisant les requêtes REST vers l'api de **JcDecaux** et stockant les informations dans un cache avec une durée de validité de **2 mins**

## Hypothèse de travail
Il a été considéré un partenariat avec JcDecaux permettant aux utilisateurs de rendre un vélo dans un contract différent de celui d'emprunt de ce dernier. L'objectif étant de favoriser les déplacements *écologiques* entre différents lieux plus ou moins éloignés. 

## Membres du projets
- TRAMIER Yaël
- GRAFFAN Jérémy

Groupe: SI4 FISA

## Procédure de lancement

### Attention

Il est obligatoire de lancer en **administrateur** les différents serveurs car nous utilisons des ports et urls différents entre le serveur et le serveur de cache, or cela n'est permit par **Windows** que en administrateur.

### Automatique
```powershell
#Lancement ActiveMQ
.\ActiveMQ_start.bat
#Lancement des serveurs et client
.\biking_start.bat
```
### Manuelle
1. Lancer activeMQ (activemq:tcp://localhost:61616)
2. Executer le proxy cache 
3. Executer le serveur 
4. Executer le client
5. Profitez !

### Accès
Il sera possible d'accèder au serveur cache via l'url http://localhost:8733/BikingCache/Service et au serveur de base via http://localhost:8733/BikingServer/Service

## Sources
Les sources du projets sont séparées en deux parties:
- Le client lourd Java situé dans le dossier `BikingClient` et utilise **maven** pour la compilation et les dependances
- Le serveur C# dans le dossier `BikingServer`
  - Pour ouvrir les sources, il est recommendé d'ouvrir le fichier `BikingServer.sln` avec VisualStudio
  - A l'interieur se trouve trois sous-projets :
    - BikingServer : Serveur SOAP permettant de calculer un chemin entre deux addresses
    - BikingServerCache : Serveur Proxy-Cache auquel se connectera le `BikingServer` afin de recupéré les informations provenant de l'api de JcDecaux
    - RestLib : Petite bibliothèque réalisée afin de simplifier les requêtes vers une api rest et l'utilisation de clé d'api. Elle est utilisée a la fois dans `BikingServer` (pour les requêtes a l'api de OpenStreetRoute) et `BikingServerCache` (pour les requêtes a l'api de JcDecaux)

## Fonctionnement
![client_step_frame](/doc/image-steps.png)
![client_step_frame](/doc/image-client.png)
