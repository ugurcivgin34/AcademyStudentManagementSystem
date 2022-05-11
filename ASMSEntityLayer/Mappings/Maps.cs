using ASMSEntityLayer.Models;
using ASMSEntityLayer.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Mappings
{
    public class Maps : Profile
    {
        // Buraya Maps ctor metodu gelecektir.
        // İçine CreateMap netodu gelecek

        public Maps()
        {
            //UserAdress'ı UserAdressesVM'ye dönüştür
            //CreateMap<UsersAddress, UsersAddressVM>(); //DAL --> BLL
            //UserAdressVM'ı UserAdresses'e dönüştür
            //CreateMap<UsersAddressVM, UsersAddress>(); //PL --> BLL --> DAL
            //Yukarıdakinin aynısı tek seferde yapmak UserAdress ve VM'yi birbirine dönüştür.

            CreateMap<UsersAddress, UsersAddressVM>().ReverseMap(); //Her iki durumu da sağlıyor.Ayrı ayrı yazmaya gerek kalmıyor.
            CreateMap<Student, StudentVM>().ReverseMap(); 
            CreateMap<City, CityVM>().ReverseMap();
            CreateMap<District, DistrictVM>().ReverseMap();
            CreateMap<Neighbourhood, NeighbourhoodVM>().ReverseMap();
            
        }



    }
}
