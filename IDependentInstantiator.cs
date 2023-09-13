// Ignore Spelling: Instantiator

using System.Reflection;

namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// Defines a mechanism for instantiating <see cref="object"/>s and automatically constructing them with the required dependencies when available.
	/// </summary>
	public interface IDependentInstantiator : INamedServiceProvider
	{
		/// <summary>
		/// Creates an instance of a given <see cref="Type"/> by using the bast fitting constructor.
		/// </summary>
		/// <param name="objectType">The <see cref="Type"/> of <see cref="object"/> to instantiate.</param>
		/// <returns>The <see cref="object"/> instance.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if no fitting constructor was found or a required <see cref="Type"/> of service was not registered.
		/// </exception>
		object Instantiate(Type objectType);

		/// <summary>
		/// Instantiates an <see cref="object"/>, using the given constructor.
		/// </summary>
		/// <param name="constructor">The constructor to use to instantiate the <see cref="object"/>.</param>
		/// <returns>The <see cref="object"/> instance.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown if a required <see cref="Type"/> of service was not registered.
		/// </exception>
		object Instantiate(ConstructorInfo constructor);
	}
}