using Microsoft.AspNetCore.Mvc;
using EasyOverTime.Models;
using System.Diagnostics;
using EasyOverTime.Models.Form;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using EasyOverTime.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EasyOverTime.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardLogicController : Controller
    {
        private IConfiguration? _configuration;

		private ApplicationDbContext Context { get; }

        public DashboardLogicController(ApplicationDbContext _context, IConfiguration configuration)
        {
            this.Context = _context;
            _configuration = configuration;
		}

		[HttpPost]
        [Route("insertovertimedata")]
        //[ValidateAntiForgeryToken]
        public IActionResult OvertimeDataInsert([Bind] OvertimeFormModel Data)
        {
            try
            {
                var Name = Data.Name;
				var Branch = Data.Branch;
				var Position = Data.Position;
				var DateFiled = Data.DateFiled;
                var IdNumber = Data.IdNumber;

				if (ModelState.IsValid)
                {
                    if (IdNumber != 0)
                    {
                        Console.WriteLine(Data);
                        this.Context.Entry(Data).State = EntityState.Added;
                        this.Context.OvertimeForms.AddAsync(Data);
                        this.Context.SaveChanges();

                        var result = Json(new
                        {
                            idnumber = IdNumber,
                            name = Name,
                            branch = Branch,
                            position = Position,
                            datefiled = DateFiled,
                            message = "success",
                            status = "sucess"
                        });


                        return result;
                    }
                }

                var ErrorResult = Json(new
                {               
                    status = "Request not successful!"
                });
                return ErrorResult;

            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        [HttpPost]
        [Route("formdata")]
        public object OvertimeFormInsert(List<OvertimeData> Datas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //IList<OvertimeData> Data = new List<OvertimeData>()
                    //{
						
                    //     new OvertimeData{ IdNumber=Datas.IdNumber, Date="2020/4/23", TimeStart="5:00", TimeEnd="6:00", TotalNumberOfHours=1, Reason="as"},
                    //     new OvertimeData{ IdNumber=Datas.IdNumber, Date="2020/5/23", TimeStart="5:00", TimeEnd="6:00", TotalNumberOfHours=1, Reason="as"}

                    //};

                    if(Datas == null)
					{
                        Datas = new List<OvertimeData>();
					}
					else
					{
                        foreach (var Data in Datas)
                        {
                            this.Context.OvertimeDatas.AddRange(Data);

                        }
                        this.Context.SaveChanges();

                        var result = Json(new
                        {
                            message = "Successfully Added",
                            status = "sucess"
                        });

                        return result;
                    }      

                }

                return View();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
