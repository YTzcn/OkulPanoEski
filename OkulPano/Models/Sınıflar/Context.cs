using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OkulPano.Models.Sınıflar
{
    public class Context : DbContext
    {
        public DbSet<Okul> Okuls { get; set; }
        public DbSet<Öğretmen> Öğretmens { get; set; }
        public DbSet<Resim> Resims { get; set; }
        public DbSet<NöbertYer> NöbertYers { get; set; }
        public DbSet<KayanResim> KayanResims { get; set; }
    }
}