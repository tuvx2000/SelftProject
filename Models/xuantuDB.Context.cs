﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSpecialProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebProjectEntities : DbContext
    {
        public WebProjectEntities()
            : base("name=WebProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<BasicSetting> BasicSettings { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<DetailedInvoice> DetailedInvoices { get; set; }
        public virtual DbSet<EntityProfile> EntityProfiles { get; set; }
        public virtual DbSet<EntityProfileItem> EntityProfileItems { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Forum> Fora { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Stuff> Stuffs { get; set; }
        public virtual DbSet<TypeOfFood> TypeOfFoods { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
