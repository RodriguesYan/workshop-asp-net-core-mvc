using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SalesRecordsService
    {
        private readonly SalesWebMVCContext _context;

        public SalesRecordsService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public List<SalesRecord> FindByDate(DateTime? min, DateTime? max)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (min.HasValue)
            {
                result = result.Where(t => t.Date >= min.Value);
            }
            if (max.HasValue)
            {
                result = result.Where(t => t.Date <= max.Value);
            }

            return result
                .Include(obj => obj.Seller)
                .Include(obj => obj.Seller.Department)
                .OrderByDescending(t => t.Date)
                .ToList();
        }

        public List<IGrouping<Department, SalesRecord>> FindByDateGrouping(DateTime? min, DateTime? max)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (min.HasValue)
            {
                result = result.Where(t => t.Date >= min.Value);
            }
            if (max.HasValue)
            {
                result = result.Where(t => t.Date <= max.Value);
            }

            return result
                .Include(obj => obj.Seller)
                .Include(obj => obj.Seller.Department)
                .OrderByDescending(t => t.Date)
                .GroupBy(t => t.Seller.Department)
                .ToList();
        }
    }
}
