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

        public Formulaire() { }
        public Formulaire(Formulaire form)
        {
            this.nom = form.nom;
            this.prenom = form.prenom;
            this.mail = form.mail;
            this.telephone = form.telephone;
            this.niveau = form.niveau;
            this.experience = form.experience;
            this.dernierEmployeur = form.dernierEmployeur;
            this.path = form.path;
        }
    }
}
