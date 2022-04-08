using Api_MaestroDetalle.Filters;
using Api_MaestroDetalle.Mappings;
using Api_MaestroDetalle.Models;
using Api_MaestroDetalle.Repository.Implementation;
using Api_MaestroDetalle.Repository.Interface;
using Api_MaestroDetalle.Services.Implemetation;
using Api_MaestroDetalle.Services.Interface;
using Api_MaestroDetalle.Utils;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Api_MaestroDetalle
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

            services.AddControllers(options =>
            {
                //Agregar el filtro de exceptiones como respuesta para todas las acciones
                options.Filters.Add<ExceptionFilter>();
            }).AddNewtonsoftJson(options =>
            {
              
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }).ConfigureApiBehaviorOptions(options =>
            {
                //Quitar la validacion del decorador '[ApiController]'
               options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api_MaestroDetalle", Version = "v1" });
            });

            services.AddCors(option =>
            {
                option.AddPolicy(name: "all",
                    builder => {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            //Inyectar configuraci�n por defecto de la paginaci�n
            services.Configure<PaginationOptions>(options => Configuration.GetSection("Pagination").Bind(options));

            //Global cadena de conexion
            /*services.AddDbContext<DBPruebaTecnicaContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("PruebaTecnica"))
           );*/

            services.AddDbContext<DBPruebaTecnicaContext>(options =>
            {
                string SERVER = Environment.GetEnvironmentVariable("SERVER");
                string PORT = Environment.GetEnvironmentVariable("PORT");
                string DATABASE = Environment.GetEnvironmentVariable("DATABASE");
                string USERNAME = Environment.GetEnvironmentVariable("USERNAME");
                string PASSWORD = Environment.GetEnvironmentVariable("PASSWORD");
                string INTEGRATED_SECURITY = Environment.GetEnvironmentVariable("INTEGRATED_SECURITY");
                string TRUST_SERVER_CERTIFICATE = Environment.GetEnvironmentVariable("TRUST_SERVER_CERTIFICATE");

                options.UseSqlServer($"Server={SERVER},{PORT};Initial Catalog={DATABASE};Persist Security Info=False;User ID={USERNAME};Password={PASSWORD};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate={TRUST_SERVER_CERTIFICATE}; Integrated Security={INTEGRATED_SECURITY};Connection Timeout=30;");
            });


            //Inyeccion de dependencias
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICiudadService, CiudadService>();
            services.AddTransient<IVendedorRepository, VendedorRepository>();
            services.AddTransient<IVendedorService, VendedorService>();

            //Mapper config
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Validaciones globales para las acciones 
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });


            services.Configure<PaginationOptions>(options => Configuration.GetSection("Pagination").Bind(options));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api_MaestroDetalle v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("all");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
