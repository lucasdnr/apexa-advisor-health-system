# AdvisorHealth System

## DESCRIPTION
AdvisorHealth is a powerful Advisor management app designed to streamline your advisory process. With AdvisorHealth, you can easily search, add, edit, or delete Advisors. 

This application is built with Angular 17 for the frontend and .Net Core 8 minimal Rest API for the backend, ensuring a smooth and seamless user experience.

This solution did not adopt any database since the solution was developed using the inMemory option of .Net.

Key Features:

1. Search Advisors:
    - Easily search for Advisors using name parameter.
2. Add New Advisor:
    - Effortlessly add new Advisors to the system with just a few clicks.
3. Edit Advisor Details:
    - Update and modify Advisor details whenever necessary to keep information up-to-date.
4. Delete Advisor:
    - Remove outdated or unnecessary Advisors from the system with a simple delete function.
5. AutomatedHealth Status:
    - AdvisorHealth automatically generates a health status for each Advisor based on a predefined formula.
        - Health Formula:
            - Green: 60% chance
            - Yellow: 20% chance
            - Red: 20% chance


## PRE REQUISITES
To install this app, the following prerequisites need to be installed:
- [Git](https://git-scm.com/downloads)
- [.Net SDK](https://dotnet.microsoft.com/en-us/download)

## INSTALL
Once Git and .Net are installed, it is necessary to clone the project using the command:
```sh
git clone https://github.com/lucasdnr/apexa-advisor-health-system.git
```
Access the folder `\AdvisorHealthAPI` and run the command:
```sh
dotnet watch run --project AdvisorHealthAPI
```
This command will build the .Net solution. At the end of the process, it will open your browser. <br>
If this does not happen, open your browser and access the address [http://localhost:5000/browser/](http://localhost:5000/browser/) to access the application. Enjoy 😃

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

### Backend - C# .Net Core
- **Add soft delete**: This is a common practice that does not occur hard delete for some types of information, probably the `Advisor` entity registration is a great candidate for the hard delete to be transformed into a soft delete.
- **Improve xUnit Testing**: I have implemented some basic unit testing cases but I would like to improve the way that some ones are being done, maybe access dBContext instead call an endpoint to confirm if a record exists and delete records after we used them to not keep old records into the database.
- **Add LOCK to Caching solution**: I have implemented a solution for `LRU Cache` and see a possible issue with memory location when we have multi threads. A possible solution would be to use the C# `LOCK` command, but I need to better analyze the tradeoffs for this.
- **Add Pagination for GET endpoint**: Needs to add pagination/limit option for GET endpoint, currently it is fetching all data from the database. A good practice is to implement pagination and limit to return only requested information or in smaller blocks reducing the load on the database.
- **Add Generate Random data for seeding**: Mainly to test unit, implement a seeding class for generating random data for name, address...

### Frontend - Angular
- **Create a loading component**: Create a loading component triggered with each call to an endpoint or in processing that takes more time. This is an important form of feedback to the users on the action they performed.
- **Add Authentication**: The same for API, implement an authentication system so that only authorized users have access to API endpoints.


As each of these items is addressed, I will update this document.💪
