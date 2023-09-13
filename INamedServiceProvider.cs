namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// Defines a mechanism for retrieving a named service <see cref="object"/>; that is, an <see cref="object"/> that provides custom support to other <see cref="object"/>s.
	/// </summary>
	public interface INamedServiceProvider : IServiceProvider
	{
		/// <summary>
		/// Gets the service <see cref="object"/> of the specified <see cref="Type"/> and name.
		/// </summary>
		/// <param name="serviceType">An <see cref="object"/> that specifies the <see cref="Type"/> of service <see cref="object"/> to get.</param>
		/// <param name="name">The name of the service <see cref="object"/> to get.</param>
		/// <returns>A service <see cref="object"/> of type <paramref name="serviceType"/>. -or- <see langword="null"/> if there is no service <see cref="object"/> of <see cref="Type"/> <paramref name="serviceType"/> with the given <paramref name="name"/>.</returns>
		object? GetService(Type serviceType, string name);
	}
}