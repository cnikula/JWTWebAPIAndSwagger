/*****************************************************
 * DEC 3, 2024
 * Simple JWT Authentication using ASP.NET Core Web API
 *
 *  MD5 Hash generator online tool
 * https://onlinehashtools.com/generate-random-md5-hash
 *
 * In this way, we have implemented the JWT Authentication
 * using ASP.NET Core and have received the token upon
 * successful authentication of user. Here is the
 * source code link for reference:
 *
 * DEC 3, 2024
 * Adding Authorization Option in Swagger
 *To add the authorize option in Swagger, you have to add the below line of code in Program.cs
   
   1. The AddSwaggerGen Method is used to configure Swagger. 
      This part configures the swagger generation for your application and specifies the title 
      to be provided for the API documentation.
   2. This part adds a security definition to the Swagger documentation. In this case, 
      it defines how to use a Bearer token for authorization. It specifies that the token should be provided in the “Authorization” header with a format of “JWT.” It specifies a description that will be visible in the swagger documentation.
   3. This part specifies the security requirements for your API endpoints. 
      It states that the “Bearer” security scheme is required for all API endpoints. 
      This informs the user that you have to use “Bearer” keyword before providing the token in order to authenticate the user.
*****************************************************/

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI.Services;
using WebAPI.Services.Ineterface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// See Item 1-3 above for explanation
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

/*****************************************************
Register the services IUserServices
*****************************************************/
builder.Services.AddScoped<IUserServices, UserService>();

/*****************************************************
 * specifies the settings for how to manipulate the JWT T
 * oken. It accesses the key that is stored in
 * appsettings.json as:
*****************************************************/
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = true;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };

 var test =   o.TokenValidationParameters.IssuerSigningKey;
});
// Claude  [12/3/24]: End.

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// https://medium.com/@meghnav274/simple-jwt-authentication-using-asp-net-core-api-5d04b496d27b
