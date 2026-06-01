public static class UserRolesEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/users/{userId}/roles/{roleId}", (int userId, int roleId) => Results.Ok(new { Message = "Role assigned to user" }));
    }
}
