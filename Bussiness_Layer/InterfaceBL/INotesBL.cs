﻿using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.InterfaceBL
{
    public interface INotesBL
    {
        public UserNotes AddNote(NotesModel notesModel, int _userId);

        public List<UserNotes> ViewNote(int _userId);





    }
}
