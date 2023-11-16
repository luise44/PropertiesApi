using Castle.Core.Logging;
using Properties.Data;
using Properties.Data.Entities;
using Properties.Data.Repositories.Repositories;
using Properties.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Test.RepositoriesTests
{
    [TestClass]
    public class PropertiesRepositoryTest
    {
        private List<Property> properties;
        private PropertiesDbContext propertiesDbContext;

        [TestInitialize]
        public void Setup()
        {
            properties = new List<Property>
            {
                new Property
                {
                    Address = "Fake ST 1",
                    CodeInternal = "001",
                    Id = 1,
                    Name = "Property 1"
                },
                new Property
                {
                    Address = "Fake ST 2",
                    CodeInternal = "002",
                    Id = 2,
                    Name = "Property 2"
                }
            };

            propertiesDbContext = DataHelper.GetDbContext<Property>(properties, x => x.Set<Property>());
        }

        [TestMethod]
        public void Find_Properties_Filtered_Not_Empty()
        {
            var expectedCount = 2;

            var propertyRepository = new PropertyRepository(propertiesDbContext);

            var filters = new
            {
                Name = "Property 2",
                Address = "Fake ST 1"
            };

            var filteredProperties = propertyRepository.GetPropertiesFiltered(filters.Name, filters.Address, null, null);
            
            Assert.AreEqual(expectedCount, filteredProperties.Count);
        }
    }
}
