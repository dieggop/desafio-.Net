using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_.Net.Contexts;
using desafio_.Net.Repository;
using desafio_.Net.Repository.Interface;
using desafio_.Net.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace desafio_.Net
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
            var connection = Configuration["ConnectionsStrings:DefaultConnection"];

            services.AddDbContext<DesafioDbContext>(options => 
            options.UseSqlite(connection)
            );
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IPhoneRepository, PhoneRepository>();
            services.AddTransient<IUsuariosServices, UsuariosServices>();
            
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
