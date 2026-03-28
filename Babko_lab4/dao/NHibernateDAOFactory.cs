using System.Reflection;
using System.Text.Json;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using ISession = NHibernate.ISession;

namespace Babko_lab4.dao;

public class NHibernateDAOFactory : DAOFactory
{
    private static ISessionFactory factory;

    private static DAOFactory instance;

    private ISession session;

    private IGoodsDAO goodsDAO;

    public NHibernateDAOFactory(ISession session)
    {
        this.session = session;
    }

    public static DAOFactory getInstance()
    {
        if (null == instance)
        {
            string jsonString = File.ReadAllText("dbsettings.json");
            var settings = JsonSerializer.Deserialize<DbConfig>(jsonString);

            ISession session = openSession(
                settings.Host, 
                settings.Port, 
                settings.Database, 
                settings.Username, 
                settings.Password
            );
            instance = new NHibernateDAOFactory(session);
        }
        return instance;
    }

    public override IGoodsDAO GetGoodsDAO()
    {
        if (null == goodsDAO)
        {
            goodsDAO = new GoodsDAO(session);
        }
        return goodsDAO;
    }

    public override void Destroy()
    {
        session.Close();
    }

    private static ISession openSession(string host, int port,
        string database, string user, string password)
    {
        ISession session = null;
        Assembly mappingAssembly = Assembly.GetExecutingAssembly();
        if (null == factory)
        {
            factory = Fluently.Configure()
                .Database(PostgreSQLConfiguration
                    .PostgreSQL82.ConnectionString(c => c
                        .Host(host)
                        .Port(port)
                        .Database(database)
                        .Username(user)
                        .Password(password)))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(mappingAssembly))
                .ExposeConfiguration(config =>
                {
                    config.SetProperty(NHibernate.Cfg.Environment.UseProxyValidator, "false");
                    BuildSchema(config);
                })
                .BuildSessionFactory();
        }
        session = factory.OpenSession();
        return session;
    }

    private static void BuildSchema(Configuration config)
    {
        new SchemaUpdate(config).Execute(false, true);
    }

}
