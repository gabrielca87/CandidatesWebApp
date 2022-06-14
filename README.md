# Candidates Web App

A simple contact (Candidate) tracking system with a search feature.

### Features

1. A way to enter Candidate information into the system. Candidate information is validated. Candidate information includes: First Name, Last Name, Email Address, Phone Number and Residential Zip Code.
2. Search Candidates. This provides the ability to search the Candidates entered in Feature 1 and present the results to the end user in a grid. Search criteria should support: First Name, Last Name, Email Address, Phone Number and Residential Zip Code.
3. Pre-population of Candidates.
4. Advanced search. Search multiple candidate areas that includes: First Name, Last Name, Email Address, Phone Number or Residential Zip Code.
5. Store Candidates in SQL Server.
6. No login.
7. Anyone can access the site and get the Candidate entry / search functions.

### Tools

* Visual Studio 2019
* SQL Server Management Studio 18.4
* Google Chrome 102.0.5005.63
* Microsoft Edge 102.0.1245.33

### Technology and Libraries

##### Back End

* .NET Framework 4.5
* ASP.NET Web Application – Web API / MVC
* Microsoft SQL Server 2017 Express Edition
* EntityFramework 6.4.4
* Ninject 3.3.6
* Swashbuickle (Swagger) 5.6.0

##### Front End

* JavaScript
* jQuery 3.1.5
* JQuery Input Mask Phone Number 1.0.14
* DataTables 1.12.1
* Toastr 2.1.4
* HTML5/CSS3
* Bootstrap 4.6.1
* Font Awesome 4.7.0

##### Unit Test

* MSTest 2.1.2
* Moq 4.17.2
* EntityFramework 6.4.4

### Requirements

##### Project

Build the project for the first time and the dependencies will be installed automatically.

##### Database

Setup the *connectionString* named *CandidatesWebAppDbContext* in the *Web.config* inside the *CandidatesWebApp* project and run the solution. The database, including the only table and its default values, will be created automatically with the name entered there.

##### Explanation
The main project is an ASP.NET Web Application that includes Web API and MVC folders and core references.

I decided to use this architecture in order to have simplicity for this code exercise by having the Web API and the UI in the same project. Personally, in a real-life scenario, I prefer to separate the Web API in one project and the UI in a different one, in order to give them independency and flexibility, especially for deployments.

I used the Repository Pattern without the usage of any service for simplicity as well, because we are handling only one entity. In real-life scenario, it could be better to handle services that take care of the interaction between business objects.

About the UI, I decided to use jQuery for the data manipulation due to the simple requirements. I didn’t even use MVC properly because only one page contains all. In real-life scenario, I would have used a modern framework like Angular or Blazor.

For the Unit Testing project, I considered important to test only the repository, focusing on the methods that return data.

- [x] Done
- [ ] Pending