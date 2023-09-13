namespace Umbrella.DependencyInjection
{
	internal class SingletonActivator : IServiceActivator
	{
		public bool IsScoped => false;

		private readonly ServiceProvider _provider;
		private readonly ServiceDescriptor _descriptor;

		public SingletonActivator(ServiceProvider provider, ServiceDescriptor descriptor)
		{
			_provider = provider;
			_descriptor = descriptor;
		}

		public object Activate()
		{
			if (_provider._singletons.ContainsKey(_descriptor.ServiceType))
				return _provider._singletons[_descriptor.ServiceType];

			object res;

			if (_descriptor.HasImplementationInstance)
				res = _descriptor.ImplementationInstance;
			else if (_descriptor.HasImplementationFactory)
				res = _descriptor.ImplementationFactory.Invoke(_provider);
			else if (_descriptor.HasImplementationType)
				res = _provider.Instantiate(_descriptor.ImplementationType);
			else
				res = _provider.Instantiate(_descriptor.ServiceType);

			return res;
		}
	}
}
