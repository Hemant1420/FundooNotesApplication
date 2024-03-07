﻿using Bussiness_Layer.InterfaceBL;
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
    public class NotesBL : INotesBL
    {
        private readonly INotesRL _notesRL;

        public NotesBL(INotesRL notesRL)
        {
            _notesRL = notesRL;
        }
        

        
        public UserNotes AddNote(NotesModel notesModel, int _userId)
        {
            return _notesRL.AddNote(notesModel,_userId);
        }

      


    }
}