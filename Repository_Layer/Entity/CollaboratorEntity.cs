using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository_Layer.Entity
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Collaborator_id { get; set; }

        public string Collaborator_Email { get; set; }

        [ForeignKey("Users")]
        public int User_Id { get; set; }

        [JsonIgnore]
        public virtual UserEntity Users { get; set; }

        [ForeignKey("Notes")]
        public int Note_Id { get; set; }

        [JsonIgnore]
        public virtual UserNotes Notes { get; set; }



    }
}
