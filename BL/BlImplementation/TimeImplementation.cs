using BlApi;

namespace BlImplementation
{
    /// <summary>
    /// Implementation of the ITime interface for managing time-related operations.
    /// </summary>
    internal class TimeImplementation : ITime
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        /// <summary>
        /// Sets the start date for time tracking.
        /// </summary>
        /// <param name="date">The start date to set.</param>
        public void SetStartDate(DateTime date) => _dal.Time.SetStartDate(date);

        /// <summary>
        /// Gets the start date for time tracking.
        /// </summary>
        /// <returns>The start date, or null if not set.</returns>
        public DateTime? StartDate() => _dal.Time.StartDate();
    }
}