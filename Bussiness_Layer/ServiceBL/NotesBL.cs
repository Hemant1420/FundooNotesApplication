using Bussiness_Layer.InterfaceBL;
using Model_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.InterfaceRL;
using Repository_Layer.ServiceRL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public List<UserNotes> ViewNote(int _userId)
        {
            return _notesRL.ViewNote(_userId);
        }

        public UserNotes EditNote(NotesModel notesModel, int _userId, int _noteId)
        {
            return _notesRL.EditNote(notesModel,_userId, _noteId);
        }

        public bool DeleteNote(int _userId, int _noteId)
        {
            return _notesRL.DeleteNote(_userId, _noteId);
        }




    }
}
