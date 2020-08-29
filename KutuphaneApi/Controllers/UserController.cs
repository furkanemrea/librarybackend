using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KutuphaneApi.Controllers
{
    
    public class UserController : ApiController
    {
        ReactKutuphaneDBEntitiess db = new ReactKutuphaneDBEntitiess();
        public IEnumerable<User> Get()
        {
            return db.User.ToList();
        }

        public User Get(int Id)
        {
            return db.User.FirstOrDefault(x=>x.Id==Id);
        }

        public HttpResponseMessage Post (User user)
        {
            try
            {
                db.User.Add(user);
                if (db.SaveChanges()>0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, user);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put (User user)
        {
            try
            {
                User us = db.User.FirstOrDefault(x => x.Id == user.Id);
                if (us==null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User Id :"+user.Id);
                }
                else
                {
                    us.Name = user.Name;
                    us.Surname = user.Surname;
                    us.Username = user.Username;
                    us.Password = user.Password;
                    us.Gender = user.Gender;
                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, user);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed");
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        public HttpResponseMessage Delete(int Id)
        {
            try
            {
                User us = db.User.FirstOrDefault(x => x.Id == Id);
                if (us == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User Id :" + Id);
                }
                else
                {
                    db.User.Remove(us);
                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "User Id :"+ Id );
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed");
                    }
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
