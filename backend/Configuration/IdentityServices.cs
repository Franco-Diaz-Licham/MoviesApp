﻿namespace backend.Configuration;

public static class IdentityServices
{
    public static void AddIdentityServices(this WebApplicationBuilder builder)
    {
        // Register identity services.
        builder.Services.AddIdentityCore<UserEntity>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddUserManager<UserManager<UserEntity>>()
        .AddSignInManager<SignInManager<UserEntity>>()
        .AddEntityFrameworkStores<DataContext>();

        // Authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            // client send token as auth header
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Token:Key")!)),
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration.GetValue<string>("Token:Issuer")!,
                ValidateAudience = false
            };
        });

        builder.Services.AddAuthorization();
    }
}
