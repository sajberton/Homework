using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace WorldCup.Models
{
    public class Match
    {
       
        [Key]
        public int MatchId { get; set; }

       
        public NationalTeam Team1 { get; set; }
        [NotMapped]
        public int Team1Id { get; set; }

        public NationalTeam Team2 { get; set; }
        [NotMapped]
        public int Team2Id { get; set; }

        [Required]
        [DisplayName("Match Date")]
        [DataType(DataType.Date)]
        public DateTime MatchDateTime { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Information { get; set; }
     
    }

    
}