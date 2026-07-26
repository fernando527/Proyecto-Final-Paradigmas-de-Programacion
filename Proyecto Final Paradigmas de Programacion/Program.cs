var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseRouting();


app.UseSession();


app.UseAuthorization();


app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}/{id?}")
    // en caso de querer que inicie desde menu directamente:
    // pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();