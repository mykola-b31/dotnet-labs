namespace Babko_lab2;

public interface IGoodsRepository
{
    public void Save(Goods goods);
    public void Update(Goods goods);
    public IList<Goods> GetAll();
    public Goods FindById(long id);
    public void Delete(long id);
    public void Destroy();
}