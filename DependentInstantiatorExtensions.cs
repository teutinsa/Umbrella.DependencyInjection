// Ignore Spelling: Instantiator

using System.Diagnostics.CodeAnalysis;

namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// Provides extensions for <see cref="IDependentInstantiator"/>
	/// </summary>
	public static class DependentInstantiatorExtensions
	{

		/// <summary>
		/// Creates an instance of a given <typeparamref name="T"/> by using the bast fitting constructor.
		/// </summary>
		/// <typeparam name="T">The <see cref="Type"/> of <see cref="object"/> to instantiate.</typeparam>
		/// <param name="instantiator">The <see cref="IDependentInstantiator"/>.</param>
		/// <returns>The <see cref="object"/> instance.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if no fitting constructor was found or a required type of service was not registered.
		/// </exception>
		public static T Instantiate<T>(this IDependentInstantiator instantiator)
		{
			return (T)instantiator.Instantiate(typeof(T));
		}

		/// <summary>
		/// Tries to create an instance of a given <typeparamref name="T"/> by using the bast fitting constructor.
		/// </summary>
		/// <typeparam name="T">The <see cref="Type"/> of <see cref="object"/> to instantiate.</typeparam>
		/// <param name="instantiator">The <see cref="IDependentInstantiator"/>.</param>
		/// <param name="object">The created instance <see cref="object"/>.</param>
		/// <returns><see langword="true"/> on success, otherwise <see langword="false"/>.</returns>
		public static bool TryInstantiate<T>(this IDependentInstantiator instantiator, [MaybeNullWhen(false)] out T @object)
		{
			T res;
			try
			{
				res = instantiator.Instantiate<T>();
			}
			catch (InvalidOperationException)
			{
				@object = default;
				return false;
			}
			if (res is not null)
			{
				@object = res;
				return true;
			}
			@object = default;
			return false;
		}
	}
}
