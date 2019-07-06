using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using OpenSpace.Sevices.Implementation;
using OpenSpace.Sevices.Interface;
using Swashbuckle.AspNetCore.Swagger;

namespace OpenSpace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Passo 2
            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Livraria do Brill",
                        Version = "v1",
                        Description = "Exemplo de API utilizando swagger",
                        Contact = new Contact
                        {
                            Name = "Lucas Leandro",
                            Url = "https://github.com/lucasviieira"
                        }
                    });

                // Passo 3
                // Adicionando o XML
                string pathApplication = PlatformServices.Default.Application.ApplicationBasePath;
                string nameApplication = PlatformServices.Default.Application.ApplicationName;
                string pathXmlDoc = Path.Combine(pathApplication, $"{nameApplication}.xml");

                c.IncludeXmlComments(pathXmlDoc);
            });

            services.AddSingleton<IBookService, BookService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            //Passo 2
            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Livraria do Brill");

                c.RoutePrefix = string.Empty;
            });

        }
    }
}
