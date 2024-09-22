using System.Net;
using System.Net.Mail;

namespace MVC_3PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email input)
        {
            var client=new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("grothfire.ms@gmail.com", "pdskgdqjlhuolzca");
            client.Send("grothfire.ms@gmail.com",input.To,input.Subject,input.Body);


        }
    }
}
