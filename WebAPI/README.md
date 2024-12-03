# JWT Web API And Swagger
Simple JWT Authentication using ASP.NET Core Web API

DEC 3, 2024
Example by https://medium.com/@meghnav274/simple-jwt-authentication-using-asp-net-core-api-5d04b496d27b
Simple JWT Authentication using ASP.NET Core Web API

MD5 Hash generator online tool
  https://onlinehashtools.com/generate-random-md5-hash
 
 In this way, we have implemented the JWT Authentication
 using ASP.NET Core and have received the token upon
 successful authentication of user. Here is the
 source code link for reference:
 
 DEC 3, 2024
 Adding Authorization Option in Swagger
 To add the authorize option in Swagger, you have to add the below line of code in Program.cs
   
   1. The AddSwaggerGen Method is used to configure Swagger. 
      This part configures the swagger generation for your application and specifies the title 
      to be provided for the API documentation.
   2. This part adds a security definition to the Swagger documentation. In this case, 
      it defines how to use a Bearer token for authorization. It specifies that the token should be provided in the “Authorization” header with a format of “JWT.” It specifies a description that will be visible in the swagger documentation.
   3. This part specifies the security requirements for your API endpoints. 
      It states that the “Bearer” security scheme is required for all API endpoints. 
      This informs the user that you have to use “Bearer” keyword before providing the token in order to authenticate the user.
