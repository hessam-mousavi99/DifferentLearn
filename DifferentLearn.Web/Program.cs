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
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);

});
#endregion

#region Context
builder.Services.AddDbContext<DiffLearnContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DiffLearnConn"));
}, ServiceLifetime.Transient);

#endregion

#region Ioc
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IWalletService, WalletService>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IPermissionService, Permissionservice>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IOrderService, OrderService>();
#endregion

#endregion


#region Piplines

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.ToString().ToLower().StartsWith("/assets/Coursefilesonline"))
    {
        var callingurl = context.Request.Headers["Referer"].ToString();
        if (callingurl != "" && (callingurl.StartsWith("http://localhost:44361") || callingurl.StartsWith("https://localhost:7262")))
        {
            await next.Invoke();
        }
        else
        {
            context.Response.Redirect("/Login");
        }
    }
    else
    {
        //ejaze dadan b raftan b middleware badi
        await next.Invoke();
    }

});
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
