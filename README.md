# Trident
A guild application to hold users, assign them to a teams as well as track their units.

Next Steps
- General styling
- User testing
- Expansion of database schema to include in-depth information
   - Under team add ability to log battles
   - Each team can have a battles and battles contain scores, characters used, members in the battle, etc

Changelog Trident .NET Application (4.7 / MVC5 / EF6)

July 1, 2019
- Added async/await architecture controllers
- Added output caching placeholder

June 30, 2019
- Added additional error handling in the form of try/catch

- Changed MemberLevel to be a string instead of an int
   - Fixed validation bug with MemberLevel 

June 23, 2019
- Added validation to forms including error handling using ModelState

- Fixed error in deleting a team if members are still in it
   - Puts all members in deleting team to default "Unassigned" team
   - Removed ability to delete "Unassigned" team
   
- Added banners to pages

June 22, 2019
- Implemented user authorization. Any person visiting the site can see the member list and team list but to view details they must be logged in and a "member" of the guild. Database post functionality is locked to the "administrator" role.

- Added delete confirmation messages

- Added success/error messages on successful post actions

- Fixed routes for better UX

June 01, 2019
- Adding front-end changes to index page (Case Study)

May 26, 2019
- Added Authorization to application without "individual accounts" on project creation
   - Created a 2nd database and migrated the ApplicationDbContext (ASP.NET Users Tables) into the database to create database structure

