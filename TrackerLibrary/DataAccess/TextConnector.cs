using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    class TextConnector : IDataConnection
    {
        

        // TODO - maKE TEEXT CONNECTIONS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public PrizeModel createPrize(PrizeModel model)
        {
            throw new NotImplementedException();
        }

        public PersonModel createPerson(PersonModel model)
        {
            throw new NotImplementedException();
        }

        public List<PersonModel> getPeopleAll()
        {
            throw new NotImplementedException();
        }

        public TeamModel createTeam(TeamModel model)
        {
            throw new NotImplementedException();
        }

        public List<TeamModel> getTeamAll()
        {
            throw new NotImplementedException();
        }

        public TournamentModel createTournament(TournamentModel model)
        {
            throw new NotImplementedException();
        }

        public List<TournamentModel> getTournamentAll()
        {
            throw new NotImplementedException();
        }

        public void updateMatchupEntry(MatchupEntryModel model)
        {
            throw new NotImplementedException();
        }

        public void updateMatchup(MatchupModel model)
        {
            throw new NotImplementedException();
        }

        public void updateMatchupEntryScore(MatchupEntryModel model)
        {
            throw new NotImplementedException();
        }

        public void updateTournament(TournamentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
