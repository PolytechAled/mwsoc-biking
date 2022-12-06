# Mwsoc-Projet

Nous avons utilisé des ports différents notamment pour le proxy cache, donc tout doit être lancé en administrateur.

Le projet contient aussi ActiveMQ que vous pouvez utiliser ou non.

## Procédure de lancement
### Automatisée:
```powershell
#Lancement des serveurs et client sans ActiveMQ
.\biking_start_without_ActiveMQ.bat
#Lancement des serveurs et client avec ActiveMQ
.\biking_start_with_ActiveMQ.bat
```
Manuelle:
1. Lancer activeMQ (activemq:tcp://localhost:61616)
2. Executer le proxy cache (http://localhost:8733/BikingCache/Service)
3. Executer le serveur (http://localhost:8733/BikingServer/Service/wsdl)
4. Executer le client
5. Profitez !
