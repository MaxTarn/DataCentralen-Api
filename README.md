# DataCentralen-Api  

## Overview  
DataCentralen-Api is a .NET 8 Web API project designed to manage articles. It provides endpoints for CRUD operations, file uploads, and grouped data retrieval.  

## Features  
- CRUD operations for articles.  
- File upload support for `.html` and `.md` files.  
- Grouped data retrieval for dropdowns.  
- Authentication and authorization for protected endpoints.  

## Technologies Used  
- **.NET 8**  
- **Entity Framework Core**  
- **JWT Authentication**  
- **BCrypt for password hashing**  
- **Swagger for API documentation**  

## Prerequisites  
- .NET 8 SDK  
- SQL Server  

## Setup Instructions  

1. **Clone the Repository**
2. **Configure Database**  
   Update the connection string in `appsettings.json` to point to your SQL Server instance.  

3. **Run Migrations**
4. **Run the Application**
5. **Access Swagger UI**  
   Navigate to `http://localhost:<port>/swagger` to explore the API.  

## Project Structure  

- **Controllers**: Contains API controllers like `ArticleController`.  
- **Models**: Includes database models and DTOs.  
- **Repo**: Repository layer for database operations.  
- **DataCentralen-Db**: Separate project for database context and models.  

## Endpoints  

### Public Endpoints  
- `GET /api/Article`: Retrieve all articles.  
- `GET /api/Article/{id}`: Retrieve an article by ID.  
- `GET /api/Article/TitleDescription`: Retrieve articles with title and description.  
- `GET /api/Article/CardDisplay`: Retrieve articles for card display.  

### Protected Endpoints  
- `POST /api/Article`: Add a new article.  
- `PUT /api/Article/with-file/{id}`: Upload a file to an article.  
- `PUT /api/Article/with-file-as-string`: Upload file content as a string.  
- `PUT /api/Article/remove-content/{id}`: Remove content from an article.  
- `PUT /api/Article/{id}`: Update an article.  
- `DELETE /api/Article/{id}`: Delete an article.  
