using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        PrizeModel createPrize(PrizeModel model);

        PersonModel createPerson(PersonModel model);

        List<PersonModel> getPeopleAll();

        TeamModel createTeam(TeamModel model);

        List<TeamModel> getTeamAll();

        TournamentModel createTournament(TournamentModel model);

        List<TournamentModel> getTournamentAll();

        void updateMatchupEntry(MatchupEntryModel model);

        void updateMatchup(MatchupModel model);

        void updateMatchupEntryScore(MatchupEntryModel model);

        void updateTournament(TournamentModel model);
    }
}
