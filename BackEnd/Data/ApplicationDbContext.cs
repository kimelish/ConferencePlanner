﻿using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<Attendee> Attendees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>().HasIndex(a => a.UserName).IsUnique();

            modelBuilder.Entity<SessionAttendee>().HasKey(ca => new { ca.AttendeeId, ca.SessionId });

            modelBuilder.Entity<SessionSpeaker>().HasKey(ss => new { ss.SessionId, ss.SpeakerId });
        }
    }
}