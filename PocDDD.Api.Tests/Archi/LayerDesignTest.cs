using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using PocDDD.Api.Injection;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace PocDDD.Api.Tests.Archi;

public class LayersDesignTest
{
    private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(DependenciesInjections).Assembly).Build();

    [Fact]
    public void DomainShouldNotDependOnInfrastructure()
    {
        var domainLayer = Types().That().ResideInNamespace("PocDDD.Api.Domain")
            .As("Domain Layer");
        var infraLayer = Types().That().ResideInNamespace("PocDDD.Api.Infrastructure")
            .As("Infrastructure Layer");

        domainLayer.Should().NotDependOnAny(infraLayer).Check(Architecture);
    }

    [Fact]
    public void DomainShouldNotDependOnApplication()
    {
        var domainLayer = Types().That().ResideInNamespace("PocDDD.Api.Domain")
            .As("Domain Layer");
        var applicationLayer = Types().That().ResideInNamespace("PocDDD.Api.Application")
            .As("Application Layer");

        domainLayer.Should().NotDependOnAny(applicationLayer).Check(Architecture);
    }

    [Fact]
    public void ApplicationShouldNotDependOnInfrastructure()
    {
        var applicationLayer = Types().That().ResideInNamespace("PocDDD.Api.Application")
            .As("Application Layer");
        var infraLayer = Types().That().ResideInNamespace("PocDDD.Api.Infrastructure")
            .As("Infrastructure Layer");

        applicationLayer.Should().NotDependOnAny(infraLayer).Check(Architecture);
    }
}