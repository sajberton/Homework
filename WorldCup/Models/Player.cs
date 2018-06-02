using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldCup.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Position { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Info { get; set; }

        public NationalTeam Team { get; set; }
        [NotMapped]
        public int TeamId { get; set; }
    }
}