﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
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

        public virtual DbSet<MetaData> MetaData { get; set; }
        public virtual DbSet<QIFDocument> QIFDocument { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetaData>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.HasOne(d => d.QP)
                    .WithMany(p => p.MetaData)
                    .HasForeignKey(d => d.QPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MetaData_QIFDocument");
            });

            modelBuilder.Entity<QIFDocument>(entity =>
            {
                entity.Property(e => e.QPID).ValueGeneratedNever();
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}