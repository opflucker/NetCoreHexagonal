using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace NetCoreHexagonal.ArchitectureUnitTests
{
    public class DependenciesTests
    {
        private static readonly Assembly domainAssembly = Assembly.Load("NetCoreHexagonal.Domain");
        private static readonly Assembly applicationAssembly = Assembly.Load("NetCoreHexagonal.Application");
        private static readonly Assembly consoleAssembly = Assembly.Load("NetCoreHexagonal.Console");
        private static readonly Assembly webapiAssembly = Assembly.Load("NetCoreHexagonal.WebApi");
        private static readonly Assembly persistenceAssembly = Assembly.Load("NetCoreHexagonal.Persistence");
        private static readonly Assembly eventsDispatchingAssembly = Assembly.Load("NetCoreHexagonal.EventsDispatching");

        private static IEnumerable<Assembly> GetInAdapters()
        {
            yield return consoleAssembly;
            yield return webapiAssembly;
        }

        private static IEnumerable<Assembly> GetOutAdapters()
        {
            yield return persistenceAssembly;
            yield return eventsDispatchingAssembly;
        }

        private static IEnumerable<Assembly> GetAllAssemblies()
        {
            yield return domainAssembly;
            yield return applicationAssembly;
            yield return persistenceAssembly;
            yield return eventsDispatchingAssembly;
            yield return consoleAssembly;
            yield return webapiAssembly;
        }

        private static IEnumerable<object[]> GetValidDependencies()
        {
            yield return new[] { domainAssembly, (object)Array.Empty<Assembly>() };

            yield return new[] { applicationAssembly, (object)new object[] { domainAssembly } };

            var coreAssemblies = new object[] { domainAssembly, applicationAssembly };
            foreach (var outAdapter in GetOutAdapters())
                yield return new[] { outAdapter, (object)coreAssemblies };

            var coreAndOutAdaptersAssemblies = coreAssemblies.Union(GetOutAdapters()).ToArray();
            foreach (var inAdapter in GetInAdapters())
            {
                yield return new[] { inAdapter, (object)coreAndOutAdaptersAssemblies };
            }
        }

        [Theory]
        [MemberData(nameof(GetValidDependencies))]
        public void Assembly_Should_Have_Only_Valid_Dependencies(Assembly dependant, Assembly[] validDependencies)
        {
            var invalidDependencies = GetAllAssemblies()
                .Except(new[] { dependant })
                .Except(validDependencies);

            var typeNamesInInvalidDependencies = Types.InAssemblies(invalidDependencies)
                .GetTypes()
                .Where(t => t.FullName != "Program") // top-level Program classes in .net 6 are exceptions
                .Select(t => t.FullName)
                .ToArray();

            var typesWithInvalidDependencies = Types.InAssembly(dependant)
                .Should().HaveDependencyOnAny(typeNamesInInvalidDependencies)
                .GetTypes();

            Assert.Empty(typesWithInvalidDependencies);
        }
    }
}