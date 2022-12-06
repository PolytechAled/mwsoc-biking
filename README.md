# Mwsoc-Projet

Requis: Lancer en **administrateur** car nous utilisons des ports différents (proxycache notamment).

Le projet contient aussi ActiveMQ que vous pouvez utiliser ou non.

## Procédure de lancement
### Automatisée:
```powershell
#Lancement ActiveMQ
.\ActiveMQ_start.bat
#Lancement des serveurs et client
.\biking_start.bat
```
Manuelle:
1. Lancer activeMQ (activemq:tcp://localhost:61616)
2. Executer le proxy cache (http://localhost:8733/BikingCache/Service)
3. Executer le serveur (http://localhost:8733/BikingServer/Service)
4. Executer le client
5. Profitez !
