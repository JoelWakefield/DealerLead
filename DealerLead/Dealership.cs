using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerLead
{
    public class Dealership
    {
        [Key]
        [Column("DealershipId")]
        public int Id { get; set; }

        [Column("DealershipName")]
        public string Name { get; set; }

        [Column("StreetAddress1")]
        public string Street1 { get; set; }
        
        [Column("StreetAddress2")]
        public string Street2 { get; set; }

        public string City { get; set; }

        [MaxLength(2)]
        public string State { get; set; }
        
        [MaxLength(10)]
        public string Zipcode { get; set; }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateDate { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime? ModifyDate { get; set; }
        
        public int CreatingUserId { get; set; }
    }
}
