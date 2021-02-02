using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary
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

        public TeamModel()
        {
            TeamMembers = new List<PersonModel>();
        }
    }
}
