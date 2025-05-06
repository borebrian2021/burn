//using BurnSociety.Application;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using System;
//using Microsoft.AspNetCore.Builder;

//var buildser = WebApplication.CreateBuilder(args);

//// Configure services
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddControllersWithViews()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
//        options.JsonSerializerOptions.PropertyNamingPolicy = null;
//    });
//builder.Services.AddRazorPages();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(14000);
//});
//builder.Services.AddTransient<SqlConnection>(_ => new SqlConnection(builder.Configuration["Default"]));
//builder.Services.AddDbContext<ApplicationDBContext>(
//    options => options.UseSqlServer(
//        builder.Configuration.GetConnectionString("Default"),
//        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
//    )
//);
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//// JWT Authentication
//var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        RequireExpirationTime = true,
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero
//    };
//});
//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.WithOrigins("http://example.com",
//                                              "http://www.contoso.com");
//                      });
//});

//// services.AddResponseCaching();

//builder.Services.AddControllers();

//// Build the application
//var app = builder.Build();

//// Use CORS

//app.UseAuthorization();
//app.MapControllers();
//// Configure middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//else
//{
//    app.UseDeveloperExceptionPage();
//    app.UseHsts();
//}

//app.UseSession();

//app.Use(async (context, next) =>
//{
//    var JWToken = context.Session.GetString("JWToken");
//    if (!string.IsNullOrEmpty(JWToken))
//    {
//        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
//    }
//    await next();
//});

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseAuthentication();
//app.UseRouting();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=HomePage}/{id?}");

//app.MapRazorPages();

//// Run the application
//app.Run();
