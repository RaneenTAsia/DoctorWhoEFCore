using DoctorWhoDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWho.Db.Repositories
{
    public class EnemyRepository
    {
        DoctorWhoDbContext _context = new DoctorWhoDbContext();
        public void Update(int EnemyId, string EnemyName, string? Description)
        {
            var enemy = _context.Enemies.Find(EnemyId);
            if (enemy != null)
            {
                enemy.EnemyName = EnemyName;
                enemy.Description = Description;
            }
            _context.SaveChanges();
        }

        public void Delete(int EnemyId)
        {
            var author = _context.Enemies.Find(EnemyId);
            _context.Enemies.FromSqlInterpolated($"DELETE FROM Enemies WHERE EnemyId = {EnemyId}");
            _context.SaveChanges();
        }

        public void Add(string EnemyName, string? Description)
        {
            _context.Enemies.Add(new Enemy{ EnemyName = EnemyName, Description = Description });
            _context.SaveChanges();
        }
    }
}
