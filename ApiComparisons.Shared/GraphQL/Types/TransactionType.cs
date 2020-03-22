using ApiComparisons.Shared.DAL;
using GraphQL.Types;

namespace ApiComparisons.Shared.GraphQL.Types
{
    public class TransactionType : ObjectGraphType<Transaction>
    {
        public TransactionType(ITransactionRepo repo)
        {
            Name = "Transaction";
            Field(o => o.ID).Description("The transaction ID.");
            Field(o => o.PersonID).Description("The ID of the person who created the transaction.");
            Field(o => o.Total).Description("The total transaction amount.");
            Field(o => o.Created).Description("When the transaction entry was created.");
            Interface(typeof(Transaction));

            //TODO: add purchases
        }
    }
}
