using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ClayOnWheels.Functions
{
    public static class User
    {
        public static string GetUserId()
        {
            var id = ClaimsPrincipal.Current.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
            return id;
        }
    }
}