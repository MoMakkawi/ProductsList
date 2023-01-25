using ProductList.Domain;

namespace ProductList.GraphQL.Types;

public class ProductInput : InputObjectType<Domain.Product>
{
    protected override void Configure(IInputObjectTypeDescriptor<Domain.Product> descriptor)
    {
        descriptor.Field(x => x.Name)
            .Type<StringType>();

        descriptor.Field(x => x.Price)
            .Type<IntType>();

        descriptor.Field(x => x.ImagePath)
            .Type<StringType>();
    }
}

