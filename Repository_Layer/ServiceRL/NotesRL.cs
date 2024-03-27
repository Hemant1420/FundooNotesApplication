using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository_Layer.ServiceRL
{
    public class NotesRL : INotesRL
    {
        private readonly UserContext _contextClass;
        private readonly IDistributedCache _cache;


        public NotesRL(UserContext userContext, IDistributedCache cache)
        {
            _contextClass = userContext;
            _cache = cache;
        }

        public UserNotes AddNote(NotesModel notesModel,int _userId )
        {

            UserNotes user = new UserNotes();      //adding data in database
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
               
               _cache.SetString(Convert.ToString(_userId), JsonSerializer.Serialize(Result));
                return Result;

            }
            return new List<UserNotes>();
        }

        public UserNotes ViewNotebyId(int _userId, int _noteId)
        {
            var result = _contextClass.Notes.FirstOrDefault(e => e.UserId == _userId && e.NoteId == _noteId);

             if (result != null)
             { 
                _cache.SetString(Convert.ToString(_noteId), JsonSerializer.Serialize(result));

                return result;
             }
            
            return null;


        }


        public string Check(string newValue,string oldValue)
        {
            if(newValue == "")
            {
                return oldValue;
            }
            return newValue;
        }
        public UserNotes EditNote(NotesModel notesModel,int _userId,int _noteId)
        {
           UserNotes userNotes = _contextClass.Notes.Where(e => e.UserId == _userId &&  e.NoteId == _noteId).FirstOrDefault();   
            
            if(userNotes != null) 
            {
                userNotes.Title = Check(notesModel.Title,userNotes.Title); 
                userNotes.Description = Check(notesModel.Description, userNotes.Description);
                userNotes.Colour = Check(notesModel.Colour, userNotes.Colour);
                userNotes.UserId = _userId;
                userNotes.NoteId = _noteId;

                _contextClass.SaveChanges();     //Saving Changes in Database


                _cache.SetString(Convert.ToString(userNotes.NoteId), JsonSerializer.Serialize(userNotes));  //Saving Changes in Cache


                //Saving changes in cache for ViewAllNotes
                var Usernotes   =  _cache.GetString(Convert.ToString(userNotes.UserId));    
                
                var list = JsonSerializer.Deserialize<List<UserNotes>>(Usernotes);      //all the notes stored in a list

                var note = list.Find(e => e.NoteId== _noteId);                          //Getting the list we updated now

                list.Remove(note);              //removing the previously stored note that we updated now
                list.Add(userNotes);            //Replacing the latest note in the list that we updated now

                _cache.SetString(Convert.ToString(userNotes.UserId), JsonSerializer.Serialize(list));  //saving the list of notes in cache(of a particular user)




            }
            return userNotes; 
                
        }

        public bool DeleteNote(int _userId, int _noteId)
        {
            
            var UserNote= _cache.GetString(Convert.ToString(_userId));


            if (UserNote!= null)
            {
                _cache.Remove(Convert.ToString(_noteId));
            }

            UserNotes userNotes = _contextClass.Notes.Where(e => e.UserId == _userId && e.NoteId == _noteId ).FirstOrDefault();

            if(userNotes != null)
            {
                _contextClass.Notes.Remove(userNotes);
                _contextClass.SaveChanges();
                return true;
            }
            return false;
            
        }

        public bool Arch_Unarchieved(int userId, int noteId)
        {
            var check = _contextClass.Notes.FirstOrDefault(e => e.UserId == userId && e.NoteId == noteId);

            if (check.IsDeleted != true)
            {



                if (check.IsArchived == true)
                {
                    check.IsArchived = false;
                    _contextClass.SaveChanges();


                }
                else
                {
                    check.IsArchived = true;
                    _contextClass.SaveChanges();

                }
                return true;


            }
            return false;


        }

        public bool Trash_UnTrash(int userId, int noteId)
        {
            var check = _contextClass.Notes.FirstOrDefault(e => e.UserId == userId && e.NoteId == noteId);
                
            if(check.IsDeleted == true)
            {
                check.IsDeleted = false;
            }
            else
            {
                check.IsDeleted = true;
            }
            _contextClass.SaveChanges();
            return true; 
        }











    }
}
