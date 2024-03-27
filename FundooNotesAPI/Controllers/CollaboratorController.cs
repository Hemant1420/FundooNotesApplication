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
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace FundooNotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        public readonly ICollaboratorBL _collaboratorBL;
        public readonly IDistributedCache _cache;

        public CollaboratorController(ICollaboratorBL collaboratorBL, IDistributedCache cache)
        {
            _collaboratorBL = collaboratorBL;
            _cache = cache;
        }

        [HttpPost]
        [Authorize]
        public ResponseModel<string> AddCollaborator (CollaboratorModel collaboratorModel, int noteId)
        {
            string userId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(userId); 

            var result = _collaboratorBL.AddCollaborator(collaboratorModel, noteId, _userId);
            var response = new ResponseModel<string>();



            string key = Convert.ToString(_userId) + Convert.ToString(noteId);  //creating the key for storing data in cache
            var cacheResult = _cache.GetString(key);                           //Getting the list of collaborators from cache 

            if (cacheResult == null)                                           //Checking if cache is null for the key, if null then first add the value or else add in the list
            {
                List<string> collabList = new List<string> { result }; 
                _cache.SetString(key, JsonSerializer.Serialize(collabList));

            }
            else
            {
                var finalResult = JsonSerializer.Deserialize<List<string>>(cacheResult);    //deserializing the list
                finalResult.Add(result);                                                                //Adding the latest collaoborator to the list of collaborators for that particular note    
                _cache.SetString(key, JsonSerializer.Serialize(finalResult));                           //Adding the finalList to cache for that note  
            }

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
            var response = new ResponseModel<List<string>>();

            string userId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(userId);

            string key = Convert.ToString(_userId) + Convert.ToString(_noteId);          //Creating key for cache
            var cacheresult = _cache.GetString(key);                                      

            if (cacheresult != null)
            {
                var finalResult = JsonSerializer.Deserialize<List<string>>(cacheresult);    //getting the result from cache     

                response.Success = true;
                response.Message = "Collaborator retrieved";
                response.Data = finalResult;
                return response;
            }
            else
            {
                var result = _collaboratorBL.ViewCollaborator(_userId, _noteId);          //Getting result from database if not found in cache


                if (result != null)
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

        }

        [HttpDelete]
        [Authorize]
        public ResponseModel<bool> RemoveCollaborator(string email,int _noteId)
        {
            string UserId = User.FindFirstValue("UserId");
            int _userId = Convert.ToInt32(UserId);  

            var result = _collaboratorBL.RemoveCollaborators(email,_userId, _noteId); 

            string key = Convert.ToString(UserId) + Convert.ToString(_noteId);  //Creating key
            _cache.Remove(key);                                                 //Removing data from cache

            var response = new ResponseModel<bool>();                           //Removing from database

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
