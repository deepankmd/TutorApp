using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using System.Configuration;
using System.Security.Claims;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Repository;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;
using TutorAppAPI.Services.IServices;
using TutorAppAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var mongodbSetting = new MongoDbSettings();
builder.Configuration.GetSection("MongoDbSettings").Bind(mongodbSetting);
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton(new MongoContext(mongodbSetting.ConnectionString, mongodbSetting.DatabaseName));
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 100 * 1024 * 1024; // 100 MB
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<ITutorLocationRepository>(sp => new TutorLocationRepository(connectionString));
builder.Services.AddTransient<IRepository<TutorLevel>>(sp => new Repository<TutorLevel>(connectionString, "TutorLevel"));
builder.Services.AddTransient<IRepository<TutorLocations>>(sp => new Repository<TutorLocations>(connectionString, "TutorLocations"));
builder.Services.AddTransient<IRepository<TutorGradeValues>>(sp => new Repository<TutorGradeValues>(connectionString, "TutorGradeValues"));
builder.Services.AddTransient<IRepository<TutorGrade>>(sp => new Repository<TutorGrade>(connectionString, "TutorGrade"));

builder.Services.AddTransient<IRepository<TutorGradesSubject>>(sp => new Repository<TutorGradesSubject>(connectionString, "TutorGradesSubject"));
builder.Services.AddTransient<IRepository<TutorCategory>>(sp => new Repository<TutorCategory>(connectionString, "TutorCategory"));
builder.Services.AddTransient<IRepository<Assignment>>(sp => new Repository<Assignment>(connectionString, "Assignment"));
builder.Services.AddTransient<IRepository<Notification>>(sp => new Repository<Notification>(connectionString, "Notification"));
builder.Services.AddTransient<IRepository<Tutors>>(sp => new Repository<Tutors>(connectionString, "Tutors"));
builder.Services.AddTransient<IRepository<ParentDetails>>(sp => new Repository<ParentDetails>(connectionString, "ParentDetails"));
builder.Services.AddTransient<IRepository<EducationLevel>>(sp => new Repository<EducationLevel>(connectionString, "EducationLevel"));
builder.Services.AddTransient<IRepository<Admins>>(sp => new Repository<Admins>(connectionString, "Admins"));
builder.Services.AddTransient<IRepository<AccountInfo>>(sp => new Repository<AccountInfo>(connectionString, "AccountInfo"));
builder.Services.AddTransient<IRepository<AgencyManagers>>(sp => new Repository<AgencyManagers>(connectionString, "AgencyManagers"));
builder.Services.AddTransient<IRepository<AssignmentApplied>>(sp => new Repository<AssignmentApplied>(connectionString, "AssignmentApplied"));
builder.Services.AddTransient<IRepository<EducationAndQualifications>>(sp => new Repository<EducationAndQualifications>(connectionString, "EducationAndQualifications"));



builder.Services.AddScoped<EducationLevelServices>();
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
