# Order-Management-Station
This is a assignment project for job application.


## Client
The client is located in [metronik-angular-datatable](https://github.com/kadattack/Order-Management-Station/tree/main/Metronik/metronik-angular-datatable)
To install the libraries use 
`npm install`
The node version used is v16.13.2
Use `ng serve` to run it.
The client sends requests to the server that is presumed to be located at https://localhost:7103/
If the server is not running on port 7103 then you can change it on the client in either
[environment.prod.ts](https://github.com/kadattack/Order-Management-Station/blob/main/Metronik/metronik-angular-datatable/src/environments/environment.prod.ts) 
or [environment.ts](https://github.com/kadattack/Order-Management-Station/blob/main/Metronik/metronik-angular-datatable/src/environments/environment.ts) 

## Server
The server presumes the client is running at https://localhost:4200/. This can be changed in [Program.cs](https://github.com/kadattack/Order-Management-Station/blob/main/Metronik/Program.cs).
It uses SQLite located at [temp.db](https://github.com/kadattack/Order-Management-Station/blob/main/Metronik/temp.db)
