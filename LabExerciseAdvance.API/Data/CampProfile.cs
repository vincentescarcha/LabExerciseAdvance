using AutoMapper;
using CoreCodeCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            //CreateMap<Camp, CampModel>().ForMember(cm => cm.Venue, opt => opt.MapFrom(c => c.Location.VenueName)).ReverseMap();
            //CreateMap<Talk, TalkModel>().ReverseMap();
            //CreateMap<Speaker, SpeakerModel>().ReverseMap();
        }
    }
}
