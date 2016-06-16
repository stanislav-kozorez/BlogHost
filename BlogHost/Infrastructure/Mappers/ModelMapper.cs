using BLL.Interface.Entities;
using BlogHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace BlogHost.Infrastructure.Mappers
{
    public static class ModelMapper
    {
        public static BllUser ToBllUser(this RegistrationViewModel model)
        {
            return new BllUser()
            {
                Name = model.Name,
                Email = model.Email,
                Password = Crypto.HashPassword(model.Password),
                CreationDate = DateTime.Now
            };
        }
    }
}