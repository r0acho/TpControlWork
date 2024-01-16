using AutoMapper;
using TpControlWork.Domain.Enums;
using TpControlWork.Domain.Models;
using TpControlWork.Domain.Models.Earnings;
using TpControlWork.Domain.Models.PaymentTypes;
using TpControlWork.Services.Interfaces;

namespace TpControlWork.Services.Implementations;

public class EmployeeDataAccessToDomainAdapterService : IEmployeeDataAccessToDomainAdapterService
{
    private readonly IMapper _mapper;

    private const string _paymentTypeIsNotFound = "Не удалось определить тип получаемой заработной платы";
    private const string _earningTypeIsNotFound = "Не удалось определить тип вознаграждения";

    public EmployeeDataAccessToDomainAdapterService()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DataAccess.Entities.Employee, Employee>()
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => MapEmployeeType(src.FkEmployeeType)))
                .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => MapPaymentType(src.FkPaymentType)))
                .ForMember(dest => dest.Earnings, opt => opt.MapFrom(src => MapEarnings(src.Earnings)));
        }).CreateMapper();
    }

    public Employee ConvertToDomainEmployee(DataAccess.Entities.Employee employeeFromDataAccess)
    {
        return _mapper.Map<Employee>(employeeFromDataAccess);
    }

    private EEmployeeType MapEmployeeType(DataAccess.Entities.EmployeeType employeeTypeFromDataAccess)
    {
        return (EEmployeeType)employeeTypeFromDataAccess.Id;
    }

    private PaymentType MapPaymentType(DataAccess.Entities.PaymentType paymentTypeFromDataAccess)
    {
        if (paymentTypeFromDataAccess.HoursWorked is not null
            && paymentTypeFromDataAccess.HourlyRate is not null)
        {
            return new HourlyPayment
            {
                HourlyRate = paymentTypeFromDataAccess.HourlyRate.Value,
                HoursWorked = paymentTypeFromDataAccess.HoursWorked.Value
            };
        }
        else if (paymentTypeFromDataAccess.RatePerPiece is not null
            && paymentTypeFromDataAccess.NumberOfPieces is not null)
        {
            return new PieceRatePayment
            {
                RatePerPiece = paymentTypeFromDataAccess.RatePerPiece.Value,
                NumberOfPieces = paymentTypeFromDataAccess.NumberOfPieces.Value
            };
        }
        else if (paymentTypeFromDataAccess.MonthlySalary is not null)
        {
            return new SalaryPayment
            {
                MonthlySalary = paymentTypeFromDataAccess.MonthlySalary.Value
            };
        }

        throw new ArgumentException(_paymentTypeIsNotFound);
    }

    private List<Earning> MapEarnings(IEnumerable<DataAccess.Entities.Earning> earningsFromDataAccess)
    {
        return earningsFromDataAccess.Select<DataAccess.Entities.Earning, Earning>(earningFromDataAccess =>
        {
            if (earningFromDataAccess.OvertimeHours is not null
            && earningFromDataAccess.OvertimeRate is not null)
            {
                return new OvertimeEarnings
                {
                    OvertimeRate = earningFromDataAccess.OvertimeRate.Value,
                    OvertimeHours = earningFromDataAccess.OvertimeHours.Value
                };
            }
            else if (earningFromDataAccess.BonusAmount is not null)
            {
                return new BonusEarnings
                {
                    BonusAmount = earningFromDataAccess.BonusAmount.Value
                };
            }
            throw new ArgumentException(_earningTypeIsNotFound);
        }).ToList();
    }
}
