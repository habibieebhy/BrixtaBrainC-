using BrixtaOS.Application.Runtime;
using BrixtaOS.Infrastructure.Persistence;
using BrixtaOS.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BrixtaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<InMemoryDataStore>();
builder.Services.AddSingleton<WorkflowRuntime>();

var app = builder.Build();

UsersEndpoints.Map(app);
RolesEndpoints.Map(app);
UserRolesEndpoints.Map(app);
EventDefinitionsEndpoints.Map(app);
WorkflowsEndpoints.Map(app);
WorkflowInstancesEndpoints.Map(app);
EventsEndpoints.Map(app);

app.Run();
