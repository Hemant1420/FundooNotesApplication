using Bussiness_Layer.InterfaceBL;
using Model_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.InterfaceRL;
using Repository_Layer.ServiceRL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.ServiceBL
{
    public class CollaboratorBL : ICollaboratorBL
    {
        private readonly ICollaboratorRL _collaboratorRL;

        public CollaboratorBL(ICollaboratorRL collaboratorRL)
        {
            _collaboratorRL = collaboratorRL;
        }

      

        public CollaboratorEntity AddCollaborator(CollaboratorModel collaboratorModel, int noteId, int _userId)
        {
           
                return _collaboratorRL.AddCollaborator(collaboratorModel, noteId, _userId);
           
        }
    }
    

    
}
