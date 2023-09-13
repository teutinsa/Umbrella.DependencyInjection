namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// Provides extensions for <see cref="IServiceCollection"/>.
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Builds the <see cref="ServiceCollection"/> into a <see cref="ServiceProvider"/>.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> to be built.</param>
		/// <returns>The built <see cref="ServiceProvider"/>.</returns>
		public static ServiceProvider BuildProvider(this IServiceCollection services)
		{
			return new ServiceProvider(services);
		}
	}
}
