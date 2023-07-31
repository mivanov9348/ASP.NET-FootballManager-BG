﻿namespace ASP.NET_FootballManager.Services.Match
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FootballManager.Core.Models.Match;
    using FootballManager.Infrastructure.Data.Constant;
    using FootballManager.Core.Services.PlayerProbability;

    public class MatchService : IMatchService
    {
        private readonly FootballManagerDbContext data;
        private readonly MatchMessages.Messages messages;
        private readonly IPlayerProbability playerProbability;
        private Random rnd;
        public MatchService(FootballManagerDbContext data, IPlayerProbability playerProbability)
        {
            rnd = new Random();
            this.data = data;
            this.messages = new MatchMessages.Messages();
            this.playerProbability = playerProbability;
        }
        public async Task<Fixture> GetCurrentFixture(List<Fixture> dayFixtures, Game currentGame)
        {
            var currentTeam = this.data.VirtualTeams.FirstOrDefault(x => x.TeamId == currentGame.TeamId && x.GameId == currentGame.Id);

            return await Task.Run(() => dayFixtures.FirstOrDefault(x => x.HomeTeamId == currentTeam.Id
                                                     || x.AwayTeamId == currentTeam.Id));

        }
        public async Task<List<Fixture>> GetFixturesByDay(Game CurrentGame)
        {
            var currentDay = this.data.Days.FirstOrDefault(x => x.CurrentDay == CurrentGame.Day && x.GameId == CurrentGame.Id && x.Year == CurrentGame.Year);
            if (currentDay == null)
            {
                return null;
            }
            return await Task.Run(() => this.data.Fixtures.Where(x => x.GameId == CurrentGame.Id && x.DayId == currentDay.Id).ToList());

        }
        public async Task<List<Player>> GetStarting11(int teamId) => await Task.Run(() => this.data.Players.Where(x => x.TeamId == teamId && x.IsStarting11 == true).ToList());
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
        public async Task<Match> GetCurrentMatch(int matchId) => await Task.Run(() => this.data.Matches.FirstOrDefault(x => x.Id == matchId));
        public void PlayerAction(VirtualTeam team, Player player, Match match)
        {
            var position = this.data.Positions.FirstOrDefault(x => x.Id == player.PositionId);
            var playerAttributes = this.data.PlayerAttributes.FirstOrDefault(x => x.PlayerId == player.Id);
            bool isTurn = false;

            var maxProbability = playerProbability.CompareProbabilities(playerAttributes);

            var randomSaveMessage = messages.GoalkeeperSaveMessages[1];

            switch (maxProbability)
            {

                case "Tackling":
                    (string message, bool retainsPossession) randomTacklingMessage = messages.TacklingMessages[rnd.Next(0, messages.TacklingMessages.Count)];
                    match.SituationText = string.Format(randomTacklingMessage.message, $"{player.FirstName} {player.LastName}");
                    isTurn = randomTacklingMessage.retainsPossession;
                    break;
                case "Dribbling":
                    (string message, bool retainsPossession) randomDribblingMessage = messages.DribblingMessages[rnd.Next(0, messages.TacklingMessages.Count)];
                    match.SituationText = string.Format(randomDribblingMessage.message, $"{player.FirstName} {player.LastName}");
                    isTurn = randomDribblingMessage.retainsPossession;
                    break;
                case "Goal":
                    (string message, bool retainsPossession) randomGoalMessage = messages.DribblingMessages[rnd.Next(0, messages.TacklingMessages.Count)];
                    match.SituationText = string.Format(randomGoalMessage.message, $"{player.FirstName} {player.LastName}");
                    isTurn = randomGoalMessage.retainsPossession;
                    Goal(player, match, team);
                    break;
                case "Heading":
                    (string message, bool retainsPossession) randomHeadingMessage = messages.DribblingMessages[rnd.Next(0, messages.TacklingMessages.Count)];
                    match.SituationText = string.Format(randomHeadingMessage.message, $"{player.FirstName} {player.LastName}");
                    isTurn = randomHeadingMessage.retainsPossession;
                    break;
                case "Passing":
                    (string message, bool retainsPossession) randomPassingMessage = messages.DribblingMessages[rnd.Next(0, messages.TacklingMessages.Count)];
                    match.SituationText = string.Format(randomPassingMessage.message, $"{player.FirstName} {player.LastName}");
                    isTurn = randomPassingMessage.retainsPossession;
                    player.Passes += 1;
                    break;
                default:
                    break;
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
        public async Task<MatchViewModel> GetMatchModel(Match match, Fixture fixture, Player player)
        {
            return new MatchViewModel
            {
                CurrentMatch = match,
                HomeTeam = fixture.HomeTeam,
                AwayTeam = fixture.AwayTeam,
                HomeTeamName = fixture.HomeTeamName,
                AwayTeamName = fixture.AwayTeamName,
                Positions = this.data.Positions.ToList(),
                HomeTeamPlayers = await GetStarting11(fixture.HomeTeamId),
                AwayTeamPlayers = await GetStarting11(fixture.AwayTeamId),
                CurrentPlayerName = player.FirstName + " " + player.LastName
            };
        }
        public void EndMatch(Match match)
        {
            match.isEnd = true;
            match.Minute = 0;
            this.data.SaveChanges();
        }
        public async Task<List<Fixture>> GetResults(Game currentGame)
        {
            var currentDay = this.data.Days.FirstOrDefault(x => x.CurrentDay == currentGame.Day - 1 && x.GameId == currentGame.Id && x.Year == currentGame.Year);
            return await Task.Run(() => this.data.Fixtures.Where(x => x.GameId == currentGame.Id && x.DayId == currentDay.Id).ToList());
        }
        public void DeleteMatches(Game CurrentGame)
        {
            var matches = this.data.Matches.Where(x => x.GameId == CurrentGame.Id);

            foreach (var item in matches)
            {
                this.data.Matches.Remove(item);
            }
            this.data.SaveChanges();
        }

    }
}
