using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.InterfaceRL
{
    public interface ICollaboratorRL
    {
        public CollaboratorEntity AddCollaborator(CollaboratorModel collaboratorModel, int noteId, int _userId);

    }
}
