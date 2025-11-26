using Knot.Configuration;
using PocketWallet.Bkash.Common.Models;
using CONSTANTS = PocketWallet.Bkash.Common.Constants.Constants;

namespace PocketWallet.Bkash.MappingProfile
{
    /// <summary>
    /// Configures SimpleMapper mappings for payment models (alternative to AutoMapper).
    /// </summary>
    public class SimpleMapperProfile : Profile
    {
        /// <summary>
        /// Configures all mappings for 
        /// </summary>
        protected override void Configure()
        {
            // CreatePaymentCommand -> CreatePaymentRequest
            CreateMap<CreatePaymentCommand, CreatePaymentRequest>(map =>
            {
                map.ForMember(dest => dest.PayerReference, src => string.IsNullOrWhiteSpace(src.PayerReference) ? " " : src.PayerReference);
                map.ForMember(dest => dest.Mode, src => CONSTANTS.WITHOUT_AGREEMENT_CODE);
                map.ForMember(dest => dest.Amount, src => src.Amount.ToString());
                map.ForMember(dest => dest.Intent, src => string.IsNullOrWhiteSpace(src.Intent) ? CONSTANTS.SALE : src.Intent);
                map.ForMember(dest => dest.Currency, src => string.IsNullOrWhiteSpace(src.Currency) ? CONSTANTS.BDT : src.Currency);
            });

            // CreatePaymentResponse -> CreatePaymentResult
            CreateMap<CreatePaymentResponse, CreatePaymentResult>(map =>
            {
                map.ForMember(dest => dest.Amount, src => float.Parse(src.Amount));
            });

            // ExecutePaymentCommand -> ExecutePaymentRequest
            CreateMap<ExecutePaymentCommand, ExecutePaymentRequest>();

            // ExecutePaymentResponse -> ExecutePaymentResult
            CreateMap<ExecutePaymentResponse, ExecutePaymentResult>(map =>
            {
                map.ForMember(dest => dest.Amount, src => float.Parse(src.Amount));
            });

            // PaymentQuery -> QueryPaymentRequest
            CreateMap<PaymentQuery, QueryPaymentRequest>();

            // QueryPaymentResponse -> QueryPaymentResult
            CreateMap<QueryPaymentResponse, QueryPaymentResult>(map =>
            {
                map.ForMember(dest => dest.Amount, src => float.Parse(src.Amount));
            });

            // RefundPaymentCommand -> RefundPaymentRequest
            CreateMap<RefundPaymentCommand, RefundPaymentRequest>(map =>
            {
                map.ForMember(dest => dest.Amount, src => src.Amount.ToString());
            });

            // RefundPaymentResponse -> RefundPaymentResult
            CreateMap<RefundPaymentResponse, RefundPaymentResult>(map =>
            {
                map.ForMember(dest => dest.Amount, src => float.Parse(src.Amount));
            });
        }
    }
}
