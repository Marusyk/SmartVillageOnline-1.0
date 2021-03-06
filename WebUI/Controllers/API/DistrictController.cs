﻿using Domain.Entities.Dictionaries;
using Domain.Abstract;
using WebUI.Infrastructure;

namespace WebUI.Controllers.API
{
    public class DistrictController : BaseApiController<District>
    {
        public DistrictController() { }

        public DistrictController(IRepository<District> repository)
            : base(repository)
        {
            Repository = repository;
        }
    }
}