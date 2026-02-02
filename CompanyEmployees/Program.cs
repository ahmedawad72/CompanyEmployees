using CompanyEmployees.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));
// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);    
builder.Services.AddAutoMapper(typeof(Program));

// [ApiController] attribute auto handle some cases of errors 
// we dont need it to show the error message created by dontet 
// instead we need to show our custom error message 
// ex:- send a createCompany request (with empty body)without these below code lines and with it then
// reallize the difference of error message

// instead you can remove [ApiController] which is found in the top part of every controller 

/*
    With this, we are suppressing a default model state validation that is 
    implemented due to the existence of the [ApiController] attribute in 
    all API controllers. So this means that we can solve the same problem 
    differently, by commenting out or removing the [ApiController] 
    attribute only, without additional code for suppressing validation

    But we like keeping it in our controllers because, as you 
    could’ve seen, it provides additional functionalities other than just 400 – 
    Bad Request responses.

 */
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;       //chapter 7 content negotiation.
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters()
    .AddApplicationPart( typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

        app.Run();
