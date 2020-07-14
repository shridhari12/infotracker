This project follows the Repository UoW (Unit of Work) pattern

Web API calls the google search url (https://www.google.com/search?q=Online+Test+Search) as a regular HTTP call
and parses the results using AngleSharp Parser to parse the returned results

Web UI (MVC) calls the repository (injected into the Startup.cs) and it further calls the API controller
This pathway is implemented to mimic real world scenario where a service layer would usually make HTTP calls

The WEB API is hardcoded to the localhost url as per my development machine. 
Please change it to the localhost url on the machine where this project would be run.
You can find this url in the InfoTrackerRepository file