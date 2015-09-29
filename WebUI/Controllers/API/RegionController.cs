﻿using Domain.Entities.Dictionaries;
using Domain.Abstract;

namespace WebUI.Controllers.API
{
    public class RegionController : BaseApiController<Region>
    {
        public RegionController()
            : base()
        {
        }

        public RegionController(IRepository<Region> repository)
            : base(repository)
        {
            this.repository = repository;
        }
    }
}