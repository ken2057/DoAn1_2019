use QLBookStore

insert into NHACUNGCAP values
('NCC0000001', 'a','0000000000','zxc@gmail.com',1),
('NCC0000002', 'a','0000000000','zxc2@gmail.com',1);

insert into KHUYENMAI values
('KM00000001',N'Giảm 10%','1/1/2019','1/1/2020',10,1),
('KM00000002',N'Giảm 20%','1/1/2019','1/1/2020',20,1),
('KM00000003',N'Giảm 30%','1/1/2019','1/1/2020',30,1),
('KM00000004',N'Giảm 40%','1/1/2019','1/1/2020',40,1),
('KM00000005',N'Giảm 50%','1/1/2019','1/1/2020',50,1);

insert into NHAXUATBAN values
('NXB0000001','Nhà Xuất Bản Trẻ',1),
('NXB0000002','Nhà Xuất Bản Tổng hợp TP.HCM',1);

insert into SACH values
('S000000001',N'Vũ Trụ Trong Vỏ Hạt Dẻ','7698722291931','NCC0000001','KM00000001','NXB0000001',100000,0,'Image/Book/VuTruTrongVoHatDe.jpg',100,N'Stephen Hawking',N'Lòng khát khao khám phá luôn là động lực cho trí sáng tạo của con người trong mọi lĩnh vực không chỉ trong khoa học. Điều đó được kiểm chứng trong quyển Vũ trụ trong vỏ hạt dẻ.','3/3/2008',1),
('S000000002',N'Lược Sử Thời Gian','8934974144175','NCC0000001','KM00000001','NXB0000001',87000,0,'Image/Book/LuocSuThoiGian.jpg',100,N'Stephen Hawking',N'Cùng với Vũ trụ trong vỏ hạt dẻ, Lược sử thời gian được xem là cuốn sách nổi tiếng và phổ biến nhất về vũ trụ học của Stephen Hawking, liên tục được nằm trong danh mục sách bán chạy nhất của các tạp chí nổi tiếng thế giới.Lược sử thời gian là cuốn sử thi về sự ra đời, sự hình thành và phát triển của vũ trụ. Tác giả đưa, vào tác phẩm của mình toàn bộ tiến bộ tiến trình khám phá của trí tuệ loài người trên nhiều lĩnh vực: Triết học, Vật lý, Thiên văn học…','1/12/2016',1),
('S000000003',N'Ông Trăm Tuổi Trèo Qua Cửa Sổ Và Biến Mất','4509304972090','NCC0000001','KM00000001','NXB0000001',170000,0,'Image/Book/OngTramTuoi.jpg',100,N'Jonas Jonasson',N'Cuốn tiểu thuyết Ông trăm tuổi bốc hơi qua cửa sổ đã trở thành hiện tượng quốc gia ở Thụy Điển, đem lại cho người đọc một cái nhìn hài hước kín đáo của văn hóa Bắc Âu, nơi có truyền thống tôn quý văn học lâu đời.','1/12/2018',1),
('S000000004',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1);

insert into THELOAI values
('TL00000001',N'Truyện dài',1),
('TL00000002',N'Kiến thức tổng hợp',1),
('TL00000003',N'Kỹ năng sống',1);

insert into CTTHELOAI values
('TL00000003','S000000003',1),
('TL00000002','S000000002',1),
('TL00000003','S000000004',1),
('TL00000001','S000000001',1),
('TL00000001','S000000002',1);

insert into TAIKHOAN values
('phatnghi','123456',N'Nguyễn Phát Nghị','0399999999','SVH','abc1@gmail.com',1,1),
('thanhhieu','123456',N'Nguyễn Thành Hiếu','0399999999','SVH','abc2@gmail.com',1,1),
('congquang','123456',N'Bùi Công Quang','0399999999','SVH','abc3@gmail.com',1,1),
('duyhau','123456',N'Vũ Nguyễn Duy Hậu','0399999999','SVH','abc5@gmail.com',1,1),
('nguyenduy','123456',N'Nguyễn Duy','0399999999','SVH','abc6@gmail.com',1,1);

insert into nhanvien values('nguyenduy','Admin', 1);

insert into QUANGCAO values('QC00000001','Google','iamngh.wordpress.com','hinh','1/1/2019','2/1/2019','a','a','a',1);

insert into CTGIOHANG values
('S000000001','phatnghi',1),
('S000000002','phatnghi',1),
('S000000003','phatnghi',1),
('S000000004','phatnghi',1),
('S000000001','thanhhieu',3),
('S000000003','congquang',10),
('S000000004','hoangkhang',35),
('S000000003','duyhau',2),
('S000000002','duyhau',1),
('S000000003','nguyenduy',2);

insert into SACH values
('S000000005',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000006',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000007',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000008',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000009',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000010',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000011',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000012',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000013',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000014',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000015',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000016',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000017',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000018',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000019',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000020',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000021',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1),
('S000000022',N'Đắc Nhân Tâm','8935086838143','NCC0000001','KM00000001','NXB0000002',76000,0,'Image/Book/DacNhanTam.jpg',100,N'Dale Carnegie',N'Được đánh giá là cuốn sách có sức lan tỏa rộng lớn, được dịch ra hầu hết các ngôn ngữ trên thế giới và luôn nằm trong Top sách bán chạy ở mọi thị trường xuất bản, Đắc Nhân Tâm đã có đời sống xứng tầm với giá trị thực tế của mình. Đây có thể coi là một trong những cuốn sách dòng self-hepl chính thống đầu tiên. Và Ngài Dale cũng trở thành một trong những tác giả ảnh hưởng trực tiếp nhiều nhất đến sự thay đổi tích cực của hàng triệu độc giả trên thế giới.','1/3/2016',1);
