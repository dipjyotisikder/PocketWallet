using Knot.Configuration;
using PocketWallet.Bkash.Common.Models;
using CONSTANTS = PocketWallet.Bkash.Common.Constants.Constants;

namespace PocketWallet.Bkash.MappingProfile
{
    /// <summary>
    /// Configures mappings for payment models.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Configures all mappings
        /// </summary>
        protected override void Configure()
        {
            CreateMap<CreatePaymentCommand, CreatePaymentRequest>(map =>
            {
                map.ForMember(dest => dest.PayerReference, src => string.IsNullOrWhiteSpace(src.PayerReference) ? " " : src.PayerReference);
                map.ForMember(dest => dest.Mode, src => CONSTANTS.WITHOUT_AGREEMENT_CODE);
                map.ForMember(dest => dest.Amount, src => src.Amount.ToString());
                map.ForMember(dest => dest.Intent, src => string.IsNullOrWhiteSpace(src.Intent) ? CONSTANTS.SALE : src.Intent);
                map.ForMember(dest => dest.Currency, src => string.IsNullOrWhiteSpace(src.Currency) ? CONSTANTS.BDT : src.Currency);
            });

            CreateMap<CreatePaymentResponse, CreatePaymentResult>(map =>
            {
                map.ForMember(dest => dest.Amount, src => float.Parse(src.Amount));
            });

            CreateMap<ExecutePaymentCommand, ExecutePaymentRequest>();

            CreateMap<ExecutePaymentResponse, ExecutePaymentResult>(map =>
            {
                map.ForMember(dest => dest.Amount, src => float.Parse(src.Amount));
            });

            CreateMap<PaymentQuery, QueryPaymentRequest>();

            CreateMap<QueryPaymentResponse, QueryPaymentResult>(map =>
            {
                map.ForMember(dest => dest.Amount, src => float.Parse(src.Amount));
            });

            CreateMap<RefundPaymentCommand, RefundPaymentRequest>(map =>
            {
                map.ForMember(dest => dest.Amount, src => src.Amount.ToString());
            });

            CreateMap<RefundPaymentResponse, RefundPaymentResult>(map =>
            {
                map.ForMember(dest => dest.Amount, src => float.Parse(src.Amount));
            });
        }
    }
}
