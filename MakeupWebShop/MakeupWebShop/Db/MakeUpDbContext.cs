using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MakeupWebShop.Db;

public partial class MakeUpDbContext : DbContext
{
    public MakeUpDbContext()
    {
    }

    public MakeUpDbContext(DbContextOptions<MakeUpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAdresa> TblAdresas { get; set; }

    public virtual DbSet<TblBrend> TblBrends { get; set; }

    public virtual DbSet<TblKolekcija> TblKolekcijas { get; set; }

    public virtual DbSet<TblKorisnik> TblKorisniks { get; set; }

    public virtual DbSet<TblKorpa> TblKorpas { get; set; }

    public virtual DbSet<TblNamena> TblNamenas { get; set; }

    public virtual DbSet<TblProizvod> TblProizvods { get; set; }

    public virtual DbSet<TblProizvodUkorpi> TblProizvodUkorpis { get; set; }

    public virtual DbSet<TblRacun> TblRacuns { get; set; }

    public virtual DbSet<TblTip> TblTips { get; set; }

    public virtual DbSet<TblUloga> TblUlogas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-7G4I5HD\\SQLEXPRESS;Initial Catalog=ERPKozmetikaDb;Integrated Security=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAdresa>(entity =>
        {
            entity.HasKey(e => e.AdresaId);

            entity.ToTable("tblAdresa");

            entity.Property(e => e.AdresaId)
                .HasColumnType("int")
                .HasColumnName("AdresaID");
            entity.Property(e => e.Broj).HasMaxLength(13);
            entity.Property(e => e.Grad).HasMaxLength(20);
            entity.Property(e => e.PostanskiBroj).HasColumnType("int");
            entity.Property(e => e.Ulica).HasMaxLength(50);
        });

        modelBuilder.Entity<TblBrend>(entity =>
        {
            entity.HasKey(e => e.BrendId);

            entity.ToTable("tblBrend");

            entity.Property(e => e.BrendId)
                .HasColumnType("int")
                .HasColumnName("BrendID");
            entity.Property(e => e.NazivBrenda).HasMaxLength(50);
        });

        modelBuilder.Entity<TblKolekcija>(entity =>
        {
            entity.HasKey(e => e.KolekcijaId);

            entity.ToTable("tblKolekcija");

            entity.Property(e => e.KolekcijaId)
                .HasColumnType("int")
                .HasColumnName("KolekcijaID");
            entity.Property(e => e.NazivKolekcije).HasMaxLength(50);
        });

        modelBuilder.Entity<TblKorisnik>(entity =>
        {
            entity.HasKey(e => e.KorisnikId);

            entity.ToTable("tblKorisnik", tb => tb.HasTrigger("trg_SetPopustAndProcenatPop"));

            entity.Property(e => e.KorisnikId)
                .HasColumnType("int")
                .HasColumnName("KorisnikID");
            entity.Property(e => e.AdresaId)
                .HasColumnType("int")
                .HasColumnName("AdresaID");
            entity.Property(e => e.BrojKupovina).HasColumnType("int");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Ime).HasMaxLength(50);
            entity.Property(e => e.Jmbg)
                .HasMaxLength(13)
                .HasColumnName("JMBG");
            entity.Property(e => e.Kontakt).HasMaxLength(20);
            entity.Property(e => e.Lozinka).HasMaxLength(200);
            entity.Property(e => e.Prezime).HasMaxLength(50);
            entity.Property(e => e.UlogaId)
                .HasColumnType("int")
                .HasColumnName("UlogaID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Adresa).WithMany(p => p.TblKorisniks)
                .HasForeignKey(d => d.AdresaId)
                .HasConstraintName("FK_Korisnik_Adresa");

            entity.HasOne(d => d.Uloga).WithMany(p => p.TblKorisniks)
                .HasForeignKey(d => d.UlogaId)
                .HasConstraintName("FK_Korisnik_Uloga");
        });

        modelBuilder.Entity<TblKorpa>(entity =>
        {
            entity.HasKey(e => e.KorpaId);

            entity.ToTable("tblKorpa", tb =>  tb.HasTrigger("trgr_calculate_discount"));

            entity.Property(e => e.KorpaId)
                .HasColumnType("int")
                .HasColumnName("KorpaID");
            entity.Property(e => e.BrProizvoda).HasColumnType("int");
            entity.Property(e => e.KorisnikId)
                .HasColumnType("int")
                .HasColumnName("KorisnikID");
            entity.Property(e => e.ProcenatPop).HasColumnType("int");
            entity.Property(e => e.UkupanIznos).HasColumnType("numeric(8,2)");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.TblKorpas)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Korpa_Korisnik");
        });

        modelBuilder.Entity<TblNamena>(entity =>
        {
            entity.HasKey(e => e.NamenaId);

            entity.ToTable("tblNamena");

            entity.Property(e => e.NamenaId)
                .HasColumnType("int")
                .HasColumnName("NamenaID");
            entity.Property(e => e.NazivNamene).HasMaxLength(50);
        });

        modelBuilder.Entity<TblProizvod>(entity =>
        {
            entity.HasKey(e => e.ProizvodId);

            entity.ToTable("tblProizvod");

            entity.Property(e => e.ProizvodId)
                .HasColumnType("int")
                .HasColumnName("ProizvodID");
            entity.Property(e => e.BrendId)
                .HasColumnType("int")
                .HasColumnName("BrendID");
            entity.Property(e => e.CenaPoKom).HasColumnType("numeric(8, 2)");
            entity.Property(e => e.KolekcijaId)
                .HasColumnType("int")
                .HasColumnName("KolekcijaID");
            entity.Property(e => e.KolicinaNaStanju).HasColumnType("int");
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.NacinUpotrebe).HasMaxLength(250);
            entity.Property(e => e.NamenaId)
                .HasColumnType("int")
                .HasColumnName("NamenaID");
            entity.Property(e => e.Sastav).HasMaxLength(250);
            entity.Property(e => e.TipId)
                .HasColumnType("int")
                .HasColumnName("TipID");

            entity.HasOne(d => d.Brend).WithMany(p => p.TblProizvods)
                .HasForeignKey(d => d.BrendId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvod_Brend");

            entity.HasOne(d => d.Kolekcija).WithMany(p => p.TblProizvods)
                .HasForeignKey(d => d.KolekcijaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvod_Kolekcija");

            entity.HasOne(d => d.Namena).WithMany(p => p.TblProizvods)
                .HasForeignKey(d => d.NamenaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvod_Namena");

            entity.HasOne(d => d.Tip).WithMany(p => p.TblProizvods)
                .HasForeignKey(d => d.TipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvod_Tip");
        });

        modelBuilder.Entity<TblProizvodUkorpi>(entity =>
        {
            entity.HasKey(e => e.ProizUkorpiId);

            entity.ToTable("tblProizvodUKorpi");

            entity.Property(e => e.ProizUkorpiId)
                .HasColumnType("int")
                .HasColumnName("ProizUKorpiID");
            entity.Property(e => e.BrojKomada).HasColumnType("int");
            entity.Property(e => e.KorpaId)
                .HasColumnType("int")
                .HasColumnName("KorpaID");
            entity.Property(e => e.ProizvodId)
                .HasColumnType("int")
                .HasColumnName("ProizvodID");

            entity.HasOne(d => d.Korpa).WithMany(p => p.TblProizvodUkorpis)
                .HasForeignKey(d => d.KorpaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProizvodUKorpi_Korpa");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.TblProizvodUkorpis)
                .HasForeignKey(d => d.ProizvodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProizvodUKorpi_Proizvod");
        });

        modelBuilder.Entity<TblRacun>(entity =>
        {
            entity.HasKey(e => e.RacunId);

            entity.ToTable("tblRacun", tb => tb.HasTrigger("trg_BrojKupovina"));

            entity.Property(e => e.RacunId)
                .HasColumnType("int")
                .HasColumnName("RacunID");
            entity.Property(e => e.DatumKupovine).HasColumnType("date");
            entity.Property(e => e.IznosPopusta).HasColumnType("numeric(8, 2)");
            entity.Property(e => e.IznosPost).HasColumnType("numeric(8, 2)");
            entity.Property(e => e.IznosSaPopustom).HasColumnType("numeric(8, 2)");
            entity.Property(e => e.IznosSaPost).HasColumnType("numeric(8, 2)");
            entity.Property(e => e.ClientSecret).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.PaymentIntentId).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.KorpaId)
                .HasColumnType("int")
                .HasColumnName("KorpaID");

            entity.HasOne(d => d.Korpa).WithMany(p => p.TblRacuns)
                .HasForeignKey(d => d.KorpaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Racun_Korpa");
        });

        modelBuilder.Entity<TblTip>(entity =>
        {
            entity.HasKey(e => e.TipId);

            entity.ToTable("tblTip");

            entity.Property(e => e.TipId)
                .HasColumnType("int")
                .HasColumnName("TipID");
            entity.Property(e => e.NazivTipa).HasMaxLength(50);
        });

        modelBuilder.Entity<TblUloga>(entity =>
        {
            entity.HasKey(e => e.UlogaId);

            entity.ToTable("tblUloga");

            entity.Property(e => e.UlogaId)
                .HasColumnType("int")
                .HasColumnName("UlogaID");
            entity.Property(e => e.KratakOpis).HasMaxLength(90);
            entity.Property(e => e.NazivUloge).HasMaxLength(30);
            entity.Property(e => e.Sifra)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });
        modelBuilder.HasSequence<int>("AdresaID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);
        modelBuilder.HasSequence<int>("BrendID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);
        modelBuilder.HasSequence<int>("KolekcijaID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);
        /*modelBuilder.HasSequence<int>("KorisnikID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);
        modelBuilder.HasSequence<int>("KorpaID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);*/
        modelBuilder.HasSequence<int>("NamenaID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);
        modelBuilder.HasSequence<int>("ProizUKorpiID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);
        modelBuilder.HasSequence<int>("ProizvodID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);
        modelBuilder.HasSequence<int>("RacunID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);
        modelBuilder.HasSequence<int>("TipID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);
        modelBuilder.HasSequence<int>("UlogaID_seq")
            .HasMin(1L)
            .HasMax(999999999999999999L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
