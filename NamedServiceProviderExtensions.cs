using System.Diagnostics.CodeAnalysis;

namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// Provides extensions for <see cref="INamedServiceProvider"/>.
	/// </summary>
	public static class NamedServiceProviderExtensions
	{

		/// <summary>
		/// Gets the service <see cref="object"/> of the specified <typeparamref name="TService"/> and name.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of service <see cref="object"/> to get.</typeparam>
		/// <param name="provider">The <see cref="INamedServiceProvider"/>.</param>
		/// <param name="name">The name of the service.</param>
		/// <returns>A service <see cref="object"/> of <see cref="Type"/> <typeparamref name="TService"/>. -or- <see langword="null"/> if there is no service <see cref="object"/> of <see cref="Type"/> <typeparamref name="TService"/>.</returns>
		public static TService? GetService<TService>(this INamedServiceProvider provider, string name)
		{
			if(name is null)
				throw new ArgumentNullException(nameof(name));

			return (TService?)provider.GetService(typeof(TService), name);
		}

		/// <summary>
		/// Gets the service <see cref="object"/> of the specified <typeparamref name="TService"/> and name.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of service <see cref="object"/> to get.</typeparam>
		/// <param name="provider">The <see cref="INamedServiceProvider"/>.</param>
		/// <param name="name">The name of the service.</param>
		/// <returns>A service <see cref="object"/> of <see cref="Type"/> <typeparamref name="TService"/>. Throws if there is no service <see cref="object"/> of <see cref="Type"/> <typeparamref name="TService"/> named <paramref name="name"/>.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if there is no service of <see cref="Type"/> <typeparamref name="TService"/> named <paramref name="name"/>.
		/// </exception>
		public static TService GetRequiredService<TService>(this INamedServiceProvider provider, string name)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));

			TService? res = provider.GetService<TService>(name);
			return res is null ? throw new InvalidOperationException($"There is no service of type \"{typeof(TService).FullName}\" named \"{name}\".") : res;
		}

		/// <summary>
		/// Tries to get the service <see cref="object"/> of the specified <typeparamref name="TService"/> and name.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of service <see cref="object"/> to get.</typeparam>
		/// <param name="provider">The <see cref="INamedServiceProvider"/>.</param>
		/// <param name="name">The name of the service.</param>
		/// <param name="service">The retrieved service <see cref="object"/>.</param>
		/// <returns><see langword="true"/> on success, otherwise <see langword="false"/>.</returns>
		public static bool TryGetService<TService>(this INamedServiceProvider provider, string name, [MaybeNullWhen(false)] out TService service)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));

			TService? res = provider.GetService<TService>(name);
			if (res is not null)
			{
				service = res;
				return true;
			}
			service = default;
			return false;
		}
	}
}
