# Promotion Engine - Technical Test Solution
## Run api
To run the solution you can use docker. Run the commands

    docker build -t promotion-api -f .\src\Api\Dockerfile .
    docker run -dp 8080:8080 promotion-api

## Justifications
### Controllers
Added some validation to the inputs. The regular expression attribute ensures the user input are 2 code characters.  

### Handler
Refactored the class to use dependency injection to follow the Dependency Inversion Principle. Also removed the reference to the DabaseConnection class to add a mayor level of abstraction to the method.

### Shared
Some classes are common to every feature for `Promotion`s, they were moved to a new folder called Shared.

### Mapster
Added the package for Mapster for simplifying model transformations. An implementation of `IRegister` was created in the Shared folder.

### Repository 
Now is injected and is in charge of connecting to the database. It also creates an instance of `DatabaseConnection` with using to ensure proper disposal of the instance
The GetAsync method is really suboptimal. Since the database exposes only the `QueryAsync` method there aren't many options. I'm passing a query different than the `GetAllAsync` to make sure than when `ToListAsync` is used there is only one (or none) item in the collection

### Tests
I considered testing the Handler was the most important part since that coverts most of the logic of every use case. I also included tests for the controllers
