using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Webbansach.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Adress { get; set; }
        public int SDT { get; set; }
        public float score { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("WebSach", throwIfV1Schema: false)
        {
        }
        public DbSet<TheLoai> theLoais { get; set; }
        public DbSet<NXB> nXBs { get; set; }
        public DbSet<KhuyenMai> khuyenMais { get; set; }
        public DbSet<TinTuc> tinTucs { get; set; }
        public DbSet<TacGia> tacGias { get; set; }
        public DbSet<SanPham> sanPhams { get; set; }

        public DbSet<Order> orders { get; set; }
        public DbSet <OrderDetail> OrderDetails { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}