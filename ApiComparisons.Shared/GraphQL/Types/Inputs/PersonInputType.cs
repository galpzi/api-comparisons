using ApiComparisons.Shared.DAL;
using ApiComparisons.Shared.GraphQL.Types.Outputs;
using GraphQL.Types;

namespace ApiComparisons.Shared.GraphQL.Types.Inputs
{
    public class PersonInputType : InputObjectGraphType<Person>
    {
        public PersonInputType()
        {
            Name = "PersonInput";
            Field(o => o.ID).Name("id").Description("The person ID.");
            Field(o => o.Name).Description("The person's name.");
            Field(o => o.Created).Description("When the person entry was created.");
            Field<ListGraphType<TransactionInputType>>(name: "transactions", description: "The persons's transactions.");
        }
    }
}
