using MappingTheMBTA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MappingTheMBTA.Data
{
    public class DatasetContext : DbContext
    {
        public DbSet<Dataset> Datasets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=datasets.db");
        }
    }

    public static class Database
    {
        public static void Capture()
        {
            Console.WriteLine($"{DateTime.Now} | ***********************************");
            Console.WriteLine($"{DateTime.Now} | *      SAVING DATASETS TO DB      *");
            Console.WriteLine($"{DateTime.Now} | ***********************************");
            int effective = DateTime.Now.ConvertToEffective();

            using (var db = new DatasetContext())
            {
                // if the data set exists, update it
                // otherwise, insert it
                var single = db.Datasets.SingleOrDefault(x => x.EffectiveDate == effective);
                if (single != default)
                    single = Sources.Today;
                else
                    db.Datasets.Add(Sources.Today);

                db.SaveChanges();
            }
        }

        public static Dataset Retrieve(int effective)
        {
            using (var db = new DatasetContext())
            {
                Console.WriteLine($"{DateTime.Now} | SINGLE (x => x.EffectiveDate =={effective})");
                // if the date exists, return it
                // otherwise, return empty set
                var single = db.Datasets.SingleOrDefault(x => x.EffectiveDate == effective);
                if (single != default)
                    return single;
                else
                    return new Dataset() { EffectiveDate = effective };
            }
        }

        public static List<int> GetDates()
        {
            using (var db = new DatasetContext())
            {
                Console.WriteLine($"{DateTime.Now} | SELECT (x => x.EffectiveDate)");
                // a list of every stored dataset's date
                return db.Datasets.Select(x => x.EffectiveDate).ToList();
            }
        }
    }
}
