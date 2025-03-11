public static class ServiceRegistrator
{
	public static void RegistrateServices(this IServiceCollection services)
	{
		services.AddTransient<SparesRepository>();
		services.AddTransient<ProposalsRepository>();
		services.AddTransient<DbSparesRepository>();
		services.AddTransient<DbProposalsRepository>();
		services.AddTransient<RequestValidator>();
	}
}