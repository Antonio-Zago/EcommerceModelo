using CrossCutting.Ioc;
using EcommerceModeloMvc.BackgroundJobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddInfraestructure(builder.Configuration);

// O Worker fica aqui pois depende de IHostedService — conceito do host ASP.NET Core
builder.Services.AddHostedService<ImportacaoWorker>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
