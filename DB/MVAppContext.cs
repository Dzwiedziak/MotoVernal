using DB.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = DB.Entities.File;
namespace DB
{
    public class MVAppContext : IdentityDbContext<User>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicResponse> TopicResponses { get; set; }
        public DbSet<VehicleOffer> VehicleOffers { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<BugReport> BugReports { get; set; }
        public DbSet<UserObservation> UserObservations { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<EventInterest> EventInterests { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

        public MVAppContext(DbContextOptions<MVAppContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Section>()
                .HasOne(s => s.Parent)
                .WithMany()
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Topic>()
                .HasOne(s => s.Publisher)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
