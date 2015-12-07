﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities.Dictionaries;
using WebUI.Controllers.API;
using UnitTests.Infrastructure;

namespace UnitTests.Dictionaries
{
    [TestClass]
    public sealed class PensionTypeTests : BaseEntityUnitTest<PensionType>
    {
        public PensionTypeTests()
        {
            // get Mock repository from base class
            var mockStorage = new MockStorage<PensionType>();

            // get Mock Repository
            var moq = mockStorage.SetupAndReturnMock();

            // create controller with Mock
            var controller = new PensionTypeController(moq);

            // Init params of controller
            ArrangeController(controller);
        }


        [TestMethod]
        public void PensionType_Get_All()
        {
            GetAll();
        }

        [TestMethod]
        public void PensionType_Get_By_Id()
        {
            GetById();
        }

        [TestMethod]
        public void PensionType_Can_Insert()
        {
            Insert();
        }

        [TestMethod]
        public void PensionType_Can_Edit()
        {
            Edit();
        }

        [TestMethod]
        public void PensionType_Can_Remove()
        {
            Remove();
        }
    }
}
