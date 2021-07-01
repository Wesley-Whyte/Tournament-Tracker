using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class TeamModel
    {
        /// <summary>
        /// List of team members
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; }
        /// <summary>
        /// Team name
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// Database id of the team
        /// </summary>
        public int id { get; set; }

        public TeamModel()
        {
            TeamMembers = new List<PersonModel>();
        }
    }
}
