﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.System;
using IIMes.Services.Runtime.Model.WOR;
using IIMes.Services.Runtime.Repository;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Model.Quality;
using System;

namespace IIMes.Services.Runtime.Services
{
    public class WORDefectService : IWORDefectService
    {
        private readonly IRepository<WorDefect> _repWorDefect;

        public WORDefectService(IRepository<WorDefect> repWorDefect)
        {
            _repWorDefect = repWorDefect;   
        }
    }
}