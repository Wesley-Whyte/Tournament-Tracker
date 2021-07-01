using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        /// <summary>
        /// Represents the entries(teams) for this particular matchup
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; }
        /// <summary>
        /// Represents the winner of this particular matchup
        /// </summary>
        public TeamModel Winner { get; set; }
        /// <summary>
        /// Represents the round this matchup wan in
        /// </summary>
        public int MatchupRound { get; set; }
        public MatchupModel()
        {
            Entries = new List<MatchupEntryModel>();
        }
    }
}
