CREATE DATABASE QLQuanTraSua
GO

-- Tạo CSDL QLTS
USE QLQuanTraSua
GO
--------------------------------TẠO BẢNG--------------------------------------
-- Bảng vị trí công việc
CREATE TABLE ViTriCongViec(
	MaViTri nchar(10) CONSTRAINT PK_ViTri PRIMARY KEY,
	TenViTri nvarchar(50) NOT NULL,
	PhuCapLuong float check (PhuCapLuong > 0)
)

-- Bảng loại nhân viên
CREATE TABLE LoaiNhanVien(
	MaLoaiNV nchar(10) CONSTRAINT PK_LoaiNV PRIMARY KEY,
	TenLoaiNV nvarchar(50) NOT NULL,
	LuongCB float check (LuongCB > 0),
)
-- Bảng nhân viên
CREATE TABLE NhanVien(
	MaNV nchar(10) CONSTRAINT PK_NhanVien PRIMARY KEY,
	TenNV nvarchar(10) NOT NULL,
	NgaySinh date check (DATEDIFF(year, NgaySinh, GETDATE())>=18),
	GioiTinh nvarchar(3) NOT NULL,
	DiaChi nvarchar(100),
	SDT nchar(11) NOT NULL check (len(SDT)=10),
	MaLoaiNV nchar(10) CONSTRAINT FK_NhanVien_Loai FOREIGN KEY REFERENCES LoaiNhanVien(MaLoaiNV),
	MaViTri nchar(10) CONSTRAINT FK_NhanVien_ViTri FOREIGN KEY REFERENCES ViTriCongViec(MaViTri),
	NgayTuyenDung date check (DATEDIFF(day, NgayTuyenDung, GETDATE())>=0)
)

-- Bảng khách hàng
CREATE TABLE KhachHang(
	SDT nchar(11) CONSTRAINT PK_KhachHang PRIMARY KEY check (len(SDT)=10),
	TenKH nvarchar(50) NOT NULL,
	DiaChi nchar(100)
)

-- Bảng nhà cung cấp
CREATE TABLE NhaCungCap(
	MaNCC nchar(10) CONSTRAINT PK_NhaCungCap PRIMARY KEY,
	TenNCC nvarchar(50) NOT NULL,
	DiaChi nchar(100),
	SDT nchar(10) NOT NULL check (len(SDT)=10)
)

-- Bảng hóa đơn nhập
CREATE TABLE HoaDonNhap(
	MaHDN nchar(10) CONSTRAINT PK_HoaDonNhap PRIMARY KEY,
	NgayNhap date check (DATEDIFF(day, NgayNhap, GETDATE())>=0),
	TriGiaDonNhap float NOT NULL,
	MaNCC nchar(10) CONSTRAINT FK_HoaDonNhap_NCC FOREIGN KEY REFERENCES NhaCungCap(MaNCC),
)

-- Bảng nguyên liệu
CREATE TABLE NguyenLieu(
	MaNL nchar(10) CONSTRAINT PK_NguyenLieu PRIMARY KEY,
	TenNL nvarchar(50) NOT NULL,
	MaNCC nchar(10) CONSTRAINT FK_NguyenLieu_NCC FOREIGN KEY REFERENCES NhaCungCap(MaNCC),
	SoLuong int check (SoLuong>0),
	DonVi nchar(10) NOT NULL,
)

-- Bảng chi tiết đơn nhập
CREATE TABLE ChiTietDonNhap(
	MaHDN nchar(10) CONSTRAINT FK_ChiTietNH_DonNH FOREIGN KEY REFERENCES HoaDonNhap(MaHDN),
	MaNL nchar(10) CONSTRAINT FK_ChiTietDN_NL FOREIGN KEY REFERENCES NguyenLieu(MaNL),
	DonGia float check (DonGia>=0),
	SoLuong int check (SoLuong>0),
	DonVi nchar(10) NOT NULL,
	TongTien float check (TongTien>=0),
	CONSTRAINT PK_ChiTietNhapHang PRIMARY KEY (MaHDN, MaNL)
)

-- Bảng loại sản phẩm
CREATE TABLE LoaiSanPham(
	MaLoaiSP nchar(10) CONSTRAINT PK_LoaiSanPham PRIMARY KEY,
	TenLoaiSP nvarchar(50) NOT NULL
)

-- Bảng sản phẩm
CREATE TABLE SanPham(
	MaSP nchar(10) CONSTRAINT PK_MaSP PRIMARY KEY,
	TenSP nvarchar(50) NOT NULL,
	DonGia float check (DonGia>0),
	TinhTrang nchar(10) DEFAULT N'Hết hàng',
	MaLoaiSP nchar(10) CONSTRAINT FK_SanPham_LoaiSP FOREIGN KEY REFERENCES LoaiSanPham(MaLoaiSP)
)

-- Bảng ứng dụng
CREATE TABLE UngDung(
	MaUD nchar(10) CONSTRAINT PK_UngDung PRIMARY KEY,
	TenUD nvarchar(50) NOT NULL,
	ChietKhau int NOT NULL
)

-- Bảng hóa đơn
CREATE TABLE HoaDonBan(
	MaHDB nchar(10) CONSTRAINT PK_HoaDonBan PRIMARY KEY,
	Ngay date check (DATEDIFF(year, Ngay, GETDATE())>=0),
	SDT nchar(11) CONSTRAINT FK_HoaDon_KH FOREIGN KEY REFERENCES KhachHang(SDT),
	MaNV nchar(10) CONSTRAINT FK_HoaDon_NV FOREIGN KEY REFERENCES NhanVien(MaNV),
	ThanhTien int check (ThanhTien>=0),
)

-- Bảng chi tiết đơn bán
CREATE TABLE ChiTietDonBan(
	MaHDB nchar(10) CONSTRAINT FK_ChiTietDB_DB FOREIGN KEY REFERENCES HoaDonBan(MaHDB),
	MaSP nchar(10) CONSTRAINT FK_ChiTietDB_SP FOREIGN KEY REFERENCES SanPham(MaSP),
	SoLuong int check (SoLuong>0),
	DonGia float check (DonGia>=0),
	TongTien float check (TongTien>=0),
	CONSTRAINT PK_ChiTietHD PRIMARY KEY (MaHDB, MaSP)
)

-- Bảng hóa đơn ứng dụng
CREATE TABLE HoaDonUngDung(
	MaHD_UD nchar(10) CONSTRAINT PK_HoaDonUngDung PRIMARY KEY,
	Ngay date check (DATEDIFF(day, Ngay, GETDATE())>=0),
	SDT nchar(11) CONSTRAINT FK_HoaDonUD_KH FOREIGN KEY REFERENCES KhachHang(SDT),
	MaUD nchar(10) CONSTRAINT FK_HoaDonUngDung_UD FOREIGN KEY REFERENCES UngDung(MaUD),
	MaNV nchar(10) CONSTRAINT FK_HoaDonUngDung_NV FOREIGN KEY REFERENCES NhanVien(MaNV),
	GiaTriHD int NOT NULL
)

-- Bảng chi tiết hóa đơn ứng dụng
CREATE TABLE ChiTietHoaDonUngDung(
	MaHD_UD nchar(10) CONSTRAINT FK_ChiTietHDUD_HDUD FOREIGN KEY REFERENCES HoaDonUngDung(MaHD_UD),
	MaSP nchar(10) CONSTRAINT FK_ChiTietHDUD_SP FOREIGN KEY REFERENCES SanPham(MaSP),
	SoLuong int check (SoLuong>0),
	DonGia float check (DonGia>=0),
	TongTien float check (TongTien>=0),
	CONSTRAINT PK_ChiTietHDUD PRIMARY KEY (MaHD_UD, MaSP)
)

-- Bảng ca làm việc
CREATE TABLE CaLamViec(
	MaCa nchar(10),
	Ngay date check (DATEDIFF(day, Ngay, GETDATE())>=0),
	GioBatDau nchar(10) NOT NULL,
	GioKetThuc nchar(10) NOT NULL,
	CONSTRAINT PK_CaLamViec PRIMARY KEY (MaCa, Ngay)
)

-- Bảng phân ca làm cho nhân viên
CREATE TABLE BangPhanCa(
	MaCa nchar(10),
	MaNV nchar(10) CONSTRAINT FK_PhanCa_NV FOREIGN KEY REFERENCES NhanVien(MaNV),
	Ngay date check (DATEDIFF(day, Ngay, GETDATE())>=0),
	CONSTRAINT PK_BangPhanCa PRIMARY KEY (MaCa, MaNV, Ngay),
	CONSTRAINT FK_PhanCa_Ca FOREIGN KEY (MaCa, Ngay) REFERENCES CaLamViec(MaCa, Ngay)
)

-- Bảng chế biến
CREATE TABLE CheBien(
	MaSP nchar(10) CONSTRAINT FK_CheBien_SP FOREIGN KEY REFERENCES SanPham(MaSP),
	MaNL nchar(10) CONSTRAINT FK_CheBien_NL FOREIGN KEY REFERENCES NguyenLieu(MaNL),
	LieuLuong int check (LieuLuong>0),
	DonVi nchar(10) NOT NULL
	CONSTRAINT PK_CheBien PRIMARY KEY (MaSP, MaNL)
)
GO
--------------------------------VIEW--------------------------------------
-- View Hóa đơn bán
CREATE VIEW [dbo].[v_HoaDonBan] AS
SELECT *
FROM dbo.HoaDonBan
GO

-- View Chi tiết đơn bán
CREATE VIEW [dbo].[v_ChiTietDonBan] AS
SELECT *
FROM dbo.ChiTietDonBan
GO

-- View Nhà Cung Cấp
CREATE VIEW [dbo].[v_NhaCungCap] AS
SELECT *
FROM dbo.NhaCungCap
GO

-- View Hóa đơn nhập
CREATE VIEW [dbo].[v_HoaDonNhap] AS
SELECT *
FROM dbo.HoaDonNhap
GO

-- View chi tiết đơn nhập
CREATE VIEW [dbo].[v_ChiTietDonNhap] AS
SELECT *
FROM dbo.ChiTietDonNhap
GO

-- View sản phẩm
CREATE VIEW [dbo].[v_SanPham] AS
SELECT *
FROM dbo.SanPham
GO

-- View nguyên liệu
CREATE VIEW [dbo].[v_LoadNguyenLieu] AS
SELECT *
FROM NguyenLieu
GO

-- Danh mục bảng phân ca
CREATE VIEW [dbo].[v_LoadBangPhanCa] AS
SELECT CaLamViec.MaCa, NhanVien.MaNV, CaLamViec.Ngay
FROM BangPhanCa, CaLamViec, NhanVien
WHERE BangPhanCa.MaCa = CaLamViec.MaCa AND BangPhanCa.MaNV = NhanVien.MaNV AND BangPhanCa.Ngay = CaLamViec.Ngay
GO

-- Danh mục vị trí công việc
CREATE VIEW [dbo].[v_LoadViTriCongViec] AS
SELECT *
FROM ViTriCongViec
GO

-- Danh mục ca làm việc
CREATE VIEW [dbo].[v_LoadCaLamViec] AS
SELECT *
FROM CaLamViec
GO

-- Danh mục loại nhân viên
CREATE VIEW [dbo].[v_LoadLoaiNhanVien] AS
SELECT *
FROM LoaiNhanVien
GO

-- Danh mục nhân viên
CREATE VIEW [dbo].[v_LoadNhanVien] AS
SELECT MaNV, TenNV, NgaySinh, GioiTinh, DiaChi, SDT, LoaiNhanVien.MaLoaiNV, ViTriCongViec.MaViTri, NgayTuyenDung
FROM NhanVien, LoaiNhanVien, ViTriCongViec
WHERE NhanVien.MaLoaiNV = LoaiNhanVien.MaLoaiNV AND NhanVien.MaViTri = ViTriCongViec.MaViTri
GO

--Khach Hang--
--view xem thong tin khach hang
CREATE VIEW [dbo].[v_KhachHang] AS
SELECT *
FROM KhachHang
GO

--UNG DUNG--
CREATE VIEW [dbo].[v_UngDung] AS
SELECT *
FROM UngDung
GO

--Hoa don ung dung
CREATE VIEW [dbo].[v_HoaDonUngDung] AS
SELECT *
FROM HoaDonUngDung
GO

-- BẢNG CHI TIẾT ĐƠN UNG DUNG
CREATE VIEW [dbo].[v_ChiTietHoadonUngDung] AS
SELECT *
FROM ChiTietHoaDonUngDung
GO

-- BẢNG LOẠI SẢN PHẨM
-- Danh mục loại sản phẩm
CREATE VIEW [dbo].[v_LoadLoaiSanPham] AS
SELECT *
FROM LoaiSanPham
GO

-- Danh mục sản phẩm hiện tên loại
CREATE VIEW [dbo].[v_LoadXemSanPham] AS
SELECT SanPham.TenSP, SanPham.DonGia, SanPham.TinhTrang, LoaiSanPham.TenLoaiSP
FROM dbo.SanPham
INNER JOIN dbo.LoaiSanPham ON SanPham.MaLoaiSP = LoaiSanPham.MaLoaiSP
GO

-- Xem số lượng sản phẩm đã bán trong ngày
CREATE VIEW [dbo].[V_SoLuongSanPhamDaBanTrongNgay] AS
SELECT sp.MaSP, SUM(cthd.SoLuong) AS SoLuongDaBanTaiQuay, SUM(cthdUD.SoLuong) AS SoLuongDaBanQuaUD
FROM dbo.SanPham sp
	JOIN dbo.ChiTietDonBan cthd ON sp.MaSP = cthd.MaSP
	JOIN dbo.HoaDonBan hd ON cthd.MaHDB = hd.MaHDB
	JOIN dbo.ChiTietHoaDonUngDung cthdUD ON sp.MaSP = cthdUD.MaSP
	JOIN dbo.HoaDonUngDung hdUD ON cthdUD.MaHD_UD = hdud.MaHD_UD
	WHERE hd.Ngay = CONVERT(DATE, GETDATE()) AND hdud.Ngay = CONVERT(DATE,GETDATE())
GROUP BY sp.MaSP
GO

-- BẢNG CHẾ BIẾN
-- Danh mục chế biến
CREATE VIEW [dbo].[v_LoadCheBien] AS
SELECT *
FROM CheBien
GO

-- Danh mục cách chế biến
CREATE VIEW [dbo].[v_LoadCachCheBien] AS
SELECT SanPham.TenSP, NguyenLieu.TenNL, CheBien.LieuLuong, CheBien.DonVi
FROM dbo.CheBien
INNER JOIN dbo.SanPham ON SanPham.MaSP = CheBien.MaSP
INNER JOIN dbo.NguyenLieu ON CheBien.MaNL = NGUYENLIEU.MaNL
GO
--------------------------------TRIGGER--------------------------------------
-- BẢNG HÓA ĐƠN BÁN
-- Trigger bắt lỗi khi thêm, cập nhật
CREATE TRIGGER trg_CheckHoaDonBan
ON HoaDonBan
FOR INSERT, UPDATE
AS
BEGIN
    -- Check MaHDB
    IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaHDB) = '')
    BEGIN
        RAISERROR('Mã hóa đơn không được để trống', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check Ngay
    IF EXISTS (SELECT * FROM inserted WHERE DATEDIFF(year, Ngay, GETDATE()) < 0)
    BEGIN
        RAISERROR('Ngày hóa đơn không thể là trong tương lai', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check SDT
    IF NOT EXISTS (SELECT * FROM KhachHang WHERE SDT = (SELECT SDT FROM inserted))
    BEGIN
        RAISERROR('Số điện thoại không tồn tại', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END

	IF EXISTS (SELECT * FROM inserted WHERE LEN(SDT) <> 10)
    BEGIN
        RAISERROR('Số điện thoại phải có đúng 10 ký tự', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check MaNV
    IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaNV) = '')
    BEGIN
        RAISERROR('Mã nhân viên không được để trống', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    IF NOT EXISTS (SELECT * FROM NhanVien WHERE MaNV = (SELECT MaNV FROM inserted))
    BEGIN
        RAISERROR('Mã nhân viên không tồn tại', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check ThanhTien
    IF EXISTS (SELECT * FROM inserted WHERE ThanhTien < 0)
    BEGIN
        RAISERROR('Giá trị Thành Tiền không hợp lệ', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
END
GO

-- BẢNG CHI TIẾT HÓA ĐƠN BÁN
-- Trigger bắt lỗi khi thêm, cập nhật
CREATE TRIGGER trg_CheckChiTietDonBan
ON ChiTietDonBan
FOR INSERT, UPDATE
AS
BEGIN
    -- Check MaHDB
    IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaHDB) = '')
    BEGIN
        RAISERROR('Mã hóa đơn không được để trống', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    IF NOT EXISTS (SELECT * FROM HoaDonBan WHERE MaHDB = (SELECT MaHDB FROM inserted))
    BEGIN
        RAISERROR('Mã hóa đơn không tồn tại', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check MaSP
    IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaSP) = '')
    BEGIN
        RAISERROR('Mã sản phẩm không được để trống', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    IF NOT EXISTS (SELECT * FROM SanPham WHERE MaSP = (SELECT MaSP FROM inserted))
    BEGIN
        RAISERROR('Mã sản phẩm không tồn tại', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check SoLuong
    IF EXISTS (SELECT * FROM inserted WHERE SoLuong <= 0)
    BEGIN
        RAISERROR('Số lượng phải lớn hơn 0', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check DonGia
    IF EXISTS (SELECT * FROM inserted WHERE DonGia < 0)
    BEGIN
        RAISERROR('Đơn giá không được âm', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check TongTien
    IF EXISTS (SELECT * FROM inserted WHERE TongTien < 0)
    BEGIN
        RAISERROR('Tổng tiền không được âm', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
END
GO

-- Kiểm tra xem nguyên liệu trong kho còn đủ để đáp ứng số lượng sản phẩm khách hàng yêu cầu hay không
CREATE TRIGGER TG_SPHetHang
ON ChiTietDonBan
AFTER INSERT
AS
BEGIN
	Declare @newMaSP nchar(10), @newSL int, @MaNLMin nchar(10), @SLMin int, @LieuLuongCan float
	Select @newMaSP = ne.MaSP, @newSL = ne.SoLuong
	From inserted ne

	Select @SLMin = min(NguyenLieu.SoLuong)
	From NguyenLieu, CheBien
	Where NguyenLieu.MaNL = CheBien.MaNL
		and CheBien.MaSP = @newMaSP
	Select TOP 1 @MaNLMin = NguyenLieu.MaNL, @LieuLuongCan = CheBien.LieuLuong
	From NguyenLieu, CheBien
	Where NguyenLieu.MaNL = CheBien.MaNL
		and CheBien.MaSP = @newMaSP
		and NguyenLieu.SoLuong = @SLMin

	IF (@newSL*@LieuLuongCan - @SLMin*1000 > 0)
	BEGIN
		UPDATE SanPham
		SET TinhTrang = 'Hết hàng'
		WHERE MaSP = @newMaSP
		ROLLBACK
	END
END
GO

-- BẢNG NHÀ CUNG CẤP
-- Trigger bắt lỗi khi thêm, cập nhật
CREATE TRIGGER trg_CheckNhaCungCap
ON NhaCungCap
FOR INSERT, UPDATE
AS
BEGIN
    -- Check MaNCC
    IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaNCC) = '')
    BEGIN
        RAISERROR('Mã nhà cung cấp không được để trống', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check TenNCC
    IF EXISTS (SELECT * FROM inserted WHERE TRIM(TenNCC) = '')
    BEGIN
        RAISERROR('Tên nhà cung cấp không được để trống', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check SDT
    IF EXISTS (SELECT * FROM inserted WHERE LEN(SDT) <> 10)
    BEGIN
        RAISERROR('Số điện thoại nhà cung cấp phải có đúng 10 ký tự', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
END
GO

-- BẢNG HÓA ĐƠN NHẬP
-- Trigger bắt lỗi khi thêm, cập nhật
CREATE TRIGGER trg_CheckHoaDonNhap
ON HoaDonNhap
FOR INSERT, UPDATE
AS
BEGIN
    -- Check MaHDN
    IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaHDN) = '')
    BEGIN
        RAISERROR('Mã hóa đơn nhập không được để trống', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check NgayNhap
    IF EXISTS (SELECT * FROM inserted WHERE DATEDIFF(DAY, NgayNhap, GETDATE()) < 0)
    BEGIN
        RAISERROR('Ngày nhập không được lớn hơn ngày hiện tại', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check TriGiaDonNhap
    IF EXISTS (SELECT * FROM inserted WHERE TriGiaDonNhap < 0)
    BEGIN
        RAISERROR('Trị giá đơn nhập không được âm', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check MaNCC
    IF NOT EXISTS (SELECT * FROM NhaCungCap WHERE MaNCC = (SELECT MaNCC FROM inserted))
    BEGIN
        RAISERROR('Mã nhà cung cấp không tồn tại', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
END
GO

-- BẢNG CHI TIẾT HÓA ĐƠN NHẬP
-- Trigger bắt lỗi khi thêm, cập nhật
CREATE TRIGGER trg_CheckChiTietDonNhap
ON ChiTietDonNhap
FOR INSERT, UPDATE
AS
BEGIN
    -- Check MaHDN
    IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaHDN) = '')
    BEGIN
        RAISERROR('Mã hóa đơn nhập không được để trống', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    IF NOT EXISTS (SELECT * FROM HoaDonNhap WHERE MaHDN = (SELECT MaHDN FROM inserted))
    BEGIN
        RAISERROR('Mã hóa đơn nhập không tồn tại', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check MaNL
    IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaNL) = '')
    BEGIN
        RAISERROR('Mã nguyên liệu không được để trống', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    IF NOT EXISTS (SELECT * FROM NguyenLieu WHERE MaNL = (SELECT MaNL FROM inserted))
    BEGIN
        RAISERROR('Mã nguyên liệu không tồn tại', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check DonGia
    IF EXISTS (SELECT * FROM inserted WHERE DonGia < 0)
    BEGIN
        RAISERROR('Đơn giá không được âm', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check SoLuong
    IF EXISTS (SELECT * FROM inserted WHERE SoLuong <= 0)
    BEGIN
        RAISERROR('Số lượng phải lớn hơn 0', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check DonVi
    IF EXISTS (SELECT * FROM inserted WHERE TRIM(DonVi) = '')
    BEGIN
        RAISERROR('Đơn vị không được để trống', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    
    -- Check TongTien
    IF EXISTS (SELECT * FROM inserted WHERE TongTien < 0)
    BEGIN
        RAISERROR('Tổng tiền không được âm', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
END
GO

-- NHÂN VIÊN
-- Trigger bắt lỗi khi thêm hoặc sửa thông tin nhân viên
CREATE TRIGGER trg_CheckNhanVien
ON NhanVien
FOR INSERT, UPDATE
AS
BEGIN
	-- check MaNV
	IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaNV) = ' ')
	BEGIN
		RAISERROR('Mã nhân viên không được để trống', 16, 1)
		ROLLBACK TRANSACTION
		RETURN
	END
	IF NOT EXISTS (SELECT * FROM NhanVien WHERE MaNV IN (SELECT MaNV FROM
	inserted))
	BEGIN
		RAISERROR('Mã nhân viên đã tồn tại', 16, 1)
		ROLLBACK TRANSACTION
		RETURN
	END
	-- check ho ten nhan vien
	IF EXISTS (SELECT * FROM inserted WHERE TRIM(TenNV) = '' )
		BEGIN
		RAISERROR('Tên nhân viên không được để trống', 16, 1)
		ROLLBACK TRANSACTION
		RETURN
	END
	IF EXISTS (SELECT 1 FROM inserted i INNER JOIN NhanVien n ON i.MaNV != n.MaNV
	AND TRIM(i.TenNV) = TRIM(n.TenNV))
	BEGIN
		RAISERROR('Tên nhân viên đã tồn tại', 16, 1)
		ROLLBACK TRANSACTION
		RETURN
	END
	-- check ngay sinh
	IF EXISTS (SELECT * FROM inserted WHERE
	datediff(year,inserted.NgaySinh,getdate())<(18))
	BEGIN
		RAISERROR ('Nhân viên phải trên 18 tuổi', 16, 1)
		ROLLBACK TRANSACTION
		RETURN
	END
	-- check dia chi
	IF EXISTS (SELECT * FROM inserted WHERE TRIM(DiaChi) = '' )
	BEGIN
		RAISERROR('Địa chỉ không được để trống', 16, 1)
		ROLLBACK TRANSACTION
		RETURN
	 END
	-- check so dien thoai
	IF EXISTS (SELECT * FROM inserted WHERE TRIM(SDT) = '' )
	BEGIN
		RAISERROR('Số điện thoại không được để trống', 16, 1)
		ROLLBACK TRANSACTION
		RETURN
	END
	IF NOT EXISTS (SELECT * FROM inserted WHERE len(TRIM(SDT)) = (10))
	BEGIN
		RAISERROR('Số điện thoại gồm 10 chữ số', 16, 1)
		ROLLBACK TRANSACTION
		RETURN
	END
	IF EXISTS (SELECT 1 FROM inserted i INNER JOIN NhanVien n ON i.MaNV != n.MaNV
	AND TRIM(i.SDT) = TRIM(n.SDT))
	BEGIN
		RAISERROR('Số điện thoại đã tồn tại', 16, 1)
		ROLLBACK TRANSACTION
		RETURN
	END
	-- check ngay tuyen dung
	IF NOT EXISTS (SELECT * FROM inserted WHERE
	(datediff(day,[NgayTuyenDung],getdate())>=(0)))
	BEGIN
		RAISERROR ('Ngày tuyển dụng không thể là trong tương lai', 16, 1)
		ROLLBACK TRANSACTION
		RETURN
	END
END
GO

-- KHÁCH HÀNG
--trigger bat loi khi them khach hang--
CREATE TRIGGER trg_InsertNewKhachHang
ON KhachHang
FOR INSERT, UPDATE
AS
BEGIN
-- check SDT
IF EXISTS (SELECT * FROM inserted WHERE TRIM(SDT) = ' ')
BEGIN
RAISERROR('S? di?n tho?i khách hàng không du?c d? tr?ng', 16, 1)
ROLLBACK TRANSACTION
RETURN
END
IF NOT EXISTS (SELECT * FROM KhachHang WHERE SDT IN (SELECT SDT FROM
inserted))
BEGIN
RAISERROR('SDT dã t?n t?i', 16, 1)
ROLLBACK TRANSACTION
RETURN
END
END;
GO

-- ỨNG DỤNG
--trigger bat loi khi them ung dung moi--
CREATE TRIGGER trg_ThemUngDungMoi
ON UngDung
FOR INSERT, UPDATE
AS
BEGIN
-- check MaUD
IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaUD) = ' ')
BEGIN
RAISERROR('Ma Ung dung khong duoc trung', 16, 1)
ROLLBACK TRANSACTION
RETURN
END
IF NOT EXISTS (SELECT * FROM UngDung WHERE MaUD IN (SELECT MaUD FROM
inserted))
BEGIN
RAISERROR('Ma ung dung dã t?n t?i', 16, 1)
ROLLBACK TRANSACTION
RETURN
END
-- check ten ung dung
IF EXISTS (SELECT * FROM inserted WHERE TRIM(TenUD) = ' ')
BEGIN
RAISERROR('Tên ung dung không du?c d? tr?ng', 16, 1)
ROLLBACK TRANSACTION
RETURN
END
END;
GO

-- LOẠI SẢN PHẨM
-- Trigger bắt lỗi khi thêm loại sản phẩm mới
CREATE TRIGGER trg_ThemLoaiSanPham
ON LoaiSanPham
FOR INSERT, UPDATE
AS
BEGIN
 -- check MaLoaiSP
IF EXISTS (SELECT * FROM inserted WHERE TRIM(MaLoaiSP) = ' ')
BEGIN
 RAISERROR('Mã loại sản phẩm không được để trống', 16, 1)
 ROLLBACK TRANSACTION
 RETURN
 END
IF NOT EXISTS (SELECT * FROM LoaiSanPham WHERE MaLoaiSP IN (SELECT MaLoaiSP FROM
inserted))
BEGIN
 RAISERROR('Mã loại sản phẩm đã tồn tại', 16, 1)
 ROLLBACK TRANSACTION
 RETURN
 END
-- check TenLoaiSP
IF EXISTS (SELECT * FROM inserted WHERE TRIM(TenLoaiSP) = ' ')
BEGIN
 RAISERROR('Tên loại sản phẩm không được để trống', 16, 1)
 ROLLBACK TRANSACTION
 RETURN
 END
END
GO

-- SẢN PHẨM
-- Trigger bắt lỗi trùng tên khi thêm và sửa sản phẩm
CREATE TRIGGER trg_TrungTenSanPham
ON dbo.SanPham
AFTER INSERT, UPDATE
AS
BEGIN
 -- Kiểm tra tên sản phẩm vừa thêm có bị trùng lặp
 IF EXISTS (
 SELECT *
 FROM inserted i
 WHERE EXISTS (
 SELECT *
 FROM dbo.SanPham sp
 WHERE sp.TenSP = i.TenSP AND sp.MaSP <> i.MaSP
 )
 )
 BEGIN
 -- Nếu trùng thì rollback
 RAISERROR ('Tên sản phẩm bị trùng', 16, 1)
 ROLLBACK;
 END
END
GO
--------------------------------CÁC THỦ TỤC PROCEDURE--------------------------------------
-- BẢNG HÓA ĐƠN BÁN
-- Thêm hóa đơn bán
CREATE PROCEDURE [dbo].[proc_ThemHoaDonBan]
	@MaHDB nchar(10),
	@Ngay date,
	@SDT nchar(11),
	@MaNV nchar(10),
	@ThanhTien int
AS
BEGIN
	INSERT INTO HoaDonBan (MaHDB, Ngay, SDT, MaNV, ThanhTien)
	VALUES (@MaHDB, @Ngay, @SDT, @MaNV, @ThanhTien)
END
GO

-- Cập nhật hóa đơn bán
CREATE PROCEDURE [dbo].[proc_SuaHoaDonBan]
	@MaHDB nchar(10),
	@Ngay date,
	@SDT nchar(11),
	@MaNV nchar(10),
	@ThanhTien int
AS
BEGIN
	BEGIN TRY
		-- Sửa thông tin hóa đơn bán
		UPDATE dbo.HoaDonBan 
		SET Ngay = @Ngay, SDT = @SDT, MaNV = @MaNV, ThanhTien = @ThanhTien
		WHERE MaHDB = @MaHDB
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa hóa đơn bán
CREATE PROCEDURE [dbo].[proc_XoaHoaDonBan]
@MaHDB nchar(10)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			-- Xoá các chi tiết hóa đơn bán theo @MaHDB trong bảng ChiTietDonBan
			DELETE FROM dbo.ChiTietDonBan WHERE ChiTietDonBan.MaHDB = @MaHDB
			-- Xoá hóa đơn bán theo @MaHDB trong bảng HoaDonBan
			DELETE FROM dbo.HoaDonBan WHERE HoaDonBan.MaHDB = @MaHDB
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @err NVARCHAR(MAX)
			SELECT @err = N'Lỗi' + ERROR_MESSAGE()
			RAISERROR(@err, 16, 1)
		END CATCH
END
GO
------------------------------------------------------------------
-- BẢNG CHI TIẾT ĐƠN BÁN
-- Thêm chi tiết đơn bán
CREATE PROCEDURE [dbo].[proc_ThemChiTietDonBan]
	@MaHDB nchar(10),
	@MaSP nchar(10),
	@SoLuong int,
	@DonGia float,
	@TongTien float
AS
BEGIN
	INSERT INTO ChiTietDonBan (MaHDB, MaSP, SoLuong, DonGia, TongTien)
	VALUES (@MaHDB, @MaSP, @SoLuong, @DonGia, @TongTien)
END
GO

-- Sửa chi tiết đơn bán
CREATE PROCEDURE [dbo].[proc_SuaChiTietDonBan]
	@MaHDB nchar(10),
	@MaSP nchar(10),
	@SoLuong int,
	@DonGia float,
	@TongTien float
AS
BEGIN
	BEGIN TRY
		-- Sửa thông tin chi tiết đơn bán
		UPDATE dbo.ChiTietDonBan 
		SET SoLuong = @SoLuong, DonGia = @DonGia, TongTien = @TongTien
		WHERE MaHDB = @MaHDB AND MaSP = @MaSP
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa chi tiết đơn bán
CREATE PROCEDURE [dbo].[proc_XoaChiTietDonBan]
	@MaHDB nchar(10),
	@MaSP nchar(10)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			-- Xoá chi tiết đơn bán theo @MaHDB và @MaSP trong bảng ChiTietDonBan
			DELETE FROM dbo.ChiTietDonBan WHERE ChiTietDonBan.MaHDB = @MaHDB AND ChiTietDonBan.MaSP = @MaSP
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @err NVARCHAR(MAX)
			SELECT @err = N'Lỗi' + ERROR_MESSAGE()
			RAISERROR(@err, 16, 1)
		END CATCH
END
GO

-- Tính tổng tiền bên chi tiết hóa đơn sau đó cập nhật qua hóa đơn bán
CREATE PROCEDURE [dbo].[proc_TinhTongTienHDB]
    @MaHDB nchar(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE HoaDonBan
    SET ThanhTien = (SELECT SUM(TongTien)
                     FROM ChiTietDonBan
                     WHERE ChiTietDonBan.MaHDB = HoaDonBan.MaHDB
                     GROUP BY MaHDB)
    WHERE MaHDB = @MaHDB
      AND EXISTS (SELECT 1
                  FROM ChiTietDonBan
                  WHERE ChiTietDonBan.MaHDB = HoaDonBan.MaHDB
                  GROUP BY MaHDB);
END;
GO
------------------------------------------------------------------
-- BẢNG NHÀ CUNG CẤP
-- Thêm nhà cung cấp
CREATE PROCEDURE [dbo].[proc_ThemNhaCungCap]
	@MaNCC nchar(10),
	@TenNCC nvarchar(50),
	@DiaChi nchar(100),
	@SDT nchar(10)
AS
BEGIN
	INSERT INTO NhaCungCap(MaNCC, TenNCC, DiaChi, SDT)
	VALUES (@MaNCC, @TenNCC, @DiaChi, @SDT)
END
GO

-- Cập nhật nhà cung cấp
CREATE PROCEDURE [dbo].[proc_SuaNhaCungCap]
	@MaNCC nchar(10),
	@TenNCC nvarchar(50),
	@DiaChi nchar(100),
	@SDT nchar(10)
AS
BEGIN
	BEGIN TRY
		-- Sửa thông tin nhà cung cấp
		UPDATE dbo.NhaCungCap 
		SET TenNCC = @TenNCC, DiaChi = @DiaChi, SDT = @SDT
		WHERE MaNCC = @MaNCC
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa nhà cung cấp
CREATE PROCEDURE [dbo].[proc_XoaNhaCungCap]
	@MaNCC nchar(10)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			-- Xoá nhà cung cấp theo @MaNCC trong bảng NhaCungCap
			DELETE FROM dbo.NhaCungCap WHERE NhaCungCap.MaNCC = @MaNCC
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @err NVARCHAR(MAX)
			SELECT @err = N'Lỗi' + ERROR_MESSAGE()
			RAISERROR(@err, 16, 1)
		END CATCH
END
GO
------------------------------------------------------------------
-- BẢNG HÓA ĐƠN NHẬP
-- Xuất hóa đơn nhập
CREATE PROCEDURE [dbo].[proc_ThemHoaDonNhap]
	@MaHDN nchar(10),
	@NgayNhap date,
	@TriGiaDonNhap float,
	@MaNCC nchar(10)
AS
BEGIN
	INSERT INTO HoaDonNhap (MaHDN, NgayNhap, TriGiaDonNhap, MaNCC)
	VALUES (@MaHDN, @NgayNhap, @TriGiaDonNhap, @MaNCC)
END
GO

-- Cập nhật hóa đơn nhập
CREATE PROCEDURE [dbo].[proc_SuaHoaDonNhap]
	@MaHDN nchar(10),
	@NgayNhap date,
	@TriGiaDonNhap float,
	@MaNCC nchar(10)
AS
BEGIN
	BEGIN TRY
		-- Sửa thông tin hóa đơn nhập
		UPDATE dbo.HoaDonNhap 
		SET NgayNhap = @NgayNhap, TriGiaDonNhap = @TriGiaDonNhap, MaNCC = @MaNCC
		WHERE MaHDN = @MaHDN
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa hóa đơn nhập
CREATE PROCEDURE [dbo].[proc_XoaHoaDonNhap]
@MaHDN nchar(10)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			-- Xoá các chi tiết hóa đơn nhập theo @MaHDN trong bảng ChiTietDonNhap
			DELETE FROM dbo.ChiTietDonNhap WHERE ChiTietDonNhap.MaHDN = @MaHDN
			-- Xoá hóa đơn nhập theo @MaHDN trong bảng HoaDonNhap
			DELETE FROM dbo.HoaDonNhap WHERE HoaDonNhap.MaHDN = @MaHDN
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @err NVARCHAR(MAX)
			SELECT @err = N'Lỗi' + ERROR_MESSAGE()
			RAISERROR(@err, 16, 1)
		END CATCH
END
GO
------------------------------------------------------------------
-- BẢNG CHI TIẾT ĐƠN NHẬP
-- Thêm chi tiết đơn nhập
CREATE PROCEDURE [dbo].[proc_ThemChiTietDonNhap]
	@MaHDN nchar(10),
	@MaNL nchar(10),
	@DonGia float,
	@SoLuong int,
	@DonVi nchar(10),
	@TongTien float
AS
BEGIN
	INSERT INTO ChiTietDonNhap (MaHDN, MaNL, DonGia, SoLuong, DonVi, TongTien)
	VALUES (@MaHDN, @MaNL, @DonGia, @SoLuong, @DonVi, @TongTien)
END
GO

-- Cập nhật chi tiết đơn nhập
CREATE PROCEDURE [dbo].[proc_SuaChiTietDonNhap]
	@MaHDN nchar(10),
	@MaNL nchar(10),
	@DonGia float,
	@SoLuong int,
	@DonVi nchar(10),
	@TongTien float
AS
BEGIN
	BEGIN TRY
		-- Sửa thông tin chi tiết đơn nhập
		UPDATE dbo.ChiTietDonNhap 
		SET DonGia = @DonGia, SoLuong = @SoLuong, DonVi = @DonVi, TongTien = @TongTien
		WHERE MaHDN = @MaHDN AND MaNL = @MaNL
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa chi tiết đơn nhập
CREATE PROCEDURE [dbo].[proc_XoaChiTietDonNhap]
	@MaHDN nchar(10),
	@MaNL nchar(10)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			-- Xoá chi tiết đơn nhập theo @MaHDN và @MaNL trong bảng ChiTietDonNhap
			DELETE FROM dbo.ChiTietDonNhap WHERE ChiTietDonNhap.MaHDN = @MaHDN AND ChiTietDonNhap.MaNL = @MaNL
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @err NVARCHAR(MAX)
			SELECT @err = N'Lỗi' + ERROR_MESSAGE()
			RAISERROR(@err, 16, 1)
		END CATCH
END
GO

-- Tính tổng tiền bên chi tiết hóa đơn sau đó cập nhật qua hóa đơn nhập
CREATE PROCEDURE [dbo].[proc_TinhTongTienHDN]
    @MaHDN nchar(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE HoaDonNhap
    SET TriGiaDonNhap = (SELECT SUM(TongTien)
                     FROM ChiTietDonNhap
                     WHERE ChiTietDonNhap.MaHDN = HoaDonNhap.MaHDN
                     GROUP BY MaHDN)
    WHERE MaHDN = @MaHDN
      AND EXISTS (SELECT 1
                  FROM ChiTietDonNhap
                  WHERE ChiTietDonNhap.MaHDN = HoaDonNhap.MaHDN
                  GROUP BY MaHDN);
END;
GO
------------------------------------------------------------------
--BẢNG PHÂN CA
--Thêm phân ca cho nhân viên
CREATE PROCEDURE [dbo].[proc_ThemBangPhanCa]
    @MaCa nchar(10),
    @MaNV nchar(10),
    @Ngay date
AS
BEGIN
    -- Thêm bản ghi vào BangPhanCa
    INSERT INTO BangPhanCa (MaCa, MaNV, Ngay)
    VALUES (@MaCa, @MaNV, @Ngay)
END
GO


-- Cập nhật bảng phân ca làm việc của nhân viên, giữ nguyên mã ca và ngày
CREATE PROCEDURE [dbo].[proc_SuaBangPhanCa]
    @MaCa nchar(10),
    @MaNV nchar(10),
    @Ngay date
AS
BEGIN
	BEGIN TRY
		-- Cập nhật thông bảng phân ca
		UPDATE BangPhanCa
		SET MaNV = @MaNV
		WHERE MaCa = @MaCa AND Ngay = @Ngay
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

--Xóa ca làm của nhân viên
CREATE PROC [dbo].[proc_XoaBangPhanCa]
	@MaNV nchar(10),
	@MaCa nchar(10),
	@Ngay nchar(10)
AS
BEGIN
	BEGIN TRY
	DELETE FROM BangPhanCa
	WHERE MaNV = @MaNV and
			MaCa = @MaCa and
			Ngay = @Ngay
	END TRY
	BEGIN CATCH
	DECLARE @err NVARCHAR(MAX)
	SELECT @err = N'Lỗi ' + ERROR_MESSAGE()
	RAISERROR(@err, 16, 1)
	END CATCH
END
GO
------------------------------------------------------------------
--BẢNG VỊ TRÍ CÔNG VIỆC
--Thêm vị trí công việc
CREATE PROCEDURE [dbo].[proc_ThemViTriCongViec]
	@MaViTri nchar(10),
	@TenViTri nvarchar(50),
	@PhuCapLuong float
AS
BEGIN
		-- Thêm vị trí công việc
		INSERT INTO ViTriCongViec (MaViTri, TenViTri, PhuCapLuong)
		VALUES (@MaViTri, @TenViTri, @PhuCapLuong)
END
GO

--Cập nhật vị trí công việc
CREATE PROCEDURE [dbo].[proc_SuaViTriCongViec]
	@MaViTri nchar(10),
	@TenViTri nvarchar(50),
	@PhuCapLuong float
AS
BEGIN
	BEGIN TRY
		-- Cập nhật thông tin vị trí công việc
		UPDATE ViTriCongViec
		SET TenViTri = @TenViTri, PhuCapLuong = @PhuCapLuong
		WHERE MaViTri = @MaViTri
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

--Xóa vị trí công việc
CREATE PROCEDURE [dbo].[proc_XoaViTriCongViec]
	@MaViTri nchar(10)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			-- Xoá vị trí công việc theo @MaViTri trong bảng NhanVien
			DELETE FROM dbo.NhanVien WHERE NhanVien.MaViTri = @MaViTri
			-- Xóa vị trí công việc theo @MaViTri trong bảng ViTriCongViec
			DELETE FROM dbo.ViTriCongViec WHERE ViTriCongViec.MaViTri = @MaViTri
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO
------------------------------------------------------------------
-- BẢNG CA LÀM VIỆC
-- Lấy ngày theo mã ca
CREATE PROCEDURE [dbo].[LayNgayTheoMaCa_CaLamViec]
    @MaCa nchar(10),
    @Ngay date OUTPUT
AS
BEGIN
    BEGIN TRY
        SELECT TOP 1 @Ngay = Ngay
        FROM CaLamViec
        WHERE MaCa = @MaCa
    END TRY
    BEGIN CATCH
        SET @Ngay = NULL
    END CATCH
END

GO

-- Thêm ca làm việc
CREATE PROCEDURE [dbo].[proc_ThemCaLamViec]
	@MaCa nchar(10),
	@Ngay date,
	@GioBatDau nchar(10),
	@GioKetThuc nchar(10)
AS
BEGIN
	-- Thêm ca làm việc
	INSERT INTO CaLamViec (MaCa, Ngay, GioBatDau, GioKetThuc)
	VALUES (@MaCa, @Ngay, @GioBatDau, @GioKetThuc)
END
GO

-- Chạy câu lệnh trên
--exec dbo.proc_ThemCaLamViec @MaCa='05', @Ngay='10-11-2022', @GioBatDau='23', @GioKetThuc='3'

GO
-- Cập nhật ca làm việc
CREATE PROCEDURE [dbo].[proc_SuaCaLamViec]
    @MaCa nchar(10),
    @Ngay date,
    @GioBatDau nchar(10),
    @GioKetThuc nchar(10)
AS
BEGIN
    BEGIN TRY
        -- Cập nhật thông tin ca làm việc
        UPDATE dbo.CaLamViec
        SET GioBatDau = @GioBatDau, GioKetThuc = @GioKetThuc
        WHERE MaCa = @MaCa AND Ngay = @Ngay

        -- Kiểm tra xem có bản ghi nào bị ảnh hưởng không
        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('Không tìm thấy ca làm việc để cập nhật.', 16, 1)
        END
    END TRY
    BEGIN CATCH
        RAISERROR('Lỗi trong quá trình cập nhật.', 16, 1)
    END CATCH
END

GO

-- Xóa ca làm việc
CREATE PROCEDURE [dbo].[proc_XoaCaLamViec]
    @MaCa nchar(10),
    @Ngay date
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        -- Xoá ca làm việc theo @MaCa và @Ngay trong bảng CaLamViec
        DELETE FROM dbo.CaLamViec WHERE CaLamViec.MaCa = @MaCa AND CaLamViec.Ngay = @Ngay

        -- Xóa ca làm việc theo @MaCa và @Ngay trong BangPhanCa
        DELETE FROM dbo.BangPhanCa WHERE BangPhanCa.MaCa = @MaCa AND BangPhanCa.Ngay = @Ngay

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RAISERROR('Lỗi trong quá trình xóa.', 16, 1)
    END CATCH
END
GO
------------------------------------------------------------------
-- BẢNG LOẠI NHÂN VIÊN
-- Thêm loại nhân viên
CREATE PROCEDURE [dbo].[proc_ThemLoaiNhanVien]
	@MaLoaiNV nchar(10),
	@TenLoaiNV nvarchar(50),
	@LuongCB float
AS
BEGIN
	-- Thêm loại nhân viên
	INSERT INTO LoaiNhanVien (MaLoaiNV, TenLoaiNV, LuongCB)
	VALUES (@MaLoaiNV, @TenLoaiNV, @LuongCB)
END
GO

-- Cập nhật loại nhân viên
CREATE PROCEDURE [dbo].[proc_SuaLoaiNhanVien]
	@MaLoaiNV nchar(10),
	@TenLoaiNV nvarchar(50),
	@LuongCB float
AS
BEGIN
	BEGIN TRY
		-- Cập nhật thông tin loại nhân viên
		UPDATE LoaiNhanVien
		SET TenLoaiNV = @TenLoaiNV, LuongCB = @LuongCB
		WHERE MaLoaiNV = @MaLoaiNV
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa loại nhân viên
CREATE PROCEDURE [dbo].[proc_XoaLoaiNhanVien]
	@MaLoaiNV nchar(10)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		-- Xoá loại nhân viên theo @MaLoaiNV trong bảng LoaiNhanVien
		DELETE FROM dbo.LoaiNhanVien WHERE LoaiNhanVien.MaLoaiNV = @MaLoaiNV
		-- Xóa loại nhân viên theo @MaLoaiNV trong bảng NhanVien
		DELETE FROM dbo.NhanVien WHERE NhanVien.MaLoaiNV = @MaLoaiNV
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO
------------------------------------------------------------------
-- BẢNG NHÂN VIÊN
-- Thêm nhân viên
CREATE PROCEDURE [dbo].[proc_ThemNhanVien]
	@MaNV nchar(10),
	@TenNV nvarchar(10),
	@NgaySinh date,
	@GioiTinh nvarchar(3),
	@DiaChi nvarchar(100),
	@SDT nchar(11),
	@MaLoaiNV nchar(10),
	@MaViTri nchar(10),
	@NgayTuyenDung date
AS
BEGIN
	-- Thêm nhân viên
	INSERT INTO NhanVien (MaNV, TenNV, NgaySinh, GioiTinh, DiaChi, SDT, MaLoaiNV, MaViTri, NgayTuyenDung)
	VALUES (@MaNV, @TenNV, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @MaLoaiNV, @MaViTri, @NgayTuyenDung)
END
GO

-- Cập nhật nhân viên
CREATE PROCEDURE [dbo].[proc_SuaNhanVien]
	@MaNV nchar(10),
	@TenNV nvarchar(10),
	@NgaySinh date,
	@GioiTinh nvarchar(3),
	@DiaChi nvarchar(100),
	@SDT nchar(11),
	@MaLoaiNV nchar(10),
	@MaViTri nchar(10),
	@NgayTuyenDung date
AS
BEGIN
	BEGIN TRY
		-- Cập nhật thông tin nhân viên
		UPDATE NhanVien
		SET TenNV = @TenNV, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, DiaChi = @DiaChi, SDT = @SDT,
			MaLoaiNV = @MaLoaiNV, MaViTri = @MaViTri, NgayTuyenDung = @NgayTuyenDung
		WHERE MaNV = @MaNV
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa nhân viên
CREATE PROCEDURE [dbo].[proc_XoaNhanVien]
	@MaNV nchar(10)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		-- Xoá nhân viên theo @MaNV trong bảng NhanVien
		DELETE FROM dbo.NhanVien WHERE NhanVien.MaNV = @MaNV
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO
------------------------------------------------------------------------------------------
-- KHÁCH HÀNG
--Ham xoa khach hang---
CREATE PROCEDURE [dbo].[proc_XoaKhachHang]
@SDT nchar(11)
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
BEGIN TRY
DELETE FROM KhachHang WHERE SDT = @SDT;
END TRY
BEGIN CATCH
DECLARE @err NVARCHAR(MAX)
SELECT @err = N'Lỗi ' + ERROR_MESSAGE()
RAISERROR(@err, 16, 1)
ROLLBACK TRANSACTION;
THROW;
END CATCH
COMMIT TRANSACTION;
END

--Chay thuc thi chuong trinh--
exec dbo.proc_XoaKhachHang @SDT='0553634423'
GO

-- Sua thong tin khach hang
CREATE PROCEDURE [dbo].[proc_SuaNhaKhachHang]
	@SDT nchar(11),
	@TenKH nvarchar(50),
	@DiaChi nchar(100)
AS
BEGIN
	BEGIN TRY
		-- Sửa thông tin hóa đơn bán
		UPDATE dbo.KhachHang 
		SET SDT = @SDT, TenKH = @TenKH, DiaChi = @DiaChi
		WHERE SDT = @SDT
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

--Them Khach Hang moi---
CREATE or alter PROCEDURE [dbo].[proc_ThemKhachHang]
@SDT nchar(11),
@TenKH nvarchar(50),
@DiaChi nvarchar(100)
AS
BEGIN
IF EXISTS (SELECT 1 FROM KhachHang WHERE SDT = @SDT)
BEGIN
RETURN;
END
INSERT INTO KhachHang (SDT, TenKH, DiaChi)
VALUES (@SDT, @TenKH, @DiaChi)
END

-- chay cau lenh tren--
--exec dbo.InsertNewKhachHang @SDT='0553634423', @TenKH=N'Ý', @DiaChi=N'232 Tr??ng ??nh'
GO
------------------------------------------------------------------------------------------
-- ỨNG DỤNG
--Them ung dung moi--
CREATE or alter PROCEDURE [dbo].[proc_ThemUngDung]
@MaUD nchar(10),
@TenUD nvarchar(50),
@ChietKhau int
AS
BEGIN
IF EXISTS (SELECT 1 FROM UngDung WHERE MaUD = @MaUD)
BEGIN
RETURN;
END
INSERT INTO UngDung(MaUD, TenUD, ChietKhau)
VALUES (@MaUD, @TenUD, @ChietKhau)
END
GO

--Xoa ung dung--
CREATE PROCEDURE [dbo].[proc_XoaUngDung]
@MaUD nchar(11)
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
BEGIN TRY
DELETE FROM UngDung WHERE MaUD = @MaUD;
END TRY
BEGIN CATCH
DECLARE @err NVARCHAR(MAX)
SELECT @err = N'Lỗi ' + ERROR_MESSAGE()
RAISERROR(@err, 16, 1)
ROLLBACK TRANSACTION;
THROW;
END CATCH
COMMIT TRANSACTION;
END

--chay cau lenh xoa ung dung--
exec dbo.proc_XoaUngDung @MaUD='UD06'
GO

-- Sua thong tin ung dung
CREATE PROCEDURE [dbo].[proc_SuaUngDung]
	@MaUD nchar(10),
	@TenUD nvarchar(50),
	@ChietKhau int
AS
BEGIN
	BEGIN TRY
		-- Sửa thông tin hóa đơn bán
		UPDATE dbo.UngDung 
		SET MaUD = @MaUD, TenUD = @TenUD, ChietKhau = @ChietKhau
		WHERE MaUD = @MaUD
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO
------------------------------------------------------------------------------------------
-- HÓA ĐƠN ỨNG DỤNG
-- Thêm hóa đơn ung dung
CREATE PROCEDURE [dbo].[proc_ThemHoaDonUngDung]
	@MaHD_UD nchar(10),
	@Ngay date,
	@SDT nchar(11),
	@MaUD nchar(10),
	@MaNV nchar(10),
	@GiaTriHD int
AS
BEGIN
	INSERT INTO HoaDonUngDung(MaHD_UD, Ngay, SDT, MaUD, MaNV, GiaTriHD)
	VALUES (@MaHD_UD, @Ngay, @SDT, @MaUD, @MaNV, @GiaTriHD)
END
GO

-- Cập nhật hóa đơn ung dung
CREATE PROCEDURE [dbo].[proc_SuaHoaDonUngDung]
	@MaHD_UD nchar(10),
	@Ngay date,
	@SDT nchar(11),
	@MaUD nchar(10),
	@MaNV nchar(10),
	@GiaTriHD int
AS
BEGIN
	BEGIN TRY
		-- Sửa thông tin hóa đơn Ung Dung
		UPDATE dbo.HoaDonUngDung 
		SET Ngay = @Ngay, SDT=@SDT, MaUD = @MaUD, MaNV = @MaNV, GiaTriHD = @GiaTriHD
		WHERE MaHD_UD = @MaHD_UD
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa hóa đơn ung dung
CREATE PROCEDURE [dbo].[proc_XoaHoaDonUngDung]
@MaHD_UD nchar(10)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			-- Xoá các chi tiết hóa đơn bán theo @MaHD_UD trong bảng ChiTietHoaDonUngDung
			DELETE FROM dbo.ChiTietHoaDonUngDung WHERE ChiTietHoaDonUngDung.MaHD_UD = @MaHD_UD
			-- Xoá hóa đơn bán theo @MaHD_UD trong bảng HoaDonUngDung
			DELETE FROM dbo.HoaDonUngDung WHERE HoaDonUngDung.MaHD_UD = @MaHD_UD
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @err NVARCHAR(MAX)
			SELECT @err = N'Lỗi' + ERROR_MESSAGE()
			RAISERROR(@err, 16, 1)
		END CATCH
END
GO
------------------------------------------------------------------------------------------
-- CHI TIẾT ỨNG DỤNG
-- Thêm chi tiết đơn ung dung
CREATE PROCEDURE [dbo].[proc_ThemChiTietHoaDonUngDung]
	@MaHD_UD nchar(10),
	@MaSP nchar(10),
	@SoLuong int,
	@DonGia float,
	@TongTien float
AS
BEGIN
	INSERT INTO ChiTietHoaDonUngDung(MaHD_UD, MaSP, SoLuong, DonGia, TongTien)
	VALUES (@MaHD_UD, @MaSP, @SoLuong, @DonGia, @TongTien)
END
GO

-- Sửa chi tiết đơn ung dung
CREATE PROCEDURE [dbo].[proc_SuaChiTietHoaDonUngDung]
	@MaHD_UD nchar(10),
	@MaSP nchar(10),
	@SoLuong int,
	@DonGia float,
	@TongTien float
AS
BEGIN
	BEGIN TRY
		-- Sửa thông tin chi tiết đơn ung dung
		UPDATE dbo.ChiTietHoaDonUngDung 
		SET SoLuong = @SoLuong, DonGia = @DonGia, TongTien = @TongTien
		WHERE MaHD_UD = @MaHD_UD AND MaSP = @MaSP
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa chi tiết đơn ung dung
CREATE PROCEDURE [dbo].[proc_XoaChiTietHoaDonUngDung]
	@MaHD_UD nchar(10),
	@MaSP nchar(10)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			-- Xoá chi tiết đơn ung dung theo @MaHD_UD và @MaSP trong bảng ChiTietHoaDonUngDung
			DELETE FROM dbo.ChiTietHoaDonUngDung WHERE ChiTietHoaDonUngDung.MaHD_UD = @MaHD_UD AND ChiTietHoaDonUngDung.MaSP = @MaSP
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @err NVARCHAR(MAX)
			SELECT @err = N'Lỗi' + ERROR_MESSAGE()
			RAISERROR(@err, 16, 1)
		END CATCH
END
GO

-- Tính tổng tiền bên chi tiết hóa đơn sau đó cập nhật qua hóa đơn ứng dụng
CREATE PROCEDURE [dbo].[proc_TinhTongTienHoaDonUngDung]
    @MaHD_UD nchar(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE HoaDonUngDung
    SET GiaTriHD = (SELECT SUM(TongTien)
                     FROM ChiTietHoaDonUngDung
                     WHERE ChiTietHoaDonUngDung.MaHD_UD = HoaDonUngDung.MaHD_UD
                     GROUP BY MaHD_UD)
    WHERE MaHD_UD = @MaHD_UD
      AND EXISTS (SELECT 1
                  FROM ChiTietHoaDonUngDung
                  WHERE ChiTietHoaDonUngDung.MaHD_UD = HoaDonUngDung.MaHD_UD
                  GROUP BY MaHD_UD);
END;
GO
------------------------------------------------------------------------------------------
-- LOẠI SẢN PHẨM
-- Thêm loại sản phẩm
CREATE PROCEDURE [dbo].[proc_ThemLoaiSanPham]
	@MaLoaiSP nchar(10),
	@TenLoaiSP nvarchar(50)
AS
BEGIN
	INSERT INTO LoaiSanPham(MaLoaiSP,TenLoaiSP )
	VALUES (@MaLoaiSP, @TenLoaiSP)
END
GO

-- Cập nhật loại sản phẩm
CREATE PROCEDURE [dbo].[proc_SuaLoaiSanPham]
	@MaLoaiSP nchar(10),
	@TenLoaiSP nvarchar(50)
AS
BEGIN
	BEGIN TRY
		-- Sửa thông tin loại sản phẩm
		UPDATE dbo.LoaiSanPham
		SET TenLoaiSP = @TenLoaiSP
		WHERE MaLoaiSP = @MaLoaiSP
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Cập nhật loại sản phẩm không thành công' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa loại sản phẩm
CREATE PROCEDURE [dbo].[proc_XoaLoaiSanPham]
	@MaLoaiSP nchar(10)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			-- Xoá các các sản phẩm thuộc loại sản phẩm bị xóa theo @MaLoaiSP trong bảng SanPham
			DELETE FROM dbo.SanPham WHERE SanPham.MaLoaiSP = @MaLoaiSP
			-- Xoá loại sản phẩm theo @MaLoaiSP trong bảng LoaiSanPham
			DELETE FROM dbo.LoaiSanPham WHERE LoaiSanPham.MaLoaiSP = @MaLoaiSP
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @err NVARCHAR(MAX)
			SELECT @err = N'Xóa loại sản phẩm không thành công' + ERROR_MESSAGE()
			RAISERROR(@err, 16, 1)
		END CATCH
END
GO

-- SẢN PHẨM
--Thêm sản phẩm
CREATE PROCEDURE [dbo].[proc_ThemSanPham]
	@MaSP nchar(10),
	@TenSP nvarchar(50),
	@DonGia float,
	@TinhTrang nchar(10),
	@MaLoaiSP nchar(10)
AS
BEGIN
		-- Thêm sản phẩm
		INSERT INTO SanPham(MaSP, TenSP, DonGia, TinhTrang, MaLoaiSP)
		VALUES (@MaSP, @TenSP, @DonGia, @TinhTrang, @MaLoaiSP)
END
GO

--Cập nhật sản phẩm
CREATE PROCEDURE [dbo].[proc_SuaSanPham]
	@MaSP nchar(10),
	@TenSP nvarchar(50),
	@DonGia float,
	@TinhTrang nchar(10),
	@MaLoaiSP nchar(10)
AS
BEGIN
	BEGIN TRY
		-- Cập nhật sản phẩm
		UPDATE SanPham
		SET TenSP = @TenSP, DonGia = @DonGia, TinhTrang = @TinhTrang, MaLoaiSP = @MaLoaiSP
		WHERE MaSP = @MaSP
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

--Xóa sản phẩm
CREATE PROCEDURE [dbo].[proc_XoaSanPham]
	@MaSP nchar(10)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			-- Xoá vị trí công việc theo @MaSP trong bảng CheBien
			DELETE FROM dbo.CheBien WHERE CheBien.MaSP = @MaSP
			-- Xóa vị trí công việc theo @MaSP trong bảng SanPham
			DELETE FROM dbo.SanPham WHERE SanPham.MaSP = @MaSP
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO
------------------------------------------------------------------------------------------
-- NGUYÊN LIỆU
-- Thêm nguyên liệu
create PROCEDURE [dbo].[proc_ThemNguyenLieu]
@MaNL nchar(10),
@TenNL nvarchar(50),
@MaNCC nchar(10),
@SoLuong int,
@DonVi nchar(10)
AS
BEGIN
-- Thêm mới nguyên liệu
INSERT INTO NguyenLieu(MaNL, TenNL, MaNCC, SoLuong, DonVi)
VALUES (@MaNL, @TenNL, @MaNCC, @SoLuong, @DonVi)
END
GO

-- Cập nhật nguyên liệu
create PROCEDURE [dbo].[proc_SuaNguyenLieu]
@MaNL nchar(10),
@TenNL nvarchar(50),
@MaNCC nchar(10),
@SoLuong int,
@DonVi nchar(10)
AS
BEGIN
BEGIN TRY
		-- Cập nhật thông tin nguyen lieu
		UPDATE NguyenLieu
		SET TenNL = @TenNL, MaNCC = @MaNCC, SoLuong = @SoLuong, DonVi= @DonVi
		WHERE MaNL = @MaNL
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa nguyên liệu
CREATE PROCEDURE [dbo].[proc_XoaNguyenLieu]
	@MaNL nchar(10)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			-- Xoá nguyên liệu theo @MaNL trong bảng NguyenLieu
			DELETE FROM dbo.NguyenLieu WHERE NguyenLieu.MaNL = @MaNL
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @err NVARCHAR(MAX)
			SELECT @err = N'Xóa nguyên liệu không thành công' + ERROR_MESSAGE()
			RAISERROR(@err, 16, 1)
		END CATCH
END
GO
------------------------------------------------------------------------------------------
-- CHẾ BIẾN
--Thêm chế biến
CREATE PROCEDURE [dbo].[proc_ThemCheBien]
	@MaSP nchar(10),
	@MaNL nchar(10),
	@LieuLuong int,
	@DonVi nchar(10)
AS
BEGIN
	-- Thêm chế biến
	INSERT INTO CheBien(MaSP, MaNL, LieuLuong, DonVi )
	VALUES (@MaSP, @MaNL, @LieuLuong, @DonVi)
END
GO

--Cập nhật chế biến
CREATE PROCEDURE [dbo].[proc_SuaCheBien]
	@MaSP nchar(10),
	@MaNL nchar(10),
	@LieuLuong int,
	@DonVi nchar(10)
AS
BEGIN
	BEGIN TRY
		-- Cập nhật chế biến
		UPDATE CheBien
		SET LieuLuong = @LieuLuong, DonVi = @DonVi
		WHERE MaSP = @MaSP AND MaNL = @MaNL
	END TRY
	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
	END CATCH
END
GO

-- Xóa chế biến
CREATE PROCEDURE [dbo].[proc_XoaCheBien]
@MaSP nchar(10),
@MaNL nchar(10)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			-- Xoá cách chế biến theo @MaSP, @MaNL trong bảng CheBien
			DELETE FROM dbo.CheBien WHERE MaSP = @MaSP AND MaNL = @MaNL
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @err NVARCHAR(MAX)
			SELECT @err = N'Xóa cách chế biến không thành công' + ERROR_MESSAGE()
			RAISERROR(@err, 16, 1)
		END CATCH
END
GO
--------------------------------CÁC HÀM FUNCTION--------------------------------------
-- Tính doanh thu theo ngày
CREATE FUNCTION dbo.DoanhThuNgay(@Ngay INT, @Thang INT, @Nam INT)
RETURNS float
AS
BEGIN
	DECLARE @DoanhThu float = 0
	SELECT @DoanhThu = COALESCE(SUM(ThanhTien), 0)
	FROM HoaDonBan
	WHERE DAY(Ngay) = @Ngay AND MONTH(Ngay) = @Thang AND YEAR(Ngay) = @Nam
	RETURN @DoanhThu
END;
GO

-- Tính doanh thu theo tháng
CREATE FUNCTION [dbo].[DoanhThuThang](@Thang INT, @Nam INT) 
RETURNS float
BEGIN
	DECLARE @DoanhThu float = 0;
	SELECT @DoanhThu = COALESCE(SUM(ThanhTien), 0)
	FROM HoaDonBan
	WHERE MONTH(Ngay) = @Thang AND YEAR(Ngay) = @Nam
	RETURN @DoanhThu;
END;
GO

-- Tính doanh thu theo năm
CREATE FUNCTION [dbo].[DoanhThuNam](@Nam INT) 
RETURNS float
BEGIN
	DECLARE @DoanhThu float = 0;
	SELECT @DoanhThu = COALESCE(SUM(ThanhTien), 0)
	FROM HoaDonBan
	WHERE YEAR(Ngay) = @Nam
	RETURN @DoanhThu
END;
GO
------------------------------------------------------------------------------------------
-- HÓA ĐƠN ỨNG DỤNG
-- Tính tổng tiền hóa đơn ung dung
CREATE FUNCTION func_TinhThanhTien (@MaHDUD nchar(10))
RETURNS INT
AS
BEGIN
    DECLARE @ThanhTien INT
    
    SELECT @ThanhTien = SUM(TongTien)
    FROM ChiTietHoaDonUngDung
    WHERE MaHD_UD = @MaHDUD
    
    RETURN @ThanhTien
END
GO
------------------------------------------------------------------------------------------
-- SẢN PHẨM
-- Tìm kiếm sản phẩm
CREATE FUNCTION [dbo].[func_TimSanPham] (@string NVARCHAR(50))
RETURNS @IngreList TABLE (MaSP nchar(10), TenSP nvarchar(50), DonGia float, TinhTrang nchar(10),MaLoaiSP nchar(10))
AS
BEGIN
 INSERT INTO @IngreList
 SELECT *
 FROM dbo.SanPham
 WHERE CONCAT(MaSP, TenSP, DonGia, TinhTrang, MaLoaiSP) LIKE N'%' + @string + '%'
 RETURN
END
GO
------------------------------------------------------------------------------------------
-- NGUYÊN LIỆU
-- Tìm kiếm nguyên liệu
CREATE FUNCTION [dbo].[func_TimNguyenLieu] (@string NVARCHAR(50))
RETURNS @IngreList TABLE (MaNL VARCHAR(10), TenNL NVARCHAR(50), MaNCC 
VARCHAR(10), SoLuong INT, DonVi NVARCHAR(10))
AS
BEGIN
 INSERT INTO @IngreList
 SELECT *
 FROM dbo.NguyenLieu
 WHERE CONCAT(MaNL, TenNL, MaNCC, DonVi) LIKE N'%' + @string + '%'
 RETURN
END
GO
------------------------------------------------------------------------------------------
-- HÓA ĐƠN BÁN
CREATE FUNCTION [dbo].[TimKiemHDBBangNgay](@NgayBan date)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HoaDonBan
    WHERE Ngay = @NgayBan
)
GO

CREATE FUNCTION [dbo].[TimKiemHDBBangSDT](@SDT nchar(11))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HoaDonBan
    WHERE SDT = @SDT
)
GO

CREATE FUNCTION [dbo].[TimKiemHDBBangMaNV](@MaNV nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HoaDonBan
    WHERE MaNV = @MaNV
)
GO
------------------------------------------------------------------------------------------
-- HÓA ĐƠN NHẬP
CREATE FUNCTION [dbo].[TimKiemHDNBangNgay](@NgayNhap date)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HoaDonNhap
    WHERE NgayNhap = @NgayNhap
)
GO

CREATE FUNCTION [dbo].[TimKiemHDNBangMaNCC](@MaNCC nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HoaDonNhap
    WHERE MaNCC = @MaNCC
)
GO
------------------------------------------------------------------------------------------
-- NGUYÊN LIỆU
CREATE FUNCTION [dbo].[TimKiemNL](@TenNL nvarchar(50))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM NguyenLieu
    WHERE TenNL = @TenNL
)
GO
------------------------------------------------------------------------------------------
-- KHÁCH HÀNG
--Tim kiem khach hang bang SDT---
CREATE FUNCTION [dbo].[TimKiemKHBangSDT](@SDT nchar(11))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM KhachHang
    WHERE SDT = @SDT
);
GO

--Tim kiem khach hang bang Địac chỉ
CREATE FUNCTION [dbo].[TimKiemKHBangDiaChi](@DiaChi nchar(100))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM KhachHang
    WHERE DiaChi = @DiaChi
);
GO

------------------------------------------------------------------------------------------
-- HÓA ĐƠN ỨNG DỤNG
-- Tìm kiếm hóa đơn ứng dụng thông qua mã hóa đơn ứng dụng
CREATE FUNCTION [dbo].[TimKiemHDUDBangMaHDUD](@MaHD_UD nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HoaDonUngDung
    WHERE MaHD_UD = @MaHD_UD
);
GO

-- Tìm kiếm hóa đơn ứng dụng thông qua mã nhân viên
CREATE FUNCTION [dbo].[TimKiemHDUDBangMaNV](@MaNV nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HoaDonUngDung
    WHERE MaNV = @MaNV
);
GO

--Tìm kiếm hóa đơn ứng dụng thông qua ngày bán
CREATE FUNCTION [dbo].[TimKiemHDUDBangNgayBan](@NgayBan date)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HoaDonUngDung
    WHERE Ngay = @NgayBan
);
GO

--Tìm kiếm hóa đơn ứng dụng thông qua số điện thoại khách hàng
CREATE FUNCTION [dbo].[TimKiemHDUDBangSDT](@SDT nchar(11))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM HoaDonUngDung
    WHERE SDT = @SDT
);
GO

------------------------------------------------------------------------------------------
-- SẢN PHẨM
-- Tìm kiếm sản phẩm theo mã sản phẩm
CREATE FUNCTION [dbo].[TimKiemSanPhamBangMaSP](@MaSP nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM SanPham
    WHERE MaSP = @MaSP
);
GO

-- Tìm kiếm sản phẩm theo mã loại sản phẩm
CREATE FUNCTION [dbo].[TimKiemSanPhamBangMaLoaiSP](@MaLoaiSP nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM SanPham
    WHERE MaLoaiSP = @MaLoaiSP
);
GO

-- Tìm kiếm sản phẩm theo tên sản phẩm
CREATE FUNCTION [dbo].[TimKiemSanPhamBangTenSP](@TenSP nvarchar(50))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM SanPham
    WHERE TenSP = @TenSP
);
GO
------------------------------------------------------------------------------------------
--BẢNG PHÂN CA
-- Tìm kiếm theo mã nhân viên
CREATE FUNCTION [dbo].[TimKiemBangPhanCaBangMaNV](@MaNV nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM BangPhanCa
    WHERE MaNV = @MaNV
);
GO

--Tìm kiếm theo ngày trong bảng phân ca
CREATE FUNCTION [dbo].[TimKiemBangPhanCaBangNgay](@Ngay date)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM BangPhanCa
    WHERE Ngay = @Ngay
);
GO

-- Tìm kiếm thông qua mã ca
CREATE FUNCTION [dbo].[TimKiemBangPhanCaBangMaCa](@MaCa nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM BangPhanCa
    WHERE MaCa  = @MaCa 
);
GO

------------------------------------------------------------------------------------------
--CHẾ BIẾN
-- Tìm kiếm chế biến thông qua mã sản phẩm
CREATE FUNCTION [dbo].[TimKiemCheBienBangMaSP](@MaSP nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM CheBien
    WHERE MaSP  = @MaSP
);
GO

-- Tìm kiếm thông qua mã nguyên liệu
CREATE FUNCTION [dbo].[TimKiemCheBienBangMaNL](@MaNL nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM CheBien
    WHERE MaNL  = @MaNL
);
GO

------------------------------------------------------------------------------------------
--NHÀ CUNG CẤP
-- Tìm kiếm qua mã nhà cung cấp
CREATE FUNCTION [dbo].[TimKiemNCCBangDiaChi](@DiaChi nchar(100))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM NhaCungCap
    WHERE DiaChi = @DiaChi
);
GO

-- Tìm kiếm qua số điện thoại
CREATE FUNCTION [dbo].[TimKiemNCCBangSDT](@SDT nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM NhaCungCap
    WHERE SDT = @SDT
);
GO
------------------------------------------------------------------------------------------
--NHÂN VIÊN
--Tìm theo tên nhân viên
CREATE FUNCTION [dbo].[TimKiemNVBangTenNV](@TenNV nvarchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM NhanVien
    WHERE TenNV = @TenNV
);
GO

-- Tìm theo số điện thoại
CREATE FUNCTION [dbo].[TimKiemNVBangSDT](@SDT nchar(10))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM NhanVien
    WHERE SDT = @SDT
);
GO

-- Tìm theo điaj chỉ
CREATE FUNCTION [dbo].[TimKiemNVBangDiaChi](@DiaChi nvarchar(100))
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM NhanVien
    WHERE DiaChi = @DiaChi
);
GO

--Tìm theo tên ngày tuyển dụng
CREATE FUNCTION [dbo].[TimKiemNVBangNgayTuyenDung](@NgayTuyenDung date)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM NhanVien
    WHERE NgayTuyenDung = @NgayTuyenDung
);
GO
--------------------------------PHÂN QUYỀN USER--------------------------------------
-- Bảng Account
CREATE TABLE Account(
	username NVARCHAR(50),
    password VARCHAR(25),
    MaNV nchar(10) CONSTRAINT FK_Account_NV FOREIGN KEY REFERENCES NhanVien(MaNV),
    roles NVARCHAR(20)
	CONSTRAINT PK_Account PRIMARY KEY (MaNV)
)
GO

-- View Account
CREATE VIEW [dbo].[v_Account] AS
SELECT *
FROM dbo.Account
GO

CREATE ROLE Staff
-- Gán các quyền trên table cho role Staff
GRANT SELECT, REFERENCES ON BangPhanCa TO Staff
GRANT SELECT, REFERENCES ON CaLamViec TO Staff
GRANT SELECT, REFERENCES ON CheBien TO Staff
GRANT SELECT, REFERENCES ON ChiTietDonBan TO Staff
GRANT SELECT, REFERENCES ON ChiTietHoaDonUngDung TO Staff
GRANT SELECT, REFERENCES ON ChiTietDonNhap TO Staff
GRANT SELECT, REFERENCES ON ViTriCongViec TO Staff
GRANT SELECT, INSERT, REFERENCES ON HoaDonNhap TO Staff
GRANT SELECT, INSERT, REFERENCES ON HoaDonBan TO Staff
GRANT SELECT, INSERT, REFERENCES ON HoaDonUngDung TO Staff
GRANT SELECT, INSERT, REFERENCES ON KhachHang TO Staff
GRANT SELECT, REFERENCES ON LoaiSanPham TO Staff
GRANT SELECT, REFERENCES ON NguyenLieu TO Staff
GRANT SELECT, REFERENCES ON NhaCungCap TO Staff
GRANT SELECT, REFERENCES ON NhanVien TO Staff
GRANT SELECT, REFERENCES ON SanPham TO Staff
GRANT SELECT, REFERENCES ON UngDung TO Staff
DENY SELECT, INSERT, REFERENCES ON v_Account to Staff
-- Gán quyền thực thi trên các procedure, function cho role Staff
GRANT EXECUTE TO Staff
GRANT SELECT TO Staff
DENY EXECUTE ON proc_ThemNhanVien to Staff;
DENY EXECUTE ON proc_XoaNhanVien to Staff;
DENY EXECUTE ON proc_SuaNhanVien to Staff;
DENY EXECUTE ON proc_SuaCheBien to Staff;
DENY EXECUTE ON proc_XoaCheBien to Staff;
DENY EXECUTE ON proc_SuaNguyenLieu to Staff;
DENY EXECUTE ON proc_XoaNguyenLieu to Staff;
DENY EXECUTE ON proc_SuaSanPham to Staff;
DENY EXECUTE ON proc_XoaSanPham to Staff;
DENY EXECUTE ON proc_XoaBangPhanCa to Staff;
DENY EXECUTE ON proc_XoaCaLamViec to Staff;
GO

-- Procedure cập nhật username password của tài khoản:
CREATE OR ALTER PROCEDURE proc_updateAccount
    @username NVARCHAR(50),
    @password VARCHAR(25),
	@MaNV nchar(10),
    @roles NVARCHAR(20)
AS
IF EXISTS(SELECT* FROM Account WHERE MaNV = @MaNV)
BEGIN
    BEGIN TRAN
    BEGIN TRY
		DECLARE @oldUsername nvarchar(50);
		SELECT @oldUsername=username FROM ACCOUNT WHERE MaNV = @MaNV;

		DECLARE @Str VARCHAR(100);
		DECLARE @SessionID INT;
		SELECT @SessionID = session_id
		FROM sys.dm_exec_sessions
		WHERE login_name = @oldUsername;
		IF @SessionID IS NOT NULL
		BEGIN
		SET @Str = 'kill ' + Convert(NVARCHAR(20), @SessionID)
		exec(@Str)
		END

        UPDATE Account SET username = @username, password = @password, roles = @roles
		WHERE MaNV = @MaNV;

		DECLARE @sql varchar(100)

		SET @sql = 'DROP USER '+ @oldUsername
		exec (@sql)
		--
		SET @sql = 'DROP LOGIN '+ @oldUsername
		exec (@sql)

        DECLARE @sqlString NVARCHAR(2000)

        -- Tạo tài khoản login cho nhân viên, tên người dùng và mật khẩu là tài khoản được tạo trên bảng Account
        SET @sqlString = 'CREATE LOGIN [' + @username + '] WITH PASSWORD=''' + @password + ''', DEFAULT_DATABASE=[QLQuanTraSua], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF'
        EXEC (@sqlString)

        -- Tạo tài khoản người dùng đối với nhân viên đó trên database (tên người dùng trùng với tên login)
		SET @sqlString = 'CREATE USER ' + @username + ' FOR LOGIN ' + @username;
		EXEC (@sqlString)

		IF (@roles = 'sysadmin')
            SET @sqlString = 'ALTER SERVER ROLE sysadmin ADD MEMBER ' + @username;
        ELSE
            SET @sqlString = 'ALTER ROLE Staff ADD MEMBER ' + @username;

        EXEC (@sqlString)


        -- Thêm người dùng vào vai trò quyền tương ứng (Staff hoặc Manager(sysadmin))

        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
        DECLARE @err NVARCHAR(MAX)
        SELECT @err = N'Lỗi ' + ERROR_MESSAGE()
        RAISERROR(@err, 16, 1)
    END CATCH
END
GO

-- Procedure xóa nhân viên:
CREATE or ALTER PROCEDURE proc_deleteEmployee
    @MaNV nchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @username nvarchar(50);
		SELECT @username=username FROM Account WHERE MaNV=@MaNV
		DECLARE @sql varchar(100)
		DECLARE @SessionID INT;
		SELECT @SessionID = session_id
		FROM sys.dm_exec_sessions
		WHERE login_name = @username;
		IF @SessionID IS NOT NULL
		BEGIN
		SET @sql = 'kill ' + Convert(NVARCHAR(20), @SessionID)
		exec(@sql)
		END
	BEGIN TRANSACTION;
    BEGIN TRY
		DELETE FROM Account WHERE MaNV=@MaNV
		--
		SET @sql = 'DROP USER '+ @username
		exec (@sql)
		--
		SET @sql = 'DROP LOGIN '+ @username
		exec (@sql)
		--
		DELETE FROM NhanVien where MaNV = @MaNV
        COMMIT;
    END TRY
   	BEGIN CATCH
		DECLARE @err NVARCHAR(MAX)
		SELECT @err = N'Lỗi ' + ERROR_MESSAGE()
		RAISERROR(@err, 16, 1)
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END;
GO

CREATE FUNCTION [dbo].[checkLogin] (@username NVARCHAR(MAX), @password NVARCHAR(MAX))
RETURNS BIT
AS
BEGIN
 DECLARE @result BIT;
 SELECT @result = CAST(COUNT(*) AS BIT)
 FROM Account
 WHERE username = @username AND password = @password;
 RETURN @result;
END;
GO

-- Trigger
CREATE OR ALTER TRIGGER UpdateMatKhauLogin
ON Account
AFTER UPDATE
AS
BEGIN
  SET NOCOUNT ON;

  IF UPDATE(password)
  BEGIN
    DECLARE @TenDangNhap nvarchar(50);
    DECLARE @MatKhau varchar(25);

    SELECT @TenDangNhap = i.username, @MatKhau = i.password
    FROM inserted i;

    DECLARE @sqlString nvarchar(2000);
    SET @sqlString = 'ALTER LOGIN [' + @TenDangNhap + '] WITH PASSWORD=''' + @MatKhau + '''';
    EXEC (@sqlString);
  END
END;
GO

-- Trigger tự tạo tài khoản khi thêm nhân viên
CREATE OR ALTER TRIGGER Trg_Create_Account
ON NhanVien
AFTER INSERT 
AS
BEGIN
    INSERT INTO Account(username, password, MaNV, roles)
    SELECT 
        inserted.MaNV,
        '12345678',
		inserted.MaNV,
        CASE 
            WHEN NhanVien.MaViTri = 'vt01' THEN 'sysadmin'
            ELSE 'Staff'
        END AS roles
    FROM inserted
    INNER JOIN NhanVien ON inserted.MaNV = NhanVien.MaNV;
END;
GO

-- Trigger add quyen cho tai khoan sau khi them nhan vien
CREATE OR ALTER TRIGGER AddQuyen ON Account
AFTER INSERT
AS
DECLARE @tendangnhap nvarchar(10), @matkhau nvarchar(30), @roles nvarchar(20)
SELECT @tendangnhap=nl.username, @matkhau=nl.password, @roles=nl.roles
FROM inserted nl
BEGIN
	DECLARE @sqlString nvarchar(2000)
	----
	SET @sqlString= ' CREATE LOGIN [' + @tendangnhap +'] WITH PASSWORD= '''+ @matkhau
	+''', DEFAULT_DATABASE=[QlQuanTraSua], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF'
	EXEC (@sqlString)

	SET @sqlString= ' CREATE USER ' + @tendangnhap +' FOR LOGIN '+ @tendangnhap
	EXEC (@sqlString)

	if (@roles = 'sysadmin')
		SET @sqlString = ' ALTER SERVER ROLE sysadmin ' + ' ADD MEMBER ' + @tendangnhap;
	if (@roles = 'Staff')
		SET @sqlString = ' ALTER ROLE Staff ADD MEMBER ' + @tendangnhap;
	EXEC (@sqlString)
END
GO
--------------------------------NHẬP DỮ LIỆU--------------------------------------
-- Nhập vào bảng Vị trí công việc
Insert into ViTriCongViec Values ('VT01', N'Quản lý', 50000)
Insert into ViTriCongViec Values ('VT02', N'Pha chế', 30000)
Insert into ViTriCongViec Values ('VT03', N'Thu ngân', 20000)
Insert into ViTriCongViec Values ('VT04', N'Phục vụ', 30000)

-- Nhập vào bảng Loại nhân viên
Insert into LoaiNhanVien Values (01, 'Part-time', 30000)
Insert into LoaiNhanVien Values (02, 'Full-time', 50000)

-- Nhập vào bảng Nhân viên
Insert into Nhanvien Values ('NV01', N'Linh', '1999-08-07', N'Nữ', N'111 Võ Văn Ngân', '0938194729', '2', 'vt01', '2022-10-23')
Insert into Nhanvien Values ('NV02', N'Khang', '2001-11-20', N'Nam', N'10 Lê Văn Việt', '0372937192', '2', 'vt02', '2022-10-23')
Insert into Nhanvien Values ('NV03', N'Chi', '2001-11-20', N'Nữ', N'45 Lê Văn Thọ', '0374529123', '1', 'vt02', '2022-12-10')
Insert into Nhanvien Values ('NV04', N'Phong', '2000-02-10', N'Nam', N'30 Phạm Văn Đồng', '0904621346', '1', 'vt03', '2023-01-23')

--Nhập vào bảng Nhà cung cấp
Insert into NhaCungCap Values ('NCC01', 'ABC', NULL, '0904182475')
Insert into NhaCungCap Values ('NCC02', 'DEF', NULL, '0939561728')

-- Nhập vào bảng Hóa đơn nhập
Insert into HoaDonNhap Values ('HDN0001', '2023-03-08', 450000, 'NCC01')
Insert into HoaDonNhap Values ('HDN0002', '2023-03-08', 300000, 'NCC02')
Insert into HoaDonNhap Values ('HDN0003', '2023-03-09', 350000 , 'NCC01')

-- Nhập vào bảng Loại sản phẩm
Insert into LoaiSanPham Values ('LNV01', N'Trà Đài Loan')
Insert into LoaiSanPham Values ('LNV02', N'Trà Chanh')
Insert into LoaiSanPham Values ('LNV03', N'Trà Latte')
Insert into LoaiSanPham Values ('LNV04', N'Trà Sữa')
Insert into LoaiSanPham Values ('LNV05', N'Topping')

-- Nhập vào bảng Sản phẩm
Insert into SanPham Values ('SP01', N'Hồng trà Đài Loan', 20000, N'Hết hàng', 'LNV01')
Insert into SanPham Values ('SP02', N'Hồng trà vải thiều', 25000, N'Hết hàng', 'LNV01')
Insert into SanPham Values ('SP03', N'Trà xanh hoa nhài ', 25000, N'Hết hàng', 'LNV01')
Insert into SanPham Values ('SP04', N'Hồng trà chanh Đài Loan', 25000, N'Hết hàng', 'LNV02')
Insert into SanPham Values ('SP05', N'Hồng trà chanh vải thiều', 25000, N'Hết hàng', 'LNV02')
Insert into SanPham Values ('SP06', N'Trà xanh chanh', 20000, N'Hết hàng', 'LNV02')
Insert into SanPham Values ('SP07', N'Hồng trà latte Đài Loan', 30000, N'Hết hàng', 'LNV03')
Insert into SanPham Values ('SP08', N'Hồng trà latte vải thiều', 30000, N'Hết hàng', 'LNV03')
Insert into SanPham Values ('SP09', N'Trà xanh latte', 30000, N'Hết hàng', 'LNV03')
Insert into SanPham Values ('SP10', N'Trà sữa Đài Loan', 25000, N'Hết hàng', 'LNV04')
Insert into SanPham Values ('SP11', N'Trà sữa vải thiều', 25000, N'Hết hàng', 'LNV04')
Insert into SanPham Values ('SP12', N'Trà xanh sữa', 25000, N'Hết hàng', 'LNV04')
Insert into SanPham Values ('SP13', N'Trân châu đường đen', 7000, N'Hết hàng', 'LNV05')
Insert into SanPham Values ('SP14', N'Trân châu trắng', 7000, N'Hết hàng', 'LNV05')
Insert into SanPham Values ('SP15', N'Thạch đào', 6000, N'Hết hàng', 'LNV05')
Insert into SanPham Values ('SP16', N'Thạch vải', 6000, N'Hết hàng', 'LNV05')

-- Nhập vào bảng Ứng dụng
Insert into UngDung Values ('UD01', N'Shopee Food', 25)
Insert into UngDung Values ('UD02', N'Grab Food', 25)
Insert into UngDung Values ('UD03', N'Be Food', 25)
Insert into UngDung Values ('UD04', N'Go Food', 25)

-- Nhập vào bảng Ca làm việc
Insert into CaLamViec Values ('C01', '2023-10-24', '7 giờ', '11 giờ')
Insert into CaLamViec Values ('C02', '2023-10-24', '11 giờ', '15 giờ')
Insert into CaLamViec Values ('C03', '2023-10-24', '15 giờ', '19 giờ')
Insert into CaLamViec Values ('C04', '2023-10-24', '19 giờ', '23 giờ')

-- Nhập vào bảng Bảng phân ca
Insert into BangPhanCa Values ('C01', 'NV01', '2023-10-24')
Insert into BangPhanCa Values ('C01', 'NV02', '2023-10-24')
Insert into BangPhanCa Values ('C02', 'NV01', '2023-10-24')
Insert into BangPhanCa Values ('C02', 'NV02', '2023-10-24')

-- Nhập vào bảng Nguyên liệu
Insert into NguyenLieu Values ('NL01', 'Trà Xanh', 'NCC01', 10, 'kg')
Insert into NguyenLieu Values ('NL02', 'Sữa', 'NCC01', 8, 'lit')
Insert into NguyenLieu Values ('NL03', 'Đường', 'NCC01', 12, 'kg')
Insert into NguyenLieu Values ('NL04', 'Vải Thiều', 'NCC01', 5, 'kg')
Insert into NguyenLieu Values ('NL05', 'Hồng Trà', 'NCC01', 7, 'kg')

-- Nhập vào bảng Chế biến
Insert into CheBien Values('SP01', 'NL05', 1, 'kg')
Insert into CheBien Values('SP01', 'NL03', 1.5, 'kg')
Insert into CheBien Values('SP02', 'NL05', 1, 'kg')
Insert into CheBien Values('SP02', 'NL03', 1.5, 'kg')

-- Nhập vào bảng Khách Hàng
Insert into KhachHang Values('0938637721', 'Tâm', '484 Lê Văn Chí')
Insert into KhachHang Values('0385634423', 'Lan', '1 Mai Chí Thọ')
Insert into KhachHang Values('0728574886', 'Trân', '4 Quang Trung')

-- Nhập vào bảng Hóa đơn bán
Insert into HoaDonBan Values('HDB1000', '11-02-2023', '0938637721', 'NV04', 40000)

-- Nhập vào bảng Chi tiết đơn bán
Insert into ChiTietDonBan Values('HDB1000', 'SP01', 2, 20000, 40000)

-- Nhập vào bảng Chi tiết đơn nhập
Insert into ChiTietDonNhap Values('HDN0001', 'NL05', 9000, 5, 'kg', 45000)

-- Nhập vào bảng Hóa đơn ứng dụng
Insert into HoaDonUngDung Values('UD_001', '11-03-2023', '0938637721', 'UD01', 'NV04', 40000) 

-- Nhập vào bảng Chi tiết hóa đơn ứng dụng
Insert into ChiTietHoaDonUngDung Values('UD_001', 'SP02', 2, 25000, 40000)