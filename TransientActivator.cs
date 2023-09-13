namespace Umbrella.DependencyInjection
{
	internal class TransientActivator : IServiceActivator
	{
		public bool IsScoped => false;

		private readonly IDependentInstantiator _provider;
		private readonly ServiceDescriptor _descriptor;

		public TransientActivator(IDependentInstantiator provider, ServiceDescriptor descriptor)
		{
			_provider = provider;
			_descriptor = descriptor;
		}

		public object Activate()
		{
			if (_descriptor.HasImplementationInstance)
				return _descriptor.ImplementationInstance;

			if (_descriptor.HasImplementationFactory)
				return _descriptor.ImplementationFactory.Invoke(_provider);

			if (_descriptor.HasImplementationType)
				return _provider.Instantiate(_descriptor.ImplementationType);
			else
				return _provider.Instantiate(_descriptor.ServiceType);
		}
	}
}