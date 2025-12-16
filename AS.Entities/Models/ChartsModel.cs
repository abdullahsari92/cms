using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;

namespace AS.Entities.Models
{
    public class ChartsModel 
    {

        //public List<String> Labels { get; set; }

        //public ChartsDataset Datasets { get; set; }
        public string Label { get; set; }
        public int Data { get; set; }



    }

    public class ChartsDataset
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
    }


}
