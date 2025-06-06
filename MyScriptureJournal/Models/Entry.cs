using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace MyScriptureJournal.Models
{
    public class Entry
    {
        public int Id { get; set; }
        [StringLength(40, MinimumLength = 3)]
        [Required]
        public string Book { get; set; } = string.Empty;
        [Range(1, 138)]
        public int Chapter { get; set; }
        [RegularExpression(@"[0-9&,s-]")]
        [StringLength(25)]
        public string? Verses { get; set; }
        [Display(Name = "Date Added"), DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
        [Display(Name ="Journal Entry")]
        [StringLength(1000, MinimumLength = 3)]
        [Required]
        public string JournalEntry { get; set; }
    }
}
