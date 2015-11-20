﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using Domain.Entities.Dictionaries;
using WebUI.Controllers.API;

namespace UnitTests.Dictionaries
{
    [TestClass]
    public class MaterialsTests : BaseDictionaryTests<Materials>
    {
        public MaterialsTests()
            : base()
        {
            // get Mock Repository from base class
            var moq = base.CreateMockRepository();

            // create controller with Mock
            var controller = new MaterialsController(moq);

            // Init params of controller
            base.ArrangeController(controller);
        }


        [TestMethod]
        public void Materials_Get_All()
        {
            base.GetAll();
        }

        [TestMethod]
        public void Materials_Get_By_Id()
        {
            base.GetById();
        }

        [TestMethod]
        public void Materials_Can_Insert()
        {
            base.Insert();
        }

        [TestMethod]
        public void Materials_Can_Edit()
        {
            base.Edit();
        }

        [TestMethod]
        public void Materials_Can_Remove()
        {
            base.Remove();
        }
    }
}
