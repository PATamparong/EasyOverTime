using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyOverTime.Models.Form
{
	public class OvertimeData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		[Key]
		[ForeignKey("OvertimeData")]
		public int IdNumber { get; set; }

		public virtual OvertimeFormModel? OvertimeForm { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode= true, DataFormatString="{0:MM/dd/yyy}")]
        [Required(AllowEmptyStrings = false)]
        public string? Date { get; set; }

        //[DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:t}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "What time did you start?")]
        public string? TimeStart { get; set; }

        //[DataType(DataType.Time)]
        //[DisplayFormat( DataFormatString = "{0:t}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "What time did you end?")]
        public string? TimeEnd { get; set; }

        //[Required(AllowEmptyStrings = false)]
        public Nullable<int> TotalNumberOfHours { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please give your reasons")]
        public string? Reason { get; set; }
    }
}
