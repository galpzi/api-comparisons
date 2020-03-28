using ApiComparisons.Shared.DAL;
using ApiComparisons.Shared.GraphQL.Types.Outputs;
using GraphQL.Types;

namespace ApiComparisons.Shared.GraphQL.Types.Inputs
{
    public class PurchaseInputType : InputObjectGraphType<Purchase>
    {
        public PurchaseInputType()
        {
            Name = "PurchaseInput";
            Field(o => o.ProductID).Description("The purchase's product ID.");
            Field(o => o.TransactionID).Description("The purchase's transaction ID.");
            Field(o => o.Count).Description("Number of products purchased.");
            Field(o => o.Price).Description("Total purchase price.");
            Field(o => o.Created).Description("Purchase date.");
            Field<ProductInputType>(name: "product", description: "The product for this purchase.");
        }
    }
}
