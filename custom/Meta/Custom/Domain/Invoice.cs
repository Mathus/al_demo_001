namespace Allors.Meta
{
    #region Allors
    [Id("88C20CE7-847E-4846-B2BB-CFD96A675E12")]
    #endregion
    [Inherit(typeof(AccessControlledObjectInterface))]
    public partial class InvoiceClass : Class
    {
        #region Allors
        [Id("5EF94BCF-58BB-4361-B3CB-70304137208A")]
        [AssociationId("FD00AB9E-184D-49A1-A2F3-877BEA0F8901")]
        [RoleId("5A56E288-0921-4213-BC02-0D59CF120E0F")]
        #endregion
        [Type(typeof(AllorsIntegerUnit))]
        public RelationType Total;
        
        public static InvoiceClass Instance { get; internal set; }

        internal InvoiceClass() : base(MetaPopulation.Instance)
        {
        }

        internal override void CustomExtend()
        {
        }
    }
}