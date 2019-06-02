# Trident
A guild application to hold users, assign them to a teams as well as track their units.

Changelog Trident .NET Application
June 01, 2019
- Adding front-end changes to index page (Case Study)

May 26, 2019
- Added Authorization to application without "individual accounts" on project creation
   - Created a 2nd database and migrated the ApplicationDbContext (ASP.NET Users Tables) into the database to create database structure

Next Steps
   - Integrate User permissions, check if can create an administrative user. If not, create that distinction so that I can allow only administrative users to edit the teams/members/characters. Otherewise, the rest will only be able to have view access. 