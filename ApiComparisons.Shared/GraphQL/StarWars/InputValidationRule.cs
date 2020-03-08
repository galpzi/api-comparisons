using GraphQL.Validation;
using System.Threading.Tasks;

namespace ApiComparisons.Shared.GraphQL.StarWars
{
    public class InputValidationRule : IValidationRule
    {
        public Task<INodeVisitor> ValidateAsync(ValidationContext context)
        {
            return Task.FromResult((INodeVisitor)new EnterLeaveListener(_ =>
            {
            }));
        }
    }
}
