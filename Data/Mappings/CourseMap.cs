﻿using Challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Data.Mappings;

public class CourseMap : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");
        
        builder.HasKey(x => x.CourseId);
        
        builder.Property(x => x.CourseTitle).
            IsRequired().
            HasColumnName("CourseTitle").
            HasColumnType("NVARCHAR").
            HasMaxLength(80);
        
        builder.Property(x => x.Tag).
            IsRequired().
            HasColumnName("Tag")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(4);

        builder.Property(x => x.Summary).
            IsRequired().
            HasColumnName("Summary").
            HasColumnType("NVARCHAR").
            HasMaxLength(160);

        builder.Property(x => x.Duration)
            .IsRequired()
            .HasColumnName("Duration")
            .HasColumnType("INT");
        
        builder.HasMany(x => x.Modules)
            .WithOne()
            .HasForeignKey("ModulesId")
            .HasConstraintName("FK_Course_ModulesId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}