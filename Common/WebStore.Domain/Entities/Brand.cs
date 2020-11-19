using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Table("ProductBrands")]
    public /*sealed*/ class Brand : NamedEntity, IOrderedEntity // sealed - нельзя! Иначе это свяжет руки EF!
    {
        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
