using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary
{
    public class PrizeModel
    {
        /// <summary>
        /// Represents the position assosciated with
        /// this prize
        /// </summary>
        public int PlaceNumber { get; set; }
        /// <summary>
        /// The place/position name 
        /// </summary>
        public string PlaceName { get; set; }
        /// <summary>
        /// The prize amount
        /// </summary>
        public decimal PrizeAmount { get; set; }
        /// <summary>
        /// Represents the percentage of the entry fees
        /// to be collected by this place/positon holder
        /// </summary>
        public double PrizePercentage { get; set; }
    }
}
