namespace Allors.Meta
{
	public partial class PersonClass : Class
	{
		#region Allors
		[Id("f92c5c86-c32a-41e0-99ff-2d94a8d6ccfa")]
		[AssociationId("0ff499d5-300f-483c-b722-757787c1f4b3")]
		[RoleId("2162765c-5fd8-42aa-85f7-20f0effbc308")]
		#endregion
		[Indexed]
		[Type(typeof(MediaClass))]
		[Multiplicity(Multiplicity.OneToOne)]
		public RelationType Picture;
	}
}