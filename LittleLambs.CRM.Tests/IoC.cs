using System;
using Autofac;
using LittleLambs.CRM.DependencyInjection;

namespace LittleLambs.CRM.Tests
{
	internal static class IoC
	{
		public static ContainerBuilder Bootstrap()
		{
			var builder = new ContainerBuilder();
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			builder.RegisterAssemblyModules(assemblies);
			builder.RegisterAssemblyModules( typeof( CrmModule ).Assembly );

			return builder;
		}
	}
}