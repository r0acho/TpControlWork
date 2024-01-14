using TpControlWork.Services.Interfaces;
using TpControlWork.Domain.Models;
using TpControlWork.Domain.Enums;
using AutoMapper;
using TpControlWork.Domain.Models.Earnings;
using TpControlWork.Domain.Models.PaymentTypes;
using System.Linq;

namespace TpControlWork.Services.Implementations;

public class EmployeeAdapterService : IEmployeeAdapterService
{
    private readonly IMapper _mapper;

    private const string _paymentTypeIsNotFound = "Не удалось определить тип получаемой заработной платы";
    private const string _earningTypeIsNotFound = "Не удалось определить тип вознаграждения";

    public EmployeeAdapterService()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Employee, DataAccess.Entities.Employee>()
                    .ForMember(dest => dest.FkEmployeeType, opt => opt.MapFrom(src => MapEmployeeType(src.EmployeeType)))
                    .ForMember(dest => dest.FkPaymentType, opt => opt.MapFrom(src => MapPaymentType(src.PaymentType)))
                    .ForMember(dest => dest.Earnings, opt => opt.MapFrom(src => MapEarnings(src.Earnings, src.Id)));

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

    public DataAccess.Entities.Employee ConvertToDataAccessEmployee(Employee employeeFromDomain)
    {
        return _mapper.Map<DataAccess.Entities.Employee>(employeeFromDomain);
    }

    private static DataAccess.Entities.EmployeeType MapEmployeeType(EEmployeeType employeeTypeFromDomain)
    {
        return new DataAccess.Entities.EmployeeType { 
            Name = employeeTypeFromDomain.ToString() 
        };
    }

    private DataAccess.Entities.PaymentType MapPaymentType(PaymentType paymentTypeFromDomain)
    {
        if (paymentTypeFromDomain is HourlyPayment hourlyPayment)
        {
            return new DataAccess.Entities.PaymentType
            {
                HourlyRate = hourlyPayment.HourlyRate,
                HoursWorked = hourlyPayment.HoursWorked
            };
        }
        else if (paymentTypeFromDomain is PieceRatePayment pieceRatePayment)
        {
            return new DataAccess.Entities.PaymentType
            {
                RatePerPiece = pieceRatePayment.RatePerPiece,
                NumberOfPieces = pieceRatePayment.NumberOfPieces
            };
        }
        else if (paymentTypeFromDomain is SalaryPayment salaryPayment)
        {
            return new DataAccess.Entities.PaymentType
            {
                MonthlySalary = salaryPayment.MonthlySalary
            };
        }

        throw new ArgumentException(_paymentTypeIsNotFound);
    }

    private static ICollection<DataAccess.Entities.Earning> MapEarnings(List<Earning> earningsFromDomain, int employeeId)
    {
        return earningsFromDomain.Select(earningFromDomain =>
        {
            if (earningFromDomain is OvertimeEarnings overtimeEarnings)
            {
                return new DataAccess.Entities.Earning
                {
                    FkEmployeeId = employeeId,
                    OvertimeRate = overtimeEarnings.OvertimeRate,
                    OvertimeHours = overtimeEarnings.OvertimeHours
                };
            }
            else if (earningFromDomain is BonusEarnings bonusEarnings)
            {
                return new DataAccess.Entities.Earning
                {
                    FkEmployeeId = employeeId,
                    OvertimeRate = bonusEarnings.BonusAmount,
                };
            }
            throw new ArgumentException(_earningTypeIsNotFound);
        }).ToList();
    }

    //-----код с dataAccess -> domain

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

