using Core.Entity;
using Model.Entities;
using Model.Maps;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.Context
{
   public class ProjectContext : DbContext
    {
        public ProjectContext()
        {
            Database.Connection.ConnectionString = "Server=.; Database=TrendyolECommerceProject; UID=sa; PWD=123;";

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new ColourMap());
            modelBuilder.Configurations.Add(new SizeToMap());
            modelBuilder.Configurations.Add(new BrandMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new SubCategoryMap());
            modelBuilder.Configurations.Add(new ThirdCategoryMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ShipMap());
            modelBuilder.Configurations.Add(new ProvinceMap());
            modelBuilder.Configurations.Add(new RegionToMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new RoadMap());
            modelBuilder.Configurations.Add(new StreetMap());
            modelBuilder.Configurations.Add(new BuildMap());
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new CampaingMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new CustomerNonMemberMap());
            modelBuilder.Configurations.Add(new ShoppingCartMap());
            modelBuilder.Configurations.Add(new WishListMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ImagePathMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new ProductDetailMap());
            modelBuilder.Configurations.Add(new MessageMap());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Colour> Colors { get; set; }
        public DbSet<SizeTo> Sizes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ThirdCategory> ThirdCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<RegionTo> Regions { get; set; }
        public DbSet<District> Districs { get; set; }
        public DbSet<Road> Roads { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Build> Builds { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Campaing> Campaings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CustomerNonMember> CustomerNonMembers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImagePath> ImagePaths { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Message> Messages { get; set; }

        public override int SaveChanges()
        {
            var ModifiedEntities = ChangeTracker.Entries().Where(m => m.State == EntityState.Modified || m.State == EntityState.Added).ToList();
            DateTime datetime = DateTime.Now;
            string ip = HttpContext.Current.Request.UserHostAddress;
            foreach (var item in ModifiedEntities)
            {
                CoreEntity entity = item.Entity as CoreEntity;
                if(item!=null)
                {
                    switch(item.State)
                    {
                        case EntityState.Added:
                            entity.CreatedIP = ip;
                            entity.CreatedDate = datetime;
                            break;
                        case EntityState.Modified:
                            entity.ModifiedIP = ip;
                            entity.ModifiedDate = datetime;
                            break;
                    }
                }
            }
          
            return base.SaveChanges();
        }
    }
}
