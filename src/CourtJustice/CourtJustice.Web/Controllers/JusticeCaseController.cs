using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using CourtJustice.Web.Requests;
using Inventor.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace CourtJustice.Web.Controllers
{
    public class JusticeCaseController : BaseController<JusticeCaseController>
    {
        private readonly IJusticeCaseRepository _justiceCaseRepository;
        private readonly ILoaneeRepository _loaneeRepository;
        private readonly ICourtRepository _courtRepository;
        private readonly ICaseResultRepository _caseResultRepository;
        private readonly ILawyerRepository _lawyerRepository;
        private readonly IJusticeAppointmentRepository _justiceAppointmentRepository;
        private readonly IJusticeCaseLawyerRepository _justiceCaseLawyerRepository;


        public JusticeCaseController(IJusticeCaseRepository justiceCaseRepository,
            ILoaneeRepository loaneeRepository,
            ICourtRepository courtRepository,
            ICaseResultRepository caseResultRepository,
            IJusticeAppointmentRepository justiceAppointmentRepository,
            ILawyerRepository lawyerRepository,
            IJusticeCaseLawyerRepository justiceCaseLawyerRepository)
        {
            _justiceCaseRepository = justiceCaseRepository;
            _loaneeRepository = loaneeRepository;
            _courtRepository = courtRepository;
            _caseResultRepository = caseResultRepository;
            _justiceAppointmentRepository = justiceAppointmentRepository;
            _lawyerRepository = lawyerRepository;
            _justiceCaseLawyerRepository = justiceCaseLawyerRepository;
        }


     

        private async Task<List<JusticeCaseViewModel>> GetAll()
        {
            var results = await _justiceCaseRepository.GetAll();
            return results.ToList();
        }

        public async Task<IActionResult> Index()
        {

            var justiceCases = await _justiceCaseRepository.GetAll();

            // var justiceCase = await _justiceCaseRepository.GetByCusId("18225-00214-2");
            // if (justiceCase == null)
            // {
            //  return RedirectToAction(nameof(Create), new { cusId = model.CusId });
            // }
            List<SelectListItem> selects = new();
            var courts = await _courtRepository.GetAll();
            courts.Insert(0, new Court { CourtId = "0", CourtName = "แสดงทั้งหมด" });

            foreach (var item in courts)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.CourtName.ToString(),
                    Value = item.CourtId.ToString(),
                });
            }
            ViewBag.Courts = selects;

            selects = new();
            var caseResults = await _caseResultRepository.GetAll();
            caseResults.Insert(0, new CaseResult { CaseResultId = 0, CaseResultName = "แสดงทั้งหมด" });

            foreach (var item in caseResults)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.CaseResultName.ToString(),
                    Value = item.CaseResultId.ToString(),
                });
            }
            ViewBag.CaseResults = selects;

           
           return View();
        }

      

        public async Task<IActionResult> Create(string id)
        {
            var loanee = await _loaneeRepository.GetByKey(id);
            var justiceCase = new JusticeCaseViewModel
            {
                CusId = loanee.CusId,
                CusName = loanee.Name
            };


            await GetViewBag();

            return View(justiceCase);
        }

        public IActionResult AppointmentConfig(string id)
        {
            var model = new JusticeAppointmentViewModel
            {
                BlackCaseNo = id
            };
            return View(model);
        }

        private async Task GetViewBag()
        {
            var courts = await _courtRepository.GetAll();
            List<SelectListItem> selects = new();
            foreach (var item in courts)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.CourtName.ToString(),
                    Value = item.CourtId.ToString(),
                });
            }
            ViewBag.Courts = selects;


            var caseResults = await _caseResultRepository.GetAll();
            selects = new();
            foreach (var item in caseResults)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.CaseResultName.ToString(),
                    Value = item.CaseResultId.ToString(),
                });
            }
            ViewBag.CaseResults = selects;

            var lawyers = await _lawyerRepository.GetAll();
            selects = new();
            foreach (var item in lawyers)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.LawyerName.ToString(),
                    Value = item.LawyerId.ToString(),
                });
            }
            ViewBag.Lawyers = selects;
        }

        [HttpPost]
        public async Task<JsonResult> AppointmentConfig([FromBody] JusticeCaseRequest request)
        {
            try
            {
                var culture = new CultureInfo("en-US");
                DateTimeStyles styles = DateTimeStyles.None;
                //2=พิพากษา ให้เอา appointment_date ล่าสุดมาใส่ วันพิพากษา
                //3ทำยอม ให้เอา appointment_date ล่าสุดมาใส่ วันพิพากษา
                //4=ถอนฟ้อง  ให้เอา appointment_date ล่าสุดมาใส่ วันพิพากษา


                var ss = Utils.DateTimeConverter.ConvertDateTime(request.CaseDate);

                var appointment = await _justiceAppointmentRepository.GetLastByCaseNo(request.BlackCaseNo);
                switch (request.CaseResultId)
                {
                    case 2:

                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
                var justiceCase = new JusticeCase();
                justiceCase.BlackCaseNo = request.BlackCaseNo;
                if (!string.IsNullOrEmpty(request.CaseDate))
                {
                    DateTime caseDate = Utils.DateTimeConverter.ConvertDateTime(request.CaseDate);
                    //DateTime.TryParse(request.CaseDate, culture, styles, out caseDate);
                    justiceCase.CaseDate = caseDate.ToDateOnly();
                }
                if (!string.IsNullOrEmpty(request.ApprovalDate))
                {
                    DateTime approvalDate = Utils.DateTimeConverter.ConvertDateTime(request.ApprovalDate);
                    //DateTime.TryParse(request.ApprovalDate, culture, styles, out approvalDate);
                    justiceCase.ApprovalDate = approvalDate.ToDateOnly();
                }

                if (!string.IsNullOrEmpty(request.JudgmentDate))
                {
                    DateTime judgmentDate = Utils.DateTimeConverter.ConvertDateTime(request.JudgmentDate);
                    justiceCase.JudgmentDate = judgmentDate.ToDateOnly();

                }
                justiceCase.AssetAmount = request.AssetAmount;
                justiceCase.CaseDocumentResult = request.CaseDocumentResult;
                justiceCase.FeeCase = request.FeeCase;

                if (!string.IsNullOrEmpty(request.SubmissionDate))
                {
                    DateTime submissionDate = Utils.DateTimeConverter.ConvertDateTime(request.SubmissionDate);
                    justiceCase.SubmissionDate = submissionDate.ToDateOnly();
                }

                justiceCase.SubmissionResult = request.SubmissionResult;
                if (!string.IsNullOrEmpty(request.CommitDate))
                {
                    DateTime commitDate = Utils.DateTimeConverter.ConvertDateTime(request.CommitDate);
                    justiceCase.CommitDate = commitDate.ToDateOnly();
                }
                if (!string.IsNullOrEmpty(request.PostingDate))
                {
                    DateTime postingDate = Utils.DateTimeConverter.ConvertDateTime(request.PostingDate);
                    justiceCase.PostingDate = postingDate.ToDateOnly();
                }

                justiceCase.CaseResultId = request.CaseResultId;
                justiceCase.CourtId = request.CourtId;
                justiceCase.CusId = request.CusId;
                await _justiceCaseRepository.Create(justiceCase);



                foreach (var item in request.JusticeLawyers)
                {
                    var justiceLawyer = new JusticeCaseLawyer
                    {
                        LawyerId = item.LawyerId,
                        BlackCaseNo = request.BlackCaseNo
                    };
                    await _justiceCaseLawyerRepository.Create(justiceLawyer);
                }

                foreach (var item in request.JusticeAppointments)
                {
                    DateTime appointmentDate = Utils.DateTimeConverter.ConvertDateTime(item.AppointmentDate);
                    //DateTime.TryParse(item.AppointmentDate, culture, styles, out appointmentDate);
                    var justiceAppointment = new JusticeAppointment();
                    justiceAppointment.AppointmentDate = appointmentDate.ToDateOnly();
                    justiceAppointment.Remark = item.Remark;
                    justiceAppointment.BlackCaseNo = request.BlackCaseNo;
                    await _justiceAppointmentRepository.Create(justiceAppointment);
                }
                return new JsonResult(new { isValid = true, html = "", returnUrl = Url.Action("Index", "JusticeCase", new { id = request.CusId }) });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message, });
            }
        }


        [HttpPost]
        public async Task<IActionResult> GetWithPaging()
        {
            try
            {

               
                var countId = Request.Form["countId"].FirstOrDefault();
                var caseResultId = Request.Form["caseResultId"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                countId = countId == "0" ? "" : countId;
                int recordsTotal = await _justiceCaseRepository.GetRecordCount(countId, caseResultId.ToInt16(), searchValue!);
                var data = await _justiceCaseRepository.GetPaging(countId, caseResultId.ToInt16(), skip, pageSize, searchValue);
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch
            {
                throw;
            }
        }
    }
}
