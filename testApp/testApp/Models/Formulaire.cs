using System.ComponentModel.DataAnnotations;

namespace testApp.Models
{
    public class Formulaire
    {
        [Key]
        public int Id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string mail { get; set; }
        public string telephone { get; set; }

        public string? niveau { get; set; }

        public int? experience { get; set; }
        public string? dernierEmployeur { get; set; }
        
        public string path { get; set; }
    }
}
