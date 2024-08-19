using BTL_Finance.Authrization;
using BTL_Finance.Models;
using BTL_Finance.Models.Identity;
using BTL_Finance.Repository.DeliveryNoteServices;
using BTL_Finance.Repository.PurchaseOrderServices;
using BTL_Finance.Repository.RequestServices;
using Business.Services.AuditTrailService;
using Business.Services.NotificationService;
using Data.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BTL_Finance.Controllers
{
    [CustomAuthorize( "Admin,InvoiceUser")]
    public class DeliveryNoteController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuditTrailService _auditTrail;
        private readonly IDeliveryNoteService _DeliveryNote_Service;
        private readonly INotificationService _notificationService;
        private readonly IRequestService Request_Service;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _attachmentPath;

       
        public DeliveryNoteController(UserManager<ApplicationUser> userManager,
            IAuditTrailService auditTrail,
            IConfiguration configuration,
            IDeliveryNoteService DeliveryNote_Service,
             IRequestService request_Service,
            INotificationService notificationService,
             RoleManager<IdentityRole> roleManager)
        {
            _attachmentPath = configuration["AttachmentPath"];
            _userManager = userManager;
            _auditTrail = auditTrail;
            _DeliveryNote_Service = DeliveryNote_Service;
            Request_Service = request_Service;
            _notificationService = notificationService;

            _roleManager = roleManager;
        }
        public  IActionResult IndexAsync()
        {
            var result =  _DeliveryNote_Service.GetAllDeliveryNote();
            return View(result);



        }
        [HttpGet]
        public async Task<IActionResult> AddDeliveryNote(Guid id)
        {
            ViewData["RequestId"] = id;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddDeliveryNote(DeliveryNote model, IFormFile file)
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(_attachmentPath, Path.GetFileName(file.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                model.AttachmentPath = filePath;
            }
            var request = await Request_Service.GetById(model.RequestFormId);
            if (request != null)
            {
                request.process = (int)Enums.EnumProcess.DeliveryNote;
                request.Status_Delivery_Note = true;
                await Request_Service.Update(request);
            }

            model.CreatedBy = Guid.Parse(user.Id);
            model.LastModifiedBy = Guid.Parse(user.Id);
            bool result = await _DeliveryNote_Service.Insert(model);
            if (result)
            {

                // we will notify the order sheet users OrderUser
                var List_User_ID = await GetUserIdsByRoleAsync("InvoiceUser");
                foreach (var item in List_User_ID)
                {
                    Notification n = await _notificationService.GetRequestNotifications(model.RequestFormId.ToString());
                    n.Title = "Adding DeliveryNote Order";
                    n.Description = "You should DeliveryNote sheet Now";
                    n.To = Guid.Parse(item);
                    n.RequestId = model.ID;
                    await _notificationService.Update(n);
                }



                if (user != null)
                {
                    AuditTrail audit = new AuditTrail();
                    audit.User = user.UserName;
                    audit.Source = user.Id;
                    audit.Type = "Adding DeliveryNote Order";
                    await _auditTrail.Insert(audit);



                }

                TempData["S_Message"] = "Your operation was successful!";
                return RedirectToAction(nameof(Index));

            }

            TempData["E_Message"] = "Your operation was Failed!";
            return View(model);
        }
        private async Task<List<string>> GetUserIdsByRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return null;
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return null;
            }



            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            return usersInRole.Select(user => user.Id).ToList();
        }
    }
}
