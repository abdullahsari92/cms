using AS.Business.Interfaces;
using System.Net;


namespace AS.Business
{
    internal class KPSService:IKPSService
    {


        public string getServis(string url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            WebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            String resultData = reader.ReadToEnd();

            return resultData;
        }

        public string getFakultyl()
        {

            string url  = $@"https://kps.dpu.edu.tr/yoklama_verileri.aspx?password=y8q5wjikujn8guxqehtdjhzptm35swebgtn6mwrg&tur=fakulteler";
   
            return getServis(url);
        }
        public string getDepartment()
        {

            string url = $@"https://kps.dpu.edu.tr/yoklama_verileri.aspx?password=y8q5wjikujn8guxqehtdjhzptm35swebgtn6mwrg&tur=bolumler";

            return getServis(url);
        }

        /// <summary>
        /// servis OGR varsa ona bakacak yoksa tcye göre sorgu atacak.
        /// </summary>
        /// <param name="TCorOGR"></param>
        /// <returns></returns>
        public string getAktifOgrenci(string TCorOGR)
        {
            if (!(TCorOGR.Length == 12 || TCorOGR.Length == 11)) return null;
            string url  = $@"https://kps.dpu.edu.tr/dpu_aktif_ogrenci_kontrol.aspx?TC_No={TCorOGR}&Password=e9fkpg7feqkyy9z4mfvsc7k85s7bvhyua4tuvkda";
            
            if (TCorOGR.Length == 12)
            {
                url += "&OGR_NO=" + TCorOGR;
            }
            return getServis(url);

        }


      


    }
}
