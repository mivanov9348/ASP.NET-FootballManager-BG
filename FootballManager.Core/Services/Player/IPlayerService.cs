﻿namespace ASP.NET_FootballManager.Services.Player
{
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models.Sorting;

    public interface IPlayerService
    {
        PlayersViewModel SortingPlayers(PlayerSorting s, int id,Game currentGame);   
        List<Player> GetPlayersByTeam(int teamId);
        void GeneratePlayers(Game game, VirtualTeam team);
        void CreateFreeAgents(Game game, int gk, int df, int mf, int st);
        void CalculatingPlayersPrice(Game CurrentGame);
        List<Player> GetStartingEleven(int teamId);
        List<Player> GetSubstitutes(int teamId);
        void Substitution(int playerId, string action);
        Player GetPlayerById(int id);
        Player GetRandomPlayer(VirtualTeam team);
        Player GetLeagueGoalscorer(Game CurrentGame, int leagueId);
        void RemovePlayers(VirtualTeam freeAgentsTeam);
        void ResetPlayerStats(Game CurrentGame);

        void UpdateAttributes(Game CurrentGame);
    }
}