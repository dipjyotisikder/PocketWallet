using AutoMapper;

namespace PocketWallet.Bkash.MappingProfile;

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
        CreateMap<CreatePaymentCommand, CreatePaymentRequest>()
            .ForMember(x => x.PayerReference, y => y.MapFrom(z => string.IsNullOrWhiteSpace(z.PayerReference) ? " " : z.PayerReference))
            .ForMember(x => x.Mode, y => y.MapFrom(z => CONSTANTS.WITHOUT_AGREEMENT_CODE))
            .ForMember(x => x.Amount, y => y.MapFrom(z => z.Amount.ToString()))
            .ForMember(x => x.Intent, y => y.MapFrom(z => string.IsNullOrWhiteSpace(z.Intent) ? CONSTANTS.SALE : z.Intent))
            .ForMember(x => x.Currency, y => y.MapFrom(z => string.IsNullOrWhiteSpace(z.Currency) ? CONSTANTS.BDT : z.Currency));

        CreateMap<CreatePaymentResponse, CreatePaymentResult>()
            .ForMember(x => x.Amount, y => y.MapFrom(z => float.Parse(z.Amount)));

        CreateMap<ExecutePaymentCommand, ExecutePaymentRequest>();
        CreateMap<ExecutePaymentResponse, ExecutePaymentResult>()
            .ForMember(x => x.Amount, y => y.MapFrom(z => float.Parse(z.Amount)));

        CreateMap<PaymentQuery, QueryPaymentRequest>();
        CreateMap<QueryPaymentResponse, QueryPaymentResult>()
            .ForMember(x => x.Amount, y => y.MapFrom(z => float.Parse(z.Amount)));

        CreateMap<RefundPaymentCommand, RefundPaymentRequest>()
            .ForMember(x => x.Amount, y => y.MapFrom(z => z.Amount.ToString()));
        CreateMap<RefundPaymentResponse, RefundPaymentResult>()
            .ForMember(x => x.Amount, y => y.MapFrom(z => float.Parse(z.Amount)));
    }
}
