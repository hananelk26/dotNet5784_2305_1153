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
        public void reset()
        {
            DataSource.Config.StartDate = null;
        }

        public void SetStartDate(DateTime date)
        {
            DataSource.Config.StartDate = date;
        }

        public DateTime? StartDate()
        {
            return DataSource.Config.StartDate;
        }
    }
}
