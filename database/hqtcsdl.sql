--View 1: Khung nhìn để xem toàn bộ các hóa đơn
Create view LichSuHoaDon(ID, TenKhach, SDT, TenNhanVien, NgayTao, TongTien)
as
select HOADON.IDHoaDon, KHACHHANG.HoTen, KHACHHANG.SDT, USERS.HoTen, HOADON.NgayTao, HOADON.TongTien
from HOADON, KHACHHANG, USERS
where HOADON.IDKhach = KHACHHANG.IDKhach
and HOADON.IDNhanVien = USERS.ID

select * from LichSuHoaDon
drop view LichSuHoaDon

--View 2: Khung nhìn để xem sản phẩm ở tab bán hàng
Create view ChonSanPham(IDSanPham, TenSanPham, Gia)
as
select IDSanPham, TenSanPham, Gia
from MATHANG

select * from ChonSanPham
drop view ChonSanPham

--View 3: Khung nhìn để xem sản phẩm được chọn ở tab bán hàng
Create view HoaDonBanHang(IDSanPham, TenSanPham, SoLuong, DonGia, ThanhTien)
as
select CT_HOADON.IDSanPham, MATHANG.TenSanPham, CT_HOADON.SoLuong, CT_HOADON.DonGia, CT_HOADON.ThanhTien
from CT_HOADON, MATHANG
where MATHANG.IDSanPham = CT_HOADON.IDSanPham
and CT_HOADON.IDHoaDon in
(Select max(IDHoaDon) from HOADON)

select * from HoaDonBanHang
drop view HoaDonBanHang

--Procedure 1: Thủ tục chuyển ca cho nhân viên
Create procedure sp_chuyenca
@idnv1 int,
@idnv2 int
as
Begin
declare @temp1 int
declare @temp2 int
set @temp1 = (select CaLamViec from USERS where ID = @idnv1)
set @temp2 = (select CaLamViec from USERS where ID = @idnv2)
update USERS set CaLamViec = @temp1 where ID = @idnv2
update USERS set CaLamViec = @temp2 where ID = @idnv1
End

Execute sp_chuyenca 2, 3
drop procedure sp_chuyenca

--Procedure 2: Thủ tục cộng điểm cho khách hàng
Create procedure sp_congdiem
@idkhach int,
@diemmoi int
as
Begin
Update KHACHHANG set DiemTichLuy = @diemmoi where IDKhach = @idkhach
End

Execute sp_congdiem 1, 10
drop procedure sp_congdiem

--Procedure 3: Thêm 1 sản phẩm mới vào chi tiết hóa đơn
create procedure sp_ThemSanPhamCTHoaDon
@idhoadon int,
@idsanpham nvarchar(10),
@soluong int,
@dongia int,
@khuyenmai float
as
begin
declare @thanhtien int
set @thanhtien = (select dbo.ft_TinhThanhTien (@idsanpham, @soluong))
Insert into CT_HOADON(IDHoaDon, IDSanPham, SoLuong, DonGia, KhuyenMai, ThanhTien) values (@idhoadon, @idsanpham, @soluong, @dongia, @khuyenmai, @thanhtien)
end

execute sp_ThemSanPhamCTHoaDon 31, 'SP1', 1, 30000, 0
drop procedure sp_ThemSanPhamCTHoaDon

--Procedure 4: Cập nhật hóa đơn
create procedure sp_CapNhatHoaDon
@idhoadon int
as
begin
declare @tongtien int
set @tongtien = (select dbo.ft_TinhTongTien (@idhoadon))
Update HOADON set TongTien = @tongtien where IDHoaDon = @idhoadon
end

drop procedure sp_CapNhatHoaDon

Select TongTien from HOADON where IDHoaDon in (select max(IDHoaDon) from HOADON)

--Function 1: Hàm trả về bảng là lịch sử hóa đơn trong 1 khoảng thời gian
Create function ft_LichSuHoaDon (@ngaybatdau date, @ngayketthuc date)
Returns Table
as
return
(select *
from LichSuHoaDon
where NgayTao >= @ngaybatdau
and NgayTao <= @ngayketthuc)

Select * from ft_LichSuHoaDon ('2020-01-04', '2021-05-20')
drop function ft_LichSuHoaDon

--Function 2: Hàm trả về kết quả đăng nhập
Create function ft_DangNhap (@username nvarchar(100), @password nvarchar(255))
Returns Table
as
return
(Select *
from USERS
where TaiKhoan = @username
and MatKhau = @password)

Select * from ft_DangNhap('trango', '12345')
drop function ft_DangNhap

--Function 3: Hàm tính tổng tiền trên hóa đơn
create function ft_TinhTongTien (@idhoadon nvarchar(10))
returns int
as
begin
declare @tongtien int;
set @tongtien = (select sum(ThanhTien) from CT_HOADON where IDHoaDon = @idhoadon)
return @tongtien
end

select dbo.ft_TinhTongTien (1)
drop function ft_TinhTongTien

--Trigger 1: Trigger cập nhật điểm cho khách hàng khi phát sinh hóa đơn
Create trigger CapNhatDiem
on HOADON
for Insert
as
Begin
declare @idkhach int
set @idkhach = (Select IDKhach from inserted)
declare @tongtien int
set @tongtien = (Select TongTien from inserted)
declare @diemhientai int
set @diemhientai = (Select DiemTichLuy from KHACHHANG where IDKhach = @idkhach)
declare @diemmoi int
if (@tongtien <= 50000) set @diemmoi = @diemhientai + 1
else if (@tongtien <= 100000) set @diemmoi = @diemhientai + 2
else if (@tongtien <= 200000) set @diemmoi = @diemhientai + 3
else if (@tongtien <= 500000) set @diemmoi = @diemhientai + 4
else set @diemmoi = @diemhientai + 6
Execute sp_congdiem @idkhach, @diemmoi
End

drop trigger CapNhatDiem

--Trigger 2: Trigger cập nhật lại số lượng cho mặt hàng khi phát sinh giao dịch mặt hàng đó
Create trigger CapNhatSoLuong
on CT_HOADON
for Insert
as
Begin
declare @soluongmua int
set @soluongmua = (select SoLuong from inserted)
declare @idsp nvarchar(10)
set @idsp = (select IDSanPham from inserted)
declare @soluongton int
set @soluongton = (select SoLuongTon from MATHANG where IDSanPham = @idsp)
declare @soluongmoi int
if (@soluongmua > @soluongton)
begin
print (N'Số lượng sản phẩm không đủ')
rollback tran
end
else
begin
set @soluongmoi = @soluongton - @soluongmua
Update MATHANG set SoLuongTon = @soluongmoi where IDSanPham = @idsp
end
End

drop trigger CapNhatSoLuong

--Triger 3: Trigger cập nhật lại số lượng cho mặt hàng khi hủy phát sinh giao dịch mặt hàng đó
Create trigger CapNhatSoLuongDelete
on CT_HOADON
for Delete
as
Begin
declare @soluongmua int
set @soluongmua = (select SoLuong from deleted)
declare @idsp nvarchar(10)
set @idsp = (select IDSanPham from deleted)
declare @soluongton int
set @soluongton = (select SoLuongTon from MATHANG where IDSanPham = @idsp)
declare @soluongmoi int
set @soluongmoi = @soluongton + @soluongmua
Update MATHANG set SoLuongTon = @soluongmoi where IDSanPham = @idsp
End

drop trigger CapNhatSoLuongDelete

-- Vũ Ngọc Hòa
-- View 1: Khung nhìn để xem danh sách nhân viên
create view ViewDSNhanVien(ID, HoTen, TaiKhoan, MatKhau, ChucVu, CaLamViec)
as
select ID,HoTen,TaiKhoan,MatKhau,ChucVu,CaLamViec
from USERS

select * from ViewDSNhanVien

-- View 2: Khung nhìn để xem danh sách sản phẩm
create view ViewDSSanPham(ID, TenSanPham, NgaySX, XuatXu, SoLuongTon, KhuyenMai, Gia)
as
select IDSanPham, TenSanPham, NgaySX, XuatXu, SoLuongTon, KhuyenMai ,Gia
from MATHANG

select * from ViewDSSanPham
drop view ViewDSSanPham

-- Procedure 1: Thủ tục thêm Nhân viên mới
create procedure sp_AddNhanVien
@HoTen nvarchar (100),
@NgaySinh date,
@GioiTinh nvarchar(5),
@DiaChi nvarchar(50),
@SDT nvarchar(15),
@NgayBatDauLam date,
@CaLamViec int,
@TaiKhoan nvarchar(50),
@MatKhau nvarchar(50),
@ChucVu nvarchar(20)
as
begin
	insert into USERS (HoTen,NgaySinh,GioiTinh,DiaChi,SDT,NgayBatDauLam,CaLamViec,TaiKhoan,MatKhau,ChucVu) values (@HoTen,@NgaySinh,@GioiTinh,@DiaChi,@SDT,@NgayBatDauLam,@CaLamViec,@TaiKhoan,@MatKhau,@ChucVu)
end

drop procedure sp_AddNhanVien
execute sp_AddNhanVien N'Lâm Thị Thu Huệ', '2000-01-11', N'Nữ', N'Nam Định', '0123554632', '2021-01-03', 3, N'huelam', N'12345', N'Nhân viên'

-- Procedure 2: Thủ tục sửa Nhân Viên
create procedure sp_EditNhanVien
@ID int,
@HoTen nvarchar (100),
@NgaySinh date,
@GioiTinh nvarchar(5),
@DiaChi nvarchar(50),
@SDT nvarchar(15),
@NgayBatDauLam date,
@CaLamViec int,
@TaiKhoan nvarchar(50),
@MatKhau nvarchar(50),
@ChucVu nvarchar(20)
as
begin
	update USERS set HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, DiaChi = @DiaChi, SDT = @SDT, NgayBatDauLam = @NgayBatDauLam, CaLamViec = @CaLamViec, TaiKhoan = @TaiKhoan, MatKhau = @MatKhau, ChucVu = @ChucVu
	where ID = @ID
end

drop procedure sp_EditNhanVien

-- Function 1: Hàm tính thành tiền dựa trên số lượng của một sp
create function ft_TinhThanhTien (@id nvarchar(10), @soluong int)
returns int
as
begin
	declare @gia int;
	declare @thanhtien int;
	select @gia = Gia from dbo.ChonSanPham
	where IDSanPham = @id
	declare @khuyenmai float
	select @khuyenmai = KhuyenMai from MATHANG
	where IDSanPham = @id
	set @thanhtien = @soluong * (@gia * (100 - @khuyenmai)/100)
	return @thanhtien
end

select dbo.ft_TinhThanhTien ('SP2',2)
drop function ft_TinhThanhTien

-- Function 2: Hàm trả về bảng tìm kiếm Nhân viên
create function ft_TimKiemNV (@hoten nvarchar(100))
returns @tknv Table(ID int, HoTen nvarchar(100), TaiKhoan nvarchar(50), MatKhau nvarchar(50), ChucVu nvarchar(20), CaLamViec nvarchar (10))
as
begin
	insert into @tknv
	select ID, HoTen, TaiKhoan, MatKhau, ChucVu, CaLamViec
	from USERS
	where HoTen = @hoten
	return
end

select * from ft_TimKiemNV(N'Vũ Ngọc Hòa')
drop function ft_TimKiemNV

-- Trigger 1: Trigger không cho thêm NV mới khi phát hiện Ngày sinh nhập vào lớn hơn ngày hiện tại và ngày vào làm
create trigger insert_NgaySinh
on USERS
for insert as
if ((select NgaySinh from inserted) >= ( select NgayBatDauLam from inserted))
begin
	Print N'Không thể thêm do Ngày Sinh lớn hơn Ngày Bắt Đầu Làm'
	ROLLBACK TRANSACTION
end
if ((select NgaySinh from inserted) >=  GETDATE())
begin
	Print N'Không thể thêm do Ngày Sinh lớn hơn Ngày hiện tại'
	ROLLBACK TRANSACTION
end

drop trigger insert_NgaySinh

-- Trigger 2: Trigger không cho thêm SP mới khi phát hiện Ngày SX lớn hơn ngày hiện tại
create trigger insert_NgaySX
on MATHANG
for insert as
if ((select NgaySX from inserted) >= GETDATE())
begin
	Print N'Không thể thêm sản phẩm do Ngày SX lớn hơn Ngày hiện tại'
	ROLLBACK TRANSACTION
end

drop trigger insert_NgaySX

--Đinh Hữu Nghĩa
--View chứa Danh muc
CREATE VIEW ViewDanhMuc(IDDanhMuc,TenDanhMuc)
AS
SELECT DANHMUC.IDDanhMuc, TenDanhMuc FROM DANHMUC

select * from ViewDanhMuc
drop view ViewDanhMuc

--View đưa ra danh sách khách hàng
CREATE VIEW ViewDSkhachhang(IDKhach, HoTen, SDT, Diem)
As
Select IDKhach,KHACHHANG.HoTen, SDT, DiemTichLuy from KHACHHANG


--Procedure Add,Update,Delete Danh Mục
-- Add Danh Mục
CREATE PROC sp_adddanhmuc (@TenDanhMuc NVARCHAR(255))
as
begin
INSERT INTO DANHMUC (TenDanhMuc)
VALUES(@TenDanhMuc)
end

drop proc sp_adddanhmuc
-- Update Danh mục
CREATE PROC sp_updatedanhmuc (@IDDanhMuc INT, @TenDanhMuc NVARCHAR(255))
as
begin
UPDATE DANHMUC SET
TenDanhMuc=@TenDanhMuc 
WHERE IDDanhMuc=@IDDanhMuc
end

-- Delete Danh Mục
CREATE PROC sp_deletedanhmuc (@IDDanhMuc INT)
as
begin
DELETE FROM DANHMUC 
WHERE IDDanhMuc=@IDDanhMuc
end

-- Function Viết hàm trả bảng tìm kiếm danh mục
create function fu_bangdanhmuc (@TenDanhMuc NVARCHAR(255))
returns @bien table(IDDanhMuc INT,TenDanhMuc NVARCHAR(255) )
as
begin
insert into @bien
select DANHMUC.IDDanhMuc, TenDanhMuc from DANHMUC
where DANHMUC.TenDanhMuc=@TenDanhMuc
return
end

-- Function Viết hàm trả bảng tìm kiếm sản phẩm
create function fu_bangsanpham (@TenSanPham NVARCHAR(100))
returns @bien table(ID NVARCHAR(10), TenSanPham NVARCHAR(100), NgaySX DATE, XuatXu NVARCHAR(50), SoLuongTon INT, KhuyenMai INT, Gia INT)
as
begin
insert into @bien
select MATHANG.IDSanPham, TenSanPham, NgaySX, XuatXu, SoLuongTon, KhuyenMai, Gia from MATHANG
where MATHANG.TenSanPham=@TenSanPham
return
end

select * from fu_bangsanpham(N'Nước Giải Khát 7Up')
drop function fu_bangsanpham

--Trigger: Kiểm tra Khuyenmai nhập vào trong mat hang có >= 0 không
CREATE TRIGGER kmaidonhanghople
ON MATHANG AFTER INSERT 
AS
IF((SELECT KhuyenMai FROM INSERTED) < 0)
BEGIN
	PRINT N'Khuyến mại phải lớn hơn hoặc bằng 0'
	ROLLBACK TRANSACTION
END

--Trigger : Không cho Thêm Nhân Viên khi phát hiện số điện thoại nhập vào lớn hơn 10 chữ số
CREATE TRIGGER triggerktrasdt
ON USERS for INSERT
AS
declare @sdt nvarchar(15)
set @sdt = (select SDT from inserted)
if (len(@sdt) > 10)
BEGIN
	PRINT N'Số điện thoại không hợp lệ'
	ROLLBACK TRANSACTION
END

drop trigger triggerktrasdt