using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Model.Models
{
    public class CompositeGenerate
    {
        public int TimeSpent { get; set; }
        public Skill Skill { get; set; }
        public List<Skill> Buffs { get; set; }
    }
}
