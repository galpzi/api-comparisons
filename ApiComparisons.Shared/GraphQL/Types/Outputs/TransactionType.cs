using ApiComparisons.Shared.DAL;
using GraphQL.Types;

namespace ApiComparisons.Shared.GraphQL.Types.Outputs
{
    public class TransactionType : ObjectGraphType<Transaction>
    {
        public TransactionType(ITransactionRepo repo)
        {
            Name = "Transaction";
            Field(o => o.ID).Name("id").Description("The transaction ID.");
            Field(o => o.PersonID).Description("The ID of the person who created the transaction.");
            Field(o => o.Total).Description("The total transaction amount.");
            Field(o => o.Created).Description("When the transaction entry was created.");
            Field<ListGraphType<PurchaseType>>(
                name: "purchases",
                description: "The transaction's purchases.",
                resolve: context => repo.GetPurchasesAsync(context.Source));
        }
    }
}
