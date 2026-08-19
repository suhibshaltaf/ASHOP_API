using ASHOP.DAL.DTO.Response;
using ASHOP.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Mapping
{
    public static class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.User, src => src.CreatedBy.UserName)
                .Map(dest => dest.Name, src => src.Translations.Where(t => t.Language == CultureInfo.CurrentUICulture.Name).Select(t=>t.Name).FirstOrDefault());
        }
         
    }
}
