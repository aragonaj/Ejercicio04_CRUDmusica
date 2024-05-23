using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio03.Models;

public partial class GrupoBContext : DbContext
{
    public GrupoBContext()
    {
    }

    public GrupoBContext(DbContextOptions<GrupoBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Albume> Albumes { get; set; }

    public virtual DbSet<Artista> Artistas { get; set; }

    public virtual DbSet<Cancione> Canciones { get; set; }

    public virtual DbSet<Ciudade> Ciudades { get; set; }

    public virtual DbSet<Concierto> Conciertos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Funcione> Funciones { get; set; }

    public virtual DbSet<FuncionesArtista> FuncionesArtistas { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Gira> Giras { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Paise> Paises { get; set; }

    public virtual DbSet<Plataforma> Plataformas { get; set; }

    public virtual DbSet<Representante> Representantes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<VideoClip> VideoClips { get; set; }

    public virtual DbSet<VideoClipsPlataforma> VideoClipsPlataformas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoB;user=as;password=P0t@t0P0t@t0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Albume>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Generos).WithMany(p => p.Albumes)
                .HasForeignKey(d => d.GenerosId)
                .HasConstraintName("FK_Albumes_Generos");

            entity.HasOne(d => d.Grupos).WithMany(p => p.Albumes)
                .HasForeignKey(d => d.GruposId)
                .HasConstraintName("FK_Albumes_Grupos");
        });

        modelBuilder.Entity<Artista>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudades).WithMany(p => p.Artista)
                .HasForeignKey(d => d.CiudadesId)
                .HasConstraintName("FK_Artistas_Ciudades");

            entity.HasOne(d => d.Generos).WithMany(p => p.Artista)
                .HasForeignKey(d => d.GenerosId)
                .HasConstraintName("FK_Artistas_Generos");

            entity.HasOne(d => d.Grupos).WithMany(p => p.Artista)
                .HasForeignKey(d => d.GruposId)
                .HasConstraintName("FK_Artistas_Grupos");
        });

        modelBuilder.Entity<Cancione>(entity =>
        {
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Albumes).WithMany(p => p.Canciones)
                .HasForeignKey(d => d.AlbumesId)
                .HasConstraintName("FK_Canciones_Albumes");
        });

        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaisesId).HasColumnName("PaisesID");

            entity.HasOne(d => d.Paises).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.PaisesId)
                .HasConstraintName("FK_Ciudades_Paises");
        });

        modelBuilder.Entity<Concierto>(entity =>
        {
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudades).WithMany(p => p.Conciertos)
                .HasForeignKey(d => d.CiudadesId)
                .HasConstraintName("FK_Conciertos_Ciudades");

            entity.HasOne(d => d.Giras).WithMany(p => p.Conciertos)
                .HasForeignKey(d => d.GirasId)
                .HasConstraintName("FK_Conciertos_Giras");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Roles).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.RolesId)
                .HasConstraintName("FK_Empleados_Roles");
        });

        modelBuilder.Entity<Funcione>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FuncionesArtista>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Artistas).WithMany(p => p.FuncionesArtista)
                .HasForeignKey(d => d.ArtistasId)
                .HasConstraintName("FK_FuncionesArtistas_Artistas");

            entity.HasOne(d => d.Funciones).WithMany(p => p.FuncionesArtista)
                .HasForeignKey(d => d.FuncionesId)
                .HasConstraintName("FK_FuncionesArtistas_Funciones");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Gira>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Grupos).WithMany(p => p.Giras)
                .HasForeignKey(d => d.GruposId)
                .HasConstraintName("FK_Giras_Grupos");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.Property(e => e.Grupo1).HasColumnName("grupo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudades).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.CiudadesId)
                .HasConstraintName("FK_Grupos_Ciudades");

            entity.HasOne(d => d.Generos).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.GenerosId)
                .HasConstraintName("FK_Grupos_Generos");

            entity.HasOne(d => d.Representantes).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.RepresentantesId)
                .HasConstraintName("FK_Grupos_Representantes");
        });

        modelBuilder.Entity<Paise>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Representante>(entity =>
        {
            entity.Property(e => e.CiudadesId).HasColumnName("CiudadesID");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mail");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudades).WithMany(p => p.Representantes)
                .HasForeignKey(d => d.CiudadesId)
                .HasConstraintName("FK_Representantes_Ciudades");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VideoClip>(entity =>
        {
            entity.HasOne(d => d.Canciones).WithMany(p => p.VideoClips)
                .HasForeignKey(d => d.CancionesId)
                .HasConstraintName("FK_VideoClips_Canciones");
        });

        modelBuilder.Entity<VideoClipsPlataforma>(entity =>
        {
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("url");

            entity.HasOne(d => d.Plataformas).WithMany(p => p.VideoClipsPlataformas)
                .HasForeignKey(d => d.PlataformasId)
                .HasConstraintName("FK_VideoClipsPlataformas_Plataformas");

            entity.HasOne(d => d.VideoClips).WithMany(p => p.VideoClipsPlataformas)
                .HasForeignKey(d => d.VideoClipsId)
                .HasConstraintName("FK_VideoClipsPlataformas_VideoClips");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
