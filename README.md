## About the project

I picked the technologies and developed this project on my own in order to get my hands on Angular and C#. 
It was done as a project assignment for a C# course + project management course. 
Reservation system seemed like a pretty interesting and fitting topic for a website.

## Technologies

**Frontend**
 <ul>
   <li>Typescript - Angular</li>
 </ul>
 
**Backend**
<ul>
  <li>C# - ASP.NET</li>
  <li>SQLite + Entity Framework</li>
  <li>Fast Endpoints</li>
</ul>

## Project description/design

The reservation system offers basic functionalities for creating 'services' and 'reservations', and their subsequent management.
Any registered user can create a service, any registered user can create a booking/reservation for the service.
Service owners can set their weekly time slot schedule, upload images and publish a short description of their services.

There are currently no app-wide settings/customization and everything is built-in.

## Requirements and Setup

If the project were to be run in some sort of production, it would require at least a mediocre desktop PC to run the BE servers reliably.

**HW requirements:**
<ul>
  <li>Intel-Core i3 10th+ gen CPU</li>
  <li>64GB+ SSD disk</li>
  <li>8GB+ RAM</li>
</ul>

**SW requirements**
<ul>
  <li>MacOS/Linux/Windows</li>
  <li>.NET 8.0+ SDK</li>
</ul>


## Deployment

**Backend**
<ol>
 <li>Build C# backend using IDE or the dotnet CLI.</li>
 <li>Publish the backend to a folder using the Publish feature in Visual Studio or the dotnet publish command.</li>
</ol>

**Frontend**
<ol>
 <li>Build the Angular application by running ng build --prod in the terminal.</li>
 <li>The build process creates a dist/ folder containing the production-ready frontend.</li>
</ol>

## Deploy remotely

<ol>
 <li>Set up IIS, Apache, Nginx, or any other web server of your choice.</li>
 <li>Configure the server to point to the backend publish folder.</li>
 <li>Ensure the backend can serve the Angular application by setting up the correct routes.</li>
 <li>Upload both the backend and frontend files to your server.</li>
</ol>

## Deploy locally

<ol>
 <li>Build and run your C# backend locally using IDE or the dotnet run command.</li>
 <li>Build the Angular application for a development environment using ng build.</li>
 <li>Use the Angular CLI to serve the frontend by running ng serve.</li>
 <li>Access the application through localhost on the port specified by the Angular CLI or config files.</li>
</ol>
**Local deployment**


