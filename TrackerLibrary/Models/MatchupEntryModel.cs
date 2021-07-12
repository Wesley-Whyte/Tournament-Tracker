using System;
using System.Collections.Generic;
using System.Text;


namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {
        /// <summary>
        /// Represents one team in the matchup
        /// </summary>
        public TeamModel TeamCompeting { get; set; }
        /// <summary>
        /// To get a value back from the database
        /// </summary>
        public int TeamCompetingid { get; set; }
        /// <summary>
        /// Represents the teams score
        /// </summary>
        public double  Score { get; set; }
        /// <summary>
        /// Represents the matchup that this team cane from
        /// </summary>
        public MatchupModel ParentMatcup { get; set; }
        /// <summary>
        /// To get a value back from the database
        /// </summary>
        public int ParentMatchupid { get; set; }
    }
}
