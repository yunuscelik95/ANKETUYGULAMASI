using ANKETUYGULAMASI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();

var app = builder.Build();

app.UseSession(); // Session middleware en baþta olmalý

// Redirect to login if not authenticated
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();
    if (!path.StartsWith("/login") && string.IsNullOrEmpty(context.Session.GetString("UserName")) && !path.StartsWith("/css") && !path.StartsWith("/js") && !path.StartsWith("/lib"))
    {
        context.Response.Redirect("/Login");
        return;
    }
    await next();
});

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

app.MapRazorPages();

app.Run();
