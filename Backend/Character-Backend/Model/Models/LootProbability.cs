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
        [Required]
        public virtual ItemGroup ItemGroup { get; set; }
        public int ItemGroupId { get; set; }

        public double Probability { get; set; }

        public int MinAmount { get; set; }

        public int MaxAmount { get; set; }
    }
}
