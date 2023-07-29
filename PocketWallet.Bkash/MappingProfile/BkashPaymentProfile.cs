using AutoMapper;

namespace PocketWallet.Bkash.MappingProfile
{
    /// <summary>
    /// Represents payment models mapping object.
    /// </summary>
    public class BkashPaymentProfile : Profile
    {
        /// <summary>
        /// Initiates mapping profile.
        /// </summary>
        public BkashPaymentProfile()
        {
            CreateMap<CreatePaymentCommand, CreateBkashPaymentRequest>();
            CreateMap<CreateBkashPaymentResponse, CreatePaymentResult>();

            CreateMap<ExecutePaymentCommand, ExecuteBkashPaymentRequest>();
            CreateMap<ExecuteBkashPaymentResponse, ExecutePaymentResult>()
                .ForMember(x => x.CustomerMSISDN, x => x.MapFrom(y => y.CustomerMsisdn))
                .ForMember(x => x.TransactionID, x => x.MapFrom(y => y.TrxID));

            CreateMap<PaymentQuery, QueryBkashPaymentRequest>();
            CreateMap<QueryBkashPaymentResponse, QueryPaymentResult>();
        }
    }
}
