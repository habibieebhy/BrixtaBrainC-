public static class UsersEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/users", () => Results.Ok(new { Message = "User created" }));
        app.MapGet("/api/users", () => Results.Ok(new List<object>()));
        app.MapGet("/api/users/{id}", (int id) => Results.Ok(new { Id = id, Name = "John Doe" }));
    }
}
