using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EasyOverTime.Models.Form
{
	public class OvertimeDataAndForm
	{
		public OvertimeFormModel? OvertimeForms { get; set; }
		public List<OvertimeData>? OvertimeDatas { get; set; }
	}
}
