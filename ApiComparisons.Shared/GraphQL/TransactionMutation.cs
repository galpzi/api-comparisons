using GraphQL.Types;

namespace ApiComparisons.Shared.GraphQL
{
    public class TransactionMutation : ObjectGraphType
    {
        public TransactionMutation(ITransactionRepo repo)
        {
            Name = "Mutation";
        }
    }
}
