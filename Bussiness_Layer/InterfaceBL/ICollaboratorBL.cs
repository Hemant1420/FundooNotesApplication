using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.InterfaceBL
{
    public interface ICollaboratorBL
    {
        public CollaboratorEntity AddCollaborator(CollaboratorModel collaboratorModel, int noteId, int _userId);


    }
}
