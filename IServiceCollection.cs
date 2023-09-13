namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// Defines methods to manipulate a collection of <see cref="ServiceDescriptor"/>s.
	/// </summary>
	public interface IServiceCollection : IList<ServiceDescriptor>, ICollection<ServiceDescriptor>, IEnumerable<ServiceDescriptor>
	{ }
}
