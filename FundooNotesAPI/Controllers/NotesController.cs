using Bussiness_Layer.InterfaceBL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Model_Layer.Models;
using Repository_Layer.Entity;
using System.Security.Claims;

namespace FundooNotesAPI.Controllers
{
    [Route("api/Note")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;

        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }

           
       
        [HttpPost]
       
        [Authorize]

        public ResponseModel<NotesModel> CreateNote(NotesModel notesModel)
        {
            var response = new ResponseModel<NotesModel>();
            string userId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(userId);
            var Result = notesBL.AddNote(notesModel, _userId);

            if( Result != null)
            {
                 
                response.Success = true;
                response.Message = "Note created Successfully";
                response.Data = notesModel;

            }
            else
            {
                response.Success = false;
                response.Message = "Error While creating the note!, Please try again";
                
            }
            return response;

           

        }

        [HttpGet]
        [Authorize]
        public ResponseModel<List<UserNotes>> ViewNote()
        {
            var response = new ResponseModel<List<UserNotes>>();

            string userId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(userId);
            var Result = notesBL.ViewNote( _userId);

            if(Result != null)
            {
                response.Success = true;
                response.Message = "Note Retrieved successfully";
                response.Data = Result;


            }
            else
            {
                response.Success = false;
                response.Message = "Error While retrieveing the note!";
            }

                return response;
        }

        [HttpPatch]
        [Authorize]
        public ResponseModel<UserNotes> EditNote(NotesModel notesModel, int _noteId)
        {
            var response = new ResponseModel<UserNotes>();

            string UserId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(UserId);

            var result = notesBL.EditNote(notesModel, _userId, _noteId );  

            if( result != null )
            {
                response.Success = true;
                response.Message = "Noted Edited successfully";
                response.Data = result;
            }
            else
            {
                response.Success = false;
                response.Message = "Error while editing the note,Please try again";
                
            }
            return response;
        }


        [HttpDelete]
        [Authorize]
        public ResponseModel<bool> DeleteNode(int nodeId)
        {
            var response = new ResponseModel<bool>();

            string UserId = User.FindFirstValue("UserId");  
            int _userId = Convert.ToInt32(UserId);

            bool result = notesBL.DeleteNote(_userId, nodeId);

            if (result)
            {
                response.Success = true;
                response.Message = "Note Deleted successfully";
                
            }
            else
            {
                response.Success = false;
                response.Message = "There was a Error while deleteing the node, Please try again";
            }
            return response;
        }




    }
}
