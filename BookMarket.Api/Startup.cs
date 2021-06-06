using BookMarket.Application;
using BookMarket.Application.Commands.UserCommands;
using BookMarket.DataAccess;
using BookMarket.Implementation.Commands.UserCommands;
using BookMarket.Implementation.Profiles;
using BookMarket.Implementation.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMarket.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookMarket.Api", Version = "v1" });
            });
            services.AddTransient<BookMarketContext>();



            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();



            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddAutoMapper(typeof(GenreProfile).Assembly);

            services.AddTransient<CreateUserValidator>();
            services.AddTransient<UpdateUserValidator>();

            services.AddTransient<CreateGenreValidator>();
            services.AddTransient<UpdateGenreValidator>();

            services.AddTransient<CreateWriterValidator>();
            services.AddTransient<UpdateWriterValidator>();

            services.AddTransient<CreatePublisherValidator>();
            services.AddTransient<UpdatePublisherValidator>();

            services.AddTransient<CreateBookValidator>();
            services.AddTransient<UpdateBookValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookMarket.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
