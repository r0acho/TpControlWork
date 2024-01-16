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
            .Include(x => x.FkEmployeeType)
            .Include(x => x.FkPaymentType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetByIdsAsync(params int[] ids)
    {
        return await _db.Employees
            .Include(x => x.FkEmployeeType)
            .Include(x => x.FkPaymentType)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task UpdateAsync(Employee updatedEntity)
    {
        _db.Update(updatedEntity);
        await _db.SaveChangesAsync();
    }
}
