using ApiComparisons.Shared.DAL;
using ApiComparisons.Shared.GraphQL.Types.Outputs;
using GraphQL.Types;

namespace ApiComparisons.Shared.GraphQL.Types.Inputs
{
    public class ProductInputType : InputObjectGraphType<Product>
    {
        public ProductInputType()
        {
            Name = "ProductInput";
            Field(o => o.ID).Name("id").Description("The product's ID.");
            Field(o => o.StoreID).Description("The product's store ID.");
            Field(o => o.Name).Description("Name of the product.");
            Field(o => o.Price).Description("Total purchase price.");
            Field(o => o.Created).Description("Product creation date.");
            Field(o => o.Description).Description("Product description.");
            Field<StoreInputType>(name: "store", description: "The store where this product is sold.");
        }
    }
}
