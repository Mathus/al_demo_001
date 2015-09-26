using System.Runtime.InteropServices;

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

        [Test]
        public void TestInvoices()
        {
            var departmentA = new DepartmentBuilder(this.Session).Build();
            var departmentB = new DepartmentBuilder(this.Session).Build();

            var accountantA = new PersonBuilder(this.Session).WithFirstName("Accountant").WithLastName("A").Build();
            var accountantB = new PersonBuilder(this.Session).WithFirstName("Accountant").WithLastName("B").Build();

            departmentA.AddAccountant(accountantA);
            departmentB.AddAccountant(accountantB);

            var invoiceA = new InvoiceBuilder(this.Session).Build();
            var invoiceB = new InvoiceBuilder(this.Session).Build();

            departmentA.AddInvoice(invoiceA);
            departmentB.AddInvoice(invoiceB);

            this.Session.Derive();

            // Accountant A
            var aclAccountatAInvoiceA = new AccessControlList(invoiceA, accountantA);
            var aclAccountatAInvoiceB = new AccessControlList(invoiceB, accountantA);

            aclAccountatAInvoiceA.CanWrite(Invoice.Meta.Total).ShouldBeTrue();
            aclAccountatAInvoiceB.CanWrite(Invoice.Meta.Total).ShouldBeFalse();

            // Accountant B
            var aclAccountatBInvoiceA = new AccessControlList(invoiceA, accountantB);
            var aclAccountatBInvoiceB = new AccessControlList(invoiceB, accountantB);

            aclAccountatBInvoiceA.CanWrite(Invoice.Meta.Total).ShouldBeFalse();
            aclAccountatBInvoiceB.CanWrite(Invoice.Meta.Total).ShouldBeTrue();
        }
    }
}
