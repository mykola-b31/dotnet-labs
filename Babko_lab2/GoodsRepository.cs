using System.Data;
using System.Data.Common;
using System.IO;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Babko_lab2;

public class GoodsRepository : IGoodsRepository
{
    private GoodsRepository() 
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        _configuration = builder.Build();
    }

    private NpgsqlConnection _connection;
    private static IGoodsRepository _instance;
    private readonly IConfiguration _configuration;

    public static IGoodsRepository GetInstance()
    {
        if (null == _instance)
        {
            _instance = new GoodsRepository();
        }

        return _instance;
    }

    private NpgsqlConnection GetConnection()
    {
        if (null == _connection)
        {
            string connectionStr = _configuration.GetConnectionString("DefaultConnection");
            
            _connection = new NpgsqlConnection(connectionStr);
            _connection.Open();
        }

        return _connection;
    }

    private void AddParameterToCommand(DbCommand command, string parameterName, DbType parameterType,
        object parameterValue)
    {
        NpgsqlParameter parameter = new NpgsqlParameter();
        parameter.ParameterName = parameterName;
        parameter.DbType = parameterType;
        parameter.Value = parameterValue;
        command.Parameters.Add(parameter);
    }

    public void Save(Goods goods)
    {
        DbCommand command = NpgsqlFactory.Instance.CreateCommand();
        command.Connection = GetConnection();
        command.CommandText =
            "insert into goods(name, category, price, unit, quantity) values(@name, @category, @price, @unit, @quantity)";
        AddParameterToCommand(command, "@name", DbType.String, goods.Name);
        AddParameterToCommand(command, "@category", DbType.String, goods.Category);
        AddParameterToCommand(command, "@price", DbType.Decimal, goods.Price);
        AddParameterToCommand(command, "@unit", DbType.String, goods.Unit);
        AddParameterToCommand(command, "@quantity", DbType.Int32, goods.Quantity);
        command.ExecuteNonQuery();
    }

    public void Update(Goods goods)
    {
        DbCommand command = NpgsqlFactory.Instance.CreateCommand();
        command.Connection = GetConnection();
        command.CommandText =
            "update goods set name = @name, category = @category, price = @price, unit = @unit, quantity = @quantity where id = @id";
        AddParameterToCommand(command, "@id", DbType.Int64, goods.Id);
        AddParameterToCommand(command, "@name", DbType.String, goods.Name);
        AddParameterToCommand(command, "@category", DbType.String, goods.Category);
        AddParameterToCommand(command, "@price", DbType.Decimal, goods.Price);
        AddParameterToCommand(command, "@unit", DbType.String, goods.Unit);
        AddParameterToCommand(command, "@quantity", DbType.Int32, goods.Quantity);
        command.ExecuteNonQuery();
    }

    public IList<Goods> GetAll()
    {
        IList<Goods> goodsList = new List<Goods>();
        DbCommand command = NpgsqlFactory.Instance.CreateCommand();
        command.Connection = GetConnection();
        command.CommandText = "SELECT * FROM goods ORDER BY id";
        DbDataReader row = command.ExecuteReader();
        while (row.Read())
        {
            long goodsId = (long)row["id"];
            string name = (String)row["name"];
            string category = (String)row["category"];
            decimal price = (decimal)row["price"];
            string unit = (String)row["unit"];
            int quantity = (int)row["quantity"];
            Goods goods = new Goods();
            goods.Id = goodsId;
            goods.Name = name;
            goods.Category = category;
            goods.Price = price;
            goods.Unit = unit;
            goods.Quantity = quantity;
            goodsList.Add(goods);
        }
        row.Close();
        return goodsList;
    }

    public Goods FindById(long id)
    {
        DbCommand command = NpgsqlFactory.Instance.CreateCommand();
        command.Connection = GetConnection();
        command.CommandText = "SELECT * FROM goods WHERE id = @id";
        AddParameterToCommand(command, "@id", DbType.Int64, id);
        DbDataReader row = command.ExecuteReader();
        Goods goods = null;
        while (row.Read())
        {
            long goodsId = (long)row["id"];
            string name = (String)row["name"];
            string category = (String)row["category"];
            decimal price = (decimal)row["price"];
            string unit = (String)row["unit"];
            int quantity = (int)row["quantity"];
            goods.Name = name;
            goods.Category = category;
            goods.Price = price;
            goods.Unit = unit;
            goods.Quantity = quantity;
        }
        row.Close();
        return goods;
    }

    public void Delete(long id)
    {
        DbCommand command = NpgsqlFactory.Instance.CreateCommand();
        command.Connection = GetConnection();
        command.CommandText = "delete from goods where id = @id";
        AddParameterToCommand(command, "@id", DbType.Int64, id);
        command.ExecuteNonQuery();
    }

    public void Destroy()
    {
        GetConnection().Close();
    }
}