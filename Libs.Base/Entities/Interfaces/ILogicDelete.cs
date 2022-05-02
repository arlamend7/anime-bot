namespace Libs.Base.Entities.Interfaces
{
    public interface ILogicDelete
    {
        bool? Deleted { get; set; }
        bool IsDeleted => Deleted == true;
        void Delete()
        {
            Deleted = true;
        }
    }
}
