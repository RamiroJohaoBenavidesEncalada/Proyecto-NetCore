using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Proyecto.Ecommerce.WebAPI.Controllers;
using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    /*
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    */
    // To Enable authorization using Swagger (JWT)  

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()  

    {  

        Name = "Authorization",  

        Type = SecuritySchemeType.ApiKey,  

        Scheme = "Bearer",  

        BearerFormat = "JWT",  

        In = ParameterLocation.Header,  

        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",  

    });  

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    //Id = "basic"
                                    Id="Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
}
);



builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

//Configurar el esquema de Autentificacion

//1. Configurar el esquema de Autentificacion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

//Configurations
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JWT"));

/*
//1. Configuracion de politicas..

builder.Services.AddAuthorization(options =>

{

   options.AddPolicy("EsEcuatorianoPolitica",

     policy => policy.RequireClaim("Ecuatoriano","True")

    );

});


//2. Configuracion de politicas Departamento

builder.Services.AddAuthorization(options =>
{
   options.AddPolicy("EsDepartamentoPolitica",

    policy => policy.RequireClaim("Departamento","Administrativo","Operaciones")
    
    );
});
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
