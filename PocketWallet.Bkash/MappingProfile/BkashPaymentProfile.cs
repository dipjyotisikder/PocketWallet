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
        CreateMap<CreatePaymentCommand, CreateBkashPaymentRequest>();
        CreateMap<CreateBkashPaymentResponse, CreatePaymentResult>();

        CreateMap<ExecutePaymentCommand, ExecuteBkashPaymentRequest>();
        CreateMap<ExecuteBkashPaymentResponse, ExecutePaymentResult>();

        CreateMap<PaymentQuery, QueryBkashPaymentRequest>();
        CreateMap<QueryBkashPaymentResponse, QueryPaymentResult>();

        CreateMap<RefundPaymentCommand, RefundBkashPaymentRequest>();
        CreateMap<RefundBkashPaymentResponse, RefundPaymentResult>();
    }
}
