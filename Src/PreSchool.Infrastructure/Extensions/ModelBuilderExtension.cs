using PreSchool.Application.Infastructures;
using PreSchool.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace PreSchool.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void ApplyAllConfiguration (this ModelBuilder modelBuilder)
        {
            var applyConfigurationMethodInfo = modelBuilder
                                            .GetType()
                                            .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                                            .First(m => m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));

            var ret = typeof(IApplicationDbContext).Assembly
                       .GetTypes()
                       .Select(t => (t, i: t.GetInterfaces().FirstOrDefault(i => i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.OrdinalIgnoreCase))))
                       .Where(it => it.i != null)
                       .Select(it => (et: it.i.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
                       .Select(it => applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] { it.cfgObj }))
                       .ToList();
        }
    }
}
