using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Application.UseCases.UserUseCases;
using JobApplicationTracker.infra.database.Sqlserver;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", corsBuilder =>
    {
        corsBuilder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddScoped<IJobRepository, MssqlEntityJobRepository>();
builder.Services.AddScoped<IUserRepository, MssqlEntityUserRepository>();
builder.Services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
builder.Services.AddScoped<ILoginUserUseCase, LoginUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.Run();