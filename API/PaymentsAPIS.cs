using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BurnSociety.Application;
using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Runtime.InteropServices;
using Pesapal.APIHelper;
using BURN_SOCIETY.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BurnSociety.umbraco.custome_models;
using System.Net.Mail;

namespace KSPRAS.Controllers;

public class PesaPal : Controller
{
    public ApplicationDBContext DBContext;
    public KeysSecret keysecrets = new KeysSecret("cmMXe59RhL4dKudhbmPLv6TEoe/gAzGw", "L86HB3X5M7uJ1ZamD8F3JhY5qLQ=");
    public float ammountToPay =0;

    public PesaPal(ApplicationDBContext DBcontext)
    {
        DBContext = DBcontext;

    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] Registrations registrations)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Random random = new Random();
        string RandomRef = new string(Enumerable.Repeat(chars, 12)
                                   .Select(s => s[random.Next(s.Length)]).ToArray());

        try
        {
            // Set default values
        
            registrations.Ammount = GetConferenceFee(registrations.PaymentCategory,registrations.KenyanNurse);
            registrations.ReffCode = RandomRef;
            ammountToPay = GetConferenceFee(registrations.PaymentCategory,registrations.KenyanNurse);

            // Save to database
            DBContext.Registrations.Add(registrations);
            await DBContext.SaveChangesAsync();

            // Authenticate and Process Payment (returns only redirect_url)
            //var redirectUrl = await Authenticate(RandomRef, registrations);
            string htmlBody = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>Registration Successful</title>
  <style>
    body {{
      font-family: Arial, sans-serif;
      background-color:#f9f9f9;
      color:#333;
      margin:0;
      padding:0;
    }}
    .container {{
      max-width:650px;
      margin:30px auto;
      background:#fff;
      padding:25px;
      border-radius:12px;
      box-shadow:0 4px 12px rgba(0,0,0,0.08);
    }}
    h2 {{
      color:#ff2744;
      margin-bottom:10px;
    }}
    p {{
      margin:10px 0;
    }}
    table {{
      width:100%;
      border-collapse:collapse;
      margin-top:20px;
      border-radius:8px;
      overflow:hidden;
    }}
    table th, table td {{
      padding:12px;
      border:1px solid #eee;
      text-align:left;
    }}
    table th {{
      background-color:#ff2744;
      color:#fff;
      font-weight:600;
    }}
    table tr:nth-child(even) {{
      background-color:#fdf0f2;
    }}
    .footer {{
      margin-top:20px;
      font-size:13px;
      color:#666;
      text-align:center;
    }}
  </style>
</head>
<body>
  <div class='container'>
    <h2>✅ Registration Successful</h2>
    <p>A new registration has been submitted for the <strong>PABS Conference</strong>.  
       See details below:</p>

    <table>
      <tr><th>Bank payment Reference Code</th><td>{registrations.ReffCode}</td></tr>
      <tr><th>Full Name</th><td>{registrations.FName} {registrations.SName}</td></tr>
      <tr><th>Email Address</th><td>{registrations.EmailAddress}</td></tr>
      <tr><th>Phone</th><td>{registrations.TelephoneNumber}</td></tr>
      <tr><th>Institution</th><td>{registrations.Institution}</td></tr>
      <tr><th>Cadre</th><td>{registrations.Cadre}</td></tr>
      <tr><th>Category</th><td>{registrations.PaymentCategory}</td></tr>
      <tr><th>Amount</th><td>{registrations.Currency} {registrations.Ammount}</td></tr>
      <tr><th>Payment Status</th><td>Paid</td></tr>
      <tr><th>Payment Confirmation</th><td>{registrations.PaymentConfirmation}</td></tr>
    </table>

    <div class='footer'>
      <p>📩 If you have any questions, please reach out to the support team: burnsociety0@gmail.com</p>
      <p><strong style='color:#ff2744;'>We look forward to hosting a successful event!</strong></p>
    </div>
  </div>
</body>
</html>";


            try
            {
                // Define SMTP client
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587) // Replace with your SMTP server
                {
                    Credentials = new NetworkCredential("burnsociety0@gmail.com", "czamuizhvneqvvpu"), // Replace with your credentials
                    EnableSsl = true // Set to true if your server requires SSL/TLS
                };

                // Create email message
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("abstracts@panafricanburns.com"), // Replace with sender email
                    Subject = "New registration made:" + registrations.FName + " " + registrations.SName,

                    IsBodyHtml = true,
                    Body = htmlBody

                };

                // Add recipient
                //mail.To.Add("abstracts@panafricanburns.com"); // Replace with recipient email
                mail.ReplyToList.Add(new MailAddress(registrations.EmailAddress.ToString()));
                mail.CC.Add(new MailAddress("kulolazee@gmail.com"));
                mail.CC.Add(new MailAddress("bkimutai2021@gmail.com"));
                mail.CC.Add(new MailAddress("sogshaban@yahoo.com"));
                mail.CC.Add(new MailAddress("burnsociety0@gmail.com"));
                mail.CC.Add(new MailAddress("Dr.mastara@gmail.com"));
                mail.CC.Add(new MailAddress(registrations.EmailAddress));
                // Send email
                client.Send(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }

            return Json(new
            {
                success = true,
                message = "Abstract submitted successfully!",
                //data = redirectUrl
            });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                success = false,
                message = "An error occurred while submitting the abstract.",
                error=ex.ToString(),
                data = "N/A"
            });
        }
    }
    public static float GetConferenceFee(string category, bool isKenyanNurse)
    {
        // Define category-based pricing (flat rates)
        var pricing = new System.Collections.Generic.Dictionary<string, float>
    {
        { "Physician Consultant", 220f },                  // USD
        { "Physician Non-Consultant", 150f },              // USD
        { "East African Nurses and allied workers", 10000f }, // KSH
        { "Non-East African Nurses Paramedics & Others", 100f }, // USD
        { "Kenyan Consultants", 25000f },                  // KSH
        { "Kenyan Registrars and Medical Officers", 15000f } // KSH
    };

        category = category?.Trim();

        if (!pricing.ContainsKey(category))
        {
            throw new ArgumentException("Invalid category selected.");
        }

        float fee = pricing[category];

        // Special condition: deposit for Kenyan Nurses
        if (isKenyanNurse && category == "East African Nurses and allied workers")
        {
            // Assuming you still want the deposit rule:
            return fee * 0.5f; // 50% deposit
        }

        return fee;
    }

    //public static float GetConferenceFee(string category, bool isKenyanNurse)
    //{
    //    // Get today's date in real-time
    //    DateTime today = DateTime.Today;

    //    // Define pricing periods
    //    DateTime superEarlyBirdEnd = new DateTime(2025, 4, 30);
    //    DateTime earlyBirdEnd = new DateTime(2025, 7, 31);
    //    DateTime depositDeadline = new DateTime(2025, 8, 1);

    //    // Define category-based pricing (Super Early Bird, Early Bird, On-site)
    //    var pricing = new System.Collections.Generic.Dictionary<string, float[]>
    //{
    //    { "Physician Consultant", new float[] { 250, 280, 300 } },
    //    { "Physician Non-Consultant", new float[] { 200, 250, 280 } },
    //    { "Nurses&Paramedics(East African)", new float[] { 1, 1, 1 } }, // KSH
    //    { "Nurses&Paramedics(Non-EastAfrican)", new float[] { 100, 100, 100 } } // USD
    //};

    //    category = category?.Trim();

    //    if (!pricing.ContainsKey(category))
    //    {
    //        throw new ArgumentException("Invalid category selected.");
    //    }
    //    // Determine the correct pricing bracket based on today's date
    //    float fee;
    //    if (today <= superEarlyBirdEnd)
    //    {
    //        fee = pricing[category][0]; // Super Early Bird Price
    //    }
    //    else if (today <= earlyBirdEnd)
    //    {
    //        fee = pricing[category][1]; // Early Bird Price
    //    }
    //    else
    //    {
    //        fee = pricing[category][2]; // On-Site Price
    //    }

    //    // Apply 50% deposit condition for Kenyan nurses
    //    if (isKenyanNurse && category == "Nurses & Paramedics (East African)" && today <= superEarlyBirdEnd)
    //    {
    //        return fee * 1f; // 50% deposit allowed
    //    }

    //    return fee;
    //}

    public async Task<string> Authenticate(string Refference, Registrations abstractModel)
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
            return await JustPay(token, Refference, abstractModel);
        }
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
            return  token;
        }
    }


    [HttpGet]
    public async Task<string> InsertIPN(string OrderTrackingId, string OrderNotificationType, string OrderMerchantReference)
    {
        IPNResponses x = new IPNResponses();
        var findRef = DBContext.Registrations.FirstOrDefault(x => x.ReffCode == OrderMerchantReference);
        string data = JsonConvert.SerializeObject(keysecrets);
        var url = "https://pay.pesapal.com/v3/api/Transactions/GetTransactionStatus?orderTrackingId="+OrderTrackingId;

        string token = GetToken();
        // To convert to JSON string (if needed)
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", "Bearer " + token);

        var response = (HttpWebResponse)request.GetResponse();
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
         
            var jsonObject = JsonConvert.DeserializeObject<JObject>(result);
            PaymentResponse checkExistance = DBContext.PaymentResponse.FirstOrDefault(X => X.merchant_reference == OrderMerchantReference);



            if (checkExistance == null) {

                checkExistance.payment_method = jsonObject["payment_method"]?.ToString();
                    checkExistance.amount = jsonObject["amount"]?.ToObject<double>() ?? 0;
                    checkExistance.created_date = jsonObject["created_date"]?.ToObject<DateTime>() ?? DateTime.UtcNow;
                    checkExistance.confirmation_code = jsonObject["confirmation_code"]?.ToString();
                    checkExistance.order_tracking_id = jsonObject["order_tracking_id"]?.ToString();
                    checkExistance.payment_status_description = jsonObject["payment_status_description"]?.ToString();
                    checkExistance.description = jsonObject["description"]?.ToString();
                    checkExistance.message = jsonObject["message"]?.ToString();
                    checkExistance.payment_account = jsonObject["payment_account"]?.ToString();
                    checkExistance.call_back_url = jsonObject["call_back_url"]?.ToString();
                   checkExistance. status_code = jsonObject["status_code"]?.ToObject<int>() ?? 0;
                    checkExistance.merchant_reference = jsonObject["merchant_reference"]?.ToString();
                    checkExistance.account_number = jsonObject["account_number"]?.ToString();
                    checkExistance.payment_status_code = jsonObject["payment_status_code"]?.ToString();
                    checkExistance.currency = jsonObject["currency"]?.ToString();
                   checkExistance. status = jsonObject["status"]?.ToString();
                    checkExistance.error_type = jsonObject["error"]?["error_type"]?.ToString();
                    checkExistance.code = jsonObject["error"]?["code"]?.ToString();
                DBContext.Add(checkExistance);
                DBContext.SaveChanges();
            }
            else
            {
                PaymentResponse update = DBContext.PaymentResponse.FirstOrDefault(j => j.merchant_reference == OrderMerchantReference);
                update.status = jsonObject["payment_status_description"]?.ToString();
                update.payment_method = jsonObject["payment_method"]?.ToString();
                update.amount = jsonObject["amount"]?.ToObject<double>() ?? 0;
                update.created_date = jsonObject["created_date"]?.ToObject<DateTime>() ?? DateTime.UtcNow;
                update.confirmation_code = jsonObject["confirmation_code"]?.ToString();
                update.order_tracking_id = jsonObject["order_tracking_id"]?.ToString();
                update.payment_status_description = jsonObject["payment_status_description"]?.ToString();
                update.description = jsonObject["description"]?.ToString();
                update.message = jsonObject["message"]?.ToString();
                update.payment_account = jsonObject["payment_account"]?.ToString();
                update.call_back_url = jsonObject["call_back_url"]?.ToString();
                update.status_code = jsonObject["status_code"]?.ToObject<int>() ?? 0;
                update.merchant_reference = jsonObject["merchant_reference"]?.ToString();
                update.account_number = jsonObject["account_number"]?.ToString();
                update.payment_status_code = jsonObject["payment_status_code"]?.ToString();
                update.currency = jsonObject["currency"]?.ToString();
                update.status = jsonObject["status"]?.ToString();
                update.error_type = jsonObject["error"]?["error_type"]?.ToString();
                update.code = jsonObject["error"]?["code"]?.ToString();
                DBContext.SaveChanges();
            }

            Registrations registrations = DBContext.Registrations.FirstOrDefault(j => j.ReffCode == OrderMerchantReference);
            //registrations.ammount = double.Parse(jsonObject["amount"]?.ToObject<double>() ?? 0);
            registrations.Payment_Status = jsonObject["payment_status_description"]?.ToString();
            registrations.Status_code = jsonObject["status_code"].ToString();
            await DBContext.SaveChangesAsync();


            x.OrderTrackingId = OrderTrackingId;
            x.OrderNotificationType = OrderNotificationType;
            x.OrderMerchantReference = OrderMerchantReference;
            DBContext.Add(x);
            await DBContext.SaveChangesAsync();


          
        }


           
        return "Payment success";
    }



    

    public string RegisterIPN()
    {
        string data = JsonConvert.SerializeObject(keysecrets);
        var url = "https://cybqa.pesapal.com/pesapalv3/api/URLSetup/RegisterIPN";
        var jsonObject = new
        {
            url = "https://81a8-102-217-67-229.ngrok-free.app",
            ipn_notification_type = "GET"
        };

        // To convert to JSON string (if needed)
        string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("Accept", "application/json");
        string authToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoiZWQ2MTkwMGYtZGNiMy00NjM2LWIxNGUtY2U1MGQwYzk2M2I1IiwidWlkIjoicWtpbzFCR0dZQVhUdTJKT2ZtN1hTWE5ydW9ac3JxRVciLCJuYmYiOjE3NDEzNDE2MzgsImV4cCI6MTc0MTM0NTIzOCwiaWF0IjoxNzQxMzQxNjM4LCJpc3MiOiJodHRwOi8vY3licWEucGVzYXBhbC5jb20vIiwiYXVkIjoiaHR0cDovL2N5YnFhLnBlc2FwYWwuY29tLyJ9.eJzwc22YbPXSW9zEAlgcX74HwP3AOJRbJwF8lVrHrFo";
        request.Headers.Add("Authorization", "Bearer " + authToken);

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(jsonObject);
        }

        var response = (HttpWebResponse)request.GetResponse();
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            return result;

        }
    }


    [HttpGet]
    public async Task<string> JustPay(string token, string refference, Registrations registrations)
    {
        var url = "https://pay.pesapal.com/v3/api/Transactions/SubmitOrderRequest";

        var paymentRequest = new
        {
            id = refference,
            currency = registrations.Currency,
            amount = ammountToPay,
            description = "Registration for the BURNS Conference",
            callback_url = "https://panafricanburns.com/home/PaymentReceived/home/PaymentReceived",
            notification_id = "893b8b61-0a0a-4152-8644-dc090efecf97",
            billing_address = new
            {
                email_address = registrations.EmailAddress,
                phone_number = registrations.TelephoneNumber,
                country_code = registrations.Currency,
                first_name = registrations.FName,
                middle_name = "",
                last_name = registrations.SName,
                line_1 = "",
                line_2 = "",
                city = "",
                state = "",
                postal_code = "",
                zip_code = ""
            }
        };

        //ADD PAYMENT RESPONSE
        PaymentResponse paymentResponse = new PaymentResponse

        {
            payment_method = "",
            amount = ammountToPay,
            created_date = DateTime.Now,
            confirmation_code ="",
            order_tracking_id = "",
            payment_status_description = ammountToPay.ToString(),
            description = "Registration for the BURNS Conference",
           
            message = "",
            payment_account = "",
            call_back_url = "https://localhost:7209/home/registrations",
            status_code = 0,
            merchant_reference = refference,
            account_number = "",
            payment_status_code = "",
            currency = registrations.Currency,
            status = "",
            error_type = "",
            code = "",

        };
        DBContext.PaymentResponse.Add(paymentResponse);
        DBContext.SaveChanges();

        string jsonString = JsonConvert.SerializeObject(paymentRequest);
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", "Bearer " + token);

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(jsonString);
        }

        var response = (HttpWebResponse)request.GetResponse();
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

            // Deserialize to extract only redirect_url
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(result);
            string redirectUrl = jsonResponse?.redirect_url ?? "N/A";

            return redirectUrl;
        }
    }

}



