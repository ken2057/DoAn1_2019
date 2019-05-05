use master
if exists (select * from sysdatabases where name='QLBookStore')
	drop database QLBookStore
go
create database QLBookStore
go
use QLBookStore
go
Create table TAIKHOAN (
	TenTaiKhoan Varchar(50) primary key,
	MauKhau Varchar(200) NOT NULL,
	HoTen nVarchar(50) NULL,
	Sdt Varchar(10) NOT NULL,
	DiaChi nvarChar(100) not null,
	Email Varchar(50) NULL,
	GioiTinh Bit NULL,
	HienThiTK Bit default 1,
)
go
Create table NHANVIEN (
	TenTaiKhoanNV Varchar(50) NOT NULL primary key foreign key references TAIKHOAN,
	ChucVuNV nVarchar(20) NULL,
	HienThiNV bit default 1,
)
go
Create table NHAXUATBAN (
	MaNhaXuatBan Char(10) NOT NULL primary key,
	TenNhaXuatBan Nvarchar(50) NULL,
	SoDauSachXB Smallint NULL,
)
go
Create table THELOAI (
	MaTheLoai Char(10) NOT NULL primary key,
	TenTheLoai nVarchar(30) NULL,
	HienThiTL Bit default 1,
)
go
Create table KHUYENMAI (
	MaKhuyenMai Char(10) NOT NULL primary key,
	TenKhuyenMai nVarchar(30) NULL,
	NgayBatDau Datetime NULL,
	NgayKetThuc Datetime NULL,
	PhanTramKhuyenMai Tinyint NULL,
	HienThiKM Bit default 1,
)
go
Create table NHACUNGCAP (
	MaNhaCungCap Char(10) NOT NULL primary key,
	DiaChiNCC nVarchar(100) NULL,
	SoDienThoaiNCC Char(10) NULL,
	EmailNCC Varchar(30),
	HienThiNCC Bit default 1,
)
go
Create table SACH (
	MaSach Char(10) NOT NULL primary key,
	TenSach nVarchar(100) not null,
	SKU varchar(20) null,
	MaNhaCungCap Char(10) NULL foreign key references NHACUNGCAP,
	MaKhuyenMai Char(10) null foreign key references KHUYENMAI,
	MaNhaXuatBan Char(10) NULL foreign key references NHAXUATBAN,
	GiaBan Integer NULL,
	SoLanTruyCap Integer default 0,
	HinhSach Varchar(100) NULL,
	SoLuongTon Smallint NULL,
	TenTacGia nVarchar(50) NULL,
	GioiThieuSach nVarchar(max) NULL,
	NgayXuatBan date NULL,
	HienThiS Bit default 1,
)
go
Create table CTTHELOAI (
	MaTheLoai Char(10) NOT NULL foreign key references THELOAI,
	MaSach Char(10) NOT NULL foreign key references SACH,
	HienThiCTTL bit default 1,
	primary key (MaTheLoai, MaSach)
)
go
Create table HOADONMUAHANG (
	MaHDMua Char(10) NOT NULL primary key,
	TinhTrangThanhToan nVarchar(10) NULL,
	ThoiGianMua date,
	TenTaiKhoan Varchar(50) NOT NULL foreign key references TAIKHOAN,
) 
go
Create table CTHOADONMUAHANG (
	MaSach Char(10) NOT NULL foreign key references SACH,
	MaHDMua Char(10) NOT NULL foreign key references HOADONMUAHANG,
	SoLuongMua Smallint NULL,
	GiaHienHanh Integer NULL,
	primary key (MaSach, MaHDMua)
) 
go
Create table CTXEMSACH (
	MaSach Char(10) NOT NULL foreign key references SACH,
	TenTaiKhoan Varchar(50) NOT NULL foreign key references TAIKHOAN,
	NgayXemSach datetime,
	primary key (MaSach, TenTaiKhoan, NgayXemSach)
)
go
Create table HOADONNHAPHANG (
	MaHDNhap Char(10) NOT NULL primary key,
	MaNhaCungCap Char(10) NOT NULL foreign key references NHACUNGCAP,
	TenTaiKhoanNV Varchar(50) NOT NULL foreign key references NHANVIEN,
	NgayLapHDNhap date NULL,
)
go
Create table CTHDNHAPHANG (
	MaSach Char(10) foreign key references SACH,
	MaHDNhap Char(10) foreign key references HOADONNHAPHANG,
	SoLuongNhap Smallint NULL,
	primary key (MaSach, MaHDNhap)
)
go
Create table HOADONGIAOHANGNHAP (
	MaHDNhap Char(10) primary key foreign key references HOADONNHAPHANG,
	MaHDGiaoNhap Char(10) not null,
	NgayGiao date(10) NULL,
	TenTaiKhoanNV Varchar(50) foreign key references NHANVIEN,
)
go
Create table CTHOADONGIAOHANGNHAP (
	MaSach Char(10) foreign key references SACH,
	MaHDGiaoNhap Char(10) foreign key references HOADONGIAOHANGNHAP,
	MaHDNhap Char(10) foreign key references HOADONNHAPHANG,
	SoLuongGiao Smallint NULL,
	primary key (MaSach, MaHDGiaoNhap, MaHDNhap)
) 
go
Create table QUANGCAO (
	MaQuangCao Char(10) NOT NULL primary key,
	TenQC nVarchar(50) NULL,
	LinkQC Varchar(200) NULL,
	HinhQC Varchar(100) NULL,
	NgayBatDauQC date NULL,
	NgayHetQC date NULL,
	ChuSoHuuQC nVarchar(50) NULL,
	SdtChuQC Char(10) NULL,
	EmailChuQC Varchar(30) NULL,
	HienThiQC Bit default 1,
)
go
Create table CTGIOHANG (
	MaSach Char(10) foreign key references SACH,
	TenTaiKhoan Varchar(50) foreign key references TAIKHOAN,
	SoLuongGioHang Smallint NULL,
	primary key (MaSach, TenTaiKhoan)
)