using System.Linq.Expressions;
using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;

namespace AS.Business.Interfaces
{
    public interface IKPSService
    {
        public string getFakultyl();

        public string getDepartment();

        public string getAktifOgrenci(string TCorOGR);


    }
}
