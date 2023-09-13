using System.Collections;

namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// Default implementation of <see cref="IServiceCollection"/>.
	/// </summary>
	public sealed class ServiceCollection : IServiceCollection
	{
		/// <inheritdoc/>
		public ServiceDescriptor this[int index]
		{
			get => _services[index];
			set => _services[index] = value;
		}

		/// <inheritdoc/>
		public int Count { get; }

		/// <inheritdoc/>
		public bool IsReadOnly { get; }

		private readonly List<ServiceDescriptor> _services;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceCollection"/> class.
		/// </summary>
		public ServiceCollection()
		{
			_services = new List<ServiceDescriptor>();
		}

		/// <inheritdoc/>
		public void Add(ServiceDescriptor item) => _services.Add(item);

		/// <inheritdoc/>
		public void Clear() => _services.Clear();

		/// <inheritdoc/>
		public bool Contains(ServiceDescriptor item) => _services.Contains(item);

		/// <inheritdoc/>
		public void CopyTo(ServiceDescriptor[] array, int arrayIndex) => _services.CopyTo(array, arrayIndex);

		/// <inheritdoc/>
		public IEnumerator<ServiceDescriptor> GetEnumerator() => _services.GetEnumerator();

		/// <inheritdoc/>
		public int IndexOf(ServiceDescriptor item) => _services.IndexOf(item);

		/// <inheritdoc/>
		public void Insert(int index, ServiceDescriptor item) => _services.Insert(index, item);

		/// <inheritdoc/>
		public bool Remove(ServiceDescriptor item) => Remove(item);

		/// <inheritdoc/>
		public void RemoveAt(int index) => _services.RemoveAt(index);

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator() => _services.GetEnumerator();
	}
}
