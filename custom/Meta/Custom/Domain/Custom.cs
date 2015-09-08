namespace Allors.Meta
{
    #region Allors
	[Id("76E27D88-70C8-4000-A908-D4A668221F3A")]
    #endregion
    [Inherit(typeof(BaseDomain))]
    public partial class CustomDomain : Domain
	{
		public static CustomDomain Instance { get; internal set; }

		private CustomDomain(MetaPopulation metaPopulation) : base(metaPopulation)
        {
        }
	}
}