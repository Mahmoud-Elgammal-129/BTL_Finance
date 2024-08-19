using Api.Hubs;
using BTL_Finance.Authrization;
using BTL_Finance.Models;
using BTL_Finance.Models.Identity;
using BTL_Finance.Repository.RequestServices;
using Business.Services.AuditTrailService;
using Business.Services.NotificationService;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BTL_Finance.Controllers
{
    
    public class RequestController : Controller
    {
        //Remeber in the add and insert and delete will add audit 

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuditTrailService _auditTrail;
        private readonly IRequestService Request_Service;
        private readonly INotificationService _notificationService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _attachmentPath;
        
        //private readonly IHubContext<NotificationsHub> notificationsHub;
        public RequestController(UserManager<ApplicationUser> userManager,
            IAuditTrailService auditTrail,
            IConfiguration configuration, 
            IRequestService request_Service, 
            INotificationService notificationService, 
             RoleManager<IdentityRole> roleManager)
        {
            _attachmentPath = configuration["AttachmentPath"];
            _userManager = userManager;
            _auditTrail = auditTrail;
            Request_Service = request_Service;
            _notificationService = notificationService;
            
            _roleManager = roleManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var result= await Request_Service.GetAll();


            return View(result);


            
        }
        //[CustomAuthorize("Admin")]
        [HttpGet]
        public IActionResult AddRequest()
        {
            return View();
        }
        public IActionResult Details(string requestName)
        {
            if (string.IsNullOrEmpty(requestName))
            {
                return BadRequest("Request name is required.");
            }
            var request = Request_Service.GetRequestDetailsByName(requestName);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }
        //[CustomAuthorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRequest(Request model, IFormFile file)
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
            model.process = (int)Enums.EnumProcess.Request;
            model.Status_Request = true;
            model.CreatedBy = Guid.Parse( user.Id);
            model.LastModifiedBy = Guid.Parse(user.Id);
            bool result= await Request_Service.Insert(model);
            if (result)
            {

                // we will notify the order sheet users OrderUser
                var List_User_ID = await GetUserIdsByRoleAsync("OrderUser");
                foreach (var item in List_User_ID)
                {
                    Notification n = new Notification();
                    n.Title = "Adding Order Sheet";
                    n.Description = "You should add order sheet Now";
                    n.To = Guid.Parse(item);
                    n.RequestId = model.ID;
                   await _notificationService.Insert(n);
                }

               

                if (user != null)
                {
                    AuditTrail audit = new AuditTrail();
                    audit.User = user.UserName;
                    audit.Source = user.Id;
                    audit.Type = "Adding Request";
                    await _auditTrail.Insert(audit);


                }
                TempData["S_Message"] = "Your operation was successful!";
                return RedirectToAction(nameof(Index));

            }
            TempData["E_Message"] = "Your operation was Failed!";

            return View(model);
        }
        public IActionResult ServeFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var mimeType = GetMimeType(filePath);
            var fileName = Path.GetFileName(filePath);

            Response.Headers.Add("Content-Disposition", $"inline; filename={fileName}");

            return File(fileBytes, mimeType);

        }

        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".txt" => "text/plain",
                ".csv" => "text/csv",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".html" => "text/html",
                ".xml" => "application/xml",
                _ => "application/octet-stream",
            };
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
