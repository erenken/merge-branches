using Microsoft.Extensions.DependencyInjection;

namespace myNOC.MergeBranchesAction.Extensions
{
	static class IServiceCollectionExtensions
	{
		internal static IServiceCollection AddMergeBranchesActionServices(this IServiceCollection services)
		{
			return services;
		}
	}
}
