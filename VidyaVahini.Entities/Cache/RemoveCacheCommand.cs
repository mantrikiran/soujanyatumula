using System.ComponentModel.DataAnnotations;

namespace VidyaVahini.Entities.Cache
{
    public class RemoveCacheCommand
    {
        [Required]
        public string Key { get; set; }
    }
}
