﻿using Model_Layer.Models;
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
        public string AddCollaborator(CollaboratorModel collaboratorModel, int noteId, int _userId);

        public List<string> ViewCollaborator(int userId, int noteId);

        public bool RemoveCollaborators(string email,int userId, int noteId);



    }
}
