﻿using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.InterfaceRL
{
    public interface INotesRL
    {
        public UserNotes AddNote(NotesModel notesModel, int _userId);

        public List<UserNotes> ViewNote(int _userId);




    }
}
