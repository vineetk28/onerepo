using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MantiScanServices.Model.Users;

namespace MantiScanServices.Model.Master
{
    public class Tower : ModelBase
    {
        [Key]
        public int TowerId { get; set; }
        public string TowerName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
