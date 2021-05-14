using CustomerApp.Helpers;
using CustomerApp.Services;
using CustomerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CustomerApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/Client")]
    public class ClientServiceController : ApiController
    {
        private ClientService clientService;
        public ClientServiceController()
        {
            clientService = new ClientService();
        }

        #region Get All CLients data
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllClients")]
        public async Task<IHttpActionResult> GetAllClients()
        {
            try
            {
              
                return Success(Messages.SUCCESS, clientService.GetAllClients());
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region Add or Update Client
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddOrUpdateClient")]
        public async Task<IHttpActionResult> AddOrUpdateClient(ClientVM model)
        {
            try
            {
                var data =await clientService.AddOrUpdateClient(model);
                var i = 0;
                try
                {
                    i = int.Parse(data.ToString());
                    return Success(Messages.SUCCESS, i);
                }
                catch (FormatException ex)
                {
                    return Error(data);
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region Check If Client Exists 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetClientById")]
        public IHttpActionResult GetClientById(int id)
        {
            try
            {
                return Success(Messages.SUCCESS, clientService.GetClientById(id));
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region Check If Client Exists by UID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetClientByUID")]
        public async Task<IHttpActionResult> GetClientByUID(string id)
        {
            try
            {
                var data = await clientService.GetClientByUID(id);
                if (data == null)
                {
                    return Error(Messages.NOT_FOUND);
                }
                return Success(Messages.SUCCESS,data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region Delete Client data 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteClientById")]
        public IHttpActionResult DeleteClientById(int id)
        {
            try
            {
                return Success(Messages.SUCCESS, clientService.Delete(id));
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        } 
        #endregion

        #region ------------------ Reply Format ----------------------

        public IHttpActionResult Success(string txt, dynamic data = null)
        {
            return PrepareReply(false, txt, data);
        }

        public IHttpActionResult Error(string txt)
        {
            return PrepareReply(true, txt);
        }

        public IHttpActionResult PrepareReply(bool isError, string txt, dynamic data = null)
        {
            var reply = new Reply
            {
                status = isError ? Messages.FAIL : Messages.SUCCESS,
                msg = isError ? "" : txt,
                error = isError ? txt : null,
                data = data,
            };
            return Ok(reply);
        }

        public class Reply
        {
            public string status { get; set; }
            public string msg { get; set; }
            public string error { get; set; }
            public dynamic data { get; set; }
        }

        #endregion

    }
}
