using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace EasyOverTime.Models.Form
{
    public class OvertimeFormModel
    {
        public int IdNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Position is required")]
        public string? Position { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Branch or division is required")]
        public string? Branch { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required")]
        public string? DateFiled { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify your overtime")]
        public string? Specification { get; set; }


        //[Key]
        //[ForeignKey("OvertimeData")]
        //public int? OvertimeDataId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual ICollection<OvertimeData> OvertimeData { get; set; }

        [Required]
        public int? Total { get; set; }

        public OvertimeFormModel()
        {
            this.OvertimeData = new HashSet<OvertimeData>();
        }
    }
}
