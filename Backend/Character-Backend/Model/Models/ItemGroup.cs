using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Model.Models
{
    public class ItemGroup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }
        [Required]
        public int Speed { get; set; }
        public virtual ICollection<Item> Items { get; set; }

        private Random r = null;

        private Random GetRandom()
        {
            if (r == null)
            {
                r = new Random();
            }
            return r;
        }

        public Item GenerateItem()
        {
            int value = GetRandom().Next(Items.Sum(i => i.Weight));
            IEnumerator<Item> i = Items.GetEnumerator();
            i.MoveNext();
            while (value >= i.Current.Weight)
            {
                value -= i.Current.Weight;
                i.MoveNext();
            }

            return i.Current;
        }
    }
}
