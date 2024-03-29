﻿using Model_Layer.Models;
using Repository_Layer.ContextClass;
using Repository_Layer.Entity;
using Repository_Layer.InterfaceRL;
using Repository_Layer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.ServiceRL
{
    public class CollaboratorRL : ICollaboratorRL
    {
        private readonly UserContext _userContext;

        public CollaboratorRL(UserContext userContext)
        {
            _userContext = userContext;
        }

        public string AddCollaborator(CollaboratorModel collaboratorModel, int noteId, int _userId)
        {

            var Check = _userContext.Notes.FirstOrDefault(e => e.NoteId ==  noteId && e.UserId == _userId);
            CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
            if (Check != null)
            {
               
                collaboratorEntity.User_Id = _userId;
                collaboratorEntity.Note_Id = noteId;
                collaboratorEntity.Collaborator_Email = collaboratorModel.Collaborator_Email;

                _userContext.Collaborator.Add(collaboratorEntity);
                _userContext.SaveChanges();
                return collaboratorEntity.Collaborator_Email;
            }

            return null;


        }

        public List<string> ViewCollaborator(int userId,int noteId)
        {
            var notes = _userContext.Notes.FirstOrDefault(e => e.UserId == userId && e.NoteId == noteId);

            if(notes != null)
            {
                var collab = _userContext.Collaborator.Where(e => e.User_Id == userId && e.Note_Id == noteId).Select(e => e.Collaborator_Email).ToList();

                return collab;
            }
            return null;
        }

        public bool RemoveCollaborators(string email,int userId, int noteId)
        {
            var check = _userContext.Notes.FirstOrDefault(e => e.UserId == userId && e.NoteId == noteId);   

            if (check != null)
            {
                CollaboratorEntity collaboratorEntity = _userContext.Collaborator.FirstOrDefault(e => e.User_Id == userId && e.Note_Id == noteId && e.Collaborator_Email == email);
                if (collaboratorEntity != null)
                {
                    _userContext.Collaborator.Remove(collaboratorEntity);
                    _userContext.SaveChanges();
                    return true;
                }

                
            }
            return false;

        }

        

    }
}
