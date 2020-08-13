namespace S3Train.WebHeThong.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableHinhVanBan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HinhVanBan",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TenHinh = c.String(),
                        TaiLieuVanBanId = c.String(maxLength: 128),
                        NgayTao = c.DateTime(nullable: false),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaiLieuVanBan", t => t.TaiLieuVanBanId)
                .Index(t => t.TaiLieuVanBanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HinhVanBan", "TaiLieuVanBanId", "dbo.TaiLieuVanBan");
            DropIndex("dbo.HinhVanBan", new[] { "TaiLieuVanBanId" });
            DropTable("dbo.HinhVanBan");
        }
    }
}
