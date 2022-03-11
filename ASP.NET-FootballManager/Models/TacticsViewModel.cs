﻿using ASP.NET_FootballManager.Data.DataModels;

namespace ASP.NET_FootballManager.Models
{
    public class TacticsViewModel
    {
        public int PlayerId { get; set; }
        public VirtualTeam CurrentTeam { get; set; }
        public List<Player> StartingEleven { get; set; } = new List<Player>();
        public List<Player> Substitutes { get; set; } = new List<Player>();
        public List<Position> Positions { get; set; } = new List<Position>();

    }
}
