using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GoldSilver.Domain.Abstract;
using GoldSilver.Domain.Entities;
using GoldSilver.Domain.Concrete;

namespace GoldSilver.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {        
            private IKernel ninjectKernel;

            public NinjectControllerFactory()
            {
                ninjectKernel = new StandardKernel();
                AddBindings();
            }


            protected override IController GetControllerInstance(RequestContext requestContext,
            Type controllerType)
            {
                return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
            }

            private void AddBindings()
            {
                //Mock<IJewelryRepository> mock = new Mock<IJewelryRepository>();

                //Gemstone rubin = new Gemstone() { GemstoneId = 1, GemstoneName = "rubin"};
                //Gemstone diamant = new Gemstone() { GemstoneId = 2, GemstoneName = "diamnt"};

                //Material gold = new Material { MaterialId = 1, MaterialName= "Золото"};
                //Material silver = new Material {MaterialId = 2, MaterialName = "Срібло" };

                //mock.Setup(m => m.Jewelries).Returns(new List<Jewelry>
                //{
                //    new Jewelry { JewelryId = 1, Name = "Золотий кулончик", Category = "Кольє", Gemstone = rubin, Material = gold, Article = "111", Description = "Чудовий золотий кулончик з рубіновою вставкою", Fineness = 555, Weight = 10 } ,
                //    new Jewelry { JewelryId = 2, Name = "Срібний кулончик", Category = "Кольє", Gemstone = diamant, Material = silver, Article = "112", Description = "Чудовий срібний кулончик з діамантовою вставкою", Fineness = 999, Weight = 30 } ,
                //    new Jewelry { JewelryId = 3, Name = "Золотий браслет", Category = "Браслети", Gemstone = diamant, Material = gold, Article = "111", Description = "Чудовий золотий кулончик з рубіновою вставкою", Fineness = 555, Weight = 25 }

                //}.AsQueryable());

                //ninjectKernel.Bind<IJewelryRepository>().ToConstant(mock.Object);

                ninjectKernel.Bind<IJewelryRepository>().To<EFJeweleryRepository>();

            }
        }
    }