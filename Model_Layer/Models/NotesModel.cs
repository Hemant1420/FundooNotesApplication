using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Layer.Models
{
    public class NotesModel
    {

        
        public string Title { get; set; }

        public string Description { get; set; }
        public string Colour { get; set; } = "";

        public bool IsArchived { get; set; } = false;

        public bool IsDeleted { get; set; } = false;    


    }
}
