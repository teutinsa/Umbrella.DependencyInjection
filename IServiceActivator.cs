namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// Provides functionality for the activation of a service.
	/// </summary>
	public interface IServiceActivator
	{
		/// <summary>
		/// Indicates if the <see cref="IServiceActivator"/> activates a scoped service.
		/// </summary>
		bool IsScoped { get; }

		/// <summary>
		/// Activates the service.
		/// </summary>
		/// <returns>The service instance.</returns>
		object Activate();
	}
}