using BookMarket.Application.Commands.GenreCommands;
using BookMarket.Application.Commands.PublisherCommands;
using BookMarket.Application.Commands.UserCommands;
using BookMarket.Application.Commands.WriterCommands;
using BookMarket.Application.Interfaces;
using BookMarket.Application.Queries.BookQueries;
using BookMarket.Application.Queries.GenreQueries;
using BookMarket.Application.Queries.PublisherQueries;
using BookMarket.Application.Queries.UserQueries;
using BookMarket.Application.Queries.WriterQueries;
using BookMarket.DataAccess;
using BookMarket.Implementation.Commands.GenreCommands;
using BookMarket.Implementation.Commands.PublisherCommands;
using BookMarket.Implementation.Commands.UserCommands;
using BookMarket.Implementation.Commands.WriterCommands;
using BookMarket.Implementation.Queries.BookQueries;
using BookMarket.Implementation.Queries.GenreQueries;
using BookMarket.Implementation.Queries.PublisherCommands;
using BookMarket.Implementation.Queries.UserQueries;
using BookMarket.Implementation.Queries.WriterQueries;
using BookMarket.Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarket.Api.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services) 
        {
            // Commands and Queries
            // Users
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();

            // Genres
            services.AddTransient<IGetGenresQuery, EfGetGenresQuery>();
            services.AddTransient<IGetGenreQuery, EfGetGenreQuery>();
            services.AddTransient<ICreateGenreCommand, EfCreateGenreCommand>();
            services.AddTransient<IUpdateGenreCommand, EfUpdateGenreCommand>();
            services.AddTransient<IDeleteGenreCommand, EfDeleteGenreCommand>();

            // Writers
            services.AddTransient<IGetWritersQuery, EfGetWritersQuery>();
            services.AddTransient<IGetWriterQuery, EfGetWriterQuery>();
            services.AddTransient<ICreateWriterCommand, EfCreateWriterCommand>();
            services.AddTransient<IUpdateWriterCommand, EfUpdateWriterCommand>();
            services.AddTransient<IDeleteWriterCommand, EfDeleteWriterCommand>();

            // Publishers
            services.AddTransient<IGetPublishersQuery, EfGetPublishersQuery>();
            services.AddTransient<IGetPublisherQuery, EfGetPublisherQuery>();
            services.AddTransient<ICreatePublisherCommand, EfCreatePublisherCommand>();
            services.AddTransient<IUpdatePublisherCommand, EfUpdatePublisherCommand>();
            services.AddTransient<IDeletePublisherCommand, EfDeletePublisherCommand>();

            // Books
            services.AddTransient<IGetBooksQuery, EfGetBooksQuery>();
            services.AddTransient<IGetBookQuery, EfGetBookQuery>();


            // Validators
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

        public static void AddApplicationActor(this IServiceCollection services)
        {
            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var user = accessor.HttpContext.User;
                if (user.FindFirst("ActorData") == null) return new UnauthorizedActor();
                var actorString = user.FindFirst("ActorData").Value;
                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);
                return actor;
            });
        }

        public static void AddJwt(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<BookMarketContext>();
                return new JwtManager(context, appSettings.JwtIssuer, appSettings.JwtSecretKey);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = appSettings.JwtIssuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookMarket", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
