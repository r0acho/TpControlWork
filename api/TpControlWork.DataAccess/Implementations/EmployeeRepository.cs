using Microsoft.EntityFrameworkCore;
using TpControlWork.DataAccess.Entities;
using TpControlWork.DataAccess.Interfaces;

namespace TpControlWork.DataAccess.Implementations;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _db;

    public EmployeeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task CreateAsync(Employee newEntity)
    {
        await SetNavigationPropsByExistingEntriesAsync(newEntity);
        await _db.AddAsync(newEntity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Employee deletingEntity)
    {
        _db.Remove(deletingEntity);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _db.Employees
            .Include(x => x.Earnings)
            .Include(x => x.FkEmployeeType)
            .Include(x => x.FkPaymentType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetByIdsAsync(params int[] ids)
    {
        return await _db.Employees
            .Include(x => x.FkEmployeeType)
            .Include(x => x.FkPaymentType)
            .Include(x => x.FkPaymentType)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task UpdateAsync(Employee updatedEntity)
    {
        await SetNavigationPropsByExistingEntriesAsync(updatedEntity);
        _db.Update(updatedEntity);
        await _db.SaveChangesAsync();
    }

    private async Task SetNavigationPropsByExistingEntriesAsync(Employee employee)
    {
        var employeeType = await _db.EmployeeTypes.FirstOrDefaultAsync(type => 
            type.Name == employee.FkEmployeeType.Name);

        if (employeeType is not null)
        {
            employee.FkEmployeeType = employeeType;
        }

        var paymentType = await _db.PaymentTypes.FirstOrDefaultAsync(type =>
            type.HourlyRate == employee.FkPaymentType.HourlyRate
            && type.HoursWorked == employee.FkPaymentType.HoursWorked
            && type.MonthlySalary == employee.FkPaymentType.MonthlySalary
            && type.RatePerPiece == employee.FkPaymentType.RatePerPiece
            && type.NumberOfPieces == employee.FkPaymentType.NumberOfPieces);

        if (paymentType is not null)
        {
            employee.FkPaymentType = paymentType;
        }
    }
}
