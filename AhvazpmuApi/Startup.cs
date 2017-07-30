﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AhvazpmuApi.Entities;
using AhvazpmuApi.Repositories;
using AhvazpmuApi.Dtos;
using Microsoft.AspNetCore.ResponseCompression;

namespace AhvazpmuApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<AhvazpmuDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConncetion")));
            services.AddScoped<IRepository<News>, NewsRepository>();
            services.AddScoped<IRepository<NewsImage>, NewsImageRepository>();
            services.AddMvc();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            AutoMapper.Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<News, NewsDto>().ReverseMap();
                mapper.CreateMap<NewsImage, NewsImageDto>().ReverseMap();
            });
            app.UseResponseCompression();
            app.UseMvc();
        }
    }
}