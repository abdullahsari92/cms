using AS.Core.ValueObjects;
using AS.Entities.Dtos;

namespace AS.Business.EmailMessage
{
    public interface IEMailSender 
    {

        public bool Sender(string kullaniciAdi, string sifre);




    }
}
