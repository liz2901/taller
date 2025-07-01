using Autofac;
using  Encuba.Product.Domain.Interfaces.Cryptography;
using  Encuba.Product.Domain.Interfaces.Repositories;
using Encuba.Product.Domain.Interfaces.Services;
using Encuba.Product.Infrastructure.Cryptography;
using Encuba.Product.Infrastructure.Redis;
using Encuba.Product.Infrastructure.Repositories;

namespace   Encuba.Product.Api.Configurations.AutofacConfig;

public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserRepository>()
            .As<IUserRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<UserPublicAccessTokenRepository>()
            .As<IUserPublicAccessTokenRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<PublicAccessTokenRepository>()
            .As<IPublicAccessTokenRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<BCryptCryptographyHelper >()
            .As<IBCryptCryptographyHelper >()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<ProductRepository>()
            .As<IProductRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<ShoppingCartRepository>()
            .As<IShoppingCartRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<CacheRepository>()
            .As<ICacheRepository>()
            .InstancePerLifetimeScope();
    }
}