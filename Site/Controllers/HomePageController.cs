using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.DataAnnotations;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using Site.Business.Categories;
using Site.Models.Categories;
using Site.Models.Pages;
using Site.Business.Categories;

namespace Site.Controllers
{
    public class HomePageController : PageController<HomePage>
    {
        public ActionResult Index(HomePage currentPage)
        {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var pages = contentRepository.GetChildren<ArticlePage>(ContentReference.StartPage, new[] {typeof (JeansCategory)});

            return View(currentPage);
        }
    }
}