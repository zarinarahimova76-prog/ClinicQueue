using ClinicQueue.Middlewares;
using ClinicQueue.Repositories;
using ClinicQueue.Models;
using Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();
app.UseMiddleware<RequestMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/api", () =>
{
    var response = new ApiResponse<string>
    {
        StatusCode = 200,
        Message = "ClinicQueue API is working",
        Data = "Welcome to ClinicQueue API"
    };

    return Results.Ok(response);
});
var patientRepository = new PatientRepository();

app.MapGet("/api/patients", async () =>
{
    var patients = await patientRepository.GetAll();

    var response = new ApiResponse<List<Patient>>
    {
        StatusCode = 200,
        Message = "Patients retrieved successfully",
        Data = patients
    };

    return Results.Ok(response);
});

app.Run();
