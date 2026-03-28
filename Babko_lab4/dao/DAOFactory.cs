namespace Babko_lab4.dao;

abstract public class DAOFactory
{
    public abstract IGoodsDAO GetGoodsDAO();

    public abstract void Destroy();
}
