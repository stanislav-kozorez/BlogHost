namespace DAL.Interface.Entities
{
    public class DalRole: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public virtual ICollection<User> Users { get; set; }
    }
}