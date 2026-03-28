using Microsoft.AspNetCore.Mvc;
using Babko_lab4.domain;
using Babko_lab4.dao;

namespace Babko_lab4.Controllers;

public class GoodsController : Controller
{
    public IActionResult GetAll()
    {
        List<Goods> goods = NHibernateDAOFactory.getInstance().GetGoodsDAO().GetAll();
        return View(goods);
    }

    [HttpPost]
    public IActionResult Add(
        [Bind("Name, Category, Price, Unit, Quantity")] Goods goods
    )
    {
        NHibernateDAOFactory.getInstance().GetGoodsDAO().SaveOrUpdate(goods);
        return RedirectToAction("GetAll");
    }

    [Route("EditForm/{id}")]
    public IActionResult EditForm(long id)
    {
        Goods goods = NHibernateDAOFactory.getInstance().GetGoodsDAO().GetById(id);
        return View(goods);
    }

    [HttpPost]
    public IActionResult Edit(
        [Bind("Id, Name, Category, Price, Unit, Quantity")] Goods goods
    )
    {
        Goods goodsToUpdate = NHibernateDAOFactory.getInstance().GetGoodsDAO().GetById(goods.Id);
        goodsToUpdate.Name = goods.Name;
        goodsToUpdate.Category = goods.Category;
        goodsToUpdate.Price = goods.Price;
        goodsToUpdate.Unit = goods.Unit;
        goodsToUpdate.Quantity = goods.Quantity;
        NHibernateDAOFactory.getInstance().GetGoodsDAO().SaveOrUpdate(goodsToUpdate);
        return RedirectToAction("GetAll");
    }

    [Route("Delete/{id}")]
    public IActionResult Delete(long id)
    {
        Goods goods = NHibernateDAOFactory.getInstance().GetGoodsDAO().GetById(id);
        NHibernateDAOFactory.getInstance().GetGoodsDAO().Delete(goods);
        return RedirectToAction("GetAll");
    }
}
