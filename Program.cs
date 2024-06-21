using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TutorAppAPI.Helpers;
using TutorAppAPI.Services;
using TutorAppAPI.Services.IServices;
using TutorAppAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var mongodbSetting = new MongoDbSettings();
builder.Configuration.GetSection("MongoDbSettings").Bind(mongodbSetting);
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton(new MongoContext(mongodbSetting.ConnectionString, mongodbSetting.DatabaseName));

builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<EducationLevelServices>();
builder.Services.AddScoped<TutorCategoryServices>();
builder.Services.AddScoped<TutorLevelServices>();
builder.Services.AddScoped<TutorSubjectServices>();

builder.Services.AddSingleton<IAccountInfoService, AccountInfoService>();
builder.Services.AddSingleton<IRegisterTutorServices, RegisterTutorServices>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Login/Logout";
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAnyRole", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c =>
                (c.Type == ClaimTypes.Role &&
                (c.Value == UserConstants.ParentDetails ||
                 c.Value == UserConstants.AdminRole ||
                 c.Value == UserConstants.TutorRole)))));
});


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
