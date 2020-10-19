namespace BattleCards.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    { 
        [HttpGet("/")]
        public HttpResponse IndexSlash()
        {
            return this.Index();
        }

        public HttpResponse Index()
        {
            //if (this.IsUserLoggedIn())
            //{
            //    var allProducts = productsService.GetAll();

            //    return this.View(allProducts, "Home");
            //}

            return this.View();
        }
    }
}