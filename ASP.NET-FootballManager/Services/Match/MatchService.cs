﻿namespace ASP.NET_FootballManager.Services.Match
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data.Constant;
    using ASP.NET_FootballManager.Models;

    public class MatchService : IMatchService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
        public MatchService(FootballManagerDbContext data)
        {
            rnd = new Random();
            this.data = data;
        }
        public Fixture GetCurrentFixture(List<Fixture> dayFixtures, Game currentGame)
        {
            var currentTeam = this.data.VirtualTeams.FirstOrDefault(x => x.TeamId == currentGame.TeamId);

            return dayFixtures.FirstOrDefault(x => x.HomeTeamId == currentTeam.Id
                                                    || x.AwayTeamId == currentTeam.Id);

        }
        public List<Fixture> GetFixturesByDay(Game CurrentGame) => this.data.Fixtures.Where(x => x.Day == CurrentGame.Day && x.GameId == CurrentGame.Id).ToList();
        public List<Player> GetStarting11(int teamId) => this.data.Players.Where(x => x.TeamId == teamId && x.IsStarting11 == true).ToList();
        public (bool isValid, string error) ValidateTactics(VirtualTeam currentTeam)
        {
            var allStartingPlayers = this.data.Players.Where(x => x.TeamId == currentTeam.Id && x.IsStarting11 == true).ToList();
            var gk = allStartingPlayers.Where(x => x.PositionId == 1).ToList();
            var df = allStartingPlayers.Where(x => x.PositionId == 2).ToList();
            var mf = allStartingPlayers.Where(x => x.PositionId == 3).ToList();
            var st = allStartingPlayers.Where(x => x.PositionId == 4).ToList();
            bool isValid = true;
            var sb = new StringBuilder();

            if (allStartingPlayers.Count != 11)
            {
                isValid = false;
                sb.AppendLine("Starting lineups must be 11 players!");
            }
            if (gk.Count != 1)
            {
                isValid = false;
                sb.AppendLine("Goalkeeper must be 1");
            }
            if (df.Count != 4)
            {
                isValid = false;
                sb.AppendLine("Defenders must be 4");
            }
            if (mf.Count != 4)
            {
                isValid = false;
                sb.AppendLine("Midlefielder must be 4");
            }
            if (st.Count != 2)
            {
                isValid = false;
                sb.AppendLine("Striker must be 2");
            }

            return (isValid, sb.ToString().TrimEnd());
        }
        public Match CreateMatch(Fixture currentFixture, Game CurrentGame)
        {
            var newMatch = new Match
            {
                CurrentFixture = currentFixture,
                CurrentFixtureId = currentFixture.Id,
                Game = CurrentGame,
                GameId = CurrentGame.Id,
                Minute = 0,
                Turn = 1,
                SituationText = "Match Start!"
            };
            this.data.Matches.Add(newMatch);
            this.data.SaveChanges();

            return newMatch;
        }
        public Match GetCurrentMatch(int matchId) => this.data.Matches.FirstOrDefault(x => x.Id == matchId);
        public void PlayerAction(VirtualTeam team, Player player, Match match)
        {
            var position = this.data.Positions.FirstOrDefault(x => x.Id == player.PositionId);
            bool isTurn = false;
            int attackNum = rnd.Next(1, player.Attack);
            int defNum = rnd.Next(1, player.Defense);

            if (position.Name == "Goalkeeper")
            {
                if (defNum >= attackNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) save the ball!";
                }

                if (attackNum > defNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) Pass the ball!";
                }
            }

            if (position.Name == "Defender")
            {
                if (defNum > attackNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) pass the ball!";
                }

                if (defNum == attackNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) lose the ball!";
                    isTurn = true;
                }

                if (attackNum > defNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) SCORE!!!";
                    Goal(player, match, team);
                    isTurn = true;
                }
            }

            if (position.Name == "Midlefielder")
            {
                if (defNum > attackNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) pass the ball";
                }
                if (attackNum == defNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) lose the ball!";
                    isTurn = true;
                }
                if (attackNum > defNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) SCORE!!!";
                    Goal(player, match, team);
                    isTurn = true;
                }
            }

            if (position.Name == "Striker")
            {

                if (attackNum < defNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) lose the ball!";
                    isTurn = true;
                }
                if (attackNum == defNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) pass the ball!";
                }
                if (attackNum > defNum)
                {
                    match.SituationText = $"{player.FirstName} {player.LastName} ({team.Name}) SCORE!!!";
                    Goal(player, match, team);
                    isTurn = true;
                }
            }
            if (isTurn)
            {
                ChangeTurn(match);
            }
            this.data.SaveChanges();
        }
        private void Goal(Player player, Match match, VirtualTeam team)
        {
            player.Goals++;
            var isHomeTeam = match.CurrentFixture.HomeTeamId == team.Id;
            if (isHomeTeam)
            {
                match.CurrentFixture.HomeTeamGoal += 1;
            }
            else
            {
                match.CurrentFixture.AwayTeamGoal += 1;
            }
            this.data.SaveChanges();
        }
        private void ChangeTurn(Match match)
        {
            if (match.Turn == 1)
            {
                match.Turn = 2;
            }
            else
            {
                match.Turn = 1;
            }
        }
        public void Time(Match match)
        {
            match.Minute += rnd.Next(1, Data.Constant.DataConstants.Match.Timespan);
            this.data.SaveChanges();
        }
        public MatchViewModel GetMatchModel(Match match, Fixture fixture, Player player)
        {
            return new MatchViewModel
            {
                CurrentMatch = match,
                HomeTeam = fixture.HomeTeam,
                AwayTeam = fixture.AwayTeam,
                HomeTeamName = fixture.HomeTeamName,
                AwayTeamName = fixture.AwayTeamName,
                Positions = this.data.Positions.ToList(),
                HomeTeamPlayers = GetStarting11(fixture.HomeTeamId),
                AwayTeamPlayers = GetStarting11(fixture.AwayTeamId),
                CurrentPlayerName = player.FirstName + " " + player.LastName
            };
        }
        public void EndMatch(Match match)
        {
            match.isEnd = true;
            match.Minute = 0;
            this.data.SaveChanges();
        }
        public List<Fixture> GetResults(Game currentGame) => this.data.Fixtures.Where(x => x.GameId == currentGame.Id && x.Day == currentGame.Day - 1).ToList();
    }
}
