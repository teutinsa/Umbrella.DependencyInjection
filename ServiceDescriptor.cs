using System.Diagnostics.CodeAnalysis;

namespace Umbrella.DependencyInjection
{
	/// <summary>
	/// Describes a service with its service type, implementation, and lifetime.
	/// </summary>
	public sealed class ServiceDescriptor
	{
		/// <summary>
		/// Gets the <see cref="Type"/> of the service.
		/// </summary>
		public Type ServiceType { get; }

		/// <summary>
		/// Gets the <see cref="ServiceLifetime">Lifetime</see> of the service.
		/// </summary>
		public ServiceLifetime Lifetime { get; }

		/// <summary>
		/// Indicates whether the service is named or not.
		/// </summary>
		[MemberNotNullWhen(true, nameof(Name))]
		public bool IsNamed => Name is not null;

		/// <summary>
		/// Gets the name of the service.
		/// </summary>
		public string? Name { get; private init; }

		/// <summary>
		/// Indicates whether the service has an implementation factory or not.
		/// </summary>
		[MemberNotNullWhen(true, nameof(ImplementationFactory))]
		public bool HasImplementationFactory => ImplementationFactory is not null;

		/// <summary>
		/// Gets the implementation factory of the service.
		/// </summary>
		public Func<IDependentInstantiator, object>? ImplementationFactory { get; private init; }

		/// <summary>
		/// Indicates whether the service has an implementation <see cref="Type"/> or not.
		/// </summary>
		[MemberNotNullWhen(true, nameof(ImplementationType))]
		public bool HasImplementationType => ImplementationType is not null;

		/// <summary>
		/// Gets the implementation <see cref="Type"/> of the service.
		/// </summary>
		public Type? ImplementationType { get; private init; }

		/// <summary>
		/// Indicates whether the service has an implementation instance or not.
		/// </summary>
		[MemberNotNullWhen(true, nameof(ImplementationInstance))]
		public bool HasImplementationInstance => ImplementationInstance is not null;

		/// <summary>
		/// Gets the implementation instance of the service.
		/// </summary>
		public object? ImplementationInstance { get; private init; }

		private ServiceDescriptor(Type type, ServiceLifetime lifetime)
		{
			ServiceType = type;
			Lifetime = lifetime;
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, and the <see cref="ServiceLifetime.Singleton">Singleton</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Singleton(Type serviceType)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Singleton);
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Singleton">Singleton</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Singleton(Type serviceType, Func<IDependentInstantiator, object> implementationFactory)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Singleton)
			{
				ImplementationFactory = implementationFactory
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, <paramref name="implementationInstance"/>, and the <see cref="ServiceLifetime.Singleton">Singleton</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="implementationInstance">The implementation instance of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Singleton(Type serviceType, object implementationInstance)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (implementationInstance is null)
				throw new ArgumentNullException(nameof(implementationInstance));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Singleton)
			{
				ImplementationInstance = implementationInstance
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, <paramref name="implementationType"/>, and the <see cref="ServiceLifetime.Singleton">Singleton</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="implementationType">The implementation <see cref="Type"/> of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Singleton(Type serviceType, Type implementationType)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (implementationType is null)
				throw new ArgumentNullException(nameof(implementationType));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Singleton)
			{
				ImplementationType = implementationType
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>, and the <see cref="ServiceLifetime.Singleton">Singleton</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <typeparam name="TImplementation">The implementation <see cref="Type"/> of the service.</typeparam>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Singleton<TService, TImplementation>()
		{
			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton)
			{
				ImplementationType = typeof(TImplementation)
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Singleton">Singleton</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <typeparam name="TImplementation">The implementation <see cref="Type"/> of the service.</typeparam>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Singleton<TService, TImplementation>(Func<IDependentInstantiator, TImplementation> implementationFactory)
		{
			if(implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton)
			{
				ImplementationFactory = implementationFactory as Func<IDependentInstantiator, object>
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <paramref name="implementationInstance"/>, and the <see cref="ServiceLifetime.Singleton">Singleton</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <param name="implementationInstance">The implementation instance of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Singleton<TService>(TService implementationInstance)
		{
			if(implementationInstance is null)
				throw new ArgumentNullException(nameof(implementationInstance));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton)
			{
				ImplementationInstance = implementationInstance
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient(Type serviceType)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Transient);
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient(Type serviceType, Func<IDependentInstantiator, object> implementationFactory)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Transient)
			{
				ImplementationFactory = implementationFactory
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, <paramref name="implementationType"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="implementationType">The implementation <see cref="Type"/> of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient(Type serviceType, Type implementationType)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (implementationType is null)
				throw new ArgumentNullException(nameof(implementationType));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Transient)
			{
				ImplementationType = implementationType
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient<TService>()
		{
			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient);
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <typeparam name="TImplementation">The implementation <see cref="Type"/> of the service.</typeparam>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient<TService, TImplementation>(Func<IDependentInstantiator, TImplementation> implementationFactory)
		{
			if(implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient)
			{
				ImplementationFactory = implementationFactory as Func<IDependentInstantiator, object>
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <typeparam name="TImplementation">The implementation <see cref="Type"/> of the service.</typeparam>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient<TService, TImplementation>()
		{
			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient)
			{
				ImplementationType = typeof(TImplementation)
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient<TService>(Func<ServiceProvider, TService> implementationFactory)
		{
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient)
			{
				ImplementationFactory = implementationFactory as Func<IDependentInstantiator, object>
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, name, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="name">The name of the service.</param>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient(Type serviceType, string name, Func<IDependentInstantiator, object> implementationFactory)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Transient)
			{
				Name = name,
				ImplementationFactory = implementationFactory
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, name, <paramref name="implementationType"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="name">The name of the service.</param>
		/// <param name="implementationType">The implementation <see cref="Type"/> of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient(Type serviceType, string name, Type implementationType)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (implementationType is null)
				throw new ArgumentNullException(nameof(implementationType));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Transient)
			{
				Name = name,
				ImplementationType = implementationType
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, name, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <typeparam name="TImplementation">The implementation <see cref="Type"/> of the service.</typeparam>
		/// <param name="name">The name of the service.</param>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient<TService, TImplementation>(string name, Func<IDependentInstantiator, TImplementation> implementationFactory)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient)
			{
				Name = name,
				ImplementationFactory = implementationFactory as Func<IDependentInstantiator, object>
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, name, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <param name="name">The name of the service.</param>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient<TService>(string name, Func<IDependentInstantiator, TService> implementationFactory)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient)
			{
				Name = name,
				ImplementationFactory = implementationFactory as Func<IDependentInstantiator, object>
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, name, <typeparamref name="TImplementation"/>, and the <see cref="ServiceLifetime.Transient">Transient</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <typeparam name="TImplementation">The implementation <see cref="Type"/> of the service.</typeparam>
		/// <param name="name">The name of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Transient<TService, TImplementation>(string name)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient)
			{
				Name = name,
				ImplementationType = typeof(TImplementation)
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped(Type serviceType)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Scoped);
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped(Type serviceType, Func<IDependentInstantiator, object> implementationFactory)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Scoped)
			{
				ImplementationFactory = implementationFactory
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, <paramref name="implementationType"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="implementationType">The implementation <see cref="Type"/> of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped(Type serviceType, Type implementationType)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (implementationType is null)
				throw new ArgumentNullException(nameof(implementationType));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Scoped)
			{
				ImplementationType = implementationType
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped<TService>()
		{
			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Scoped);
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <typeparam name="TImplementation">The implementation <see cref="Type"/> of the service.</typeparam>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped<TService, TImplementation>(Func<IDependentInstantiator, TImplementation> implementationFactory)
		{
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Scoped)
			{
				ImplementationFactory = implementationFactory as Func<IDependentInstantiator, object>
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped<TService>(Func<ServiceProvider, TService> implementationFactory)
		{
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Scoped)
			{
				ImplementationFactory = implementationFactory as Func<IDependentInstantiator, object>
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <typeparam name="TImplementation">The implementation <see cref="Type"/> of the service.</typeparam>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped<TService, TImplementation>()
		{
			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Scoped)
			{
				ImplementationType = typeof(TImplementation)
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, <paramref name="name"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="name">The name of the service.</param>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped(Type serviceType, string name, Func<IDependentInstantiator, object> implementationFactory)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Scoped)
			{
				Name = name,
				ImplementationFactory = implementationFactory
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="serviceType"/>, <paramref name="name"/>, <paramref name="implementationType"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> of the service.</param>
		/// <param name="name">The name of the service.</param>
		/// <param name="implementationType">The implementation <see cref="Type"/> of the service.</param>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped(Type serviceType, string name, Type implementationType)
		{
			if (serviceType is null)
				throw new ArgumentNullException(nameof(serviceType));
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (implementationType is null)
				throw new ArgumentNullException(nameof(implementationType));

			return new ServiceDescriptor(serviceType, ServiceLifetime.Scoped)
			{
				Name = name,
				ImplementationType = implementationType
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <paramref name="name"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <param name="name">The name of the service.</param>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <typeparam name="TImplementation">The implementation <see cref="Type"/> of the service.</typeparam>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped<TService, TImplementation>(string name, Func<IDependentInstantiator, TImplementation> implementationFactory)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Scoped)
			{
				Name = name,
				ImplementationFactory = implementationFactory as Func<IDependentInstantiator, object>
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <paramref name="name"/>, <paramref name="implementationFactory"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <param name="name">The name of the service.</param>
		/// <param name="implementationFactory">The implementation factory of the service.</param>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped<TService>(string name, Func<IDependentInstantiator, TService> implementationFactory)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (implementationFactory is null)
				throw new ArgumentNullException(nameof(implementationFactory));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Scoped)
			{
				Name = name,
				ImplementationFactory = implementationFactory as Func<IDependentInstantiator, object>
			};
		}

		/// <summary>
		/// Creates an instance of <see cref="ServiceDescriptor"/> with the specified <typeparamref name="TService"/>, <paramref name="name"/>, <typeparamref name="TImplementation"/>, and the <see cref="ServiceLifetime.Scoped">Scoped</see> lifetime.
		/// </summary>
		/// <param name="name">The name of the service.</param>
		/// <typeparam name="TService">The <see cref="Type"/> of the service.</typeparam>
		/// <typeparam name="TImplementation">The implementation <see cref="Type"/> of the service.</typeparam>
		/// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
		public static ServiceDescriptor Scoped<TService, TImplementation>(string name)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));

			return new ServiceDescriptor(typeof(TService), ServiceLifetime.Scoped)
			{
				Name = name,
				ImplementationType = typeof(TImplementation)
			};
		}
	}
}
