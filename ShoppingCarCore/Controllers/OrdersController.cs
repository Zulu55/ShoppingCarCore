using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCarCore.Data;
using ShoppingCarCore.Data.Entities;
using ShoppingCarCore.Models;

namespace ShoppingCarCore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IRepository repository;

        public OrdersController(IRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult IncreaseQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailTmp = this.repository.GetOrderDetailTmp(id.Value);
            if (orderDetailTmp == null)
            {
                return NotFound();
            }

            this.repository.ModifyOrderDetailTmp(id.Value, 1);
            this.repository.SaveAllAsync().Wait();
            return RedirectToAction("Create");
        }

        public IActionResult DecraseQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailTmp = this.repository.GetOrderDetailTmp(id.Value);
            if (orderDetailTmp == null)
            {
                return NotFound();
            }

            this.repository.ModifyOrderDetailTmp(id.Value, -1);
            this.repository.SaveAllAsync().Wait();
            return RedirectToAction("Create");
        }

        public IActionResult DeleteDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailTmp = this.repository.GetOrderDetailTmp(id.Value);
            if (orderDetailTmp == null)
            {
                return NotFound();
            }

            this.repository.DeleteOrderDetailTmp(id.Value);
            this.repository.SaveAllAsync().Wait();
            return RedirectToAction("Create");
        }

        public IActionResult Index()
        {
            return View(this.repository.GetOrders());
        }

        public IActionResult Create()
        {
            return View(this.repository.GetOrderDetailTmps());
        }

        [HttpPost]
        public IActionResult Create(List<OrderDetailTmp> model)
        {
            if (ModelState.IsValid)
            {
                this.repository.SaveOrder();
                this.repository.SaveAllAsync().Wait();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult AddProduct()
        {
            var model = new OrderDetailViewModel
            {
                Quantity = 1,
                Products = this.GetProductsList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(OrderDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddProductToOrder(model);
                await this.repository.SaveAllAsync();
                return RedirectToAction("Create");
            }

            return View(model);
        }

        private IEnumerable<SelectListItem> GetProductsList()
        {
            var products = this.repository.GetProducts().ToList();
            products.Insert(0, new Product
            {
                Id = 0,
                Name = "(Select a product...)"
            });

            return products.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();
        }
    }
}