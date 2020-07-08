﻿namespace S3Train.WebHeThong.Migrations
{
    using S3Train.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var vanBans = new List<TaiLieuVanBan>()
            {
                new TaiLieuVanBan
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = "4f1e5643-ed59-4c98-a600-9437930a7e34",
                    Dang = "Văn Bản Đến",
                    DuongDan="",
                    GhiChu = "",
                    HinhAnh = "",
                    NgayBanHanh = DateTime.Now,
                    Ten="Test Demo",
                    SoKyHieu="",
                    NoiNhan = "",
                    TinhTrang = EnumTinhTrang.TrongKho,
                    SoTo= 4 ,
                    NoiDung="",
                    NoiBanHanhId="44dfb8f3-21b7-407f-85d3-9709cf77aed4",
                    HoSoId="85306916-f834-49fc-8178-0c9c0a69aa30",
                    Loai = "Nghị Quyết",
                    NguoiDuyet = "Nguyễn Văn B",
                    NguoiKy = "Nguyễn Văn B",
                    NguoiGuiHoacNhan="Lê Thị Hoa",
                    TrangThai = true,
                    NgayTao = DateTime.Now,
                },

            };
            vanBans.ForEach(x => context.TaiLieuVanBans.AddOrUpdate(c => c.Ten, x));
            context.SaveChanges();


            //var tus = new List<Tu>()
            //{
            //    new Tu
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Tủ lưu trữ sô 1",
            //        DonViTinh = "Kệ",
            //        DienTich = "10x10",
            //        NgươiQuanLy = "",
            //        SoLuongMax = 30,
            //        SoLuongHienTai = 25,
            //        TinhTrang = "",
            //        ViTri = "Trường học",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new Tu
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Tủ lưu trữ sô 2",
            //        DonViTinh = "Kệ",
            //        DienTich = "10x8",
            //        NgươiQuanLy = "",
            //        SoLuongMax = 28,
            //        SoLuongHienTai = 28,
            //        TinhTrang = "",
            //        ViTri = "7/6/2 đường linh đông/phường linh đông/quận thủ đức",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new Tu
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Tủ lưu trữ số 3",
            //        DonViTinh = "Kệ",
            //        DienTich = "10x10",
            //        NgươiQuanLy = "",
            //        SoLuongMax = 30,
            //        SoLuongHienTai = 30,
            //        TinhTrang = "",
            //        ViTri = "25 nguyễn huệ quân 9",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new Tu
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "=Tủ lưu trữ sô 3",
            //        DonViTinh = "Kệ",
            //        DienTich = "10x10",
            //        NgươiQuanLy = "",
            //        SoLuongMax = 30,
            //        SoLuongHienTai = 15,
            //        TinhTrang = "",
            //        ViTri = "Tầng 4 trường học",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new Tu
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Tủ lưu trữ sô 4",
            //        DonViTinh = "Kệ",
            //        DienTich = "10x10",
            //        NgươiQuanLy = "",
            //        SoLuongMax = 30,
            //        SoLuongHienTai = 30,
            //        TinhTrang = "",
            //        ViTri = "Khu công nghệ cao quận 9",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    }
            //};
            //tus.ForEach(x => context.Tus.AddOrUpdate(c => c.Ten, x));
            //context.SaveChanges();

            //var noiBanHanhs = new List<NoiBanHanh>()
            //{
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Văn Phòng Chính Phủ",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Văn Phòng Quốc Hội",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Tổng Cục Thuế",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Tổng Cục Hải Quan",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Bộ Y Tế",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Bộ Văn Hóa",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Bộ Tư Pháp",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Bộ Xây Dựng",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Bộ Tài Chính",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Bộ Quốc Phòng",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Bộ Thương Mại",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Bộ Thông Tin Và Truyền Thông",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Bộ Văn Hóa Và Thể Thao Du Lịch",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Thủ Tướng Chính Phủ",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Thanh Tra Chính Phủ",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Viện Kiểm Soát Nhân Dân Tối Cao",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Tòa Án Nhân Dân Tối Cao",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Ngân Hàng Nhà Nước Việt Nam",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new NoiBanHanh
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Tổng Liên Đoàn Lao Động",
            //        MoTa = "",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    }
            //};
            //noiBanHanhs.ForEach(x => context.NoiBanHanhs.AddOrUpdate(c => c.Ten, x));
            //context.SaveChanges();

            //var pBans = new List<PhongBan>()
            //{
            //    new PhongBan
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Phòng Tổ Chức Hành Chính",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new PhongBan
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Phòng Đào Tạo",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new PhongBan
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Phòng Công Tác HSSV",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new PhongBan
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Phòng Khoa Học Công Nghệ",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new PhongBan
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Phòng Tài Chính Kế Toán",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new PhongBan
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Phòng Quản Trị",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new PhongBan
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ten = "Phòng Thanh Tra Giáo Dục",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    }
            //};
            //pBans.ForEach(x => context.phongBans.AddOrUpdate(c => c.Ten, x));
            //context.SaveChanges();

            //var roles = new List<ApplicationRole>()
            //{
            //    new ApplicationRole
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name ="Cán Bộ Thủ Thư",
            //        Description ="",
            //    },
            //    new ApplicationRole
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name ="Ban Quản Lý",
            //        Description ="",
            //    },
            //    new ApplicationRole
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name ="Cán Bộ Thư Văn",
            //        Description ="",
            //    }
            //};
            //roles.ForEach(x => context.Roles.AddOrUpdate(c => c.Name, x));
            //context.SaveChanges();
            //var loaiHoSos = new List<LoaiHoSo>()
            //{
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "CV",
            //        Ten ="Công Văn",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "QĐQP",
            //        Ten ="Quyết địng quy phạm",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "CT",
            //        Ten ="Chỉ Thị",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "TB",
            //        Ten ="Thông Báo",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "QĐ",
            //        Ten ="Quyết địng",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "GM",
            //        Ten ="Giấy Mời",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "KH",
            //        Ten ="Kế hoạch",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "BC",
            //        Ten ="Báo Cáo",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "KQXN",
            //        Ten ="Kết quả xét nghiệm",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "KQ",
            //        Ten ="Kết Quả",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "TTr",
            //        Ten ="Tờ trình",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "QC",
            //        Ten ="Quy chế",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "TT",
            //        Ten ="Thông Tư",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "NQ",
            //        Ten ="Nghị Quyết",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "BB",
            //        Ten ="Biên Bản",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    },
            //    new LoaiHoSo
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Ma = "TL",
            //        Ten ="Tài Liệu",
            //        NgayTao = DateTime.Now,
            //        TrangThai = true
            //    }
            //};
            //loaiHoSos.ForEach(x => context.LoaiHoSos.AddOrUpdate(c => c.Ten, x));
            //context.SaveChanges();
        }
    }
}
