//------------------------------------------------------------------------------------------------- 
// <copyright file="DomainTest.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the DomainTest type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors
{
    using System.IO;
    using System.Xml;


    using Allors.Meta;

    using global::Allors.Domain;

    using NUnit.Framework;

    public class DomainTest
    {
        private ISession session;

        public ISession Session
        {
            get { return this.session; }
        }

        [SetUp]
        public virtual void Init()
        {
            this.Init(true);
        }

        [TearDown]
        public virtual void TearDown()
        {
            this.session.Rollback();
            this.session = null;
        }

        protected void Init(bool setup)
        {
            if (setup)
            {
                var stringReader = new StringReader(Fixture.FullXml);
                var reader = new XmlTextReader(stringReader);
                Config.Default.Load(reader);
            }
            else
            {
                Config.Default.Init();
            }

            this.session = Config.Default.CreateSession();

            new SecurityCache(this.session).Invalidate();
        }

        protected IObject[] GetObjects(ISession session, Composite objectType)
        {
            return session.Extent(objectType);
        }
    }
}