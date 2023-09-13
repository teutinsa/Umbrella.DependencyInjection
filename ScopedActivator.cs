namespace Umbrella.DependencyInjection
{
	internal class ScopedActivator : IServiceActivator
	{
		public bool IsScoped => true;

#pragma warning disable IDE0052
		private readonly IDependentInstantiator _provider;
		private readonly ServiceDescriptor _descriptor;
#pragma warning restore IDE0052

		public ScopedActivator(IDependentInstantiator provider, ServiceDescriptor descriptor)
		{
			_provider = provider;
			_descriptor = descriptor;
		}

		public object Activate()
		{
			throw new NotImplementedException("Scoped activation will soon be implemented.");
		}
	}
}