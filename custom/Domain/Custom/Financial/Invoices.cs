namespace Allors.Domain
{
    public partial class Invoices
    {
        protected override void CustomSecure(Security config)
        {
            var full = new[] { Operation.Read, Operation.Write, Operation.Execute };

            config.GrantAdministrator(this.ObjectType, full);
            config.GrantEmployee(this.ObjectType, Operation.Read);

            //config.GrantAccountant(this.ObjectType, Operation.Write);
            config.Grant(Roles.AccountantId, this.ObjectType, Meta.Total, Operation.Write);
        }
    }
}