using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerLead
{
    public class Vehicle
    {
        [Key]
        [Column("VehicleId")]
        public int Id { get; set; }

        public decimal MSRP { get; set; }
        public string StockNumber { get; set; }
        public string Color { get; set; }
        public DateTime? SellDate { get; set; }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifyDate { get; set; }

        public int ModelId { get; set; }
        public SupportedModel Model { get; set; }

        public int DealershipId { get; set; }
        public Dealership Dealership { get; set; }
    }
}


//--ModelId INT NOT NULL,
//-- 	MSRP MONEY NOT NULL,
//-- 	StockNumber VARCHAR(30) NOT NULL,
//-- 	Color VARCHAR(30) NOT NULL,
//-- 	DealershipId INT NOT NULL,
//-- 	SellDate DATETIME,
//-- 	CreateDate DATETIME NOT NULL DEFAULT(getdate()),
//--ModifyDate DATETIME,
//--CONSTRAINT FK_Vehicle_SupportedModel FOREIGN KEY (ModelId) REFERENCES SupportedModel,
//-- 	CONSTRAINT FK_Vehicle_Dealership FOREIGN KEY (DealershipId) REFERENCES Dealership