using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {
        /// <summary>
        /// The name of the tournament
        /// </summary>
        public string TournamentName { get; set; }
        /// <summary>
        /// Represents entryfee if any
        /// </summary>
        public decimal EntryFee { get; set; }
        /// <summary>
        /// Represents the teams participating in the tournament
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; }
        /// <summary>
        /// Represents the prizes for the tournament winners
        /// </summary>
        public List<PrizeModel> Prizes { get; set; }
        /// <summary>
        /// Represents the rounds in the tournament
        /// A round being a list of matchups in that particular round
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; }

        public TournamentModel()
        {
            EnteredTeams = new List<TeamModel>();
            Prizes = new List<PrizeModel>();
            Rounds = new List<List<MatchupModel>>();
        }
    }
}
