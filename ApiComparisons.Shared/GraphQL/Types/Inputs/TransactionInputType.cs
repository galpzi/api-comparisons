using ApiComparisons.Shared.DAL;
using ApiComparisons.Shared.GraphQL.Types.Outputs;
using GraphQL.Types;

namespace ApiComparisons.Shared.GraphQL.Types.Inputs
{
    public class TransactionInputType : InputObjectGraphType<Transaction>
    {
        public TransactionInputType()
        {
            Name = "TransactionInput";
            Field(o => o.ID).Name("id").Description("The transaction ID.");
            Field(o => o.PersonID).Description("The ID of the person who created the transaction.");
            Field(o => o.Total).Description("The total transaction amount.");
            Field(o => o.Created).Description("When the transaction entry was created.");
            Field<ListGraphType<PurchaseInputType>>(name: "purchases", description: "The transaction's purchases.");
        }
    }
}
