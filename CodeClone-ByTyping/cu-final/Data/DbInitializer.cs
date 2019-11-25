﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BerkayMarket.Models;

namespace BerkayMarket.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MarketContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any Musteris.
            if (context.Musteris.Any())
            {
                return;   // DB has been seeded
            }

            var Musteris = new Musteri[]
            {
                new Musteri { FirstMidName = "Carson",   LastName = "Alexander",
                    SiparisDate = DateTime.Parse("2010-09-01") },
                new Musteri { FirstMidName = "Meredith", LastName = "Alonso",
                    SiparisDate = DateTime.Parse("2012-09-01") },
                new Musteri { FirstMidName = "Arturo",   LastName = "Anand",
                    SiparisDate = DateTime.Parse("2013-09-01") },
                new Musteri { FirstMidName = "Gytis",    LastName = "Barzdukas",
                    SiparisDate = DateTime.Parse("2012-09-01") },
                new Musteri { FirstMidName = "Yan",      LastName = "Li",
                    SiparisDate = DateTime.Parse("2012-09-01") },
                new Musteri { FirstMidName = "Peggy",    LastName = "Justice",
                    SiparisDate = DateTime.Parse("2011-09-01") },
                new Musteri { FirstMidName = "Laura",    LastName = "Norman",
                    SiparisDate = DateTime.Parse("2013-09-01") },
                new Musteri { FirstMidName = "Nino",     LastName = "Olivetto",
                    SiparisDate = DateTime.Parse("2005-09-01") }
            };

            foreach (Musteri s in Musteris)
            {
                context.Musteris.Add(s);
            }
            context.SaveChanges();

            var Saticis = new Satici[]
            {
                new Satici { FirstMidName = "Kim",     LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Satici { FirstMidName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Satici { FirstMidName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Satici { FirstMidName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Satici { FirstMidName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };

            foreach (Satici i in Saticis)
            {
                context.Saticis.Add(i);
            }
            context.SaveChanges();

            var Kategoris = new Kategori[]
            {
                new Kategori { Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    SaticiID  = Saticis.Single( i => i.LastName == "Abercrombie").ID },
                new Kategori { Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    SaticiID  = Saticis.Single( i => i.LastName == "Fakhouri").ID },
                new Kategori { Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    SaticiID  = Saticis.Single( i => i.LastName == "Harui").ID },
                new Kategori { Name = "Economics",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    SaticiID  = Saticis.Single( i => i.LastName == "Kapoor").ID }
            };

            foreach (Kategori d in Kategoris)
            {
                context.Kategoris.Add(d);
            }
            context.SaveChanges();

            var Uruns = new Urun[]
            {
                new Urun {UrunID = 1050, Title = "Chemistry",      Credits = 3,
                    KategoriID = Kategoris.Single( s => s.Name == "Engineering").KategoriID
                },
                new Urun {UrunID = 4022, Title = "Microeconomics", Credits = 3,
                    KategoriID = Kategoris.Single( s => s.Name == "Economics").KategoriID
                },
                new Urun {UrunID = 4041, Title = "Macroeconomics", Credits = 3,
                    KategoriID = Kategoris.Single( s => s.Name == "Economics").KategoriID
                },
                new Urun {UrunID = 1045, Title = "Calculus",       Credits = 4,
                    KategoriID = Kategoris.Single( s => s.Name == "Mathematics").KategoriID
                },
                new Urun {UrunID = 3141, Title = "Trigonometry",   Credits = 4,
                    KategoriID = Kategoris.Single( s => s.Name == "Mathematics").KategoriID
                },
                new Urun {UrunID = 2021, Title = "Composition",    Credits = 3,
                    KategoriID = Kategoris.Single( s => s.Name == "English").KategoriID
                },
                new Urun {UrunID = 2042, Title = "Literature",     Credits = 4,
                    KategoriID = Kategoris.Single( s => s.Name == "English").KategoriID
                },
            };

            foreach (Urun c in Uruns)
            {
                context.Uruns.Add(c);
            }
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment {
                    SaticiID = Saticis.Single( i => i.LastName == "Fakhouri").ID,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    SaticiID = Saticis.Single( i => i.LastName == "Harui").ID,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    SaticiID = Saticis.Single( i => i.LastName == "Kapoor").ID,
                    Location = "Thompson 304" },
            };

            foreach (OfficeAssignment o in officeAssignments)
            {
                context.OfficeAssignments.Add(o);
            }
            context.SaveChanges();

            var UrunSaticis = new Envanter[]
            {
                new Envanter {
                    UrunID = Uruns.Single(c => c.Title == "Chemistry" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Kapoor").ID
                    },
                new Envanter {
                    UrunID = Uruns.Single(c => c.Title == "Chemistry" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Harui").ID
                    },
                new Envanter {
                    UrunID = Uruns.Single(c => c.Title == "Microeconomics" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Zheng").ID
                    },
                new Envanter {
                    UrunID = Uruns.Single(c => c.Title == "Macroeconomics" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Zheng").ID
                    },
                new Envanter {
                    UrunID = Uruns.Single(c => c.Title == "Calculus" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Fakhouri").ID
                    },
                new Envanter {
                    UrunID = Uruns.Single(c => c.Title == "Trigonometry" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Harui").ID
                    },
                new Envanter {
                    UrunID = Uruns.Single(c => c.Title == "Composition" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Abercrombie").ID
                    },
                new Envanter {
                    UrunID = Uruns.Single(c => c.Title == "Literature" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Abercrombie").ID
                    },
            };

            foreach (Envanter ci in UrunSaticis)
            {
                context.Envanters.Add(ci);
            }
            context.SaveChanges();

            var Sipariss = new Siparis[]
            {
                new Siparis {
                    MusteriID = Musteris.Single(s => s.LastName == "Alexander").ID,
                    UrunID = Uruns.Single(c => c.Title == "Chemistry" ).UrunID,
                    Grade = Grade.A
                },
                    new Siparis {
                    MusteriID = Musteris.Single(s => s.LastName == "Alexander").ID,
                    UrunID = Uruns.Single(c => c.Title == "Microeconomics" ).UrunID,
                    Grade = Grade.C
                    },
                    new Siparis {
                    MusteriID = Musteris.Single(s => s.LastName == "Alexander").ID,
                    UrunID = Uruns.Single(c => c.Title == "Macroeconomics" ).UrunID,
                    Grade = Grade.B
                    },
                    new Siparis {
                        MusteriID = Musteris.Single(s => s.LastName == "Alonso").ID,
                    UrunID = Uruns.Single(c => c.Title == "Calculus" ).UrunID,
                    Grade = Grade.B
                    },
                    new Siparis {
                        MusteriID = Musteris.Single(s => s.LastName == "Alonso").ID,
                    UrunID = Uruns.Single(c => c.Title == "Trigonometry" ).UrunID,
                    Grade = Grade.B
                    },
                    new Siparis {
                    MusteriID = Musteris.Single(s => s.LastName == "Alonso").ID,
                    UrunID = Uruns.Single(c => c.Title == "Composition" ).UrunID,
                    Grade = Grade.B
                    },
                    new Siparis {
                    MusteriID = Musteris.Single(s => s.LastName == "Anand").ID,
                    UrunID = Uruns.Single(c => c.Title == "Chemistry" ).UrunID
                    },
                    new Siparis {
                    MusteriID = Musteris.Single(s => s.LastName == "Anand").ID,
                    UrunID = Uruns.Single(c => c.Title == "Microeconomics").UrunID,
                    Grade = Grade.B
                    },
                new Siparis {
                    MusteriID = Musteris.Single(s => s.LastName == "Barzdukas").ID,
                    UrunID = Uruns.Single(c => c.Title == "Chemistry").UrunID,
                    Grade = Grade.B
                    },
                    new Siparis {
                    MusteriID = Musteris.Single(s => s.LastName == "Li").ID,
                    UrunID = Uruns.Single(c => c.Title == "Composition").UrunID,
                    Grade = Grade.B
                    },
                    new Siparis {
                    MusteriID = Musteris.Single(s => s.LastName == "Justice").ID,
                    UrunID = Uruns.Single(c => c.Title == "Literature").UrunID,
                    Grade = Grade.B
                    }
            };

            foreach (Siparis e in Sipariss)
            {
                var SiparisInDataBase = context.Sipariss.Where(
                    s =>
                            s.Musteri.ID == e.MusteriID &&
                            s.Urun.UrunID == e.UrunID).SingleOrDefault();
                if (SiparisInDataBase == null)
                {
                    context.Sipariss.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}