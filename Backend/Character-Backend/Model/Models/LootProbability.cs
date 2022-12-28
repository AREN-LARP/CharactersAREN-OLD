using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    public class LootProbability
    {
        [Key]
        public int Id { get; set; }
        

        [ForeignKey("ItemGroupId")]
        public virtual ItemGroup ItemGroup { get; set; }

        public decimal Probability { get; set; }

        public int MinAmount { get; set; }

        public int MaxAmount { get; set; }
    }
}
