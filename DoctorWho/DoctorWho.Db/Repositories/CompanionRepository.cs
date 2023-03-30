using DoctorWhoDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWho.Db.Repositories
{
    public class CompanionRepository
    {
        DoctorWhoDbContext _context = new DoctorWhoDbContext();
        public void Update(int CompanionId, string CompanionName)
        {
            var companion = _context.Companions.Find(CompanionId);
            if (companion != null)
                companion.CompanionName = CompanionName;
            _context.SaveChanges();
        }

        public void Delete(int CompanionId)
        {
            var author = _context.Companions.Find(CompanionId);
            _context.Companions.FromSqlInterpolated($"DELETE FROM Companions WHERE CompanionId = {CompanionId}");
            _context.SaveChanges();
        }

        public void Add(string CompanionName, string WhoPlayed)
        {
            _context.Companions.Add(new Companion { CompanionName = CompanionName, WhoPlayed = WhoPlayed});
            _context.SaveChanges();
        }
    }
}
