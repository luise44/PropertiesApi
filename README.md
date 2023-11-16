
# Property APi

Solution, to show how to implement, EFCore, Repository Pattern, Automapper, Unit Tests, etc

# Database

Here you will find the .sql script to create a clean database.
https://github.com/luise44/PropertiesApi/blob/master/Properties.Data/Database/DBScript.sql

## Build and Run solution

- Open the solution using VS. 
- Find the appsettings https://github.com/luise44/PropertiesApi/blob/5fe3b7fd453775305d6a1c0273a9c50e574a8215/Properties.Api/appsettings.json#L15
and update it locally to connect to your created database.
- Run the solution using the IIS Espress option. 
- After that you will be able to see the Swagger page and interact with the api endpoints.

## Endpoints

To be able to use all the endpoints, first execute the **/api/login/login** endpoint and then update the value in the authorize section on swagger.

[POST]  **/api/login/register**  Use it to create a new user in the database.

[POST]  **/api/login/login**  Use it to get a token to be able to use the authenticated endpoints.

[POST] **/api/owner**  Use it to create a new owner in the database.

[GET]    **/api/owner/list**  Get all the owners in the database.

[POST]    **/api/property**  Use it to create a new property in the database.

[PUT]    **/api/property**  Use it to update properties values.

[PUT]    **/api/property/price**  Use it to update property price only.

[GET]    **/api/property/list**  Use it to get all properties from database.

[GET]    **/api/property/listFiltered**  Use it to get all properties from database using filters. 

[POST]    **/api/property/image**  Use it to add an image to a property