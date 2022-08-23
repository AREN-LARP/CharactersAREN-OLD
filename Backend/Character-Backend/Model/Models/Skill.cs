using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Level { get; set; }
        [ForeignKey("SkillCategoryId")]
        public virtual SkillCategory SkillCategory { get; set; }
        [Required]
        public int SkillCategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<Character> Characters { get; set; }

    }
}
