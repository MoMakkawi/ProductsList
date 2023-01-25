using ProductList.Domain;

namespace ProductList.GraphQL.Types;

public class ProductType : ObjectType<Domain.Product>
{
    protected override void Configure(IObjectTypeDescriptor<Domain.Product> descriptor)
    {
        descriptor.Field(x => x.Id)
            .Type<NonNullType<IdType>>();

        descriptor.Field(x => x.Name)
            .Type<StringType>();

        descriptor.Field(x => x.Price)
            .Type<IntType>();

        descriptor.Field(x => x.ImagePath)
            .Type<StringType>();
    }
}