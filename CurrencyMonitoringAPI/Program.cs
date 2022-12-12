using CurrencyMonitoring.Data.EF;
using CurrencyMonitoring.Data.Models;
using CurrencyMonitoringAPI.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using CurrencyMonitoring.Data.Service;
using Newtonsoft.Json;

var _currencyDataProvider = new CurrencyDataProvider();
var _userDataProvider = new UserDataProvider();
var _portfolioDataProvider = new PortfolioDataRepository();
var builder = WebApplication.CreateBuilder();


builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

string HashingPassword(string pass)
{
    var salt = Encoding.Unicode.GetBytes(pass);

    var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
    password: pass!,
    salt: salt,
    prf: KeyDerivationPrf.HMACSHA256,
    iterationCount: 100000,
    numBytesRequested: 256 / 8));

    return hashed;
}

app.MapPost("/registration", (User userdata) =>
{
    if (_userDataProvider.IsLoginFree(userdata.Login)) { return Results.Json("Invalid login"); }

    _userDataProvider.AddNewUser(userdata.Login, HashingPassword(userdata.Password));

    var response = new
    {
        userdata.Login
    };

    return Results.Json(response);
});

app.MapPost("/login", (User userdata) =>
{
    // находим пользователя 
    var isUser = _userDataProvider.IsUserExist(userdata.Login, HashingPassword(userdata.Password));
    // если пользователь не найден, отправляем статусный код 401
    if (!isUser) return Results.Unauthorized();

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, userdata.Login.ToUpper()) };
    // создаем JWT-токен
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

    // формируем ответ
    var response = new
    {
        access_token = encodedJwt,
        username = userdata.Login.ToUpper()
    };

    return Results.Json(response);
});
app.Map("/data", [Authorize] () => new { message = "Hello World!" });

app.MapGet("/currencies", [Authorize] () =>
{
    return Results.Json(JsonConvert.SerializeObject(_currencyDataProvider.InformationAboutCurrencies()));
});

app.MapPost("/covert/{from}/{to}/{value}", [Authorize] (string currencyFrom, string currecyTo, decimal value) =>
{
    var result = _currencyDataProvider.ConvertToSpecificCurrency(currencyFrom, currecyTo, value);

    if(result == null)
    {
        return Results.Json("Incorrect data");
    }

    return Results.Json(_currencyDataProvider.ConvertToSpecificCurrency(currencyFrom,currecyTo,value));
});

app.MapPost("/{userid}/CreateNewPortfio", [Authorize] (string portfolioName, int userId) =>
{
    if (_portfolioDataProvider.IsPortfolioExist(portfolioName, userId))
    {
        return Results.Json("Already created");
    }

    _portfolioDataProvider.CreateNewPortfolio(portfolioName, userId);
    return Results.Ok();
    
});

a

app.Run();
