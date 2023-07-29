using AutoMapper;

namespace PocketWallet.Bkash.MappingProfile
{
    public class BkashPaymentProfile : Profile
    {
        public BkashPaymentProfile()
        {
            CreateMap<CreatePaymentCommand, CreateBkashPaymentRequest>();

            CreateMap<CreateBkashPaymentResponse, CreatePaymentResponse>();

            CreateMap<ExecutePaymentCommand, ExecuteBkashPaymentRequest>();

            CreateMap<ExecuteBkashPaymentResponse, ExecutePaymentResponse>()
                .ForMember(x => x.CustomerMSISDN, x => x.MapFrom(y => y.CustomerMsisdn))
                .ForMember(x => x.TransactionID, x => x.MapFrom(y => y.TrxID));
        }
    }
}
