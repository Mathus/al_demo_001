using System.Linq;

namespace Allors.Domain
{
    public partial class Department
    {
        public void CustomOnBuild(ObjectOnBuild method)
        {
            this.AccountantUsergroup = new UserGroupBuilder(this.Strategy.Session).WithName("XXX Accountants").Build();
            this.AccountantSecurityToken = new SecurityTokenBuilder(this.Strategy.Session).Build();

            var accountantRole = new Roles(this.Strategy.Session).Accountant;

            new AccessControlBuilder(this.Strategy.Session)
                .WithRole(accountantRole)
                .WithObject(this.AccountantSecurityToken)
                .WithSubjectGroup(this.AccountantUsergroup)
                .Build();
        }

        public void CustomOnPreDerive(ObjectOnPreDerive method)
        {
            var derivation = method.Derivation;

            foreach (Object invoice in this.Invoices)
            {
                derivation.AddDependency(this, invoice);
            }
        }


        public void CustomOnDerive(ObjectOnDerive method)
        {
            this.AccountantUsergroup.Members = this.Accountants.Cast<User>().ToArray();
        }

    }
}