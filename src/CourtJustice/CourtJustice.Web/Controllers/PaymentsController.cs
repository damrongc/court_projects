using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Utils;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;

namespace CourtJustice.Web.Controllers
{
    public class PaymentsController : BaseController<PaymentsController>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILoaneeRepository _loaneeRepository;
        private readonly IReceiptSummaryRepository _receiptSummaryRepository;
        public PaymentsController(IPaymentRepository paymentRepository, ILoaneeRepository loaneeRepository, IReceiptSummaryRepository receiptSummaryRepository)
        {
            _paymentRepository = paymentRepository;
            _loaneeRepository = loaneeRepository;
            _receiptSummaryRepository = receiptSummaryRepository;
        }

        [HttpGet]        
        public async Task<IActionResult> AddOrEdit(int id)
        {
            if (id == 0)
            {            
                return View(new Payment { PaymentDate = DateOnly.FromDateTime(DateTime.Today)});
            }
            else
            {
                var payment= await _paymentRepository.GetByKey(id);
                return View(payment);
            }
        }
        
        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] Payment model)
        {
            var loanee = await _loaneeRepository.GetByKey(model.CusId);
            var isExisting = _paymentRepository.IsExisting(model.PaymentId);
            if (isExisting)
            {
                await _paymentRepository.Update(model.PaymentId, model);
            }
            else
            {
                var seq = await _paymentRepository.GetPaymentSeq(model.CusId);
                model.EmployerCode= loanee.EmployerCode;
                model.PaymentSeq = seq;
                await _paymentRepository.Create(model);
            }

            //var receipt = new ReceiptSummary();
            //var receiptMonth = model.PaymentDate.Month;
            //var receiptYear = model.PaymentDate.Year;

            //receipt.EmployerCode = loanee.EmployerCode;
            //receipt.CollectorId = loanee.EmployeeCode;
            //receipt.ReceiptMonth = receiptMonth;
            //receipt.ReceiptYear = receiptYear;
            //receipt.TotalAmont = model.Amount;
            //receipt.Fee = model.Fee;
            //receipt.CreatedDatetime = DateTime.Now;

            //await _receiptSummaryRepository.AddOrUpdateReceipt(receipt);

            var payments = await _paymentRepository.GetByCusId(model.CusId);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_PaymentCard", payments);
            return new JsonResult(new { isValid = true, html });
        }

        [HttpGet]
        public async Task<JsonResult> GetByCusId(string id = "")
        {
            var payment = await _paymentRepository.GetByCusId(id);

            for (int i = 0; i < payment.Count; i++)
            {
                payment[i].PaymentSeq = i + 1;
            }
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_PaymentCard", payment);
            return new JsonResult(new { isValid = true, message = "", html });
        }
     

        [HttpDelete, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirmed(int id,string cusId)
        {
            try
            {
                var payment =await _paymentRepository.GetByKey(id);
                var loanee = await _loaneeRepository.GetByKey(cusId);
                await _paymentRepository.Delete(id);

                //var receipt = new ReceiptSummary();
                //var receiptMonth = payment.PaymentDate.Month;
                //var receiptYear = payment.PaymentDate.Year;

                //receipt.EmployerCode = payment.EmployerCode;
                //receipt.CollectorId = loanee.EmployeeCode;
                //receipt.ReceiptMonth = receiptMonth;
                //receipt.ReceiptYear = receiptYear;
                //receipt.TotalAmont = payment.Amount;
                //receipt.Fee = payment.Fee;
                //await _receiptSummaryRepository.AddOrUpdateReceipt(receipt);

                var payments = await _paymentRepository.GetByCusId(cusId);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_PaymentCard", payments);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });
            }
        }


        [HttpGet]
        public IActionResult IndexImport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmImport(IList<IFormFile> files)
        {
            var rowCount = 0;
            var rowIndex = 0;
            Program.Progress = 0;
            List<PaymentExcelViewModel> payments = new();
            var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            try
            {
                if (files[0]?.Length != 0)
                {
                    var stream = files[0].OpenReadStream();
                    using var reader = ExcelReaderFactory.CreateReader(stream);
                    var result = reader.AsDataSet();
                    var dt = result.Tables[0];
                    var messsage = "";
                    dt.Rows[0].Delete();
                    dt.AcceptChanges();

                    var compDt = dt.DefaultView.ToTable();
                    int columnCount = compDt.Columns.Count;
                    for (int i = 0; i < compDt.Rows.Count; i++)
                    {
                        var cusId = dt.Rows[i][2].ToString().Trim();
                        var loanee = await _loaneeRepository.GetByKey(cusId);
                        if (loanee == null)
                        {
                            throw new Exception("ไม่พบข้อมูลลูกหนี้ " + cusId);
                        }

                        var payment = new PaymentExcelViewModel
                        {
                            EmployerCode = loanee.EmployerCode,
                            AssignDate = dt.Rows[i][0].ToString().ConvertDateFormatTHToUS(),
                            ExpireDate = dt.Rows[i][1].ToString().ConvertDateFormatTHToUS(),
                            CusId = cusId,
                            NationalityId = dt.Rows[i][3].ToString().Trim(),
                            CusName = dt.Rows[i][4].ToString().Trim(),
                            ContractNo = dt.Rows[i][5].ToString().Trim(),
                            ReceiptDate = dt.Rows[i][6].ToString().ConvertDateFormatTHToUS(),
                            BookingDate = dt.Rows[i][7].ToString().ConvertDateFormatTHToUS(),
                            TotalReceived = dt.Rows[i][8].ToString().ToDecimal(),
                            WOBalance = dt.Rows[i][9].ToString().ToDecimal(),
                            StartOverdueStatus = dt.Rows[i][10].ToString().Trim(),
                            EndOverdueStatus = dt.Rows[i][11].ToString().Trim(),
                            UserCreated = loanee.EmployeeCode,
                        };
                        payments.Add(payment);
                        rowIndex += 1;
                        Program.Progress = (int)((float)rowIndex / (float)compDt.Rows.Count * 100.0);
                        await Task.Delay(10); // It is only to make the process slower
                    }
                    await _paymentRepository.BulkInsertOrUpdate(payments);

                    //foreach (var item in payments)
                    //{
                    //    var receipt = new ReceiptSummary();
                    //    var receiptMonth = item.ReceiptDate.Month;
                    //    var receiptYear = item.ReceiptDate.Year;

                    //    receipt.EmployerCode = item.EmployerCode;
                    //    receipt.CollectorId = item.UserCreated;
                    //    receipt.ReceiptMonth= receiptMonth;
                    //    receipt.ReceiptYear= receiptYear;
                    //    receipt.TotalAmont = item.TotalReceived;
                    //    receipt.Fee = 0;
                    //    receipt.CreatedDatetime= DateTime.Now;

                    //    await _receiptSummaryRepository.AddOrUpdateReceipt(receipt);
                    //}
                }
                var message = $"Import ข้อมูลการชำระ จำนวน {payments.Count} ข้อมูลเรียบร้อยแล้ว.";
                _notify.Success(message);
                ViewBag.Message = message;
                return Json(new { isvalid = true , message });
            }
            catch (Exception err)
            {
                string msgError = err.Message;
                Program.Progress = (int)(rowIndex / rowCount * 100.0);
                return Json(new { isvalid = false, message = msgError });
            }
        }

        [HttpPost]
        public IActionResult Progress()
        {
            return this.Content(Program.Progress.ToString());
        }
    }
}

