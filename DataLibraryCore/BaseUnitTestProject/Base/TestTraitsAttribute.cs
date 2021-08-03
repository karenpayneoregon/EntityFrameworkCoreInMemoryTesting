using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseUnitTestProject.Base
{
    public enum Trait
    {
        /// <summary>
        /// Place holder 
        /// </summary>
        InMemoryTesting,
        /// <summary>
        /// Entity Framework Core test
        /// </summary>
        ReadDataTesting,
        ReadDataLike,
        InMemoryTesting_CRUD,
        NoInMemoryTesting,
        JsonCreate,
        InMemoryTestingRelations,
        InMemoryTestingCreateCustomer
    }
    /// <summary>
    /// Declarative class for using Trait enum about for traits on test method.
    /// </summary>
    public class TestTraitsAttribute : TestCategoryBaseAttribute
    {
        private readonly Trait[] _traits;

        public TestTraitsAttribute(params Trait[] traits)
        {
            _traits = traits;
        }

        public override IList<string> TestCategories => _traits.Select(trait => Enum.GetName(typeof(Trait), trait)).ToList();
    }

}