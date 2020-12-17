namespace Webbansach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KhuyenMais",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenKM = c.String(),
                        Mota = c.String(),
                        PTKM = c.Int(nullable: false),
                        HinhAnh = c.String(),
                        ThoiGianBatDau = c.DateTime(nullable: false),
                        ThoiGianKetThuc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NXBs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenNXB = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        SanPhamID = c.Int(nullable: false),
                        Gia = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.SanPhams", t => t.SanPhamID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.SanPhamID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderName = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        PaymentType = c.String(),
                        Status = c.String(),
                        Name = c.String(),
                        Phone = c.Int(nullable: false),
                        Email = c.String(),
                        Adress = c.String(),
                        UserID = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.IdentityUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenSP = c.String(),
                        MaTG = c.Int(nullable: false),
                        MaNXB = c.Int(nullable: false),
                        NamXB = c.DateTime(nullable: false),
                        MaLoai = c.Int(nullable: false),
                        MaKM = c.Int(nullable: false),
                        DanhGia = c.String(),
                        BinhLuan = c.String(),
                        Mota = c.String(),
                        ChieuCao = c.Int(nullable: false),
                        ChieuRong = c.Int(nullable: false),
                        SoTrang = c.Int(nullable: false),
                        SoLuongSach = c.Int(nullable: false),
                        HinhAnh = c.String(),
                        GiaSP = c.Double(nullable: false),
                        PTKM = c.Double(nullable: false),
                        KhuyenMai_ID = c.Int(),
                        NXB_ID = c.Int(),
                        TacGia_ID = c.Int(),
                        TheLoai_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.KhuyenMais", t => t.KhuyenMai_ID)
                .ForeignKey("dbo.NXBs", t => t.NXB_ID)
                .ForeignKey("dbo.TacGias", t => t.TacGia_ID)
                .ForeignKey("dbo.TheLoais", t => t.TheLoai_ID)
                .Index(t => t.KhuyenMai_ID)
                .Index(t => t.NXB_ID)
                .Index(t => t.TacGia_ID)
                .Index(t => t.TheLoai_ID);
            
            CreateTable(
                "dbo.TacGias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenTacGia = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TheLoais",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenTheLoai = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TinTucs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(),
                        NoiDung = c.String(),
                        HinhAnh = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Adress = c.String(),
                        SDT = c.Int(nullable: false),
                        score = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderDetails", "SanPhamID", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "TheLoai_ID", "dbo.TheLoais");
            DropForeignKey("dbo.SanPhams", "TacGia_ID", "dbo.TacGias");
            DropForeignKey("dbo.SanPhams", "NXB_ID", "dbo.NXBs");
            DropForeignKey("dbo.SanPhams", "KhuyenMai_ID", "dbo.KhuyenMais");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.IdentityUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SanPhams", new[] { "TheLoai_ID" });
            DropIndex("dbo.SanPhams", new[] { "TacGia_ID" });
            DropIndex("dbo.SanPhams", new[] { "NXB_ID" });
            DropIndex("dbo.SanPhams", new[] { "KhuyenMai_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUsers", "UserNameIndex");
            DropIndex("dbo.Orders", new[] { "IdentityUser_Id" });
            DropIndex("dbo.OrderDetails", new[] { "SanPhamID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TinTucs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TheLoais");
            DropTable("dbo.TacGias");
            DropTable("dbo.SanPhams");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.IdentityUsers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.NXBs");
            DropTable("dbo.KhuyenMais");
        }
    }
}
