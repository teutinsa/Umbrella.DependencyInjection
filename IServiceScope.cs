namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// The <see cref="IDisposable.Dispose()">Dispose()</see> method ends the scope lifetime. Once Dispose is called, any scoped services that have been resolved from <see cref="IServiceScope.ServiceProvider">ServiceProvider</see> will be disposed.
	/// </summary>
	public interface IServiceScope : IDisposable
	{
		/// <summary>
		/// The <see cref="INamedServiceProvider"/> used to resolve dependencies from the scope.
		/// </summary>
		INamedServiceProvider ServiceProvider { get; }
	}
}
