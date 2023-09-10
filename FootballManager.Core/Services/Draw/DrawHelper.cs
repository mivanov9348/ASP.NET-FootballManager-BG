namespace FootballManager.Core.Services.Draw
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Core.Services.Fixture;
    using FootballManager.Infrastructure.Data.DataModels;
    using System;
    using System.Collections.Generic;

    internal class DrawHelper
    {
        private FootballManagerDbContext data;
        private Random rnd;
        public DrawHelper(FootballManagerDbContext data)
        {
            this.data = data;
            this.rnd = new Random();
        }

        internal Draw CreateDraw(Game currentGame, List<VirtualTeam> currentCupTeams, List<Fixture> currentFixtures)
        {            
            var newDraw = new Draw
            {
                Teams = currentCupTeams,
                Game = currentGame,
                GameId = currentGame.Id,
                Fixtures = currentFixtures,
                IsDrawStarted = true                
            };

            this.data.Draws.Add(newDraw);
            this.data.SaveChanges();
            return newDraw;
        }

        internal List<Fixture> FillFixtures(List<VirtualTeam> allTeams, DrawViewModel model)
        {
            var fixtures = new List<Fixture>();

            for (int i = 0; i < model.NumberOfTeams / 2; i++)
            {
                var newFixture = new Fixture();
                fixtures.Add(newFixture);
            }
            this.data.Fixtures.AddRange(fixtures);

            return fixtures;
        }

        internal List<VirtualTeam> FillTeams(List<VirtualTeam> allTeams, DrawViewModel model)
        {
            var teams = new List<VirtualTeam>();

            for (int i = 0; i < model.NumberOfTeams; i++)
            {
                var team = allTeams[rnd.Next(0, allTeams.Count)];
                while (IsExist(team, teams))
                {
                    team = allTeams[rnd.Next(0, allTeams.Count)];
                }
                teams.Add(team);
            }
            return teams;
        }

        internal bool IsExist(VirtualTeam team, List<VirtualTeam> teams)
        {
            if (teams.Contains(team))
            {
                return true;
            }

            return false;
        }

    }
}
