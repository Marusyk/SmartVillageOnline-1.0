﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities.Dictionaries;
using WebUI.Controllers.API;
using UnitTests.Infrastructure;

namespace UnitTests.Dictionaries
{
    [TestClass]
    public class DocumentTypeTests : BaseEntityUnitTest<DocumentType>
    {
        public DocumentTypeTests()
        {
            // get Mock Repository from base class
            var mockStorage = new MockStorage<DocumentType>();

            // get Mock Repository
            var moq = mockStorage.SetupAndReturnMock();

            // create controller with Mock
            var controller = new DocumentTypeController(moq);

            // Init params of controller
            base.ArrangeController(controller);
        }


        [TestMethod]
        public void DocumentType_Get_All()
        {
            base.GetAll();
        }

        [TestMethod]
        public void DocumentType_Get_By_Id()
        {
            base.GetById();
        }

        [TestMethod]
        public void DocumentType_Can_Insert()
        {
            base.Insert();
        }

        [TestMethod]
        public void DocumentType_Can_Edit()
        {
            base.Edit();
        }

        [TestMethod]
        public void DocumentType_Can_Remove()
        {
            base.Remove();
        }
    }
}
