namespace PocDDD.Api.Injection;

public static class DependenciesInjections
{
    public static void InjectDependencies(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.InjectExpenses();
        webApplicationBuilder.InjectUser();
    }
}
