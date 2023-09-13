using System.Reflection;

namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// The default <see cref="IServiceProvider"/>.
	/// </summary>
	public sealed class ServiceProvider : IDependentInstantiator
	{
		private readonly struct NamedServiceKey
		{
			public Type ServiceType { get; }

			public string Name { get; }

			public NamedServiceKey(Type serviceType, string name)
			{
				ServiceType = serviceType;
				Name = name;
			}

			public override int GetHashCode()
			{
				return HashCode.Combine(ServiceType, Name);
			}
		}

		private readonly Dictionary<Type, IServiceActivator> _services;
		private readonly Dictionary<NamedServiceKey, IServiceActivator> _namedServices;
		internal readonly Dictionary<Type, object> _singletons;

		internal ServiceProvider(IServiceCollection serviceCollection)
		{
			if (serviceCollection is null)
				throw new ArgumentNullException(nameof(serviceCollection));

			_services = new();
			_namedServices = new();
			_singletons = new();

			foreach (ServiceDescriptor sd in serviceCollection)
			{
				switch (sd.Lifetime)
				{
					case ServiceLifetime.Singleton:
						if (!_services.TryAdd(sd.ServiceType, new SingletonActivator(this, sd)))
							throw new InvalidOperationException($"Duplicate service type of \"{sd.ServiceType.FullName}\".");
						break;

					case ServiceLifetime.Scoped:
						if (sd.IsNamed)
						{
							if (!_namedServices.TryAdd(new(sd.ServiceType, sd.Name), new ScopedActivator(this, sd)))
								throw new InvalidOperationException($"Duplicate named service type of \"{sd.ServiceType.FullName}\" with name \"{sd.Name}\".");
						}
						else
						{
							if (!_services.TryAdd(sd.ServiceType, new ScopedActivator(this, sd)))
								throw new InvalidOperationException($"Duplicate service type of \"{sd.ServiceType.FullName}\".");
						}
						break;

					case ServiceLifetime.Transient:
						if (sd.IsNamed)
						{
							if (!_namedServices.TryAdd(new(sd.ServiceType, sd.Name), new TransientActivator(this, sd)))
								throw new InvalidOperationException($"Duplicate named service type of \"{sd.ServiceType.FullName}\" with name \"{sd.Name}\".");
						}
						else
						{
							if (!_services.TryAdd(sd.ServiceType, new TransientActivator(this, sd)))
								throw new InvalidOperationException($"Duplicate service type of \"{sd.ServiceType.FullName}\".");
						}
						break;
				}
			}
		}

		/// <inheritdoc/>
		public object? GetService(Type serviceType)
		{
			if(serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));

#pragma warning disable CS8600
			if (_services.TryGetValue(serviceType, out IServiceActivator activator))
				return activator.Activate();
#pragma warning restore CS8600
			return null;
		}

		/// <inheritdoc/>
		public object? GetService(Type serviceType, string name)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if(name is null)
				throw new ArgumentNullException(nameof(name));

#pragma warning disable CS8600
			if (_namedServices.TryGetValue(new(serviceType, name), out IServiceActivator activator))
				return activator.Activate();
#pragma warning restore CS8600
			return null;
		}

		/// <summary>
		/// Creates a scope.
		/// </summary>
		/// <returns>The created <see cref="IServiceScope">scope</see>.</returns>
		/// <exception cref="NotImplementedException"><see cref="CreateScope"/> will soon be implemented.</exception>
		public IServiceScope CreateScope()
		{
			throw new NotImplementedException($"{nameof(CreateScope)} will soon be implemented.");
		}

		/// <inheritdoc/>
		public object Instantiate(Type objectType)
		{
			if (objectType is null)
				throw new ArgumentNullException(nameof(objectType));

			ConstructorInfo[] constructors = objectType.GetConstructors();

			if(constructors.Length == 1)
				return Instantiate(constructors[0]);

			IEnumerable<ConstructorInfo> infos = constructors.OrderByDescending(ci => ci.GetParameters().Length).Where(ci => ci.GetParameters().All(pi => _services.ContainsKey(pi.ParameterType)));

			if (!infos.Any())
				throw new InvalidOperationException($"No suitable constructor for \"{objectType.FullName}\" found.");

			return Instantiate(infos.First());
		}

		/// <inheritdoc/>
		public object Instantiate(ConstructorInfo constructor)
		{
			if (constructor is null)
				throw new ArgumentNullException(nameof(constructor));

			Type[] serviceTypes = constructor.GetParameters().Select(p => p.ParameterType).ToArray();

			if (serviceTypes.Length == 0)
				return constructor.Invoke(null);

			object[] @params = new object[serviceTypes.Length];
			for (int i = 0; i < @params.Length; i++)
				@params[i] = this.GetRequiredService(serviceTypes[i]);

			return constructor.Invoke(@params);
		}
	}
}
