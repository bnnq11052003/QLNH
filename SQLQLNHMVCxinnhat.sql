-- Tạo cơ sở dữ liệu mới
CREATE DATABASE LAT_MvcQLNH;
GO

USE LAT_MvcQLNH;
GO

-- Bảng tbAccount: Lưu thông tin tài khoản
CREATE TABLE tbAccount (
    AccountID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    AccountType NVARCHAR(20) NOT NULL
);

-- Bảng tbUserInfo: Lưu thông tin nhân viên
CREATE TABLE tbUserInfo (
    UserInfoID INT IDENTITY(1,1) PRIMARY KEY,
    AccountID INT,
    FullName NVARCHAR(100) NOT NULL,
    BirthDay DATE NOT NULL,
    Sex INT NOT NULL,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    FOREIGN KEY (AccountID) REFERENCES tbAccount(AccountID) ON DELETE CASCADE ON UPDATE CASCADE
);

-- Bảng tbDMFood: Lưu danh mục thức ăn
CREATE TABLE tbDMFood (
    DMFoodID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);

-- Bảng tbFood: Lưu thông tin món ăn
CREATE TABLE tbFood (
    FoodID INT IDENTITY(1,1) PRIMARY KEY,
    FoodName NVARCHAR(100) NOT NULL,
    DMFoodID INT,
    Price INT NOT NULL,
	AvtFood varchar(100) ,
    FOREIGN KEY (DMFoodID) REFERENCES tbDMFood(DMFoodID) ON DELETE CASCADE ON UPDATE CASCADE
);


-- Bảng tbDSTable: Lưu thông tin bàn
CREATE TABLE tbDSTable (
    TableID INT IDENTITY(1,1) PRIMARY KEY,
    TableName NVARCHAR(50) NOT NULL,
    Status NVARCHAR(20) NOT NULL 
);

-- Bảng tbBillHistory: Lưu thông tin hóa đơn đã thanh toán
CREATE TABLE tbBillHistory (
    BillID INT IDENTITY(1,1) PRIMARY KEY,
    FoodName NVARCHAR(100) NOT NULL,
	Quantity INT NOT NULL,
	Price INT NOT NULL,
    TableName NVARCHAR(50),
    BillDate DATE NOT NULL,
    TotalAmount INT NOT NULL,
	UserInfoID INT,
	CustomerName NVARCHAR(100),
	SDT INT,
    FOREIGN KEY (UserInfoID) REFERENCES tbUserInfo(UserInfoID) ON DELETE CASCADE ON UPDATE CASCADE
);

-- Bảng tbBillDetails: Lưu chi tiết hóa đơn (số lượng món ăn và tổng giá tiền)
CREATE TABLE tbBillDetails (
    BillID INT IDENTITY(1,1) PRIMARY KEY,
    FoodName NVARCHAR(100) NOT NULL,
	Quantity INT NOT NULL,
	Price INT NOT NULL,
    TableName NVARCHAR(50),
    BillDate DATE NOT NULL,
    TotalAmount INT NOT NULL,
	CustomerName NVARCHAR(100),
	SDT INT
);

-- Bảng tbRevenu: Lưu thông tin doanh thu
CREATE TABLE tbRevenu (
    RevenuID INT IDENTITY(1,1) PRIMARY KEY,
	SlTable INT not null,
    RevenuDateIn DATE NOT NULL,
    RevenuDateOut DATE NOT NULL,
	SumMoney INT not null,
    FOREIGN KEY (SlTable) REFERENCES tbDSTable(TableID) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE tbReport (
	ReportID int IDENTITY(1,1) PRIMARY KEY,
	IDBill int NOT NULL,
	SLBan int NOT NULL, 
	DateCheckin Datetime default getdate(),
	DateCheckout Datetime ,
	TotalPrice int NOT NULL,
	FOREIGN KEY (IDBill) REFERENCES tbDSTable(TableID) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (SLBan) REFERENCES tbFood(FoodID) ON DELETE CASCADE ON UPDATE CASCADE
); 


INSERT INTO tbAccount (Username, Password, AccountType) VALUES
('LAT', '123', '1'),
('admin', '1', '0'),
('NVT', '123', '1'),
('NST', '123', '1'),
('HTP', '123', '1');


INSERT INTO tbUserInfo (AccountID, FullName, BirthDay, Sex, Email, PhoneNumber) VALUES
(1, 'Lê Anh Tú', '2003-10-16', 1, 'lat@gmail.com', '19001610'),
(2, 'Quản Trị Viên', '2024-06-06', 0, 'admin@gmail.com', '19001000'),
(3, 'Nguyễn Văn Trường', '2003-01-01', 1, 'nvt@gmail.com', '12341234'),
(4, 'Nguyễn Sơn Tùng', '2003-06-01', 1, 'nst@gmail.com', '12345678'),
(5, 'Huỳnh Thịnh Phát', '2003-05-28', 1, 'htp@gmail.com', '09050506');

INSERT INTO tbDSTable (TableName, Status) VALUES
(N'Bàn 1', N'Trống'),
(N'Bàn 2', N'Trống'),
(N'Bàn 3', N'Trống'),
(N'Bàn 4', N'Trống'),
(N'Bàn 5', N'Trống'),
(N'Bàn 6', N'Trống'),
(N'Bàn 7', N'Có người'),
(N'Bàn 8', N'Trống'),
(N'Bàn 9', N'Có người'),
(N'Bàn 10', N'Trống'),
(N'Bàn 11', N'Trống'),
(N'Bàn 12', N'Trống'),
(N'Bàn 13', N'Có người'),
(N'Bàn 14', N'Trống'),
(N'Bàn 15', N'Trống'),
(N'Bàn 16', N'Có người'),
(N'Bàn 17', N'Trống'),
(N'Bàn 18', N'Trống'),
(N'Bàn 19', N'Trống'),
(N'Bàn 20', N'Trống');

INSERT INTO tbDMFood (CategoryName) VALUES
(N'Đồ Uống'),
(N'Rau'),
(N'Đồ Khai Vị'),
(N'Lẩu'),
(N'Hải Sản'),
(N'Món Tươi Sống'),
(N'Súp');

-- Thêm lại dữ liệu vào bảng tbFood
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Heniken', 1, 20000, 'heniken.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Tiger', 1, 18000, 'tiger.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Bia Quy Nhon', 1, 7000, 'quynhonbeer.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Bia Sài Gòn', 1, 15000, 'saigon.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Cocacola', 1, 18000, 'coca.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'PepSi', 1, 18000, 'pepsi.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'7Up', 1, 17000, '7up.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Nước lọc', 1, 6000, 'lavie.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Rau Muống', 2, 2000, 'raumuong.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Rau Mùng Tơi', 2, 2000, 'raumongtoi.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Rau Cải Thảo', 2, 3000, 'raucaithao.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Hoa chuối', 2, 3000, 'hoachuoi.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Rau Cần', 2, 2000, 'raucan.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Phồng Tôm', 3, 6000, 'phongtom.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Đậu Phộng', 3, 6000, 'dauphong.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Khoai Tây Chiên', 3, 10000, 'khoaitaychien.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Salat mắm cải', 3, 12000, 'salatmamcai.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Salat cổ hủ dừa', 3, 15000, 'salatcohudua.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Lẩu Thái', 4, 150000, 'lauthai.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Lẩu Tứ Xuyên', 4, 170000, 'lautuxuyen.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Lẩu Kim Chi', 4, 150000, 'laukimchi.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Lẩu Nấm', 4, 150000, 'launam.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Lẩu Riêu Cua', 4, 160000, 'laurieucua.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Lẩu Không cay', 4, 130000, 'laukhongcay.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Lẩu Măng Chua', 4, 150000, 'laumangchua.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Cá tuyết đút lò kiểu Thái', 5, 99000, 'catuyet.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Bóng cá hầm hải sâm', 5, 99000, 'bongcahamhaisam.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Bóng cá chiên tôm', 5, 99000, 'bongcachientom.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Mỳ xào hải sản', 5, 50000, 'myxaohaisan.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Miến xào hải sản', 5, 50000, 'mienxaohaisan.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Măng tây xào sò điệp', 5, 40000, 'mangtayxaosodiep.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Cơm chiên hải sản', 5, 35000, 'comchienhaisan.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Bào ngư nướng', 5, 199000, 'baongunuong.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Hàu hướng mỡ hành', 5, 35000, 'haunuongmohanh.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Cá Mú hấp xã', 5, 69000, 'camuhapxa.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Tôm chiên xù', 5, 49000, 'tomchienxu.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Cá Bớp xào', 5, 49000, 'cabopxao.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Ba chỉ bò Mỹ', 6, 99000, 'bachibomy.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Ba chỉ bò cuộn nấm kim châm', 6, 99000, 'bachibocuonnamkimcham.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Cổ bò Mỹ', 6, 99000, 'cobomy.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Bắp bò ta', 6, 99000, 'bapbota.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Lưỡi bò', 6, 35000, 'luoibo.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Tim lợn', 6, 35000, 'timlon.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Sườn sụn lợn', 6, 35000, 'suonsunlon.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Thăn lợn', 6, 99000, 'thanlon.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Gà đùi', 6, 49000, 'gadui.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Cánh gà', 6, 49000, 'canhga.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Bạch tuộc', 6, 79000, 'bachtuot.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Tôm viên', 6, 79000, 'tomvien.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Há cảo', 6, 45000, 'hacao.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Thanh cua', 6, 49000, 'thanhcua.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Mực ống', 6, 49000, 'mucong.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Súp cua gà xé', 7, 49000, 'supcuagaxe.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Súp măng tây', 7, 49000, 'supmangtay.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Súp thập cẩm', 7, 49000, 'supthapcam.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Súp hải sâm đông cô', 7, 49000, 'suphaisandongco.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Súp tổ yến', 7, 49000, 'suptoyen.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Súp sò điệp', 7, 49000, 'supsodiep.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Súp bào ngư vi cá', 7, 99000, 'supbaonguvica.jpg');
INSERT INTO tbFood (FoodName, DMFoodID, Price, AvtFood) VALUES (N'Súp cua rong biển', 7, 69000, 'supcuarongbien.jpg');
