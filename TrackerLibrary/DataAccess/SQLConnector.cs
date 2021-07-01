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
    }
}
