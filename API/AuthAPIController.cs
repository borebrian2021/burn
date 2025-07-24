using BurnSociety.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using BURN_SOCIETY.Models;
using Microsoft.AspNetCore.Authorization;
using BurnSociety.umbraco.custome_models;
using Microsoft.AspNetCore.Cors;
using System.Net.Mail;
using System.Net;
using System.Numerics;
namespace BURN_SOCIETY.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public RegisterController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Register
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Register>>> GetRegister()
        {
            return await _context.Register.ToListAsync();
        }

        // GET: api/Register/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Register>> GetRegister(int id)
        {
            var Register = await _context.Register.FindAsync(id);

            if (Register == null)
            {
                return NotFound();
            }

            return Register;
        }
        //[HttpPost]
        //public async Task<ActionResult<Register>> PostRegister(Register register)
        //{
        //    var test = register.FullNames;
        //    // At this point, the 'register' parameter will have the values from the JSON payload
        //    _context.Register.Add(register);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetRegister), new { id = register.Id }, register);
        //}

       
        // PUT: api/Register/5
        [HttpPut("{id}")]
        [Route("/api/register/")]
        public async Task<IActionResult> PutRegister(int id, Register Register)
        { 
            if (id != Register.Id)
            {
                return BadRequest();
            }
            var checkExists = _context.Register.FirstOrDefault(x=>x.Email==Register.Email);
            if(checkExists != null)
            {
                return Ok(new
                {
                    message = "User with this email already exists kindly log in to proceed!",
                    status = "warning",
                    success=false
                });
            }
            _context.Entry(Register).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new
            {
                message ="Registration successful!",
                status= "success",
                success=true
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/login")]
        public IActionResult LogIn(Register users)
        {
            string pass = users.Password;
            TokenProvider TokenProviderr = new TokenProvider(_context);
            string userToken = TokenProviderr.LoginUser(users.Email.ToString(), users.Password);
            if (userToken == null)
            {

                return BadRequest(new
                {
                    status = false,
                    message = "Invalid username or password."
                });
            }
            HttpContext.Session.SetString("JWToken", userToken);
            Register user_id = _context.Register.Where( x => x.Email == users.Email).SingleOrDefault();
           
            
            return Ok(new
            {
                status = "success",
                message = "Login successful!",
                data = new { userId = user_id }
            });


        }  
        
        [AllowAnonymous]
        [HttpPost]
        [Route("/api/upload/")]
        [Consumes("application/json")]
        [HttpOptions]
        [Obsolete]
        public async Task<IActionResult> Uploaod(Files files)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*"); // Allow all origins
            Response.Headers.Add("Access-Control-Allow-Methods", "POST"); // Allow POST and OPTIONS
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            string pass = files.Email;
             _context.Files.Add(files);
            _context.SaveChanges();
            //sendMail();
            string htmlBody = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>User Details</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .email-container {{
            max-width: 600px;
            margin: 20px auto;
           
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }}
.email-button {{
           background-color:#ff2744; 
border-radius:8px; 
padding:10px;
color:white;
margin-15px;
        }}
        .email-header {{
            background-color: #ff2744;
            color: #ffffff;
            padding: 20px;
            text-align: center;
        }}
        .email-header h1 {{
            margin: 0;
            font-size: 24px;
            color:white;

          
        }}
        .email-body {{
            padding: 20px;
            color: #333333;
        }}
        .details-table {{
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }}
        .details-table th, .details-table td {{
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #dddddd;
        }}
        .details-table th {{
            background-color: #f8f9fa;
            font-weight: bold;
        }}
        .details-table td {{
            background-color: #ffffff;
        }}
        .email-footer {{
            background-color: #f8f9fa;
            padding: 10px;
            text-align: center;
            font-size: 12px;
            color: #666666;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='email-header'>
            <h1>Abstract Details</h1>
        </div>
        <div class='email-body'>
            <table class='details-table'>
                <tr>
                    <th><b>Field</b></th>
                    <th><b>Details</b></th>
                </tr>
                <tr>
                    <td><b>Email</b></td>
                    <td>{files.Email}</td>
                </tr>
                <tr>
                    <td><b>First Name</b></td>
                    <td>{files.FirstName}</td>
                </tr>
                <tr>
                    <td><b>Last Name</b></td>
                    <td>{files.LastName}</td>
                </tr>
                <tr>
                    <td><b>Institution</b></td>
                    <td>{files.Institution}</td>
                </tr>
                <tr>
                    <td><b>Abstract</b></td>
                    <td>{files.Abstract}</td>
                </tr>
                <tr>
                    <td>Phone</td>
                    <td>{files.Phone}</td>
                </tr>
            </table>
        </div>
        <div class='email-footer'>
<a href='www.panafricanburns.com'  class='email-button'>View All Submissions</a>
            <p>This is an automated email. Please do not reply.</p>
        </div>
    </div>
</body>
</html>";
            try
                {
                    // Define SMTP client
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587) // Replace with your SMTP server
                    {
                        Credentials = new NetworkCredential("bkimutai2021@gmail.com", "kvsehgumrdbydgsz"), // Replace with your credentials
                        EnableSsl = true // Set to true if your server requires SSL/TLS
                    };

                // Create email message
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("abstracts@panafricanburns.com"), // Replace with sender email
                    Subject = "New Abstract Uploaded:" + files.FirstName +" "+ files.LastName,

                    IsBodyHtml = true,
                    Body = htmlBody

                };

                    // Add recipient
                    mail.To.Add("abstracts@panafricanburns.com"); // Replace with recipient email
                    mail.ReplyToList.Add(new MailAddress(files.Email.ToString()));
                    mail.CC.Add(new MailAddress("kulolazee@gmail.com"));
                    mail.CC.Add(new MailAddress("bkimutai2021@gmail.com"));
                    mail.CC.Add(new MailAddress("sogshaban@yahoo.com"));
                // Send email
                client.Send(mail);
                    Console.WriteLine("Email sent successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email. Error: " + ex.Message);
                }

            //lblStatus.Text = "Sent email (" + txtSubject.Text + ") to " + txtTo.Text;


            return Ok(new
            {
                status = "success",
                message = "Login successful!",
                data = new { userId = files }
            });


        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/api/contactus")]
        public IActionResult SendContactMail(ContactUs contactus)
        {

            return Ok(new
            {
                status = contactus.Email,
                
            });

        }




        // DELETE: api/Register/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegister(int id)
        {
            var Register = await _context.Register.FindAsync(id);
            if (Register == null)
            {
                return NotFound();
            }

            _context.Register.Remove(Register);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        public void sendMail()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            string server = "mail.panafricanburns.com"; // replace it with your own domain name
            int port = 25; // alternative port is 8889
            bool enableSsl = false; // false for port 25 or 8889, true for port 465
            string from = "abstracts@panafricanburns.com"; // from address and SMTP username
            string password = "Pabs2687??"; // replace it with your real password
            string to = "bkimutai2021@gmail.com"; // recipient address

            var message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress("abstracts@panafricanburns.com", from)); // replace from_name with real name
            message.To.Add(new MimeKit.MailboxAddress("bkimutai2021@gmail.com", to)); // replace to_name with real name
            message.Subject = "This is an email";
            message.Body = new MimeKit.TextPart("plain")
            {
                Text = @"This is from MailKit.Net.Smtp using C sharp with SMTP authentication."
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(server, port, enableSsl);
                client.Authenticate(from, password);
                client.Send(message);
                client.Disconnect(true);
            }


        }
        private bool RegisterExists(int id)
        {
            return _context.Register.Any(e => e.Id == id);
        }
    }
}
