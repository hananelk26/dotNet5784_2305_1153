using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class TimeImplementation : ITIme
    {
        /// <summary>
        /// Resets the time tracking by setting the start date to null.
        /// </summary>
        public void reset()
        {
            DataSource.Config.StartDate = null;
        }

        /// <summary>
        /// Sets the start date for time tracking.
        /// </summary>
        /// <param name="date">The start date.</param>
        public void SetStartDate(DateTime date)
        {
            DataSource.Config.StartDate = date;
        }

        /// <summary>
        /// Retrieves the start date for time tracking.
        /// </summary>
        /// <returns>The start date.</returns>
        public DateTime? StartDate()
        {
            return DataSource.Config.StartDate;
        }
    }
}
