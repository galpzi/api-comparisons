using ApiComparisons.Shared.DAL;
using GraphQL.Types;

namespace ApiComparisons.Shared.GraphQL.Types
{
    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType(ITransactionRepo repo)
        {
            Name = "Person";
            Field(o => o.ID).Name("id").Description("The person ID.");
            Field(o => o.Name, nullable: true).Description("The person's name.");
            Field(o => o.Created).Description("When the person entry was created.");
            Field<ListGraphType<TransactionType>>(
                name: "transactions",
                description: "The persons's transactions.",
                resolve: context => repo.GetTransactionsAsync(context.Source));
        }
    }
}
