using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Interfaces;

namespace Syki.Back.Configs;

public static class DocsConfigs
{
    public static void AddDocsConfigs(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Syki API Docs",
                Description = ReadResource("api-intro.md"),
                Extensions = new Dictionary<string, IOpenApiExtension>
                {
                    { "x-logo", new OpenApiObject
                    {
                        { "url", new OpenApiString("/syki-logo.png") },
                    }}
                },
            });

            options.EnableAnnotations();

            options.TagActionsBy(api =>
            {
                var group = api.RelativePath.Split("/")[0];
                if (group == "academic") return ["🏫 Academic"];
                if (group == "student") return ["👩🏻‍🎓 Student"];
                if (group == "teacher") return ["👨🏻‍🏫 Teacher"];
                if (group == "adm") return ["🛡️ Adm"];
                return ["🧱 Cross"];
            });
            options.DocInclusionPredicate((name, api) => true);

            options.SchemaFilter<EnumSchemaFilter>();
            options.OperationFilter<AuthOperationsFilter>();
            options.DocumentFilter<HttpMethodSorterDocumentFilter>();

            options.ExampleFilters();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer",
            });

            options.DescribeAllParametersInCamelCase();

            var xmlPath = Path.Combine(AppContext.BaseDirectory, "Back.xml");
            options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

            var xmlPath2 = Path.Combine(AppContext.BaseDirectory, "Shared.xml");
            options.IncludeXmlComments(xmlPath2, includeControllerXmlComments: true);
        });

        builder.Services.AddSwaggerExamplesFromAssemblyOf(typeof(Program));
        builder.Services.AddOpenApi();
    }

    public static void UseDocs(this IApplicationBuilder app)
    {
        app.UseStaticFiles();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "Syki API Docs";
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Syki 1.0");
            options.DefaultModelsExpandDepth(-1);
        });
    }

    private static string ReadResource(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));

        using Stream stream = assembly.GetManifestResourceStream(resourcePath)!;
        using StreamReader reader = new(stream);

        return reader.ReadToEnd();
    }
}
