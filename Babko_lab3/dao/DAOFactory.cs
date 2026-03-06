namespace Babko_lab3.dao;

abstract public class DAOFactory
{
    public abstract IGoodsDAO GetGoodsDAO();

    public abstract IOrderDAO GetOrderDAO();

    public abstract IOrderItemDAO GetOrderItemDAO();

    public abstract void Destroy();
}
