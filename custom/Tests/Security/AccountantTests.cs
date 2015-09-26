namespace Allors.Domain
{
    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class AccountantTests : DomainTest
    {
        [Test]
        public void TestPopulation()
        {
            var employeeRole = new Roles(this.Session).Employee;
            var accountantRole = new Roles(this.Session).Accountant;

            employeeRole.ShouldNotBeNull();
            accountantRole.ShouldNotBeNull();
        }

        [Test]
        public void TestEmployeesCanRead()
        {
            var employeeRole = new Roles(this.Session).Employee;

            var employees = new UserGroupBuilder(this.Session)
                .WithName("Employees")
                .Build();

            var john = new PersonBuilder(this.Session).WithFirstName("John").WithLastName("Doe").Build();
            employees.AddMember(john);

            var invoice = new InvoiceBuilder(this.Session).Build();

            var singleton = Singleton.Instance(this.Session);
            var defaultSecurityToken = singleton.DefaultSecurityToken;
            
            var accessControl = new AccessControlBuilder(this.Session)
                .WithRole(employeeRole)
                .WithObject(defaultSecurityToken)
                .WithSubjectGroup(employees)
                .Build();

            var acl = new AccessControlList(invoice, john);

            acl.CanRead(Invoice.Meta.Total).ShouldBeTrue();
        }
    }
}
