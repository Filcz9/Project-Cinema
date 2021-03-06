﻿using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CinemaApi.Models
{
    public partial class CinemaDBContext : IdentityDbContext<ApplicationUser>
    {
        public CinemaDBContext()
        {
        }

        public CinemaDBContext(DbContextOptions<CinemaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CulturalEvent> CulturalEvent { get; set; }
        public virtual DbSet<EventAddress> EventAddress { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Newsletter> Newsletter { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Screening> Screening { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<SeatReservation> SeatReservation { get; set; }
        public virtual DbSet<SigningIn> SigningIn { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<UserReview> UserReview { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CulturalEvent>(entity =>
            {
                entity.HasKey(e => e.IdCulturalEvent);

                entity.ToTable("Cultural_Event");

                entity.Property(e => e.IdCulturalEvent).HasColumnName("id_cultural_event");

                entity.Property(e => e.EventDate)
                    .HasColumnName("event_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventDescription)
                    .IsRequired()
                    .HasColumnName("event_description")
                    .IsUnicode(false);

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnName("event_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdEventAddress).HasColumnName("id_event_address");

                entity.Property(e => e.SeatsLimit).HasColumnName("seats_limit");

                entity.HasOne(d => d.IdEventAddressNavigation)
                    .WithMany(p => p.CulturalEvent)
                    .HasForeignKey(d => d.IdEventAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cultural___id_ev__1332DBDC");
            });

            modelBuilder.Entity<EventAddress>(entity =>
            {
                entity.HasKey(e => e.IdEventAddress);

                entity.ToTable("Event_Address");

                entity.Property(e => e.IdEventAddress).HasColumnName("id_event_address");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movies>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2048);

                entity.Property(e => e.Director)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Picture)
                    .IsRequired()
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Rating).HasDefaultValueSql("(N'')");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.WatchingTime)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.HasKey(e => e.IdNewsletter);

                entity.Property(e => e.IdNewsletter).HasColumnName("id_newsletter");

                entity.Property(e => e.IdUserAccount).HasColumnName("id_user_account");

                entity.HasOne(d => d.IdUserAccountNavigation)
                    .WithMany(p => p.Newsletter)
                    .HasForeignKey(d => d.IdUserAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Newslette__id_us__160F4887");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.IdRating);

                entity.Property(e => e.IdRating).HasColumnName("id_rating");

                entity.Property(e => e.IdMovies).HasColumnName("id_Movies");

                entity.Property(e => e.RatingNumber).HasColumnName("rating_number");

                entity.HasOne(d => d.IdMoviesNavigation)
                    .WithMany(p => p.RatingNavigation)
                    .HasForeignKey(d => d.IdMovies)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rating__id_Movie__29221CFB");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.IdReservation);

                entity.Property(e => e.IdReservation).HasColumnName("id_reservation");

                entity.Property(e => e.IdScreening).HasColumnName("id_screening");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasMaxLength(450);

                entity.Property(e => e.IdUserAccount).HasColumnName("id_user_account");

                entity.Property(e => e.SeatsReserved)
                    .IsRequired()
                    .HasColumnName("seats_reserved")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Showtime).HasColumnName("showtime");

                entity.HasOne(d => d.IdScreeningNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.IdScreening)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservati__id_sc__41EDCAC5");

                entity.HasOne(d => d.IdUserAccountNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.IdUserAccount)
                    .HasConstraintName("FK__Reservati__id_us__40F9A68C");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.IdReview);

                entity.Property(e => e.IdReview).HasColumnName("id_review");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.IdMovies).HasColumnName("id_Movies");

                entity.Property(e => e.IdResponse).HasColumnName("id_response");

                entity.Property(e => e.Review1)
                    .IsRequired()
                    .HasColumnName("review")
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.IdMoviesNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.IdMovies)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__id_Movie__5224328E");

            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRoles);

                entity.Property(e => e.IdRoles).HasColumnName("id_roles");

                entity.Property(e => e.RoleDescription)
                    .IsRequired()
                    .HasColumnName("role_description")
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.IdRoom);

                entity.Property(e => e.IdRoom).HasColumnName("id_room");

                entity.Property(e => e.IdSeat).HasColumnName("id_seat");

                entity.Property(e => e.Layout)
                    .HasColumnName("layout")
                    .IsUnicode(false);

                entity.Property(e => e.RoomNumber).HasColumnName("room_number");

                entity.HasOne(d => d.IdSeatNavigation)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.IdSeat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Room__id_seat__30C33EC3");
            });

            modelBuilder.Entity<Screening>(entity =>
            {
                entity.HasKey(e => e.IdScreening);

                entity.Property(e => e.IdScreening).HasColumnName("id_screening");

                entity.Property(e => e.IdMovies).HasColumnName("id_movies");

                entity.Property(e => e.IdRoom).HasColumnName("id_Room");

                entity.Property(e => e.ScreeningDate)
                    .HasColumnName("screening_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.showtime1).HasColumnName("showtime1");

                entity.Property(e => e.showtime2).HasColumnName("showtime2");

                entity.Property(e => e.showtime3).HasColumnName("showtime3");

                entity.HasOne(d => d.IdMoviesNavigation)
                    .WithMany(p => p.Screening)
                    .HasForeignKey(d => d.IdMovies)
                    .HasConstraintName("FK__Screening__id_mo__09746778");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Screening)
                    .HasForeignKey(d => d.IdRoom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Screening__id_Ro__3493CFA7");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasKey(e => e.IdSeat);

                entity.Property(e => e.IdSeat).HasColumnName("id_seat");

                entity.Property(e => e.RowNumb)
                    .IsRequired()
                    .HasColumnName("row_numb")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SeatNumb).HasColumnName("seat_numb");
            });

            modelBuilder.Entity<SeatReservation>(entity =>
            {
                entity.HasKey(e => e.IdSeatReservation);

                entity.ToTable("Seat_Reservation");

                entity.Property(e => e.IdSeatReservation).HasColumnName("id_seat_reservation");

                entity.Property(e => e.IdSeat).HasColumnName("id_seat");

                entity.HasOne(d => d.IdSeatNavigation)
                    .WithMany(p => p.SeatReservation)
                    .HasForeignKey(d => d.IdSeat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seat_Rese__id_se__245D67DE");
            });

            modelBuilder.Entity<SigningIn>(entity =>
            {
                entity.HasKey(e => e.IdSigningIn);

                entity.ToTable("Signing_In");

                entity.Property(e => e.IdSigningIn).HasColumnName("id_signing_in");

                entity.Property(e => e.IdCulturalEvent).HasColumnName("id_cultural_event");

               // entity.Property(e => e.IdUserAccount).HasColumnName("id_user_account");

                entity.HasOne(d => d.IdCulturalEventNavigation)
                    .WithMany(p => p.SigningIn)
                    .HasForeignKey(d => d.IdCulturalEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Signing_I__id_cu__1CBC4616");

                /*entity.HasOne(d => d.IdUserAccountNavigation)
                    .WithMany(p => p.SigningIn)
                    .HasForeignKey(d => d.IdUserAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Signing_I__id_us__1DB06A4F"); */
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.IdUserAccount);

                entity.ToTable("User_Account");

                entity.Property(e => e.IdUserAccount).HasColumnName("id_user_account");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasColumnName("user_password")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserSurname)
                    .HasColumnName("user_surname")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserReview>(entity =>
            {
                entity.ToTable("User_Review");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.UserReview)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Revi__Revie__05A3D694");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserReview)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User_Revi__UserI__04AFB25B");
            });
        }
    }
}
