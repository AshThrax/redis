namespace RedisDemo.models
{
    public class Todoitems
    {
        public Guid Id { get; set; } =Guid.NewGuid();
        public string Name { get; set; }
    }
}
