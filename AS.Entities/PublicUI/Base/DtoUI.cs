using System.ComponentModel.DataAnnotations;
namespace AS.Entities.PublicUI.Base
{
    public class DtoUI: IDtoUI
    {
        [Key]
        /// <summary>
        /// Birincil anahtar
        /// </summary>
        public Guid Id { get; set; }
    }

    public interface IDtoUI
    {
        public Guid Id { get; set; }
    
    }
}
