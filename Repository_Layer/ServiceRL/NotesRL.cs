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
using System.Reflection.Metadata.Ecma335;
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

        public List<UserNotes> ViewNote(int _userId)
        {

            var Result = _contextClass.Notes.Where(e => e.UserId == _userId).ToList();

            if(Result != null)
            {
                return Result;
            }
            return Result;
        }

        public UserNotes EditNote(NotesModel notesModel,int _userId,int _noteId)
        {
           UserNotes userNotes = _contextClass.Notes.Where(e => e.UserId == _userId &&  e.NoteId == _noteId).FirstOrDefault();   
            
            if(userNotes != null) 
            {
                userNotes.Title = notesModel.Title; 
                userNotes.Description = notesModel.Description;
                userNotes.Colour = notesModel.Colour;
                userNotes.UserId = _userId;
                userNotes.NoteId = _noteId;

                _contextClass.SaveChanges();
            }
            return userNotes; 
                
        }

        public bool DeleteNote(int _userId, int _noteId)
        {
            UserNotes userNotes = _contextClass.Notes.Where(e => e.UserId == _userId && e.NoteId == _noteId ).FirstOrDefault();

            if(userNotes != null)
            {
                _contextClass.Notes.Remove(userNotes);
                _contextClass.SaveChanges();
                return true;
            }
            return false;
            
        }

       









    }
}
