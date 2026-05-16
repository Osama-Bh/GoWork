using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GoWork.Authorization.Operations
{
    public static class ApplicationOperations
    {
        public static OperationAuthorizationRequirement Withdraw = new() { Name = nameof(Withdraw) };
    }
}
