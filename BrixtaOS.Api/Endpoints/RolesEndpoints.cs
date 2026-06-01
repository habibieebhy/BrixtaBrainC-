public static class RolesEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/roles", () => Results.Ok(new { Message = "Role created" }));
        app.MapGet("/api/roles", () => Results.Ok(new List<object>()));
        app.MapGet("/api/roles/{id}", (int id) => Results.Ok(new { Id = id, Name = "Admin" }));
    }
}
