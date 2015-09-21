﻿using Domain;
using Domain.Abstract;
using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using WebUI.Infrastructure;

namespace WebUI.Controllers.API
{
    public class BaseApiController<T> : ApiController, IBaseApiInterface<T>  where T : BaseEntity
    {
        public BaseApiController()
        {
            repository = unitOfWork.EFRepository<T>();
        }

        public BaseApiController(IRepository<T> repository)
        {
            this.repository = repository;
        }

        #region Protected

        protected UnitOfWork unitOfWork = new UnitOfWork();
        protected IRepository<T> repository;

        protected string GenericTypeName
        {
            get { return typeof(T).Name; }
        }

        protected HttpResponseMessage ErrorMsg(HttpStatusCode statusCode, string errorMsg)
        {
            HttpError error = new HttpError()
            {
                Message = string.Format("code: {0}", (int)statusCode),
                MessageDetail = errorMsg
            };            
            return Request.CreateResponse(statusCode, error);
        }
        #endregion

        #region Private
        private int NormalizePageNo(int pageNo)
        {
            return pageNo > 0 ? pageNo - 1 : 0;
        }

        private int NormalizePageSize(int pageSize)
        {
            return pageSize > 0 ? pageSize : 0;
        }

        private int CalculatePageCount(int total, int pageSize)
        {
            return total > 0 ? (int)Math.Ceiling(total / (double)pageSize) : 0;
        }
        #endregion

        #region GET

        [EnableQuery]
        public virtual HttpResponseMessage Get()
        {
            var entity = repository.Table;

            if (entity == null || !entity.Any())
            {
                var message = string.Format("{0}: No content", GenericTypeName);
                return ErrorMsg(HttpStatusCode.NotFound, message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

        [HttpGet]
        public virtual HttpResponseMessage GetFull(int id, string entities)
        {
            string[] a = entities.Split(new[] { "," }, StringSplitOptions.None);

            string s1 = string.Empty;
            
            foreach (var prop in typeof(T).GetProperties().Where(p => p.GetGetMethod().IsVirtual))
            {
                Type s = prop.PropertyType;
                if (s.IsClass && !s.FullName.StartsWith("System."))
                {
                    s1 += prop.Name + ",";
                }
                
            }
            s1 = s1.Remove(s1.Length - 1);
            if (!entities.Equals("0"))
                s1 = entities;
            string entityName = typeof(T).Name;
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var response = Request.CreateResponse(HttpStatusCode.Found);

            response.Headers.Location = new Uri(baseUrl + "/api/" + entityName + "/" + id.ToString() + "?$expand=" + s1);
            return response;
           // return Request.CreateResponse(HttpStatusCode.OK, s);
        }

        [EnableQuery]
        public virtual HttpResponseMessage Get(int pageNo, int pageSize)
        {
            pageNo = NormalizePageNo(pageNo);
            pageSize = NormalizePageSize(pageSize);

            int total = repository.Table.Count();
            int pageCount = CalculatePageCount(total, pageSize);

            var entity = repository.Table
                .OrderBy(c => c.ID)
                .Skip(pageNo * pageSize)
                .Take(pageSize);

            if (entity == null || !entity.Any())
            {
                var message = string.Format("{0}: No content", GenericTypeName);
                return ErrorMsg(HttpStatusCode.NotFound, message);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK, entity);
            response.Headers.Add("X-Paging-PageNo", (pageNo + 1).ToString());
            response.Headers.Add("X-Paging-PageSize", pageSize.ToString());
            response.Headers.Add("X-Paging-PageCount", pageCount.ToString());
            response.Headers.Add("X-Paging-TotalRecordCount", total.ToString());

            return response;
        }

        [EnableQuery]
        public virtual HttpResponseMessage GetById([FromODataUri]int id)
        {
            var entity = repository.GetById(id);

            if (entity == null)
            {                
                var message = string.Format("No {0} with ID = {1}", GenericTypeName, id);
                return ErrorMsg(HttpStatusCode.NotFound, message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

        public virtual HttpResponseMessage GetById(int id, string all)
        {
            if (all.Equals("all"))
            {
                repository.LazyLoadingManage = true;
                return GetById(id);
            }

            return ErrorMsg(HttpStatusCode.BadRequest, "Error: BadRequest");

        }
        #endregion

        #region POST
        public virtual HttpResponseMessage Post([FromBody]T entity)
        {           
            try
            { 
                repository.Insert(entity);
                return Request.CreateResponse(HttpStatusCode.Created, entity);             
            }
            catch (Exception ex)
            {
                return ErrorMsg(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        #endregion

        #region DELETE
        public virtual HttpResponseMessage Delete(int id)
        {
            string message;
            T toDelete = repository.GetById(id);

            if (toDelete == null)
            {
                message = string.Format("No {0} with ID = {1}", GenericTypeName, id);
                return ErrorMsg(HttpStatusCode.NotFound, message);
            }
            try
            {
                repository.Delete(toDelete);
                message = string.Format("{0} with ID = {1} was deleted", GenericTypeName, id);
                return ErrorMsg(HttpStatusCode.OK, message);
            }
            catch (Exception ex)
            {
                return ErrorMsg(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        #region PUT
        public virtual HttpResponseMessage Put([FromBody]T entity)
        {
            T oldEntity = repository.GetById(entity.ID);

            if (oldEntity == null)
            {
                return ErrorMsg(HttpStatusCode.NotFound, string.Format("No {0} with ID = {1}", GenericTypeName, entity.ID));
            }
            try
            {
                oldEntity = entity;
                oldEntity.LastUpdDT = DateTime.Now;
                repository.Update(oldEntity);
                return Request.CreateResponse(HttpStatusCode.OK, oldEntity);
            }
            catch (Exception ex)
            {
                return ErrorMsg(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
