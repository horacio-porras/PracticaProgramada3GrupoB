using Microsoft.AspNetCore.Authentication.JwtBearer; // SE INCLUYE PARA LA AUTENTICACIÓN JWT
using Microsoft.IdentityModel.Tokens; // SE INCLUYE PARA LA VALIDACIÓN DE TOKENS JWT
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var key = "my_secret_key_12345___________________________________________________"; // Clave secreta para firmar el token (debe ser segura y almacenada de forma segura)
var issuer = "myapp"; // Emisor del token
var audience = "myapp_users"; // Audiencia del token


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

//MANCOMUNAR CONTRASEÑAS: Es una práctica de seguridad que implica compartir contraseñas entre usuarios o sistemas. Aunque puede ser conveniente en algunos casos, esta práctica es generalmente desaconsejada debido a los riesgos de seguridad que conlleva, como la posibilidad de que las contraseñas sean comprometidas o mal utilizadas.

/*
    OPERACIONES: my_secret
    SEGURIDA:_key_12345___________________________________________________
 
    Cada vez que se necesitar ingresar la contraseña mancomunada se tiene que contactar a las 2 áreas para obtener la contraseña completa, lo que añade una capa de seguridad adicional al proceso de autenticación. Esto es especialmente útil en entornos donde la seguridad es una preocupación importante, ya que reduce el riesgo de que una sola persona tenga acceso completo a la contraseña y, por lo tanto, a los recursos protegidos.
 
 */

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // SE HABILITA LA AUTENTICACIÓN
app.UseAuthorization();

#region Variables
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

List<Persona> personas = new List<Persona>()
    {
            new Persona("Juan", "Perez", 30),
            new Persona("Maria", "Gomez", 25),
            new Persona("Carlos", "Lopez", 40)
    };
#endregion

#region EndPointsClima
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
#endregion EndPointsClima


app.MapPost("/oAuth", (string usuario, string contraseña) =>
{
    if (usuario == "admin" && contraseña == "test")
    {
        var claims = new List<Claim> //Lista de permisos
        {
            new Claim(ClaimTypes.Name, usuario)
        };
        var token = new JwtSecurityToken
        (
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256) // Metodo de encriptacion de BITCOIN
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Results.Ok(new { token = tokenString }); //forma de devolverlo
    }

    return Results.Unauthorized();

}).WithOpenApi();

app.MapGet("/personas", () =>
{
    return Results.Ok(personas);
})
.WithName("GetPersonas")
.WithOpenApi();


app.MapPost("/personas", (Persona persona) =>
{
    personas.Add(persona);
    return Results.Ok(persona);
})
.WithName("AddPersona")
.WithOpenApi();

app.MapGet("/personas/{posicion}", (int posicion) =>
{
    if (posicion < 0 || posicion >= personas.Count)
    {
        return Results.NotFound("Persona no encontrada");
    }
    return Results.Ok(personas[posicion]);
})
.WithName("GetPersona")
.WithOpenApi();

// PUT: update a persona at the specified position (posicion is required in the route)
app.MapPut("/personas/{posicion}", (int posicion, Persona persona) =>
{
    if (posicion < 0 || posicion >= personas.Count)
    {
        return Results.NotFound("Persona no encontrada");
    }

    // Replace the persona at the given index
    personas[posicion] = persona;
    return Results.Ok(persona);
})
.WithName("UpdatePersona")
.WithOpenApi();

// DELETE: remove a persona by position (use position as required parameter to avoid ambiguity)
app.MapDelete("/personas/{posicion}", (int posicion) =>
{
    if (posicion < 0 || posicion >= personas.Count)
    {
        return Results.NotFound("Persona no encontrada");
    }

    personas.RemoveAt(posicion);
    return Results.Ok();
})
.WithName("DeletePersona")
.WithOpenApi();



app.Run();

/*REGION DE CLASES*/
#region Clases
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
public record Persona
{
    /// <summary>
    /// First name.
    /// </summary>
    [Required(ErrorMessage = "Nombre requerido")]
    public string Nombre { get; init; }

    /// <summary>
    /// Last name.
    /// </summary>
    [Required(ErrorMessage = "Apellido requerido")]
    public string Apellido { get; init; }

    /// <summary>
    /// Age (must be greater than 0).
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Edad debe ser mayor que 0")]
    public int Edad { get; init; }

    public Persona(string nombre, string apellido, int edad)
    {
        Nombre = nombre;
        Apellido = apellido;
        Edad = edad;
    }
}
#endregion






/*SEGURIDAD EN LAS APIS

BÁSICA oAuth2.0: Es un protocolo de autorización que permite a las aplicaciones obtener acceso limitado a los recursos de un usuario en un servicio HTTP. Es ampliamente utilizado para permitir a los usuarios iniciar sesión en aplicaciones utilizando sus cuentas de redes sociales o servicios de terceros.



Autenticación MTLS (Mutual TLS): Es un método de autenticación que utiliza certificados digitales para verificar la identidad tanto del cliente como del servidor. En este proceso, el cliente presenta su certificado al servidor, y el servidor también presenta su certificado al cliente. Esto proporciona una capa adicional de seguridad al garantizar que ambas partes sean quienes dicen ser.

*/