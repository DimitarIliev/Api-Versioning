using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Examples;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(x =>
{
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    x.ReportApiVersions = true; //returns api-supported-versions header in the response
    x.ApiVersionReader = new QueryStringApiVersionReader("ver", "api-version");
});

builder.Services.AddVersionedApiExplorer(x =>
{
    x.GroupNameFormat = "'v'VVV";
    x.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddSwaggerGen(x =>
{
    x.OperationFilter<SwaggerDefaultValues>();
});



var app = builder.Build();

// Configure the HTTP request pipeline.

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        foreach (var item in provider.ApiVersionDescriptions)
        {
            x.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
