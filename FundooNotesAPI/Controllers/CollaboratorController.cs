using Azure;
using Bussiness_Layer.ServiceBL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using Repository_Layer.Entity;
using System.Security.Claims;
using Model_Layer.Models;
using Bussiness_Layer.InterfaceBL;


namespace FundooNotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        public readonly ICollaboratorBL _collaboratorBL;

        public CollaboratorController(ICollaboratorBL collaboratorBL)
        {
            _collaboratorBL = collaboratorBL;
        }

        [HttpPost]
        [Authorize]
        public ResponseModel<CollaboratorEntity> AddCollaborator (CollaboratorModel collaboratorModel, int noteId)
        {
            string userId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(userId); 

            var result = _collaboratorBL.AddCollaborator(collaboratorModel, noteId, _userId);
            var response = new ResponseModel<CollaboratorEntity>();

            if(result != null)
            {
                response.Success = true;
                response.Message = "Collaborator added Successfully";
                response.Data = result;
            }
            else
            {
                response.Success = false;
                response.Message = "Error While Adding Collaborator! Please try again";
            }
            return response;
            

        }
        [HttpGet]
        [Authorize]
        public ResponseModel<List<string>> ViewCollaborator ( int _noteId) 
        {

            string userId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(userId);

            var result = _collaboratorBL.ViewCollaborator(_userId, _noteId);
            var response = new ResponseModel<List<string>>();

            if( result != null )
            {
                response.Success = true;
                response.Message = "Collaborater retrieved";
                response.Data = result;
            }
            else
            {
                response.Success = false;
                response.Message = "Error while fetching records";
            }
            return response;

        }

        [HttpDelete]
        [Authorize]
        public ResponseModel<bool> RemoveCollaborator(string email,int _noteId)
        {
            string UserId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(UserId);  

            var result = _collaboratorBL.RemoveCollaborators(email,_userId, _noteId); 

            var response = new ResponseModel<bool>();

            if (result)
            {
                response.Success = true;
                response.Message = "Collaborator removed successfully";
                
            }
            else
            {
                response.Success = false;
                response.Message = "No Collaborators added to Remove";
            }
            return response;
        } 


    }
}
