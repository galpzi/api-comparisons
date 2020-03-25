using ApiComparisons.Shared.DAL;
using GraphQL.Types;

namespace ApiComparisons.Shared.GraphQL.Types
{
    public class PurchaseType : ObjectGraphType<Purchase>
    {
        public PurchaseType(ITransactionRepo repo)
        {
            Name = "Purchase";
            Field(o => o.ProductID).Description("The purchase's product ID.");
            Field(o => o.TransactionID).Description("The purchase's transaction ID.");
            Field(o => o.Count).Description("Number of products purchased.");
            Field(o => o.Price).Description("Total purchase price.");
            Field(o => o.Created).Description("Purchase date.");
            Field<ProductType>(
                name: "product",
                description: "The product for this purchase.",
                resolve: context => repo.GetProductAsync(context.Source));
        }
    }
}
