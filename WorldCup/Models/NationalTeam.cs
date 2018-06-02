using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldCup.Models
{
    public class NationalTeam
    {
        
        [Key]
        public int NationalTeamId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Continent { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Information { get; set; }

        public List<Player> Players { get; set; }
    }
}