using MedicalAppointmentsAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper;
using MedicalAppointmentsAPI.Infrastructure.Mappings;
using MedicalAppointmentsAPI.Application.Interfaces;
using MedicalAppointmentsAPI.Application.Services;
using MedicalAppointmentsAPI.Domain.Interfaces;
using MedicalAppointmentsAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Services y Repositorios
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddScoped<IMedicalCenterRepository, MedicalCenterRepository>();
builder.Services.AddScoped<IMedicalCenterService, MedicalCenterService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MedicalAppointmentsAPI", Version = "v1" });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MedicalAppointmentsAPI v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
