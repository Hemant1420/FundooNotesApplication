using Bussiness_Layer.InterfaceBL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Model_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.InterfaceRL;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.Json;

namespace FundooNotesAPI.Controllers
{
    [Route("api/Note")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;
        private readonly IDistributedCache _cache;

        public NotesController(INotesBL notesBL, IDistributedCache cache)
        {
            this.notesBL = notesBL;
            _cache = cache;
            
        }

           
       
        [HttpPost]
       
        [Authorize]

        public ResponseModel<NotesModel> CreateNote(NotesModel notesModel)
        {

            var response = new ResponseModel<NotesModel>();
            string userId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(userId);
            var Result = notesBL.AddNote(notesModel, _userId);                                     //Adding data in database 

            
            _cache.SetString(Convert.ToString(Result.NoteId), JsonSerializer.Serialize(Result));   //Adding data in cache memory (noteid as key)
            
            var cacheResult = _cache.GetString(Convert.ToString(_userId));                          //Getting the list of notes stored by user in cache(with userId)
            if (cacheResult == null)
            {
                List<UserNotes> noteList = new List<UserNotes> { Result };

                _cache.SetString(Convert.ToString(_userId), JsonSerializer.Serialize(noteList));   //Adding data in cache memory (noteid as key)

            }
            else
            {
                var finalResult = JsonSerializer.Deserialize<List<UserNotes>>(cacheResult);             //Deserializing the list
                finalResult.Add(Result);                                                                //Adding the latest created note to the list
                _cache.SetString(Convert.ToString(_userId), JsonSerializer.Serialize(finalResult));     //Again reassigining the list to that particular userId with added note in cache (Userid as key)
            }

            if ( Result != null)
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

            var cacheResponse = _cache.GetString(Convert.ToString(_userId));            //checking if the data is present in cache

            if (cacheResponse != null)
            {
                var finalResponse = JsonSerializer.Deserialize<List<UserNotes>>(cacheResponse);         //deserializing the data

                response.Success = true;
                response.Message = "Note Retrieved successfully";
                response.Data = finalResponse;
                return response;
            }
            else
            {

                var Result = notesBL.ViewNote(_userId);

                if (Result != null)
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


        }

        [HttpGet("NotebyId")]
        [Authorize]

        public ResponseModel<UserNotes> ViewNotesbyId( int noteId)
        {
            var response = new ResponseModel<UserNotes>();

            var cacheResult = _cache.GetString(Convert.ToString(noteId));
            var finalResult = JsonSerializer.Deserialize<UserNotes>(cacheResult);

            if (finalResult != null)
            {
                response.Success = true;
                response.Message = "Notes retrieved Successfully";
                response.Data = finalResult;
                return response;
            }
            else
            {

                string UserId = User.FindFirstValue("UserId");
                int _userId = Convert.ToInt32(UserId);

                var result = notesBL.ViewNotebyId(_userId, noteId);

                if (result != null)
                {
                    response.Success = true;
                    response.Message = "Notes retrieved Successfully";
                    response.Data = result;
                }
                else
                {
                    response.Success = false;
                    response.Message = "No notes Found,please create a note first.";

                }
                return response;
            }
        }

        [HttpPut]
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
            bool result = notesBL.DeleteNote(_userId, nodeId);      //Remove  from database
            _cache.Remove(Convert.ToString(nodeId));                //Remove from cache 

            if (result)
            {
                response.Success = true;
                response.Message = "Note Deleted successfully";
                response.Data = true;
            }
            else
            {
                response.Success = false;
                response.Message = "There was a Error while deleting the node, Please try again";
            }
            return response;
        }

        [HttpPatch("Arch/UnArchieved")]
        [Authorize]
        public ResponseModel<bool> Archieved_Unarchieved(int noteId)
        {
            var response = new ResponseModel<bool>();

            string UserId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(UserId);
            
            bool result = notesBL.Arch_Unarchieved(_userId, noteId);

            if (result)
            {
                response.Success = true;
                response.Message = "Operation Performed Successfully";
                response.Data = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Error while execution, Please try again";
            }
            return response;
        }

        [HttpPatch("Trash/UnTrash")]
        [Authorize]

        public ResponseModel<bool> Trash_UnTrash(int noteId)
        {
            var response = new ResponseModel<bool>();

            string UserId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(UserId);  

            bool result = notesBL.Trash_UnTrash(_userId, noteId);   

            if(result)
            {
                response.Success = true;
                response.Message = "Operation Performed Successfully";
                response.Data = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Unexpected Error Occured, Please try again ";
            }
            return response;
        }


        



    }
}
