

namespace BlApi;
using DO;

public interface IEngineer
{
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);
    public BO.Engineer? Read(int id);
    public int Create(BO.Engineer boEngineer);
    public void Delete(int id);
    public void Update(BO.Engineer item);

}
