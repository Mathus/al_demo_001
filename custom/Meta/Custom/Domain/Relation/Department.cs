namespace Allors.Meta
{
    #region Allors
    [Id("B585E9D4-9EE8-49C0-909D-D5E4022FB6C1")]
    #endregion
    public partial class DepartmentClass : Class
    {
        #region Allors
        [Id("C696EA84-0CF1-46EE-93E3-77367FFD025D")]
        [AssociationId("7CF2201B-CD4D-42DA-BA9D-1868BDC244AE")]
        [RoleId("944F441F-CEC5-48CC-B80B-F833EB7E1124")]
        #endregion
        [Type(typeof(PersonClass))]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType Accountant;
        
        public static DepartmentClass Instance { get; internal set; }

        internal DepartmentClass() : base(MetaPopulation.Instance)
        {
        }

        internal override void CustomExtend()
        {
        }
    }
}