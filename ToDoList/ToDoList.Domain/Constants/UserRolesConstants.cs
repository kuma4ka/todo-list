namespace ToDoList.Domain.Constants;

public static class UserRolesConstants
{
    private static readonly IReadOnlyCollection<string> AllowedRoles = ["User", "Admin"];

    public static bool IsRoleAllowed(string role) => AllowedRoles.Contains(role);
}