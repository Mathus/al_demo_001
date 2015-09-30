namespace Allors.Domain
{
    using Allors.Meta;

    public partial class Departments
    {
        protected override void CustomSecure(Security config)
        {
            var full = new[] { Operation.Read, Operation.Write, Operation.Execute };

            config.GrantAdministrator(this.ObjectType, full);

            config.GrantEmployee(this.ObjectType, Operation.Read);
        }
    }
}