using PocketWallet.Bkash.Common.Models;
using CONSTANTS = PocketWallet.Bkash.Common.Constants.Constants;

namespace PocketWallet.Bkash.MappingProfile
{
    /// <summary>
    /// Configures SimpleMapper mappings for payment models (alternative to AutoMapper).
    /// </summary>
    public static class SimpleMapperProfile
    {
        /// <summary>
        /// Configures all mappings for SimpleMapper.
        /// </summary>
        public static void Configure()
        {
            // CreatePaymentCommand -> CreatePaymentRequest
            SimpleMapper.CreateMap<CreatePaymentCommand, CreatePaymentRequest>()
                .ForMember(x => x.PayerReference, m => m.MapFrom(z => string.IsNullOrWhiteSpace(z.PayerReference) ? " " : z.PayerReference))
                .ForMember(x => x.Mode, m => m.MapFrom(z => CONSTANTS.WITHOUT_AGREEMENT_CODE))
                .ForMember(x => x.Amount, m => m.MapFrom(z => z.Amount.ToString()))
                .ForMember(x => x.Intent, m => m.MapFrom(z => string.IsNullOrWhiteSpace(z.Intent) ? CONSTANTS.SALE : z.Intent))
                .ForMember(x => x.Currency, m => m.MapFrom(z => string.IsNullOrWhiteSpace(z.Currency) ? CONSTANTS.BDT : z.Currency));

            // CreatePaymentResponse -> CreatePaymentResult
            SimpleMapper.CreateMap<CreatePaymentResponse, CreatePaymentResult>()
                .ForMember(x => x.Amount, m => m.MapFrom(z => float.Parse(z.Amount)));

            // ExecutePaymentCommand -> ExecutePaymentRequest
            SimpleMapper.CreateMap<ExecutePaymentCommand, ExecutePaymentRequest>();

            // ExecutePaymentResponse -> ExecutePaymentResult
            SimpleMapper.CreateMap<ExecutePaymentResponse, ExecutePaymentResult>()
                .ForMember(x => x.Amount, m => m.MapFrom(z => float.Parse(z.Amount)));

            // PaymentQuery -> QueryPaymentRequest
            SimpleMapper.CreateMap<PaymentQuery, QueryPaymentRequest>();

            // QueryPaymentResponse -> QueryPaymentResult
            SimpleMapper.CreateMap<QueryPaymentResponse, QueryPaymentResult>()
                .ForMember(x => x.Amount, m => m.MapFrom(z => float.Parse(z.Amount)));

            // RefundPaymentCommand -> RefundPaymentRequest
            SimpleMapper.CreateMap<RefundPaymentCommand, RefundPaymentRequest>()
                .ForMember(x => x.Amount, m => m.MapFrom(z => z.Amount.ToString()));

            // RefundPaymentResponse -> RefundPaymentResult
            SimpleMapper.CreateMap<RefundPaymentResponse, RefundPaymentResult>()
                .ForMember(x => x.Amount, m => m.MapFrom(z => float.Parse(z.Amount)));
        }
    }
}
