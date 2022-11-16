using AutoMapper;
using XYZ.Billing.Core.Models.Request;
using XYZ.Billing.Core.Models.Response;

namespace XYZ.Billing.Core.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Request
            CreateMap<OrderRequest, OrderModel>();

            //Response
            CreateMap<ReceiptModel, ReceiptResponse>();

            //Models
            CreateMap<OrderModel, PaymentModel>();
        }
    }
}
