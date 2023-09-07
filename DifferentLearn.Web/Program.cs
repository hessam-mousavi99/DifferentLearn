using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
//builder.Services.AddRazorPages();
//builder.Services.AddControllersWithViews();

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

//app.MapRazorPages();
app.UseMvc(routes =>
{
    routes.MapRoute(
   name: "areas",
   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
 );
    routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
});

app.Run();


#region Comments
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//          name: "areas",
//          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//        );
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//});
#endregion