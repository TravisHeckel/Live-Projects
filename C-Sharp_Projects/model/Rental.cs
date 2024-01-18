using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheatreCMS3.Areas.Rent.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }
        public string RentalName { get; set; }
        public int RentalCost { get; set; }
        public string FlawsAndDamages { get; set; }
        public string RentalType { get; set; }
        public virtual RentalEquipment RentalEquipment { get; set; }
        public virtual RentalRoom RentalRoom { get; set; }

    }

    public class RentalEquipment
    {
        public bool? ChokingHazard { get; set; }
        public bool? SuffocationHazard { get; set; }
        public int? PurchasingPrice { get; set; }

    }

    public class RentalRoom
    {
        public int? RoomNumber { get; set; }
        public int? SquareFootage { get; set; }
        public int? MaxOccupancy { get; set; }

    }
}