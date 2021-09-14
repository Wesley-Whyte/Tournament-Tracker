using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        /// <summary>
        /// Creates Rounds fora tournament passed as a parameter
        /// </summary>
        /// <param name="model">TournamentModel</param>
        public static void createRounds(TournamentModel model)
        {
            // randomize teams
            List<TeamModel> randomizedTeams = randomizeTeams(model.EnteredTeams);
            //calculate number of rounds
            int roundsNum = getNumberOfRounds(randomizedTeams);
            //calculate number of byes
            int byeNum = getNumberOfByes(roundsNum, randomizedTeams.Count);
            //create first round
            model.Rounds.Add(createFirstRound(byeNum, randomizedTeams));
            //create other rounds
            createOtherRounds(model, roundsNum);
        }
        // Todo- sort prizes out
        public static decimal calculatePrizes(this TournamentModel model, PrizeModel prize)
        {
            decimal totalIncome = model.EnteredTeams.Count * model.EntryFee;
            decimal output;

            if (prize.Type == PrizeType.Amount)
            {
                output = prize.PrizeAmount;
                return output;
            }
            else
            {
                output = totalIncome * Convert.ToDecimal(prize.PrizePercentage / 100);
                return output;
            }
            

        }
        
        public static void sortOutByes(this TournamentModel model)
        {
            foreach (MatchupModel matchup in model.Rounds[0])
            {
                if (matchup.Entries.Count < 2)
                {
                    matchup.Winner = matchup.Entries[0].TeamCompeting;
                    matchup.Winnerid = matchup.Entries[0].TeamCompeting.id;  
                    GlobalConfig.Connection.updateMatchup(matchup);
                    if (model.Rounds.Count != 1)
                    {
                        model.advanceWinners(1);
                    }
                    else
                    {
                        model.completeTournament();
                    }
                }
            }
            
        }

        public static void advanceWinners(this TournamentModel model,int currRound)
        {
            foreach (MatchupModel matchup in model.Rounds[currRound])
            {
                foreach (MatchupEntryModel entry in matchup.Entries)
                {
                    if (entry.ParentMatcup.Winner != null)
                    {
                        entry.TeamCompeting = entry.ParentMatcup.Winner;
                        entry.TeamCompetingid = entry.ParentMatcup.Winnerid;
                        GlobalConfig.Connection.updateMatchupEntry(entry);
                    }
                }
            }
        }

        public static void declareWinner(this MatchupModel model)
        {
            if (model.Entries[0].Score == model.Entries[1].Score)
            {
                throw new Exception("Tournaments don't accept draws.");
            }
            else
            {
                if (model.Entries[0].Score > model.Entries[1].Score)
                {
                    model.Winner = model.Entries[0].TeamCompeting;
                    model.Winnerid = model.Entries[0].TeamCompeting.id;
                }
                else
                {
                    model.Winner = model.Entries[1].TeamCompeting;
                    model.Winnerid = model.Entries[1].TeamCompeting.id;
                }
                foreach (MatchupEntryModel entry in model.Entries)
                {
                    GlobalConfig.Connection.updateMatchupEntryScore(entry);
                }
                GlobalConfig.Connection.updateMatchup(model);
            }
            
        }
        /// <summary>
        /// Randomizes the order of a list of teams passed as a parameter
        /// </summary>
        /// <param name="teams">List of TeamModel</param>
        /// <returns>Randomized List of TeamModel</returns>
        private static List<TeamModel> randomizeTeams(List<TeamModel> teams)
        {
            teams.Shuffle();
            return teams;
        }
        /// <summary>
        /// Determines the number of a rounds a tournament will have 
        /// based on the list of teams passed in as a parameter
        /// </summary>
        /// <param name="teams">List of TeamModel</param>
        /// <returns>Number of rounds</returns>
        private static int getNumberOfRounds(List<TeamModel> teams)
        {
            int numberOfTeams = teams.Count;
            int numberOfRounds = 1;
            int count = 2;

            while (count < numberOfTeams)
            {
                count *= 2;
                numberOfRounds++;
            }
            return numberOfRounds;
        }
        /// <summary>
        /// Determines the number of teams that get to skip the first round
        /// </summary>
        /// <param name="numofRounds">Number of rounds in the tournament</param>
        /// <param name="numofTeams">Number of teams in the tournament</param>
        /// <returns></returns>
        private static int getNumberOfByes(int numofRounds,int numofTeams)
        {
            int count = 1;
            for (int i = 1; i <= numofRounds; i++)
            {
                count *= 2; 
            }

            return count - numofTeams;
        }
        /// <summary>
        /// Creates the first round of the tournament
        /// </summary>
        /// <param name="numofByes">Number of theams to skip the first round</param>
        /// <param name="teams">Numner of teams in the tournament</param>
        /// <returns>List of MatchupModel representing the first round</returns>
        private static List<MatchupModel> createFirstRound(int numofByes,List<TeamModel> teams)
        {
            List<MatchupModel> round = new List<MatchupModel>();
            MatchupModel curr = new MatchupModel();
            int byes = numofByes;

            foreach (TeamModel team in teams)
            {
                curr.Entries.Add(new MatchupEntryModel() { TeamCompeting = team });

                if (byes > 0 || curr.Entries.Count > 1)
                {
                    curr.MatchupRound = 1;
                    round.Add(curr);
                    curr = new MatchupModel();

                    if (byes > 0)
                    {
                        byes--;
                    }
                }
            }
            return round;

        }
        /// <summary>
        /// Creates Other rounds of tournament
        /// </summary>
        /// <param name="model">TournamentModel</param>
        /// <param name="rounds">Number of rounds in tournament</param>
        private static void createOtherRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currRound = new List<MatchupModel>();
            MatchupModel curr = new MatchupModel();

            while (round <= rounds)
            {
                foreach (MatchupModel match in previousRound)
                {
                    curr.Entries.Add(new MatchupEntryModel { ParentMatcup = match });

                    if (curr.Entries.Count > 1)
                    {
                        curr.MatchupRound = round;
                        currRound.Add(curr);
                        curr = new MatchupModel();
                    }
                }
                model.Rounds.Add(currRound);
                previousRound = currRound;
                currRound = new List<MatchupModel>();
                round++;
            }


        }
    }
}
