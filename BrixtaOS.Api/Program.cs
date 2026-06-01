var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

UsersEndpoints.Map(app);
RolesEndpoints.Map(app);
UserRolesEndpoints.Map(app);
EventDefinitionsEndpoints.Map(app);
WorkflowsEndpoints.Map(app);
WorkflowInstancesEndpoints.Map(app);
EventsEndpoints.Map(app);

app.Run();
