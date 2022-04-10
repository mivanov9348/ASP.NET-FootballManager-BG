namespace FootballManager.Infrastructure.Seeding.SeedingModels
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

    public class TeamsSeeder : ISeeder
    {
        public TeamsSeeder()
        {
        }

        public async Task SeedAsync(FootballManagerDbContext dbContext, IServiceProvider serviceProvider)
        {
            var bulgaria = dbContext.Nations.FirstOrDefault(x => x.Name == "Bulgaria");
            var firstleague = dbContext.Leagues.FirstOrDefault(x => x.NationId == bulgaria.Id && x.Level == 1);
            var secondLeague = dbContext.Leagues.FirstOrDefault(x => x.NationId == bulgaria.Id && x.Level == 2);
            var thirdLeague = dbContext.Leagues.FirstOrDefault(x => x.NationId == bulgaria.Id && x.Level == 3);
            var fourthLeague = dbContext.Leagues.FirstOrDefault(x => x.NationId == bulgaria.Id && x.Level == 4);

            if (!dbContext.Teams.Any())
            {
                Team[] firstLeagueTeams = new Team[]
                {
                   new Team() { Name = "CSKA", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Sofia")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="CSKA.jpg" },
                   new Team() { Name = "Levski", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Sofia")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="Levski.jpg" },
                   new Team() { Name = "Lokomotiv Plovdiv", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Plovdiv")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="LokomotivPlovdiv.jpg" },
                   new Team() { Name = "Botev Plovdiv", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Plovdiv")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="BotevPlovdiv.jpg" },
                   new Team() { Name = "Slavia", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Sofia")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="Slavia.jpg" },
                   new Team() { Name = "Lokomotiv Sofia", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Sofia")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="LokomotivSofia.jpg" },
                   new Team() { Name = "Pirin Blagoevgrad", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Blagoevgrad")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="Pirin.jpg" },
                   new Team() { Name = "Ludogorets", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Razgrad")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="Ludogorets.jpg" },
                   new Team() { Name = "Arda", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Kardzhali")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="Arda.jpg" },
                   new Team() { Name = "Cherno more", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Varna")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="ChernoMore.jpg" },
                   new Team() { Name = "Beroe", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Stara Zagora")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="Beroe.jpg" },
                   new Team() { Name = "Botev Vratsa", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Vratsa")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="BotevVratsa.jpg" },
                   new Team() { Name = "Dunav", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Ruse")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="Dunav.jpg" },
                   new Team() { Name = "Etar", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Veliko Turnovo")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="Etar.jpg" },
                   new Team() { Name = "Dobrudzha", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Dobrich")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="Dobrudzha.jpg" },
                   new Team() { Name = "Spartak Varna", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Varna")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=firstleague.Id,IsPlayable=true,ImageUrl="SpartakVarna.jpg" },
                };

                await dbContext.Teams.AddRangeAsync(firstLeagueTeams);

                Team[] secondLeagueTeams = new Team[]
               {
                   new Team() { Name = "Minior Pernik", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Pernik")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="MiniorPernik.jpg" },
                   new Team() { Name = "Hebar", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Hebar")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Hebar.jpg" },
                   new Team() { Name = "Septemvri Sofia", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Sofia")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="SeptemvriSofia.jpg" },
                   new Team() { Name = "Montana", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Montana")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Montana.jpg" },
                   new Team() { Name = "Litex", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Lovech")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Litex.jpg" },
                   new Team() { Name = "Neftochimik", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Burgas")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Neftochimik.jpg" },
                   new Team() { Name = "Marek", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Dupnitsa")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Marek.jpg" },
                   new Team() { Name = "Yantra", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Yantra")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Yantra.jpg" },
                   new Team() { Name = "Sliven", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Sliven")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Sliven.jpg" },
                   new Team() { Name = "Chernomorets", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Burgas")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Chernomorets.jpg" },
                   new Team() { Name = "Rakovski", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Rakovski")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Rakovski.jpg" },
                   new Team() { Name = "Pirin Razlog", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Razlog")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="PirinRazlog.jpg" },
                   new Team() { Name = "Botev Galabovo", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Galabovo")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="BotevGalabovo.jpg" },
                   new Team() { Name = "Bansko", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Bansko")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Bansko.jpg" },
                   new Team() { Name = "Lokomotiv Mezdra", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Mezdra")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="LokomotivMezdra.jpg" },
                   new Team() { Name = "Vereya", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Stara Zagora")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=secondLeague.Id,IsPlayable=true,ImageUrl="Vereya.jpg" },
               };

                await dbContext.Teams.AddRangeAsync(secondLeagueTeams);

                Team[] thirdLeagueTeams = new Team[]
             {
                   new Team() { Name = "Spartak Pleven", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Pleven")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="SpartakPleven.jpg" },
                   new Team() { Name = "Maritsa Plovdiv", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Plovdiv")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="MaritsaPlovdiv.jpg" },
                   new Team() { Name = "Volov", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Shumen")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="VolovShumen.jpg" },
                   new Team() { Name = "Lokomotiv Ruse", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Ruse")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="LokomotivRuse.jpg" },
                   new Team() { Name = "Nesebar", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Nesebar")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="Nesebar.jpg" },
                   new Team() { Name = "Rodopa Smolyan", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Smolyan")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="RodopaSmolyan.jpg" },
                   new Team() { Name = "Levski Lom", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Lom")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="LevskiLom.jpg" },
                   new Team() { Name = "Sozopol", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Sozopol")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="Sozopol.jpg" },
                   new Team() { Name = "Balkan Botevgrad", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Botevgrad")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="BalkanBotevgrad.jpg" },
                   new Team() { Name = "Chavdar Etropole", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Etropole")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="ChavdarEtropole.jpg" },
                   new Team() { Name = "Belasitsa Petrich", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Petrich")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="Belasitsa.jpg" },
                   new Team() { Name = "Zagorets", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Zagorets")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="Zagorets.jpg" },
                   new Team() { Name = "Bdin", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Vidin")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="Bdin.jpg" },
                   new Team() { Name = "Yantra", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Gabrovo")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="Yantra.jpg" },
                   new Team() { Name = "Dorostol", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Silistra")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="Dorostol.jpg" },
                   new Team() { Name = "Vihar Sandanski", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Sandanski")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=thirdLeague.Id,IsPlayable=true,ImageUrl="Vihar.jpg" },
             };

                await dbContext.Teams.AddRangeAsync(thirdLeagueTeams);

                Team[] fourthLeagueTeams = new Team[]
             {
                   new Team() { Name = "Sevlievo", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Sevlievo")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="Sevlievo.jpg" },
                   new Team() { Name = "Vihar Slavyanovo", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Slavyanovo")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="ViharSlavyanovo.jpg" },
                   new Team() { Name = "Drenovets", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Drenovets")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="Drenovets.jpg" },
                   new Team() { Name = "Akademik Svishtov", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Svishtov")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="AkademikSvishtov.jpg" },
                   new Team() { Name = "Chernomorets Balchik", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Balchik")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="ChernomoretsBalchik.jpg" },
                   new Team() { Name = "Riltsi", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Riltsi")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="Riltsi.jpg" },
                   new Team() { Name = "Slivnishki Geroi", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Slivnitsa")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="SlivnishkiGeroi.jpg" },
                   new Team() { Name = "Granit Vladaya", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Vladaya")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="GranitVladaya.jpg" },
                   new Team() { Name = "Oborishte", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Oborishte")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="Oborishte.jpg" },
                   new Team() { Name = "Kyustendil", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Kyustendil")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="Kyustendil.jpg" },
                   new Team() { Name = "Gigant Saedinenie", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Saedinenie")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="GigantSaedinenie.jpg" },
                   new Team() { Name = "Levski Karlovo", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Karlovo")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="LevskiKarlovo.jpg" },
                   new Team() { Name = "Rozova Dolina", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Kazanlak")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="RozovaDolina.jpg" },
                   new Team() { Name = "Sokol", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Makovo")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="SokolMarkovo.jpg" },
                   new Team() { Name = "Dimitrovgrad", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Dimitrovgrad")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="Dimitrovgrad.jpg" },
                   new Team() { Name = "Dulo", CityId=dbContext.Cities.FirstOrDefault(x=>x.Name=="Vidin")?.Id??null,NationId = bulgaria.Id,CupId = null,EuropeanCupId=null,LeagueId=fourthLeague.Id,IsPlayable=true,ImageUrl="Dulo.jpg" },
             };

                await dbContext.Teams.AddRangeAsync(fourthLeagueTeams);

                Team[] europeanTeams = new Team[]
               {
                   new Team() { Name = "Barcelona", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Spain")?.Id??null, CupId = null, EuropeanCupId=null, LeagueId=null, IsPlayable=false, IsEuroParticipant=true, ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Real Madrid", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Spain")?.Id??null, CupId = null, EuropeanCupId=null, LeagueId=null, IsEuroParticipant=true, IsPlayable=false, ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Sevilla", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Spain")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Atletico Madrid", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Spain")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Manchester United", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="England")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Arsenal", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="England")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Chelsea", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="England")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "PSG", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="France")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Lyon", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="France")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Monaco", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="France")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Bayern Munich", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Germany")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Borussia Dortmund", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Germany")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Schalke 04", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Germany")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true,IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Werder Bremen", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Germany")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Partizan", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Serbia")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Crvena Zvezda", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Serbia")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Cluj", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Romania")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Steaua", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Romania")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Ajax", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Netherlands")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Feyenoord", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Netherlands")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Twente", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Netherlands")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "PSV", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Netherlands")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Roma", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Italy")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Juventus", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Italy")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Milan", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Italy")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Inter", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Italy")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Napoli", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Italy")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Manchester City", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="England")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Porto", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Portugal")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Sporting Lisbon", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Portugal")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Benfica", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Portugal")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Bayer Leverkusen", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Germany")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Slavia Prague", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Czech Republic")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Olympiakos", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Greece")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Celtic", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Scotland")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Rangers", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Scotland")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Zenit", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Russia")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "CSKA Moscow", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Russia")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Villareal", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Spain")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Atalanta", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Italy")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "RB Leipzig", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Germany")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Tottenham Hotspur", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="England")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "West Ham", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="England")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Lille", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="France")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Shaktar Donetsk", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Ukraine")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Freiburg", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Germany")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Dinamo Zagreb", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Croatia")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Dinamo Kiev", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Ukraine")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "AZ Alkmaar", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Netherlands")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Aston Villa", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="England")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Braga", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Portugal")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Club Brugge", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Belgium")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "BATE Borisov", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Belarus")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Maccabi Tel-Aviv", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Israel")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Slovan Bratislava", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Slovenia")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Basel", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Switzerland")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Fenerbahce", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Turkey")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Molde", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Sweden")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Dinamo Moscow", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Russia")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Qarabag", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Azerbaidjan")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Slovacko", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Slovakia")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Anderlecht", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Belgium")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "AEK", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Greece")?.Id??null,CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "PAOK", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Greece")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Maribor", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Slovenia")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "AIK", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Sweden")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true, IsPlayable=false,ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Rijeka", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Croatia")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true,IsPlayable=false, ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Hajduk Split", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Croatia")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true,IsPlayable=false, ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Rapid Wien", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Austria")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true,IsPlayable=false, ImageUrl="Barcelona.jpg" },
                   new Team() { Name = "Genk", CityId=null, NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Belgium")?.Id??null, CupId = null,EuropeanCupId=null,LeagueId=null, IsEuroParticipant=true,IsPlayable=false, ImageUrl="Barcelona.jpg" },
               };

                await dbContext.Teams.AddRangeAsync(europeanTeams);

                var freeAgentsTeam = new Team() { Name = "FreeAgents", CityId = null, NationId = null, LeagueId = null, CupId = null, EuropeanCupId = null, ImageUrl = "", IsPlayable = false };

                dbContext.Teams.Add(freeAgentsTeam);
            }
        }
    }
}
