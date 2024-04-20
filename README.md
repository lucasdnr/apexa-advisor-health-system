# Advisor Health System

## DETAILS
This application is coded in Angular and utilizes a Rest API in C# .Net. 

The solution adopted for the REST API uses the MINIMAL REST API in .Net.

I did not adopt any database since the solution was developed using the inMemory option of .Net

- Angular Version: 17
- .Net Version: 8 with all packages in the latest version

## PRE REQUISITE
To install this app, the following prerequisites need to be installed:
- [GIT](https://git-scm.com/downloads)
- [.NET SDK](https://dotnet.microsoft.com/en-us/download)

## INSTALL
Once GIT and .NET are installed, it is necessary to clone the project using the command:
```sh
git clone https://github.com/lucasdnr/apexa-advisor-health-system.git
```
Access the folder \AdvisorHealthAPI and run the command:
```sh
dotnet watch run --project AdvisorHealthAPI
```
This command will build the .Net solution. At the end of the process, it will open a browser. If this does not happen, open your browser and access the address [http://localhost:5000/browser/](http://localhost:5000/browser/) to access the application. Enjoy :)

## FOLDERS STRUCTURE
This repository has 2 main folders, `AdvisorHealthAPI` and `FrontEndAngular`.

**FrontEndAngular**: This folder is where the Angular project is located. You can install the Angular project by running:
```sh
npm i
```
To install all packages and run:
```sh
ng serve
```
To start the Angular server.

**AdvisorHealthAPI**: This folder is where the .Net project is located. Inside this project, we have the folder `wwwroot` where the built static files from Angular are.

When you are working on the Angular Project, you can build your project and send the files to `wwwroot` running the command:
```sh
ng build --base-href=/browser/
```

## TODO
Some points I believe can be improved and will be over time.

### API - C# .Net Core
- **Add soft delete**: This is a common practice that does not occur hard delete for some types of information, probably the `Advisor` entity registration is a great candidate for the hard delete to be transformed into a soft delete.
- **Improve xUnit Testing**: I have implemented some basic unit testing cases but I would like to improve the way that some ones are being done, maybe access dBContext instead call an endpoint to confirm if a record exists and delete records after we used them to not keep old records into the database.
- **Add LOCK to Caching solution**: I have implemented a solution for `LRU Cache` and see a possible issue with memory location when we have multi threads. A possible solution would be to use the C# `LOCK` command, but I need to better analyze the tradeoffs for this.
- **Add Pagination for GET endpoint**: Needs to add pagination/limit option for GET endpoint, currently it is fetching all data from the database. A good practice is to implement pagination and limit to return only requested information or in smaller blocks reducing the load on the database.
- **Add Generate Random data for seeding**: Mainly to test unit, implement a seeding class for generating random data for name, address...

### APP - Angular
- **Create a loading component**: Create a loading component triggered with each call to an endpoint or in processing that takes more time. This is an important form of feedback to the users on the action they performed.
- **Add Authentication**: The same for API, implement an authentication system so that only authorized users have access to API endpoints.


As each of these items is addressed, I will update this document.
