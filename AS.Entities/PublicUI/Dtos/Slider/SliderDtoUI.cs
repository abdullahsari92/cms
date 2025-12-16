using AS.Entities.Dtos;
using AS.Entities.PublicUI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Entities.PublicUI.Dtos.Slider
{
    public class SliderDtoUI:DtoUI
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public List<DocumentSummeryDto>? DocumentList { get; set; }
 
    }
}
