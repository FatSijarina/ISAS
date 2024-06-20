using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using ISAS_Project.Configurations;
using ISAS_Project.Services.Implementation;
using ISAS_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ISASDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Monitoring configuration
builder.Host.UseMetricsWebTracking()
    .UseMetrics(options =>
    {
        options.EndpointOptions = endpointsOptions =>
        {
            endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
            endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
            endpointsOptions.EnvironmentInfoEndpointEnabled = false;
        };
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddTransient<IBiologicalTraceService, BiologicalTraceService>();
builder.Services.AddTransient<IBiologicalEvidence, BiologicalEvidenceService>();
builder.Services.AddTransient<IEvidenceService, EvidenceService>();
builder.Services.AddTransient<IInvolvedParty, InvolvedParty>();
builder.Services.AddTransient<IPhysicalEvidenceService, PhysicalEvidenceService>();
builder.Services.AddTransient<IStatementService, StatementService>();
builder.Services.AddTransient<ISuspectService, SuspectService>();
builder.Services.AddTransient<IVictimService, VictimService>();
builder.Services.AddTransient<IWitnessService, WitnessService>();

//builder.Services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:InstrumentationKey"]);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable App Metrics endpoints
app.UseMetricsAllEndpoints();
app.UseMetricServer();

app.MapControllers();

app.Run();
