using ApiComparisons.Shared.DAL;
using GraphQL.Types;

namespace ApiComparisons.Shared.GraphQL.Types.Outputs
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(ITransactionRepo repo)
        {
            Name = "Product";
            Field(o => o.ID).Name("id").Description("The product's ID.");
            Field(o => o.StoreID).Description("The product's store ID.");
            Field(o => o.Name).Description("Name of the product.");
            Field(o => o.Price).Description("Total purchase price.");
            Field(o => o.Created).Description("Product creation date.");
            Field(o => o.Description).Description("Product description.");
            Field<StoreType>(
                name: "store",
                description: "The store where this product is sold.",
                resolve: context => repo.GetStoreAsync(context.Source));
        }
    }
}
