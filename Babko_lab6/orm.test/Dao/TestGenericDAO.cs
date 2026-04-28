using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using orm.Domain;
using orm.Dao;

namespace orm.test.Dao
{
    [TestClass]
    public abstract class TestGenericDao<T> where T : EntityBase
    {
        public TestContext TestContext { get; set; }

        protected IGenericDAO<T> Dao { get; set; }
        
        protected List<T> TestEntities { get; private set; } = new();

        [TestInitialize]
        public void Setup()
        {
            Dao = GetDaoInstance();
            Assert.IsNotNull(Dao, "Помилка: DAO не ініціалізовано.");

            TestEntities = GenerateEntities();
            Assert.IsTrue(TestEntities.Count >= 3, "Для повноцінного тестування потрібно хоча б 3 сутності.");

            for (int i = 0; i < TestEntities.Count; i++)
            {
                TestEntities[i] = Dao.Merge(TestEntities[i]);
                Assert.IsNotNull(TestEntities[i], $"Помилка збереження сутності під індексом {i}");
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            foreach (var entity in TestEntities)
            {
                var fromDb = Dao.GetById(entity.Id);
                if (fromDb != null)
                {
                    Dao.Delete(fromDb);
                }
            }
            TestEntities.Clear();
        }

        [TestMethod]
        public void TestMerge()
        {
            var entityToUpdate = TestEntities[0];
            ModifyEntityForUpdate(entityToUpdate);

            Dao.Merge(entityToUpdate);

            var updatedEntity = Dao.GetById(entityToUpdate.Id);
            Assert.IsNotNull(updatedEntity, "Оновлений об'єкт не знайдено в БД");
            AssertEntitiesEqual(entityToUpdate, updatedEntity);
        }

        [TestMethod]
        public void TestGetById()
        {
            var expectedEntity = TestEntities[1];
            
            var foundEntity = Dao.GetById(expectedEntity.Id);
            Assert.IsNotNull(foundEntity);
            AssertEntitiesEqual(expectedEntity, foundEntity);

            var notFound = Dao.GetById(long.MaxValue); 
            Assert.IsNull(notFound, "Метод повинен повертати null, якщо запис відсутній");
        }

        [TestMethod]
        public void TestGetAll()
        {
            var allEntities = Dao.GetAll();

            Assert.IsNotNull(allEntities);
            Assert.IsTrue(allEntities.Count >= TestEntities.Count);

            foreach (var testEntity in TestEntities)
            {
                Assert.IsTrue(allEntities.Any(e => e.Id == testEntity.Id), 
                    $"Сутність з Id {testEntity.Id} не знайдена серед усіх записів");
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            var entityToDelete = TestEntities[2];

            Dao.Delete(entityToDelete);

            var deletedEntity = Dao.GetById(entityToDelete.Id);
            Assert.IsNull(deletedEntity, "Об'єкт все ще існує в БД після видалення");

            var remainingEntity = Dao.GetById(TestEntities[0].Id);
            Assert.IsNotNull(remainingEntity, "Сусідні записи були випадково видалені");
        }

        protected abstract IGenericDAO<T> GetDaoInstance();
        protected abstract List<T> GenerateEntities();
        protected abstract void ModifyEntityForUpdate(T entity);
        protected abstract void AssertEntitiesEqual(T expected, T actual);
    }
}