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
using GoldSilver.WebUI.Infrastructure.Abstract;
using GoldSilver.WebUI.Infrastructure.Concrete;
using GoldSilver.WebUI.Models;

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
            ninjectKernel.Bind<IJewelryRepository>().To<EFJeweleryRepository>();
            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            ninjectKernel.Bind<ICurrencyExangeService>().To<CurrencyExchangeService>();
            ninjectKernel.Bind<IAppSettings>().To<AppSettings>().InSingletonScope();
        }
    }
}