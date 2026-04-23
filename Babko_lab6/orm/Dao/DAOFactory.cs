namespace orm.Dao;

abstract public class DAOFactory
{
    public abstract IGoodsDAO GetGoodsDAO();

    public abstract void Destroy();
}
