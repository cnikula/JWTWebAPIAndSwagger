//1. The login user will check whether the incoming data matches the predefined credentials i.e. “Admin” and “Password” in this case.
//2. If the loginUser is null, it means the credentials are different and it will return an empty string.
//3. If loginUser contains some value, we will generate a new JwtTokenHandler and assign it to tokenHandler.
//4.  We will access the JWT key from appsettings and store it in a variable named key.
//5.  We will create a new token descriptor where we will assign the UserName as a claim.
//6.  We have to assign the Expiry time, which can be set as per your preference.
//7.  Using the appropriate Security Algorithm, new signing credentials will be created
//8.  Using the token descriptor parameter, we will create a new token.
// 9.Store this token in a variable and return it upon successfully validating the user.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authenticetion;
using WebAPI.Services.Ineterface;


namespace WebAPI.Services
{
    public class UserService :  IUserServices
    {
       

        private readonly IConfiguration _configuration;

        /// <summary>
        /// In order to access the JWT Key from the appsettings.json, we have to inject the Configuration as below:
        /// </summary>
        /// <param name="configuration"></param>
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Create a new List of User
        /// Here is where you can Retrieve the user information from the database
        /// Or it will be provided by the front end application.
        /// </summary>
        private List<UserModel> users = new List<UserModel>
        {
            new UserModel { UserName = "admin", Password = "admin" },
            new UserModel { UserName = "nick", Password = "1497" }
        };

        public string Login(UserModel user)
        {
            // 1. The login user will check whether the incoming data matches the predefined credentials i.e. “Admin” and “Password” in this case.
            var loginUser = users.SingleOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (loginUser == null)
            {
                // 2. If the loginUser is null, it means the credentials are different and it will return an empty string.
                return string.Empty;
            }

            // 3. If loginUser contains some value, we will generate a new JwtTokenHandler and assign it to tokenHandler.
            var tokenHandler = new JwtSecurityTokenHandler();

            // 4.  We will access the JWT key from appsettings and store it in a variable named key.
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // 5.  We will create a new token descriptor where we will assign the UserName as a claim.
                    new Claim(ClaimTypes.Name, user.UserName)
                }),

                // 6.  We have to assign the Expiry time, which can be set as per your preference.
                Expires = DateTime.UtcNow.AddMinutes(30),

                // 7.  Using the appropriate Security Algorithm, new signing credentials will be created
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // 8.  Using the token descriptor parameter, we will create a new token.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 9.Store this token in a variable and return it upon successfully validating the user.
            string userToken = tokenHandler.WriteToken(token);
            return userToken;


        }
    }
}
