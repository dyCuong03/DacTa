namespace SmartDevice.Installer;

public static class ExtenstionInstaller
{
    public static void InstallController(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}