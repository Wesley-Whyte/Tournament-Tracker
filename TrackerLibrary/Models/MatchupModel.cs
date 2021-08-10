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
        /// To get data back from database
        /// </summary>
        public int Winnerid { get; set; }
        /// <summary>
        /// Represents the winner of this particular matchup
        /// </summary>
        public TeamModel Winner { get; set; }
        /// <summary>
        /// Represents the round this matchup wan in
        /// </summary>
        public int MatchupRound { get; set; }

        public string Matchup 
        {
            get
            {
                if (Entries.Count < 2)
                {
                    return $"{Entries[0].TeamCompeting.TeamName} vs bye";
                }
                else
                {
                    string teamone;
                    string teamtwo;
                    if (Entries[0].TeamCompeting == null && Entries[1].TeamCompeting == null)
                    {
                        return "Matchup not yet determined";
                    }

                    if (Entries[0].TeamCompeting == null)
                    {
                        teamone = ".";
                    }
                    else
                    {
                        teamone = Entries[0].TeamCompeting.TeamName;
                    }
                    if (Entries[1].TeamCompeting == null)
                    {
                        teamtwo = ".";
                    }
                    else
                    {
                        teamtwo = Entries[1].TeamCompeting.TeamName;
                    }
                    return $"{teamone} vs {teamtwo}";
                }
            }
        }

        public int id { get; set; }
        public MatchupModel()
        {
            Entries = new List<MatchupEntryModel>();
        }
    }
}
