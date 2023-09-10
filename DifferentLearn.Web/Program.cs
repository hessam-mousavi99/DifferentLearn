using DifferentLearn.Core.Convertors;
using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Core.Services.Services;
using DifferentLearn.Data.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);
 

#region Containers

// Add services to the container.
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath= "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);

});
#endregion

#region Context
builder.Services.AddDbContext<DiffLearnContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DiffLearnConn"));
});

#endregion

#region Ioc
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IWalletService, WalletService>();
builder.Services.AddTransient<IAdminService,AdminService>();
builder.Services.AddTransient<IPermissionService, Permissionservice>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
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
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
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
