# Movie API

Simple API for searching movies and log the searches and is written in c# and using .Net Core 3.1.

# Description
  
  The MovieApi let you filter for movie titles from an external endpoint http://www.omdbapi.com/ that uses ApiKey authentication.
  The api key is already provided in appsettings, so the user can search without an ApiKey.
  
  The MovieAPI is logging every request made to the external api, so we save some statistics in a **MongoDB** database.
  
  The movie API can return some statistics from the DB with filter options and lastly can delete logs from DB.
  
  It has a **Swagger UI**, where all this requests can be tested, but all this endpoints are secured with an ApiKey.
  After entering the api key the endpoint is reachable and data is returned in a json format.
  The api key can be addded in the query like in the omdbAPI. 
  
  This project was raw estimated for 7-8 hours.
  The project was finished in 7 hours with dev testing.
  
  Not included: understanding the requirments, installing mongoDB locally and writing the readme file.
  
  
   **Note: the api key is located in the appsettings!**
