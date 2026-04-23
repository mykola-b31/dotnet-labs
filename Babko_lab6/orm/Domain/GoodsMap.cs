using FluentNHibernate.Mapping;

namespace orm.Domain;

public class GoodsMap : ClassMap<Goods>
{
    public GoodsMap()
    {
        Table("Goods");
        Id(x => x.Id).GeneratedBy.Native();
        Map(x => x.Name);
        Map(x => x.Category);
        Map(x => x.Price);
        Map(x => x.Quantity);
        Map(x => x.Unit);
    }
}
