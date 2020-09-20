Przed uruchomieniem poszczególnych aplikacji należy wykonać polecenie
dotnet dev-certs https –-trust
ta komenda służy do tego aby można było ominąć konieczność posiadania certyfikatu SSL koniecznego przy
komunikacji Grpc.
Działanie powyższej komendy można cofnąć 
dotnet dev-certs https –-clean


Aby uruchomić poszczególne aplikacje należy przejść do folderów WarehouseClient.MVC i WarehouseGrpc
i wykonać komendę dotnet run.

Testy należy uruchomić poleceniem dotnet test

Wymaganiami do odpalenia apliakcji jest posiadanie na komputerze zainstalowanego sdk .net CORE 3.1