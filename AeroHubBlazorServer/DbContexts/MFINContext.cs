using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AeroHubBlazorServer.Models;

namespace AeroHubBlazorServer.DbContexts
{
    public partial class MFINContext : DbContext
    {
        public MFINContext()
        {
        }

        public MFINContext(DbContextOptions<MFINContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActualComponentSet> ActualComponentSet { get; set; }
        public virtual DbSet<CharacteristicFeatureReference> CharacteristicFeatureReference { get; set; }
        public virtual DbSet<CharacteristicItems> CharacteristicItems { get; set; }
        public virtual DbSet<FeatureItems> FeatureItems { get; set; }
        public virtual DbSet<MeasuredCharacterFeatureReference> MeasuredCharacterFeatureReference { get; set; }
        public virtual DbSet<MeasuredCharacteristics> MeasuredCharacteristics { get; set; }
        public virtual DbSet<MeasuredFeatures> MeasuredFeatures { get; set; }
        public virtual DbSet<MeasurementResults> MeasurementResults { get; set; }
        public virtual DbSet<MetaData> MetaData { get; set; }
        public virtual DbSet<QIFDocument> QIFDocument { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActualComponentSet>(entity =>
            {
                entity.HasKey(e => new { e.Updateid, e.QPID });

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.QP)
                    .WithMany(p => p.ActualComponentSet)
                    .HasForeignKey(d => d.QPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActualComponentSet_QIFDocument");
            });

            modelBuilder.Entity<CharacteristicFeatureReference>(entity =>
            {
                entity.HasKey(e => new { e.QPID, e.UpdatedFeatureid, e.UpdatedCharacteristicid });

                entity.Property(e => e.FileVersion).HasColumnType("datetime");

                entity.HasOne(d => d.QP)
                    .WithMany(p => p.CharacteristicFeatureReference)
                    .HasForeignKey(d => d.QPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharacteristicFeatureReference_QIFDocument");

                entity.HasOne(d => d.CharacteristicItems)
                    .WithMany(p => p.CharacteristicFeatureReference)
                    .HasForeignKey(d => new { d.UpdatedCharacteristicid, d.QPID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharacteristicFeatureReference_CharacteristicItems");

                entity.HasOne(d => d.FeatureItems)
                    .WithMany(p => p.CharacteristicFeatureReference)
                    .HasForeignKey(d => new { d.UpdatedFeatureid, d.QPID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharacteristicFeatureReference_FeatureItems");
            });

            modelBuilder.Entity<CharacteristicItems>(entity =>
            {
                entity.HasKey(e => new { e.Updateid, e.QPID });

                entity.Property(e => e.CharacteristicType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FileVersion).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.QP)
                    .WithMany(p => p.CharacteristicItems)
                    .HasForeignKey(d => d.QPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharacteristicItems_QIFDocument");
            });

            modelBuilder.Entity<FeatureItems>(entity =>
            {
                entity.HasKey(e => new { e.Updateid, e.QPID })
                    .HasName("PK_FeatureItems_1");

                entity.Property(e => e.FeatureName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FeatureType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FileVersion).HasColumnType("datetime");

                entity.HasOne(d => d.QP)
                    .WithMany(p => p.FeatureItems)
                    .HasForeignKey(d => d.QPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeatureItems_QIFDocument");
            });

            modelBuilder.Entity<MeasuredCharacterFeatureReference>(entity =>
            {
                entity.HasKey(e => new { e.QPID, e.UpdatedFeatureid, e.UpdatedCharacteristicid });

                entity.Property(e => e.FIleVersion).HasColumnType("datetime");

                entity.HasOne(d => d.MeasuredCharacteristics)
                    .WithMany(p => p.MeasuredCharacterFeatureReference)
                    .HasForeignKey(d => new { d.UpdatedCharacteristicid, d.QPID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeasuredCharacterFeatureReference_MeasuredCharacteristics");

                entity.HasOne(d => d.MeasuredFeatures)
                    .WithMany(p => p.MeasuredCharacterFeatureReference)
                    .HasForeignKey(d => new { d.UpdatedFeatureid, d.QPID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeasuredCharacterFeatureReference_MeasuredFeatures");
            });

            modelBuilder.Entity<MeasuredCharacteristics>(entity =>
            {
                entity.HasKey(e => new { e.Updateid, e.QPID })
                    .HasName("PK_MeasuredCharacteristics_1");

                entity.Property(e => e.CharacteristicType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FileVersion).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.QP)
                    .WithMany(p => p.MeasuredCharacteristics)
                    .HasForeignKey(d => d.QPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeasuredCharacteristics_QIFDocument");

                entity.HasOne(d => d.ActualComponentSet)
                    .WithMany(p => p.MeasuredCharacteristics)
                    .HasForeignKey(d => new { d.ActualComponentid, d.QPID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeasuredCharacteristics_ActualComponentSet1");

                entity.HasOne(d => d.MeasurementResults)
                    .WithMany(p => p.MeasuredCharacteristics)
                    .HasForeignKey(d => new { d.Resultsid, d.ActualComponentid, d.QPID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeasuredCharacteristics_MeasurementResults");
            });

            modelBuilder.Entity<MeasuredFeatures>(entity =>
            {
                entity.HasKey(e => new { e.Updateid, e.QPID });

                entity.Property(e => e.FeatureType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FileVersion).HasColumnType("datetime");

                entity.HasOne(d => d.QP)
                    .WithMany(p => p.MeasuredFeatures)
                    .HasForeignKey(d => d.QPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeasuredFeatures_QIFDocument");

                entity.HasOne(d => d.ActualComponentSet)
                    .WithMany(p => p.MeasuredFeatures)
                    .HasForeignKey(d => new { d.ActualComponentid, d.QPID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeasuredFeatures_ActualComponentSet");

                entity.HasOne(d => d.MeasurementResults)
                    .WithMany(p => p.MeasuredFeatures)
                    .HasForeignKey(d => new { d.Resultsid, d.ActualComponentid, d.QPID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeasuredFeatures_MeasurementResults");
            });

            modelBuilder.Entity<MeasurementResults>(entity =>
            {
                entity.HasKey(e => new { e.Updateid, e.ActualComponentid, e.QPID });

                entity.Property(e => e.InspectionStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.QP)
                    .WithMany(p => p.MeasurementResults)
                    .HasForeignKey(d => d.QPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeasurementResults_QIFDocument");
            });

            modelBuilder.Entity<MetaData>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.DocumentType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EngineOperator)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EngineSerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileLink)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.OverhaulDate).HasColumnType("datetime");

                entity.Property(e => e.PartNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.QP)
                    .WithMany(p => p.MetaData)
                    .HasForeignKey(d => d.QPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MetaData_QIFDocument");
            });

            modelBuilder.Entity<QIFDocument>(entity =>
            {
                entity.HasKey(e => e.QPID);

                entity.Property(e => e.QPID).ValueGeneratedNever();

                entity.Property(e => e.MBD)
                    .IsRequired()
                    .HasColumnType("xml");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
