using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        /// <summary>
        ///  Creates a prize model with none of the properties initialized 
        /// </summary>
        public PrizeModel()
        {

        }
        /// <summary>
        /// Creates a prize model with properties initialized by parameters
        /// </summary>
        /// <param name="number"></param>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        /// <param name="percentage"></param>
        public PrizeModel(string number, string name, string prize, PrizeType prizeType)
        {
            PlaceNumber = Int32.Parse(number);
            PlaceName = name;
            Type = prizeType;

            switch (prizeType)
            {
                case PrizeType.Amount:
                    PrizeAmount = decimal.Parse(prize);
                    break;
                case PrizeType.Percentage:
                    PrizePercentage = double.Parse(prize);
                    break;
                default:
                    break;
            }
            
            
        }

        public PrizeType Type { get; set; }
        /// <summary>
        /// Database id of the model
        /// </summary>
        public int id { get; set; }
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
