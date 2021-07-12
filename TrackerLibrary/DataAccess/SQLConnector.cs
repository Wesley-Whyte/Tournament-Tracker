using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace TrackerLibrary.DataAccess
{
    class SQLConnector : IDataConnection
    {
        

        //@PlaceNumber int,
        //@PlaceName varchar(20),
        //@PrizeAmount decimal,
        //@PrizePercentage float,
        //@id int = 0 output
        /// <summary>
        /// Saves the prize model passed as a parameter to the
        /// Tournaments database prize table
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize infoemation including unique identifier</returns>
        public PrizeModel createPrize(PrizeModel model)
        {

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_insert", p, commandType: CommandType.StoredProcedure);
                model.id = p.Get<int>("@id"); 

                return model;
            }
        }
        /// <summary>
        /// Saves the PersonModel passed as a parametet to the 
        /// Tournaments Database people table
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returns the model with a unique</returns>
        //@FirstName varchar(20),
        //@LastName varchar(50),
        //@EMail varchar(50),
        //@CellPhoneNumber varchar(20),
        //@id int = 0 output
        public PersonModel createPerson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EMail", model.Email);
                p.Add("@CellPhoneNUmber", model.CellPhoneNumber);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPerson_insert", p, commandType: CommandType.StoredProcedure);

                model.id = p.Get<int>("@id");

                return model;
            }

        }
        /// <summary>
        /// Gets all person models saved in the Tournaments database Poeple table
        /// </summary>
        /// <returns>returns a list of person models with unique ids</returns>
        public List<PersonModel> getPeopleAll()
        {
            List<PersonModel> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_all_select", commandType: CommandType.StoredProcedure).AsList<PersonModel>();
            }


            return output;
        }

        /// <summary>
        /// Saves the team model passed as a parameter to the Tournaments database
        /// Teams table and stores the team members to the Team Members table
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returns the model with a unique id</returns>
        public TeamModel createTeam(TeamModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();

                p.Add("@TeamName", model.TeamName);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeams_insert", p, commandType: CommandType.StoredProcedure);

                model.id =  p.Get<int>("@id");

                foreach (PersonModel person in model.TeamMembers)
                {
                    p = new DynamicParameters();
                    p.Add("@Teamid", model.id);
                    p.Add("@Personid", person.id);
                    //p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute("dbo.spTeamMembers_insert",p, commandType: CommandType.StoredProcedure);
                }
            }

            return model;
        }

        public List<TeamModel> getTeamAll()
        {
            List<TeamModel> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                output = connection.Query<TeamModel>("dbo.spTeams_all_select", commandType: CommandType.StoredProcedure).AsList<TeamModel>();

                foreach (TeamModel team in output)
                {
                    p.Add("@Teamid", team.id);
                    
                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_select", p, commandType: CommandType.StoredProcedure).AsList<PersonModel>();
                }
            }
            return output;
        }

        public TournamentModel createTournament(TournamentModel model)
        {

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@TournamentName", model.TournamentName);
                p.Add("@EntryFee", model.EntryFee);
                p.Add("@id", model.id, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTournament_insert", p, commandType: CommandType.StoredProcedure);
                model.id = p.Get<int>("@id");

                saveTournamentTeams(model, connection);
                saveTournamentPrizes(model, connection);
                saveTournamentRounds(model, connection);
            }
            return model;

        }

        private void saveTournamentTeams(TournamentModel model,IDbConnection connection)
        {
            foreach (TeamModel team in model.EnteredTeams)
            {
                var p = new DynamicParameters();
                p.Add("@Tournamentid", model.id);
                p.Add("@Teamid", team.id);
                connection.Execute("dbo.spTournamentEntries_insert", p, commandType: CommandType.StoredProcedure);
            }
        }

        private void saveTournamentPrizes(TournamentModel model, IDbConnection connection)
        {
            foreach (PrizeModel prize in model.Prizes)
            {
                var p = new DynamicParameters();
                p.Add("@Tournamentid", model.id);
                p.Add("@Prizeid", prize.id);
                connection.Execute("dbo.spTournamentPrizes_insert", p, commandType: CommandType.StoredProcedure);
            }
        }

        private void saveTournamentRounds(TournamentModel model, IDbConnection connection)
        {
            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel match in round)
                {
                    var p = new DynamicParameters();
                    p.Add("@Tournamentid", model.id);
                    p.Add("@MatchupRound", match.MatchupRound);
                    p.Add("@id", model.id, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute("dbo.spMatchup_insert", p, commandType: CommandType.StoredProcedure);
                    match.id = p.Get<int>("@id");

                    foreach (MatchupEntryModel entry in match.Entries)
                    {
                        if (match.MatchupRound == 1)
                        {
                            p = new DynamicParameters();
                            p.Add("@Matchupid", match.id);
                            p.Add("@TeamCompetingid", entry.TeamCompeting.id);

                            connection.Execute("dbo.spRoundOneMatchupEntries_Insert", p, commandType: CommandType.StoredProcedure); 
                        }
                        else
                        {
                            p = new DynamicParameters();
                            p.Add("@Matchupid", match.id);
                            p.Add("@ParentMatchupid", entry.ParentMatcup.id);

                            connection.Execute("dbo.OtherRoundsMatchupEntries_insert", p, commandType: CommandType.StoredProcedure);
                        }
                    }
                }
            }
        }

        public List<TournamentModel> getTournamentAll()
        {
            List<TournamentModel> output = new List<TournamentModel>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                output = connection.Query<TournamentModel>("dbo.spTournaments_all_select", commandType: CommandType.StoredProcedure).AsList<TournamentModel>();
                List<MatchupModel> matchups = new List<MatchupModel>();

                foreach (TournamentModel tournament in output)
                {
                    //Get teams
                    var p = new DynamicParameters();
                    p.Add("@Tournamentid", tournament.id);

                    tournament.EnteredTeams = connection.Query<TeamModel>("dbo.spTeamsByTournament_select",p, commandType: CommandType.StoredProcedure).AsList<TeamModel>();

                    foreach (TeamModel team in tournament.EnteredTeams)
                    {
                        p = new DynamicParameters();
                        p.Add("@Teamid", team.id);

                        team.TeamMembers = connection.Query<PersonModel>("dbo.spPersonByTeams_select", p, commandType: CommandType.StoredProcedure).AsList<PersonModel>();
                    }
                    //Get prizes
                    p = new DynamicParameters();
                    p.Add("@Tournamentid", tournament.id);

                    tournament.Prizes = connection.Query<PrizeModel>("dbo.spPrizesByTournament_select",p, commandType: CommandType.StoredProcedure).AsList<PrizeModel>();
                    //Get rounds
                    p = new DynamicParameters();
                    p.Add("@Tournamentid", tournament.id);

                    matchups = connection.Query<MatchupModel>("dbo.spMatchupByTournaments_select", p, commandType: CommandType.StoredProcedure).AsList<MatchupModel>();

                    foreach (MatchupModel matchup in matchups)
                    {
                        p = new DynamicParameters();
                        p.Add("@Matchupid", matchup.id);

                        matchup.Entries = connection.Query<MatchupEntryModel>("dbo.spMatchupEntries_select", p, commandType: CommandType.StoredProcedure).AsList<MatchupEntryModel>();

                        foreach (MatchupEntryModel entry in matchup.Entries)
                        {
                            entry.TeamCompeting = tournament.EnteredTeams.Find(x => x.id == entry.TeamCompetingid);
                            entry.ParentMatcup = matchups.Find(x => x.id == entry.ParentMatchupid);
                        }

                    }

                    int round = 1;
                    List<MatchupModel> currRound = new List<MatchupModel>();
                    foreach (MatchupModel matchup in matchups)
                    {
                        if (matchup.MatchupRound > round)
                        {
                            tournament.Rounds.Add(currRound);
                            currRound = new List<MatchupModel>();
                            round++;
                        }
                        currRound.Add(matchup);

                    }
                    tournament.Rounds.Add(currRound);
                }
            }
            return output;

        }
    }
}
