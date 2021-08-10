using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {

        public event EventHandler<DateTime> OnTournamentComplete;
        /// <summary>
        /// The name of the tournament
        /// </summary>
        public uint Active { get; set; }

        public string TournamentDisplay 
        { 
            get
            {
                if (Active == 1)
                {
                    return $"{TournamentName}(ongoing)";
                }
                else
                {
                    return $"{TournamentName}(complete)";
                }
            }
        }
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

        public int id { get; set; }

        public TournamentModel()
        {
            EnteredTeams = new List<TeamModel>();
            Prizes = new List<PrizeModel>();
            Rounds = new List<List<MatchupModel>>();
        }

        public void completeTournament()
        {
            OnTournamentComplete?.Invoke(this, DateTime.Now);
        }
    }
}
