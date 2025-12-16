using AS.Entities.Base;
using AS.Entities.Entity;
using AS.Entities.Enums;

namespace AS.Entities.Dtos
{
    public class OBSStudentDto
    {

        public string OGR_NO { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string ADI { get; set; }
        public string SOYAD { get; set; }

        public string KIMLIK_BABAAD { get; set; }

        public string KIMLIK_ANAAD { get; set; }

        public string FAK_KOD { get; set; }
        public string FAK_AD { get; set; }


        public double SINIF { get; set; }
        public string KAYIT_YILI { get; set; }
        public string KAYIT_TARIH { get; set; }
        public string BOLUM_AD { get; set; }
        public double BOL_ID { get; set; }

        //public double? OGRENIM_SURE { get; set; }

        public double KIMLIK_IL_ID { get; set; }

        public string CINSIYET { get; set; }
        public string KIMLIK_IL_AD { get; set; }
        public string DOG_TARIH { get; set; }
        public string ACIKLAMA { get; set; }

    }
}
