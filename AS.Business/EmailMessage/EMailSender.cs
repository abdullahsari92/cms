using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using AS.Core.Helpers;

namespace AS.Business.EmailMessage
{
    public class EMailSender : IEMailSender
    {
        protected readonly string _emailFrom;
        protected readonly string _emailFromPassword;
        protected readonly string _isUseEmail;

        public IConfiguration _configuration { get; }

        public EMailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _emailFrom = _configuration.GetSection("EmailSetting:email").Value ?? "";
            _emailFromPassword = _configuration.GetSection("EmailSetting:password").Value ?? "";
            _isUseEmail = _configuration.GetSection("EmailSetting:isUseEmail").Value ?? "";

        }


        public bool Sender(string kullaniciAdi, string sifre)
        {

            if (!_isUseEmail.ToBoolean()) return true;


            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress(_emailFrom);
            //
            ePosta.To.Add(kullaniciAdi);

            ePosta.Subject = "DPU IME Otomasyonu Hesabý";
            //
            //ePosta.Body = "<div>DPU IME Otomasyonunu Hesabý aþaðýdaki kullanýcý adi ve þifre ile kullanabilirsiniz.</div> <br><br><br>" +
            //    "<div><table width='60%' border='0.1'>" +
            //          "<tr><td><b>Kullanýcý Adý</b></td><td>" + kullaniciAdi + "</td></tr>" +
            //          "<tr><td><b>Þifre</b></td><td>" + sifre + "</td></tr>" +
            //          "</table></div>";

            ePosta.Body = "<div>\r\n  <br>\r\n     Dumlupýnar Üniversitesi <br>\r\n   Ýþletmelerde Mesleki Eðitim Otomasyonuna Hoþ geldiniz!\r\n    \r\n  <br>Hesabýnýzý aþaðýdaki kullanýcý adi ve þifre ile <a href='https://ime.dpu.edu.tr' target='_blank'>https://ime.dpu.edu.tr</a> adresinden kullanabilirsiniz.</div> \r\n  <br><br>\r\n<div>\r\n  <table width='60%' border='0.5'>\r\n    <tr>\r\n      <td style='border: 0.1px solid #8d898975;padding: 10px;'><b>Kullanýcý Adý</b></td>\r\n      <td style='border: 0.1px solid #8d898975;padding: 10px;'>" + kullaniciAdi + "</td>\r\n    </tr>\r\n    <tr>\r\n      <td style='border: 0.1px solid #8d898975;padding: 10px;'><b>Þifre</b></td>\r\n      <td style='border: 0.1px solid #8d898975;padding: 10px;'>" + sifre + "</td>\r\n    </tr>\r\n  </table>\r\n</div>";

            SmtpClient smtp = new SmtpClient();
            //
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            smtp.Credentials = new System.Net.NetworkCredential(_emailFrom, _emailFromPassword);
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            ePosta.IsBodyHtml = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            object userState = ePosta;
            bool kontrol = true;
            try
            {
                smtp.Send(ePosta);
            }
            catch (SmtpException ex)
            {
                throw new Exception(ex.Message);
            }
            return kontrol;
        }


    }

}
