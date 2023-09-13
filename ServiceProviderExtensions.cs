using System.Diagnostics.CodeAnalysis;

namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// Provides extensions for <see cref="IServiceProvider"/>.
	/// </summary>
	public static class ServiceProviderExtensions
	{
		/// <summary>
		/// Gets the service <see cref="object"/> of the specified <paramref name="serviceType"/>.
		/// </summary>
		/// <param name="provider">The <see cref="IServiceProvider"/>.</param>
		/// <param name="serviceType">An <see cref="object"/> that specifies the <see cref="Type"/> of service <see cref="object"/> to get.</param>
		/// <returns>A service <see cref="object"/> of <see cref="Type"/> <paramref name="serviceType"/>. Throws if there is no service <see cref="object"/> of <see cref="Type"/> <paramref name="serviceType"/>.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if there is no service of <see cref="Type"/> <paramref name="serviceType"/>.
		/// </exception>
		public static object GetRequiredService(this IServiceProvider provider, Type serviceType)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));

			object? res = provider.GetService(serviceType);
			return res is null ? throw new InvalidOperationException($"There is no service of type \"{serviceType.FullName}\".") : res;
		}

		/// <summary>
		/// Gets the service <see cref="object"/> of the specified <typeparamref name="TService"/>.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of service <see cref="object"/> to get.</typeparam>
		/// <param name="provider">The <see cref="IServiceProvider"/>.</param>
		/// <returns>A service <see cref="object"/> of <see cref="Type"/> <typeparamref name="TService"/>. -or- <see langword="null"/> if there is no service <see cref="object"/> of <see cref="Type"/> <typeparamref name="TService"/>.</returns>
		public static TService? GetService<TService>(this IServiceProvider provider)
		{
			return (TService?)provider.GetService(typeof(TService));
		}

		/// <summary>
		/// Gets the service <see cref="object"/> of the specified <typeparamref name="TService"/>.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of service <see cref="object"/> to get.</typeparam>
		/// <param name="provider">The <see cref="IServiceProvider"/>.</param>
		/// <returns>A service <see cref="object"/> of <see cref="Type"/> <typeparamref name="TService"/>. Throws if there is no service <see cref="object"/> of <see cref="Type"/> <typeparamref name="TService"/>.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if there is no service of <see cref="Type"/> <typeparamref name="TService"/>.
		/// </exception>
		public static TService GetRequiredService<TService>(this IServiceProvider provider)
		{
			TService? res = provider.GetService<TService>();
			return res is null ? throw new InvalidOperationException($"There is no service of type \"{typeof(TService).FullName}\".") : res;
		}

		/// <summary>
		/// Tries to get the service <see cref="object"/> of the specified <typeparamref name="TService"/>.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of service <see cref="object"/> to get.</typeparam>
		/// <param name="provider">The <see cref="IServiceProvider"/>.</param>
		/// <param name="service">The retrieved service <see cref="object"/>.</param>
		/// <returns><see langword="true"/> on success, otherwise <see langword="false"/>.</returns>
		public static bool TryGetService<TService>(this IServiceProvider provider, [MaybeNullWhen(false)] out TService service)
		{
			TService? res = provider.GetService<TService>();
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
