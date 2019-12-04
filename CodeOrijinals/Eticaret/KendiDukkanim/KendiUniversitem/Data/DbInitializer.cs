#define Kategori2
#define Satici2
#define Final // or Intro

#if Intro
#region snippet_Intro
using KendiDukkanim.Models;
using System;
using System.Linq;

namespace KendiDukkanim.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Musteris.
            if (context.Musteris.Any())
            {
                return;   // DB has been seeded
            }

            var Musteris = new Musteri[]
            {
            new Musteri{FirstMidName="Carson",LastName="Alexander",SiparisDate=DateTime.Parse("2005-09-01")},
            new Musteri{FirstMidName="Meredith",LastName="Alonso",SiparisDate=DateTime.Parse("2002-09-01")},
            new Musteri{FirstMidName="Arturo",LastName="Anand",SiparisDate=DateTime.Parse("2003-09-01")},
            new Musteri{FirstMidName="Gytis",LastName="Barzdukas",SiparisDate=DateTime.Parse("2002-09-01")},
            new Musteri{FirstMidName="Yan",LastName="Li",SiparisDate=DateTime.Parse("2002-09-01")},
            new Musteri{FirstMidName="Peggy",LastName="Justice",SiparisDate=DateTime.Parse("2001-09-01")},
            new Musteri{FirstMidName="Laura",LastName="Norman",SiparisDate=DateTime.Parse("2003-09-01")},
            new Musteri{FirstMidName="Nino",LastName="Olivetto",SiparisDate=DateTime.Parse("2005-09-01")}
            };
            foreach (Musteri s in Musteris)
            {
                context.Musteris.Add(s);
            }
            context.SaveChanges();

            var Uruns = new Urun[]
            {
            new Urun{UrunID=1050,Title="Chemistry",Credits=3},
            new Urun{UrunID=4022,Title="Microeconomics",Credits=3},
            new Urun{UrunID=4041,Title="Macroeconomics",Credits=3},
            new Urun{UrunID=1045,Title="Calculus",Credits=4},
            new Urun{UrunID=3141,Title="Trigonometry",Credits=4},
            new Urun{UrunID=2021,Title="Composition",Credits=3},
            new Urun{UrunID=2042,Title="Literature",Credits=4}
            };
            foreach (Urun c in Uruns)
            {
                context.Uruns.Add(c);
            }
            context.SaveChanges();

            var Sipariss = new Siparis[]
            {
            new Siparis{MusteriID=1,UrunID=1050,Grade=Grade.A},
            new Siparis{MusteriID=1,UrunID=4022,Grade=Grade.C},
            new Siparis{MusteriID=1,UrunID=4041,Grade=Grade.B},
            new Siparis{MusteriID=2,UrunID=1045,Grade=Grade.B},
            new Siparis{MusteriID=2,UrunID=3141,Grade=Grade.F},
            new Siparis{MusteriID=2,UrunID=2021,Grade=Grade.F},
            new Siparis{MusteriID=3,UrunID=1050},
            new Siparis{MusteriID=4,UrunID=1050},
            new Siparis{MusteriID=4,UrunID=4022,Grade=Grade.F},
            new Siparis{MusteriID=5,UrunID=4041,Grade=Grade.C},
            new Siparis{MusteriID=6,UrunID=1045},
            new Siparis{MusteriID=7,UrunID=3141,Grade=Grade.A},
            };
            foreach (Siparis e in Sipariss)
            {
                context.Sipariss.Add(e);
            }
            context.SaveChanges();
        }
    }
}
#endregion

#elif Final
#region snippet_Final
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KendiDukkanim.Models;

namespace KendiDukkanim.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
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
#if Satici1

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
#elif Satici2
            var Saticis = new Satici[]
            {
                new Satici { FirstMidName = "Kim",     LastName = "Abercrombie" },
                new Satici { FirstMidName = "Fadi",    LastName = "Fakhouri" },
                new Satici { FirstMidName = "Roger",   LastName = "Harui" },
                new Satici { FirstMidName = "Candace", LastName = "Kapoor"},
                new Satici { FirstMidName = "Roger",   LastName = "Zheng", }
            };

#endif
            foreach (Satici i in Saticis)
            {
                context.Saticis.Add(i);
            }
            context.SaveChanges();
#if Kategori1

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
            var Dukkans = new Dukkan[]
            {
                new Dukkan {
                    SaticiID = Saticis.Single( i => i.LastName == "Fakhouri").ID,
                    Location = "Smith 17" },
                new Dukkan {
                    SaticiID = Saticis.Single( i => i.LastName == "Harui").ID,
                    Location = "Gowan 27" },
                new Dukkan {
                    SaticiID = Saticis.Single( i => i.LastName == "Kapoor").ID,
                    Location = "Thompson 304" },
            };

            foreach (Dukkan o in Dukkans)
            {
                context.Dukkans.Add(o);
            }
            context.SaveChanges();

#elif Kategori2
            var Kategoris = new Kategori[]
            {
                new Kategori { Adi = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01") },
                new Kategori { Adi = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01") },
                new Kategori { Adi = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01") },
                new Kategori { Adi = "Economics",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01") }
            };
            var Dukkans = new Dukkan[]
            {
                new Dukkan{Adi="DukkkanOnline", SaticiId=0, Yerleske="Sivas 58"},
                new Dukkan{Adi="BerkayTicaret", SaticiId=1, Yerleske="İstanbul 58"},
                new Dukkan{Adi="DukkkanOnline2", SaticiId=0, Yerleske="Sivas1 58"},
                new Dukkan{Adi="DukkkanOnline3", SaticiId=0, Yerleske="Sivas2 58"}
            };
            foreach (Dukkan item in Dukkans)
            {
                context.Dukkans.Add(item);
            }
            context.SaveChanges();
#endif
            foreach (Kategori d in Kategoris)
            {
                context.Kategoris.Add(d);
            }
            context.SaveChanges();

            var Uruns = new Urun[]
            {
                new Urun {UrunID = 1050, Title = "Chemistry",      Puani = 3,
                    KategoriID = Kategoris.Single( s => s.Adi == "Engineering").KategoriID
                },
                new Urun {UrunID = 4022, Title = "Microeconomics", Puani = 3,
                    KategoriID = Kategoris.Single( s => s.Adi == "Economics").KategoriID
                },
                new Urun {UrunID = 4041, Title = "Macroeconomics", Puani = 3,
                    KategoriID = Kategoris.Single( s => s.Adi == "Economics").KategoriID
                },
                new Urun {UrunID = 1045, Title = "Calculus",       Puani = 4,
                    KategoriID = Kategoris.Single( s => s.Adi == "Mathematics").KategoriID
                },
                new Urun {UrunID = 3141, Title = "Trigonometry",   Puani = 4,
                    KategoriID = Kategoris.Single( s => s.Adi == "Mathematics").KategoriID
                },
                new Urun {UrunID = 2021, Title = "Composition",    Puani = 3,
                    KategoriID = Kategoris.Single( s => s.Adi == "English").KategoriID
                },
                new Urun {UrunID = 2042, Title = "Literature",     Puani = 4,
                    KategoriID = Kategoris.Single( s => s.Adi == "English").KategoriID
                },
            };

            foreach (Urun c in Uruns)
            {
                context.Uruns.Add(c);
            }
            context.SaveChanges();

            var UrunSaticis = new Satis[]
            {
                new Satis {
                    UrunID = Uruns.Single(c => c.Title == "Chemistry" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Kapoor").ID
                    },
                new Satis {
                    UrunID = Uruns.Single(c => c.Title == "Chemistry" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Harui").ID
                    },
                new Satis {
                    UrunID = Uruns.Single(c => c.Title == "Microeconomics" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Zheng").ID
                    },
                new Satis {
                    UrunID = Uruns.Single(c => c.Title == "Macroeconomics" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Zheng").ID
                    },
                new Satis {
                    UrunID = Uruns.Single(c => c.Title == "Calculus" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Fakhouri").ID
                    },
                new Satis {
                    UrunID = Uruns.Single(c => c.Title == "Trigonometry" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Harui").ID
                    },
                new Satis {
                    UrunID = Uruns.Single(c => c.Title == "Composition" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Abercrombie").ID
                    },
                new Satis {
                    UrunID = Uruns.Single(c => c.Title == "Literature" ).UrunID,
                    SaticiID = Saticis.Single(i => i.LastName == "Abercrombie").ID
                    },
            };

            foreach (Satis ci in UrunSaticis)
            {
                context.Satiss.Add(ci);
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
#endregion
#endif