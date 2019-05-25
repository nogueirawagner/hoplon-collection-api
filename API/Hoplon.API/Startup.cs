using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoplon.API.Configurations;
using Hoplon.Domain.Implementation;
using Hoplon.Domain.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Hoplon.API
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
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      //Configurações do Swagger
      services.AddSwaggerGen(s =>
      {
        s.SwaggerDoc("v1", new Info
        {
          Version = "v1",
          Title = "Hoplon",
          Description = "API do projeto Hoplon",
          TermsOfService = "Nenhum",
          Contact = new Contact { Name = "Hoplon API", Email = "arquitetowagnernogueira@gmail.com", Url = "http://www.hoplon.com/site/" },
          License = new License { Name = "CC BY-NC-ND", Url = "https://creativecommons.org/licenses/by-nc-nd/4.0/legalcode" }
        });

        s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
      });

      services.ConfigureSwaggerGen(opt =>
      {
        opt.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
      });

      services.AddSingleton<IHoplonCollection, HoplonCollection>();
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

      // Usar Swagger
      app.UseSwagger();
      app.UseSwaggerUI(s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "Hoplon API v1.0"); });
    }
  }
}
