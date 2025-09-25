using System.Diagnostics;
using System.Net;
using BURN_SOCIETY.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BurnSociety.Application;

namespace BURN.Controllers
{
    public class HomeController : Controller
    {

        public ApplicationDBContext DBContext;
        public KeysSecret keysecrets = new KeysSecret("cmMXe59RhL4dKudhbmPLv6TEoe/gAzGw", "L86HB3X5M7uJ1ZamD8F3JhY5qLQ=");
        public float ammountToPay = 0;

        public HomeController(ApplicationDBContext DBcontext)
        {
            DBContext = DBcontext;

        }

        public IActionResult AboutUS()
        {
            return View();
        }
        public string GetToken()
        {
            string data = JsonConvert.SerializeObject(keysecrets);
            var url = "https://pay.pesapal.com/v3/api/Auth/RequestToken";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Accept", "application/json");

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(result);

                // Extract the token
                string token = jsonResponse?.token;

                // Get redirect_url from JustPay
                return token;
            }
        }


        [HttpGet]
        [HttpGet]
        public IActionResult PaymentsReceived(string orderTrackingId, string orderMerchantReference)
        {
     
            //IPNResponses x = new IPNResponses();

            //string data = JsonConvert.SerializeObject(keysecrets);
            //var url = "https://pay.pesapal.com/v3/api/Transactions/GetTransactionStatus?orderTrackingId=" + OrderTrackingId;

            //string token = GetToken();
            //// To convert to JSON string (if needed)
            //var request = (HttpWebRequest)WebRequest.Create(url);
            //request.Method = "GET";
            //request.ContentType = "application/json";
            //request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("Authorization", "Bearer " + token);

            //var response = (HttpWebResponse)request.GetResponse();
            //using (var streamReader = new StreamReader(response.GetResponseStream()))
            //{
            //    var result = streamReader.ReadToEnd();

            //    var jsonObject = JsonConvert.DeserializeObject<JObject>(result);
            //    PaymentResponse checkExistance = DBContext.PaymentResponse.FirstOrDefault(X => X.merchant_reference == OrderMerchantReference);



            //    if (checkExistance == null)
            //    {

            //        checkExistance.payment_method = jsonObject["payment_method"]?.ToString();
            //        checkExistance.amount = jsonObject["amount"]?.ToObject<double>() ?? 0;
            //        checkExistance.created_date = jsonObject["created_date"]?.ToObject<DateTime>() ?? DateTime.UtcNow;
            //        checkExistance.confirmation_code = jsonObject["confirmation_code"]?.ToString();
            //        checkExistance.order_tracking_id = jsonObject["order_tracking_id"]?.ToString();
            //        checkExistance.payment_status_description = jsonObject["payment_status_description"]?.ToString();
            //        checkExistance.description = jsonObject["description"]?.ToString();
            //        checkExistance.message = jsonObject["message"]?.ToString();
            //        checkExistance.payment_account = jsonObject["payment_account"]?.ToString();
            //        checkExistance.call_back_url = jsonObject["call_back_url"]?.ToString();
            //        checkExistance.status_code = jsonObject["status_code"]?.ToObject<int>() ?? 0;
            //        checkExistance.merchant_reference = jsonObject["merchant_reference"]?.ToString();
            //        checkExistance.account_number = jsonObject["account_number"]?.ToString();
            //        checkExistance.payment_status_code = jsonObject["payment_status_code"]?.ToString();
            //        checkExistance.currency = jsonObject["currency"]?.ToString();
            //        checkExistance.status = jsonObject["status"]?.ToString();
            //        checkExistance.error_type = jsonObject["error"]?["error_type"]?.ToString();
            //        checkExistance.code = jsonObject["error"]?["code"]?.ToString();
            //        DBContext.Add(checkExistance);
            //        DBContext.SaveChanges();
            //    }
            //    else
            //    {
            //        PaymentResponse update = DBContext.PaymentResponse.FirstOrDefault(j => j.merchant_reference == OrderMerchantReference);
            //        update.status = jsonObject["payment_status_description"]?.ToString();
            //        update.payment_method = jsonObject["payment_method"]?.ToString();
            //        update.amount = jsonObject["amount"]?.ToObject<double>() ?? 0;
            //        update.created_date = jsonObject["created_date"]?.ToObject<DateTime>() ?? DateTime.UtcNow;
            //        update.confirmation_code = jsonObject["confirmation_code"]?.ToString();
            //        update.order_tracking_id = jsonObject["order_tracking_id"]?.ToString();
            //        update.payment_status_description = jsonObject["payment_status_description"]?.ToString();
            //        update.description = jsonObject["description"]?.ToString();
            //        update.message = jsonObject["message"]?.ToString();
            //        update.payment_account = jsonObject["payment_account"]?.ToString();
            //        update.call_back_url = jsonObject["call_back_url"]?.ToString();
            //        update.status_code = jsonObject["status_code"]?.ToObject<int>() ?? 0;
            //        update.merchant_reference = jsonObject["merchant_reference"]?.ToString();
            //        update.account_number = jsonObject["account_number"]?.ToString();
            //        update.payment_status_code = jsonObject["payment_status_code"]?.ToString();
            //        update.currency = jsonObject["currency"]?.ToString();
            //        update.status = jsonObject["status"]?.ToString();
            //        update.error_type = jsonObject["error"]?["error_type"]?.ToString();
            //        update.code = jsonObject["error"]?["code"]?.ToString();
            //        DBContext.SaveChanges();
            //    }

            //    Registrations registrations = DBContext.Registrations.FirstOrDefault(j => j.ReffCode == OrderMerchantReference);
            //    //registrations.ammount = double.Parse(jsonObject["amount"]?.ToObject<double>() ?? 0);
            //    registrations.Payment_Status = jsonObject["payment_status_description"]?.ToString();
            //    registrations.Status_code = jsonObject["status_code"].ToString();
            //    await DBContext.SaveChangesAsync();


            //    x.OrderTrackingId = OrderTrackingId;
            //    x.OrderNotificationType = OrderNotificationType;
            //    x.OrderMerchantReference = OrderMerchantReference;
            //    DBContext.Add(x);
            //    await DBContext.SaveChangesAsync();


            return View();
            



            
        }   
        
        public IActionResult Registrations()
        {
            return View();
        }  
        
        public IActionResult dashboard()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
            public IActionResult Events()
        {
            return View();
        }     public IActionResult Gallery()
        {
            return View();
        }     public IActionResult HomePage()
        {
            return View();
        }  public IActionResult MasterPage()
        {
            return View();
        }  public IActionResult Media()
        {
            return View();
        }  public IActionResult MemberShip()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }  
        public IActionResult Services()
        {
            return View();
        }
          public IActionResult Upload()
        {
            return View();
        }   
          
            
           public IActionResult ConferenceInformation()
        {
             
            return View();
        }
        public IActionResult Abstracts()
        {
            var abstracts = DBContext.Files.ToList();
            ViewBag.abstracts = abstracts;
            return View(ViewBag.abstracts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
