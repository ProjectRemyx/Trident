# Trident
A guild application to hold users, assign them to a teams as well as track their units.

Changelog Trident .NET Application (4.7 / MVC5 / EF6)

June 22, 2019
- Implemented user authorization. Any person visiting the site can see the member list and team list but to view details they must be logged in and a "member" of the guild. Database post functionality is locked to the "administrator" role.

- Added delete confirmation messages

- Added success/error messages on successful post actions

- Added validation to forms

- Fixed routes for better UX

June 01, 2019
- Adding front-end changes to index page (Case Study)

May 26, 2019
- Added Authorization to application without "individual accounts" on project creation
   - Created a 2nd database and migrated the ApplicationDbContext (ASP.NET Users Tables) into the database to create database structure

Next Steps
   - Integrate User permissions, check if can create an administrative user. If not, create that distinction so that I can allow only administrative users to edit the teams/members/characters. Otherewise, the rest will only be able to have view access. 
