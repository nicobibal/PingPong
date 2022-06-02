using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PingPong.Models;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.Linq;

namespace PingPong
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
            //string mySqlConnectionStr = Configuration.GetConnectionString("server='mysql-pingpongbdd.alwaysdata.net';user='271403';password='-PingPong-';database='pingpongbdd_database'");
            //services.AddDbContextPool<MatchContext>(options => options.UseSqlServer(mySqlConnectionStr,(ServerVersion.AutoDetect(mySqlConnectionStr)));

            //services.AddControllers();
            //services.AddEntityFrameworkMySql();
            services.AddDbContext<PlayerContext>(options =>
                   options.UseMySql(Configuration.GetConnectionString("Default")));

            services.AddDbContext<MatchContext>(options =>
                   options.UseMySql(Configuration.GetConnectionString("Default")));

            //services.AddDbContext<MatchContext>(opt =>
            //    opt.UseInMemoryDatabase("Match"));
            //services.AddDbContext<PlayerContext>(opt =>
            //    opt.UseInMemoryDatabase("Player"));
            //services.AddDbContext<TournamentContext>(opt =>
            //    opt.UseInMemoryDatabase("Tournament"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
