using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using orm.Domain;
using orm.Dao;

namespace orm.test.Dao;

[TestClass]
public class TestGoodsDAO : TestGenericDao<Goods>
{
    protected override IGenericDAO<Goods> GetDaoInstance()
    {
        return NHibernateDAOFactory.getInstance().GetGoodsDAO();
    }

    protected override List<Goods> GenerateEntities()
    {
        return new List<Goods>
        {
            new Goods { Name = "Apples", Category = "Fruits", Price = 33.28M, Unit = "kg", Quantity = 20},
            new Goods { Name = "Bread", Category = "Bakery", Price = 15.00M, Unit = "loaf", Quantity = 50},
            new Goods { Name =  "Bananas", Category = "Fruits", Price = 25.56M, Unit = "kg", Quantity = 17},
            new Goods { Name = "Milk", Category = "Dairy", Price = 12.75M, Unit = "liter", Quantity = 30}
        };
    }

    protected override void ModifyEntityForUpdate(Goods entity)
    {
        entity.Price = 29.99M;
    }
    protected override void AssertEntitiesEqual(Goods expected, Goods actual)
    {
        Assert.AreEqual(expected.Name, actual.Name, "Назва товару не співпадає");
        Assert.AreEqual(expected.Category, actual.Category, "Категорія товару не співпадає");
        Assert.AreEqual(expected.Price, actual.Price, "Ціна товару не співпадає");
        Assert.AreEqual(expected.Unit, actual.Unit, "Одиниця виміру товару не співпадає");
        Assert.AreEqual(expected.Quantity, actual.Quantity, "Кількість товару не співпадає");
    }

    [TestMethod]
    public void TestSearchByNativeSql()
    {
        var goodsDao = Dao as IGoodsDAO;
        Assert.IsNotNull(goodsDao, "Dao не реалізує IGoodsDAO");

        string searchTerm = "ruit";

        var results = goodsDao.SearchByNativeSql(searchTerm);

        Assert.IsNotNull(results, "Результат пошуку не повинен бути null");
        Assert.HasCount(2, results, "Очікується 2 товари у категорії");

        foreach (var item in results)
        {
            Assert.IsTrue(item.Category.Contains("ruit"), $"Категорія '{item.Category}' не містить '{searchTerm}'");
        }
    }

}
