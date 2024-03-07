using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Model_Layer.Models;
using Repository_Layer.ContextClass;
using Repository_Layer.Entity;
using Repository_Layer.InterfaceRL;
using Repository_Layer.User_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.ServiceRL
{
    public class NotesRL : INotesRL
    {
        private readonly UserContext _contextClass;

        public NotesRL(UserContext userContext)
        {
            _contextClass = userContext;
        }

        public UserNotes AddNote(NotesModel notesModel,int _userId )
        {
            UserNotes user = new UserNotes();
            user.Title = notesModel.Title;
            user.Description = notesModel.Description;
            user.Colour = notesModel.Colour;
            user.UserId = _userId;


            _contextClass.Notes.Add(user);
            _contextClass.SaveChanges();

            return user;

        }
        

        
      
       


       
    }
}
