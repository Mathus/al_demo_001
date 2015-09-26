using System.CodeDom;

namespace Allors.Meta
{
    #region Allors
    [Id("1F378616-7B24-4D35-90C6-224F7DBC3732")]
    #endregion
  public partial class DepartmentClass : Class
    {
        #region Allors
        [Id("71FA73EC-2432-4C2E-BD07-08BDB761E275")]
        [AssociationId("44639CC0-C710-426B-9E63-C98D6A5E8F4D")]
        [RoleId("B0811328-A171-4464-900E-4D7F80CB8B5D")]
        #endregion
        [Indexed]
        [Type(typeof(PersonClass))]
        [Multiplicity(Multiplicity.OneToMany)]
        public RelationType Accountant;

        #region Allors
        [Id("4A2A35E0-7F63-4F85-B77F-BFA9C4196059")]
        [AssociationId("D0A57816-E6A7-46A0-9C91-6F256D5459AF")]
        [RoleId("97D05BA9-357A-437C-A576-DE1866E3CBB0")]
        #endregion
        [Indexed]
        [Type(typeof(UserGroupClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType AccountantUsergroup;


        #region Allors
        [Id("3A22FFF2-4EB0-4E99-B93F-D2A119DAAF4F")]
        [AssociationId("0A5AD5F8-0EFC-4F59-A4C1-86668EF2F94C")]
        [RoleId("4F2D119A-AEF7-4143-8650-3919CE35807F")]
        #endregion
        [Indexed]
        [Type(typeof(SecurityTokenClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType AccountantSecurityToken;

        #region Allors
        [Id("15FEC6C6-D5E3-48EA-92E9-D6C6AC86CFCA")]
        [AssociationId("E4DC0810-67BD-4DB2-BDD5-9B87B8290A3A")]
        [RoleId("BE925387-E0B7-410E-8D0E-293F2DA44EE6")]
        #endregion
        [Indexed]
        [Type(typeof(InvoiceClass))]
        [Multiplicity(Multiplicity.OneToMany)]
        public RelationType Invoice;


        public static DepartmentClass Instance { get; internal set; }

        internal DepartmentClass() : base(MetaPopulation.Instance)
        {
        }
        internal override void CustomExtend()
        {
        }
    }
}