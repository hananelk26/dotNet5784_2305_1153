using BlApi;

namespace BlImplementation
{
    internal class TimeImplementation : ITime
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public void SetStartDate(DateTime date) => _dal.Time.SetStartDate(date);

        public DateTime? StartDate()=> _dal.Time.StartDate();
    }
}