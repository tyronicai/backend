using Microsoft.AspNetCore.Cors;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class BaseController : ControllerBase
    {
        public int? UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    foreach (var item in User.Claims)
                    {
                        if (item.Type.Equals(System.Security.Claims.ClaimTypes.NameIdentifier))
                            return int.Parse(item.Value);
                    }
                }

                return null;
            }
        }
    }
}
