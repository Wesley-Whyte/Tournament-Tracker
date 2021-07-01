using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class PersonModel
    {
        /// <summary>
        /// Database id of the person
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// First name of participant/team member
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of participant/team member
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Email of participant/team member
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Cellphone number of participant/team member
        /// </summary>
        public string CellPhoneNumber { get; set; }

        

        public string FullName
        {
            get 
            {
                return $"{FirstName} {LastName}"; 
            }
            
        }

    }
}
