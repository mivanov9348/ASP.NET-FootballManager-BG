namespace FootballManager.Core.Services.Draw.EliminationDraw
{
    using FootballManager.Core.Models.Draw;
    using FootballManager.Infrastructure.Data.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IEliminationDrawService
    {

        Draw CreateContinentalCupEliminationDraw(Game currentGame, DrawViewModel model, ContinentalCup currentCup);
        Draw CreateDomesticCupEliminationDraw(Game currentGame, DrawViewModel model, Cup currentCup);
        void FillEliminationFixtures(Draw currentDraw, VirtualTeam drawedTeam);


    }
}
