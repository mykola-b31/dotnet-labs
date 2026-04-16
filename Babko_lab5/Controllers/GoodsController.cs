using Microsoft.AspNetCore.Mvc;
using Babko_lab5.Domain;
using Babko_lab5.Dao;

namespace Babko_lab5.Contollers;

[ApiController]
[Route("goods")]
public class GoodsContoller : ControllerBase
{
    [HttpGet]
    [Consumes("application/json")]
    [Produces("application/json")]
    public IList<Goods> GetAllGoods()
    {
        IList<Goods> goods = NHibernateDAOFactory.getInstance().GetGoodsDAO().GetAll();
        return goods;
    }

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    public Goods AddGoods(Goods goods)
    {
        Goods resultGoods = NHibernateDAOFactory.getInstance().GetGoodsDAO().Merge(goods);
        return resultGoods;
    }

    [HttpPut]
    [Consumes("application/json")]
    [Produces("application/json")]
    public Goods UpdateGoods (Goods goods)
    {
        Goods resultGoods = NHibernateDAOFactory.getInstance().GetGoodsDAO().Merge(goods);
        return resultGoods;
    }

    [HttpDelete]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("{id}")]
    public IActionResult Delete (long id)
    {
        Goods goods = NHibernateDAOFactory.getInstance().GetGoodsDAO().GetById(id);
        if (goods != null)
        {
            NHibernateDAOFactory.getInstance().GetGoodsDAO().Delete(goods);
            return Ok(new { message = "Goods was successfully removed." });
        }
        else
        {
            return NotFound(new { message = "Nothing was removed. Goods not found." });
        }
    }

    [HttpGet]
    [Produces("application/xml")]
    [Route("search/criteria")]
    public IActionResult SearchByCriteria([FromHeader(Name = "search-query")] string? searchQuery)
    {
        string query = searchQuery ?? string.Empty;

        IList<Goods> result = NHibernateDAOFactory.getInstance().GetGoodsDAO().SearchByCriteria(query);

        return Ok(result);
    }
}
