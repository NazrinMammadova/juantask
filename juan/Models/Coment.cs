namespace juan.Models
{
    public class Coment
    {

        public int Id { get; set; }

        public string Message { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public string AppUserId {  get; set; } 
        public AppUser AppUser {  get; set; } 
        public DateTime CreatedAt { get; set; }




    }
}
