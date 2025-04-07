using System;

public static class ServiceConfiguration
{
	public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
	{
        var allowedOrigins = configuration.GetValue<string>("allowedOrigins");
        // allow my angular client to access this api 
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
            });
        });

        return services;
    }  
}
