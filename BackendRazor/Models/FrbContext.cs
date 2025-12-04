using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackendRazor.Models;

public partial class FrbContext : DbContext
{
    public FrbContext()
    {
    }

    public FrbContext(DbContextOptions<FrbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerMatchStat> PlayerMatchStats { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5423;Database=FRB;Username=admin;Password=pitesti");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Idadmin).HasName("admin_pk");

            entity.ToTable("admin");

            entity.HasIndex(e => e.Username, "admin_user_uk").IsUnique();

            entity.Property(e => e.Idadmin)
                .HasColumnType("character varying")
                .HasColumnName("idadmin");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasColumnType("character varying")
                .HasColumnName("username");
        });

        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => e.Idcoach).HasName("coach_pk");

            entity.ToTable("coach", tb => tb.HasComment("tabela antrenori"));

            entity.HasIndex(e => new { e.Firstname, e.Lastname, e.Birthdate }, "coach_uk").IsUnique();

            entity.Property(e => e.Idcoach).HasColumnName("idcoach");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Nationality)
                .HasColumnType("character varying")
                .HasColumnName("nationality");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Idmatch).HasName("match_pk");

            entity.ToTable("match");

            entity.Property(e => e.Idmatch).HasColumnName("idmatch");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Idguest).HasColumnName("idguest");
            entity.Property(e => e.Idhome).HasColumnName("idhome");
            entity.Property(e => e.Scoreguest).HasColumnName("scoreguest");
            entity.Property(e => e.Scorehome).HasColumnName("scorehome");

            entity.HasOne(d => d.IdguestNavigation).WithMany(p => p.MatchIdguestNavigations)
                .HasForeignKey(d => d.Idguest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("match_guest_fk");

            entity.HasOne(d => d.IdhomeNavigation).WithMany(p => p.MatchIdhomeNavigations)
                .HasForeignKey(d => d.Idhome)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("match_home_fk");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Idplayer).HasName("player_pkey");

            entity.ToTable("player");

            entity.Property(e => e.Idplayer).HasColumnName("idplayer");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Idteam).HasColumnName("idteam");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Position)
                .HasColumnType("character varying")
                .HasColumnName("position");

            entity.HasOne(d => d.IdteamNavigation).WithMany(p => p.Players)
                .HasForeignKey(d => d.Idteam)
                .HasConstraintName("player_team_fk");
        });

        modelBuilder.Entity<PlayerMatchStat>(entity =>
        {
            entity.HasKey(e => e.Idpms).HasName("player_match_stats_pk");

            entity.ToTable("player_match_stats");

            entity.HasIndex(e => new { e.Idmatch, e.Idplayer }, "player_match_stats_unique").IsUnique();

            entity.Property(e => e.Idpms).HasColumnName("idpms");
            entity.Property(e => e.Assists).HasColumnName("assists");
            entity.Property(e => e.Idmatch).HasColumnName("idmatch");
            entity.Property(e => e.Idplayer).HasColumnName("idplayer");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.Rebounds).HasColumnName("rebounds");

            entity.HasOne(d => d.IdmatchNavigation).WithMany(p => p.PlayerMatchStats)
                .HasForeignKey(d => d.Idmatch)
                .HasConstraintName("player_match_stats_match_fk");

            entity.HasOne(d => d.IdplayerNavigation).WithMany(p => p.PlayerMatchStats)
                .HasForeignKey(d => d.Idplayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("player_match_stats_player_fk");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Idteam).HasName("team_pk");

            entity.ToTable("team");

            entity.HasIndex(e => e.Name, "team_unique").IsUnique();

            entity.HasIndex(e => e.Idcoach, "team_unique_fk").IsUnique();

            entity.Property(e => e.Idteam).HasColumnName("idteam");
            entity.Property(e => e.City)
                .HasColumnType("character varying")
                .HasColumnName("city");
            entity.Property(e => e.Idcoach).HasColumnName("idcoach");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.IdcoachNavigation).WithOne(p => p.Team)
                .HasForeignKey<Team>(d => d.Idcoach)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("team_coach_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
