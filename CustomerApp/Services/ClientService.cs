using CustomerApp.Entity;
using CustomerApp.Helpers;
using CustomerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApp.Services
{
    public class ClientService
    {
        CustomerEntiity db = new CustomerEntiity();

        #region Add Or Update Client
        /// <summary>
        /// Add Or Update Client
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AddOrUpdateClient(ClientVM model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Email))
                    throw new Exception(Messages.BAD_DATA);

                var client = db.Clients.FirstOrDefault(x => x.Id == model.Id);
                if (client == null)
                {
                    if (!string.IsNullOrWhiteSpace(model.Email) && IsClientExists(model.UID))
                        throw new Exception(Messages.UID_EXISTS);
                    client = new Client();
                    client.Email = model.Email;
                    client.ExpiryDate = model.ExpiryDate;
                    client.Mobile = model.Mobile;
                    client.UID = model.UID;
                    db.Clients.Add(client);
                    await db.SaveChangesAsync();
                    return client.Id.ToString();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(model.Email) && IsClientExists(model.UID) && IsClientExists(model.UID, model.Id))
                        throw new Exception(Messages.UID_EXISTS);

                    db.Entry(client).CurrentValues.SetValues(model);
                    await db.SaveChangesAsync();
                    return client.Id.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion


        #region Delete Client
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            if (id > 0)
            {
                db.Clients.Remove(db.Clients.Find(id));
                return db.SaveChanges() > 0;
            }
            return false;
        } 
        #endregion

        #region Check If Client Exist By UID
        /// <summary>
        /// Check if Client Exists
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public bool IsClientExists(string UID)
        {
            return db.Clients.Count(x => x.UID.ToLower() == UID.ToLower()) > 0;
        }
        #endregion

        #region Check if Client Exits
        /// <summary>
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsClientExists(string UID, int id)
        {
            return db.Clients.Count(x => x.UID.ToLower() == UID.ToLower() && x.Id != id) > 0;
        }
        #endregion

        #region Get All Clirents data
        /// <summary>
        /// GetAllClients
        /// </summary>
        /// <returns></returns>
        /// 
        public List<ClientVM> GetAllClients()
        {

            return db.Clients
               .Select(x => new ClientVM()
               {
                   ExpiryDate = x.ExpiryDate,
                   Email = x.Email,
                   Mobile = x.Mobile,
                   UID = x.UID,
                   Id = x.Id
               })
               .ToList();

        } 
        #endregion

        #region Get Client By UID
        /// <summary>
        /// Get Clien By UID
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Client GetClientByUID(string uid)
        {
            return db.Clients.Where(x => x.UID == uid).FirstOrDefault();
        }
        #endregion

        #region Get Client By id
        /// <summary>
        /// Get Clien By UID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientVM GetClientById(int id)
        {
            return db.Clients.Where(x=>x.Id==id)
               .Select(x => new ClientVM()
               {
                   ExpiryDate = x.ExpiryDate,
                   Email = x.Email,
                   Mobile = x.Mobile,
                   UID = x.UID,
                   Id = x.Id
               })
               .FirstOrDefault();
        }
        #endregion
    }
}