using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string IcName { get; set; }
        [Required]
        public Status Status { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Required]
        public int UserId { get; set; }

        [ForeignKey("CareerId")]
        public virtual Career Career { get; set; }
        public int? CareerId { get; set; }

        public string Backstory { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
