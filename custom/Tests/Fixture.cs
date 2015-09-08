// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fixture.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Xml;

    using Allors.Domain;

    public class Fixture
    {
        private static string basicXml;
        private static string fullXml;

        public static string BasicXml
        {
            get
            {
                if (basicXml == null)
                {
                    SetupBasic();
                }

                return basicXml;
            }
        }

        public static string FullXml
        {
            get
            {
                if (fullXml == null)
                {
                    SetupFull();
                }

                return fullXml;
            }
        }

        private static void SetupBasic()
        {
            var configuration = new Adapters.Memory.IntegerId.Configuration { ObjectFactory = Config.ObjectFactory };
            Config.Default = new Adapters.Memory.IntegerId.Database(configuration);
            
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");

            var database = Config.Default;
            database.Init();

            using (var session = database.CreateSession())
            {
                new Setup(session, null).Apply();
                new Security(session).Apply();

                session.Derive(true);
                session.Commit();

                using (var stringWriter = new StringWriter())
                {
                    using (var writer = new XmlTextWriter(stringWriter))
                    {
                        database.Save(writer);
                        basicXml = stringWriter.ToString();
                    }
                }
            }
        }

        private static void SetupFull()
        {
            var configuration = new Adapters.Memory.IntegerId.Configuration { ObjectFactory = Config.ObjectFactory };
            Config.Default = new Adapters.Memory.IntegerId.Database(configuration);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");

            var database = Config.Default;
            database.Init();

            using (var session = database.CreateSession())
            {
                new Setup(session, null).Apply();
                new Security(session).Apply();

                session.Derive(true);
                session.Commit();

                using (var stringWriter = new StringWriter())
                {
                    using (var writer = new XmlTextWriter(stringWriter))
                    {
                        database.Save(writer);
                        basicXml = stringWriter.ToString();
                    }
                }

                var singleton = Singleton.Instance(session);
                singleton.Guest = new PersonBuilder(session).WithUserName("guest").WithLastName("guest").Build();

                var administrator = new PersonBuilder(session).WithUserName("administrator").WithLastName("Administrator").Build();

                var belgium = new Countries(session).CountryByIsoCode["BE"];
                var euro = belgium.Currency;

                session.Derive(true);
                session.Commit();

                using (var stringWriter = new StringWriter())
                {
                    using (var writer = new XmlTextWriter(stringWriter))
                    {
                        database.Save(writer);
                        fullXml = stringWriter.ToString();
                    }
                }
            }
        }
    }
}