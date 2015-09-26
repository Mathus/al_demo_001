namespace Allors.Domain
{
    using System;

    public partial class Roles
    {
        public const string EmployeeName = "Employee";
        public const string AccountantName = "Accountant";

        public static readonly Guid EmployeeId = new Guid("CD995269-8002-4C6F-BB3E-866E5ED4C07E");
        public static readonly Guid AccountantId = new Guid("823AA4E5-3CD1-4EE7-A169-34532CBC4B8C");


        public Role Employee
        {
            get
            {
                return this.RoleCache.Get(EmployeeId);
            }
        }

        public Role Accountant
        {
            get { return this.RoleCache.Get(AccountantId); }
        }

        protected override void CustomSetup(Setup setup)
        {
            new RoleBuilder(this.Session).WithName(EmployeeName).WithUniqueId(EmployeeId).Build();
            new RoleBuilder(this.Session).WithName(AccountantName).WithUniqueId(AccountantId).Build();

            new SecurityCache(this.Session).Invalidate();
        }
    }
}