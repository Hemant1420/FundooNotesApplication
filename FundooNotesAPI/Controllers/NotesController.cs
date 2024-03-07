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

           
       
        [HttpPost("AddNote")]
       
        [Authorize]

        public ResponseModel<NotesModel> CreateNote(NotesModel notesModel)
        {
            var response = new ResponseModel<NotesModel>();
            string userId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(userId);
            var Result = notesBL.AddNote(notesModel, _userId);

            if( Result != null)
            {
                 
                response.IsSuccess = true;
                response.Message = "Note created Successfully";
                response.Data = notesModel;

            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Error While creating the note!, Please try again";
                
            }
            return response;

           

        }
       

       
    }
}
