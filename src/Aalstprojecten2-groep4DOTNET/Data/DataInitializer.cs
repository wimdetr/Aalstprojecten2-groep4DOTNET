using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aalstprojecten2_groep4DOTNET.Models;
using Aalstprojecten2_groep4DOTNET.Models.Domein;
using Microsoft.AspNetCore.Identity;

namespace Aalstprojecten2_groep4DOTNET.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task InitializeData()
        {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                _context.Doelgroepen.Add(new Doelgroep("werknemer minder dan 25 jaar laaggeschoold", 1550, 2500, false));
                _context.Doelgroepen.Add(new Doelgroep("werknemer minder dan 25 jaar middengeschoold", 1000, 2500, false));
                _context.Doelgroepen.Add(new Doelgroep("werknemer meer of gelijk aan 55 of minder dan 60 jaar", 1150, 4466.66, false));
                _context.Doelgroepen.Add(new Doelgroep("werknemer meer of evenveel als 60 jaar", 1500, 4466.66, false));
                _context.Doelgroepen.Add(new Doelgroep("ander", 0, 0, false));

                JobCoach mark = new JobCoach("De Witte", "Andreas", "andreas.dewitte@hotmail.com", "Kairos", "Zevekootstraat", 129, 9420, "Erpe");
                mark.MoetWachtwoordVeranderen = false;
                _context.JobCoaches.Add(mark);

                Analyse a = new Analyse(mark, DateTime.Now.AddDays(-5));

                KostOfBaat k1 = new KostOfBaat(a, 1, KOBEnum.Kost, Formule.FormuleKost1);
                KOBRij k1Rij1 = new KOBRij(k1, 1);
                KOBVak k1R1Vak1 = new KOBVak(k1Rij1, 1, "programmeur");
                KOBVak k1R1Vak2 = new KOBVak(k1Rij1, 2, "10");
                KOBVak k1R1Vak3 = new KOBVak(k1Rij1, 3, "3000");
                KOBVak k1R1Vak4 = new KOBVak(k1Rij1, 4, "werknemer minder dan 25 jaar middengeschoold");
                KOBVak k1R1Vak5 = new KOBVak(k1Rij1, 5, "30");
                k1Rij1.VulKOBVakIn(k1R1Vak1);
                k1Rij1.VulKOBVakIn(k1R1Vak2);
                k1Rij1.VulKOBVakIn(k1R1Vak3);
                k1Rij1.VulKOBVakIn(k1R1Vak4);
                k1Rij1.VulKOBVakIn(k1R1Vak5);
                KOBRij k1Rij2 = new KOBRij(k1, 2);
                KOBVak k1R2Vak1 = new KOBVak(k1Rij2, 1, "netwerker");
                KOBVak k1R2Vak2 = new KOBVak(k1Rij2, 2, "15");
                KOBVak k1R2Vak3 = new KOBVak(k1Rij2, 3, "2000,30");
                KOBVak k1R2Vak4 = new KOBVak(k1Rij2, 4, "werknemer minder dan 25 jaar laaggeschoold");
                KOBVak k1R2Vak5 = new KOBVak(k1Rij2, 5, "20");
                k1Rij2.VulKOBVakIn(k1R2Vak1);
                k1Rij2.VulKOBVakIn(k1R2Vak2);
                k1Rij2.VulKOBVakIn(k1R2Vak3);
                k1Rij2.VulKOBVakIn(k1R2Vak4);
                k1Rij2.VulKOBVakIn(k1R2Vak5);
                k1.VulKOBRijIn(k1Rij1);
                k1.VulKOBRijIn(k1Rij2);
                a.SlaKostMetNummerOp(k1);

                KostOfBaat k2 = new KostOfBaat(a, 2, KOBEnum.Kost, Formule.FormuleGeefVak2);
                KOBRij k2Rij1 = new KOBRij(k2, 1);
                KOBVak k2R1Vak1 = new KOBVak(k2Rij1, 1, "voorbereidende kost");
                KOBVak k2R1Vak2 = new KOBVak(k2Rij1, 2, "500");
                k2Rij1.VulKOBVakIn(k2R1Vak1);
                k2Rij1.VulKOBVakIn(k2R1Vak2);
                k2.VulKOBRijIn(k2Rij1);
                a.SlaKostMetNummerOp(k2);

                KostOfBaat k3 = new KostOfBaat(a, 3, KOBEnum.Kost, Formule.FormuleGeefVak2);
                KOBRij k3Rij1 = new KOBRij(k3, 1);
                KOBVak k3R1Vak1 = new KOBVak(k3Rij1, 1, "kledij");
                KOBVak k3R1Vak2 = new KOBVak(k3Rij1, 2, "80");
                k3Rij1.VulKOBVakIn(k3R1Vak1);
                k3Rij1.VulKOBVakIn(k3R1Vak2);
                k3.VulKOBRijIn(k3Rij1);
                a.SlaKostMetNummerOp(k3);

                KostOfBaat k4 = new KostOfBaat(a, 4, KOBEnum.Kost, Formule.FormuleGeefVak2);
                KOBRij k4Rij1 = new KOBRij(k4, 1);
                KOBVak k4R1Vak1 = new KOBVak(k4Rij1, 1, "een kost");
                KOBVak k4R1Vak2 = new KOBVak(k4Rij1, 2, "50");
                k4Rij1.VulKOBVakIn(k4R1Vak1);
                k4Rij1.VulKOBVakIn(k4R1Vak2);
                k4.VulKOBRijIn(k4Rij1);
                KOBRij k4Rij2 = new KOBRij(k4, 2);
                KOBVak k4R2Vak1 = new KOBVak(k4Rij2, 1, "nog een kost");
                KOBVak k4R2Vak2 = new KOBVak(k4Rij2, 2, "150");
                k4Rij2.VulKOBVakIn(k4R2Vak1);
                k4Rij2.VulKOBVakIn(k4R2Vak2);
                k4.VulKOBRijIn(k4Rij2);
                KOBRij k4Rij3 = new KOBRij(k4, 3);
                KOBVak k4R3Vak1 = new KOBVak(k4Rij3, 1, "geen inspiratie");
                KOBVak k4R3Vak2 = new KOBVak(k4Rij3, 2, "180");
                k4Rij3.VulKOBVakIn(k4R3Vak1);
                k4Rij3.VulKOBVakIn(k4R3Vak2);
                k4.VulKOBRijIn(k4Rij1);
                a.SlaKostMetNummerOp(k4);

                KostOfBaat k5 = new KostOfBaat(a, 5, KOBEnum.Kost, Formule.FormuleGeefVak2);
                KOBRij k5Rij1 = new KOBRij(k5, 1);
                KOBVak k5R1Vak1 = new KOBVak(k5Rij1, 1, "zitte dinges");
                KOBVak k5R1Vak2 = new KOBVak(k5Rij1, 2, "2467");
                k5Rij1.VulKOBVakIn(k5R1Vak1);
                k5Rij1.VulKOBVakIn(k5R1Vak2);
                k5.VulKOBRijIn(k5Rij1);
                a.SlaKostMetNummerOp(k5);

                KostOfBaat k6 = new KostOfBaat(a, 6, KOBEnum.Kost, Formule.FormuleKost6);
                KOBRij k6Rij1 = new KOBRij(k6, 1);
                KOBVak k6R1Vak2 = new KOBVak(k6Rij1, 1, "2467");
                KOBVak k6R1Vak3 = new KOBVak(k6Rij1, 2, "2467");
                k6Rij1.VulKOBVakIn(k6R1Vak2);
                k6Rij1.VulKOBVakIn(k6R1Vak3);
                k6.VulKOBRijIn(k6Rij1);
                a.SlaKostMetNummerOp(k6);

                KostOfBaat k7 = new KostOfBaat(a, 7, KOBEnum.Kost, Formule.FormuleGeefVak2);
                KOBRij k7Rij1 = new KOBRij(k7, 1);
                KOBVak k7R1Vak1 = new KOBVak(k7Rij1, 1, "haarkleuring");
                KOBVak k7R1Vak2 = new KOBVak(k7Rij1, 2, "37");
                k7Rij1.VulKOBVakIn(k7R1Vak1);
                k7Rij1.VulKOBVakIn(k7R1Vak2);
                k7.VulKOBRijIn(k7Rij1);
                a.SlaKostMetNummerOp(k7);


                KostOfBaat b1 = new KostOfBaat(a, 1, KOBEnum.Baat, Formule.FormuleBaat1);
                KOBRij b1Rij1 = new KOBRij(b1, 1);
                KOBVak b1R1Vak1 = new KOBVak(b1Rij1, 1, "3");
                KOBVak b1R1Vak2 = new KOBVak(b1Rij1, 2, "500");
                b1Rij1.VulKOBVakIn(b1R1Vak1);
                b1Rij1.VulKOBVakIn(b1R1Vak2);
                b1.VulKOBRijIn(b1Rij1);
                KOBRij b1Rij2 = new KOBRij(b1, 2);
                KOBVak b1R2Vak1 = new KOBVak(b1Rij2, 1, "5");
                KOBVak b1R2Vak2 = new KOBVak(b1Rij2, 2, "800");
                b1Rij2.VulKOBVakIn(b1R2Vak1);
                b1Rij2.VulKOBVakIn(b1R2Vak2);
                b1.VulKOBRijIn(b1Rij2);
                a.SlaBaatMetNummerOp(b1);

                KostOfBaat b2 = new KostOfBaat(a, 2, KOBEnum.Baat, Formule.FormuleGeefVak1);
                KOBRij b2Rij1 = new KOBRij(b2, 1);
                KOBVak b2R1Vak1 = new KOBVak(b2Rij1, 1, "15000");
                b2Rij1.VulKOBVakIn(b2R1Vak1);
                b2.VulKOBRijIn(b2Rij1);
                a.SlaBaatMetNummerOp(b2);

                KostOfBaat b3 = new KostOfBaat(a, 3, KOBEnum.Baat, Formule.FormuleBaat3En4);
                KOBRij b3Rij1 = new KOBRij(b3, 1);
                KOBVak b3R1Vak1 = new KOBVak(b3Rij1, 1, "25");
                KOBVak b3R1Vak2 = new KOBVak(b3Rij1, 2, "2900");
                b3Rij1.VulKOBVakIn(b3R1Vak1);
                b3Rij1.VulKOBVakIn(b3R1Vak2);
                b3.VulKOBRijIn(b3Rij1);
                a.SlaBaatMetNummerOp(b3);

                KostOfBaat b4 = new KostOfBaat(a, 4, KOBEnum.Baat, Formule.FormuleBaat3En4);
                KOBRij b4Rij1 = new KOBRij(b4, 1);
                KOBVak b4R1Vak1 = new KOBVak(b4Rij1, 1, "5");
                KOBVak b4R1Vak2 = new KOBVak(b4Rij1, 2, "6000");
                b4Rij1.VulKOBVakIn(b4R1Vak1);
                b4Rij1.VulKOBVakIn(b4R1Vak2);
                b4.VulKOBRijIn(b4Rij1);
                KOBRij b4Rij2 = new KOBRij(b4, 2);
                KOBVak b4R2Vak1 = new KOBVak(b4Rij2, 1, "10");
                KOBVak b4R2Vak2 = new KOBVak(b4Rij2, 2, "4000");
                b4Rij2.VulKOBVakIn(b4R2Vak1);
                b4Rij2.VulKOBVakIn(b4R2Vak2);
                b4.VulKOBRijIn(b4Rij2);
                KOBRij b4Rij3 = new KOBRij(b4, 3);
                KOBVak b4R3Vak1 = new KOBVak(b4Rij3, 1, "18");
                KOBVak b4R3Vak2 = new KOBVak(b4Rij3, 2, "3200");
                b4Rij3.VulKOBVakIn(b4R3Vak1);
                b4Rij3.VulKOBVakIn(b4R3Vak2);
                b4.VulKOBRijIn(b4Rij3);
                a.SlaBaatMetNummerOp(b4);

                KostOfBaat b5 = new KostOfBaat(a, 5, KOBEnum.Baat, Formule.FormuleGeefVak2);
                KOBRij b5Rij1 = new KOBRij(b5, 1);
                KOBVak b5R1Vak1 = new KOBVak(b5Rij1, 1, "uitzend besparing");
                KOBVak b5R1Vak2 = new KOBVak(b5Rij1, 2, "11010");
                b5Rij1.VulKOBVakIn(b5R1Vak1);
                b5Rij1.VulKOBVakIn(b5R1Vak2);
                b5.VulKOBRijIn(b5Rij1);
                a.SlaBaatMetNummerOp(b5);

                KostOfBaat b6 = new KostOfBaat(a, 6, KOBEnum.Baat, Formule.FormuleVermenigvuldigVak1En2);
                KOBRij b6Rij1 = new KOBRij(b6, 1);
                KOBVak b6R1Vak1 = new KOBVak(b6Rij1, 1, "12300");
                KOBVak b6R1Vak2 = new KOBVak(b6Rij1, 2, "10");
                b6Rij1.VulKOBVakIn(b6R1Vak1);
                b6Rij1.VulKOBVakIn(b6R1Vak2);
                b6.VulKOBRijIn(b6Rij1);
                a.SlaBaatMetNummerOp(b6);

                KostOfBaat b7 = new KostOfBaat(a, 7, KOBEnum.Baat, Formule.FormuleGeefVak1);
                KOBRij b7Rij1 = new KOBRij(b7, 1);
                KOBVak b7R1Vak1 = new KOBVak(b7Rij1, 1, "500");
                b7Rij1.VulKOBVakIn(b7R1Vak1);
                b7.VulKOBRijIn(b7Rij1);
                a.SlaBaatMetNummerOp(b7);

                KostOfBaat b8 = new KostOfBaat(a, 8, KOBEnum.Baat, Formule.FormuleGeefVak1);
                KOBRij b8Rij1 = new KOBRij(b8, 1);
                KOBVak b8R1Vak1 = new KOBVak(b8Rij1, 1, "890");
                b8Rij1.VulKOBVakIn(b8R1Vak1);
                b8.VulKOBRijIn(b8Rij1);
                a.SlaBaatMetNummerOp(b8);

                KostOfBaat b9 = new KostOfBaat(a, 9, KOBEnum.Baat, Formule.FormuleGeefVak2);
                KOBRij b9Rij1 = new KOBRij(b9, 1);
                KOBVak b9R1Vak1 = new KOBVak(b9Rij1, 1, "schoonmaak");
                KOBVak b9R1Vak2 = new KOBVak(b9Rij1, 2, "800");
                b9Rij1.VulKOBVakIn(b9R1Vak1);
                b9Rij1.VulKOBVakIn(b9R1Vak2);
                b9.VulKOBRijIn(b9Rij1);
                KOBRij b9Rij2 = new KOBRij(b9, 2);
                KOBVak b9R2Vak1 = new KOBVak(b9Rij2, 1, "souvenirs");
                KOBVak b9R2Vak2 = new KOBVak(b9Rij2, 2, "2300");
                b9Rij2.VulKOBVakIn(b9R2Vak1);
                b9Rij2.VulKOBVakIn(b9R2Vak2);
                b9.VulKOBRijIn(b9Rij2);
                a.SlaBaatMetNummerOp(b9);

                KostOfBaat b10 = new KostOfBaat(a, 10, KOBEnum.Baat, Formule.FormuleSomVak1En2);
                KOBRij b10Rij1 = new KOBRij(b10, 1);
                KOBVak b10R1Vak1 = new KOBVak(b10Rij1, 1, "12");
                KOBVak b10R1Vak2 = new KOBVak(b10Rij1, 2, "54");
                b10Rij1.VulKOBVakIn(b10R1Vak1);
                b10Rij1.VulKOBVakIn(b10R1Vak2);
                b10.VulKOBRijIn(b10Rij1);
                a.SlaBaatMetNummerOp(b10);

                KostOfBaat b11 = new KostOfBaat(a, 11, KOBEnum.Baat, Formule.FormuleGeefVak2);
                KOBRij b11Rij1 = new KOBRij(b11, 1);
                KOBVak b11R1Vak1 = new KOBVak(b11Rij1, 1, "warm water");
                KOBVak b11R1Vak2 = new KOBVak(b11Rij1, 2, "900");
                b11Rij1.VulKOBVakIn(b11R1Vak1);
                b11Rij1.VulKOBVakIn(b11R1Vak2);
                b11.VulKOBRijIn(b11Rij1);
                a.SlaBaatMetNummerOp(b11);

                mark.VoegAnalyseToe(a);
                _context.Analyses.Add(a);
                Werkgever w1 = new Werkgever(mark.Email, "Carrefour");
                Departement d1 = new Departement(a, w1, "IT", "Zevekootstraat", 129, null, 9420, "Erpe")
                {
                    ContactPersoonNaam = "De Troyer",
                    ContactPersoonVoornaam = "Wim",
                    ContactPersoonEmail = "andreas.dewitte@hotmail.com"
                };
                _context.Werkgevers.Add(w1);
                _context.Departementen.Add(d1);

                Werkgever b = new Werkgever(mark.Email, "Delhaize");
                _context.Werkgevers.Add(b);

                Werkgever c = new Werkgever(mark.Email, "Dreamland");
                _context.Werkgevers.Add(c);


                Admin admin = new Admin("bartmoens@gmail.com", "Moens", "Bart");
                admin.Superadmin = true;

                InterneMail mail1 = new InterneMail(admin, "welkom", "Welkom op kairos", DateTime.Now.AddDays(-2));
                InterneMail mail2 = new InterneMail(admin, "Hallo", "Hallo, nog wat\n\n\n tekst", DateTime.Now);
                InterneMail mail3 = new InterneMail(admin, "welkom", "Welkom op kairos", DateTime.Now.AddDays(-2));
                InterneMail mail4 = new InterneMail(admin, "Hallo", "Hallo, nog wat tekst", DateTime.Now);
                InterneMail mail5 = new InterneMail(admin, "welkom", "Welkom op kairos", DateTime.Now.AddDays(-2));
                InterneMail mail6 = new InterneMail(admin, "Hallo", "Hallo, nog wat tekst", DateTime.Now);
                InterneMail mail7 = new InterneMail(admin, "welkom", "Welkom op kairos", DateTime.Now.AddDays(-10));
                InterneMail mail8 = new InterneMail(admin, "Hallo", "Hallo, nog wat tekst", DateTime.Now);

                mark.AddMail(mail1);
                mark.AddMail(mail3);
                mark.AddMail(mail5);
                mark.AddMail(mail6);
                foreach (InterneMailJobcoach i in mark.InterneMailJobcoaches)
                {
                    i.IsGelezen = true;
                }
                mark.AddMail(mail2);
                mark.AddMail(mail4);
                mark.AddMail(mail7);
                mark.AddMail(mail8);
                AdminMail adminMail = new AdminMail(mark, admin,
                    "admin mail", "dit is een mail voor de admin", DateTime.Now);
                _context.AdminMails.Add(adminMail);

                

                _context.SaveChanges();

                ApplicationUser user1 = new ApplicationUser { UserName = mark.Email, Email = mark.Email, Naam = "De Witte", Voornaam = "Andreas"};
                await _userManager.CreateAsync(user1, "woopwoop");

                
            }
        }
    }
}
