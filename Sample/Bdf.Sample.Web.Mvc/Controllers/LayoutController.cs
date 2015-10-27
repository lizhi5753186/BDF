using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Sample.Application.Dtos.Paged;

namespace Bdf.Sample.Web.Mvc.Controllers
{
    public class LayoutController : BaseController
    {
        #region Shared Layout Partial view Actions

        public ActionResult _LoginPartial()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (var client = new HttpClient())
                {
                    ViewBag.ShoppingCartItems = client.GetAsync("").Result;
                }
            }
            return PartialView();
        }

        public ActionResult CategoriesPartial()
        {
            using (var client = new HttpClient())
            {
                var categories = client.GetAsync("").Result;
                return PartialView(categories);
            }
        }

        public ActionResult NewProductsPartial()
        {
            using (var client = new HttpClient())
            {
                var newProducts = client.GetAsync("").Result;
                return PartialView(newProducts);
            }
        }

        public ActionResult ProductsPartial(string categoryId = null, bool? fromIndexPage = null, int pageNumber = 1)
        {
            using (var client = new HttpClient())
            {
                var numberOfProductsPerPage = int.Parse(ConfigurationManager.AppSettings["productsPerPage"]);
                var pagination = new Pagination { PageSize = numberOfProductsPerPage, PageNumber = pageNumber };
                ProductDtoWithPagination productsDtoWithPagination = null;

                //productsDtoWithPagination = string.IsNullOrEmpty((categoryId)) ?
                //    client.GetAsync("").Result:
                //    client.GetAsync("").Result;

                if (string.IsNullOrEmpty(categoryId))
                    ViewBag.CategoryName = "所有商品";
                else
                {
                    var category = client.GetAsync("").Result;
                    //ViewBag.CategoryName = category.Name;
                }

                ViewBag.CategoryId = categoryId;
                ViewBag.FromIndexPage = fromIndexPage;
                if (fromIndexPage == null || fromIndexPage.Value)
                    ViewBag.Action = "Index";
                else
                    ViewBag.Action = "Category";
                ViewBag.IsFirstPage = productsDtoWithPagination.Pagination.PageNumber == 1;
                ViewBag.IsLastPage = productsDtoWithPagination.Pagination.PageNumber == productsDtoWithPagination.Pagination.TotalPages;
                return PartialView(productsDtoWithPagination);
            }
        }
        #endregion 
    }
}