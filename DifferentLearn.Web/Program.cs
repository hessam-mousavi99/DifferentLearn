using DifferentLearn.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);
 

#region Containers

// Add services to the container.
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);


#region Context
builder.Services.AddDbContext<DiffLearnContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DiffLearnConn"));
});
#endregion

#endregion


#region Piplines

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMvc(routes =>
{
    routes.MapRoute(
   name: "areas",
   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
 );
    routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

#endregion
