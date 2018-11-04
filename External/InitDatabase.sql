
USE Ordering

DROP TABLE IF EXISTS Orders
DROP TABLE IF EXISTS Users
DROP TABLE IF EXISTS Products
DROP TABLE IF EXISTS Roles



--CREATE DATABASE Ordering;


CREATE TABLE Roles (
    ID int IDENTITY(0,1) PRIMARY KEY,
    RoleName nvarchar(100)
);

CREATE TABLE Users (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(200),
    Email nvarchar(200) UNIQUE,
	Permission int
	FOREIGN KEY (Permission) REFERENCES Roles(ID)
);


CREATE TABLE Products (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(200),
	Category nvarchar(200),
    Price int,
	ImgUrl nvarchar(1500)
);



CREATE TABLE Orders (
    ID int IDENTITY(1,1) PRIMARY KEY,
    CustomerID int,
    CustomerAddress nvarchar(200),
	ProductSet nvarchar(2000)
	FOREIGN KEY (CustomerID) REFERENCES Users(ID)
);

INSERT INTO Roles (RoleName)
VALUES ('administrator');
INSERT INTO Roles (RoleName)
VALUES ('customer');

INSERT INTO Users (Name, Email, Permission)
VALUES ('John Smith', 'admin@ordering.com', 0);

INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Medium Fries', 'Side', 150, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\fries.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Large Fries', 'Side', 190, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\fries.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Rice', 'Side', 120, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\rice.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Vegetables', 'Side', 170, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\vegetables.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Gyros Pita', 'Gyros', 400, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\gyrospita.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Small Gyros', 'Gyros', 450, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\gyros.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Medium Gyros', 'Gyros', 550, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\gyros.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Large Gyros', 'Gyros', 700, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\gyros.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Pizza Margherita', 'Pizza', 650, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\margherita.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Mushroom Pizza', 'Pizza', 650, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\mushroompizza.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Sausage Pizza', 'Pizza', 650, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\sausagepizza.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Coca Cola', 'Drink', 200, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\coca_cola.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Coca Cola Zero', 'Drink', 220, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\coca_zero.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Orange Juice', 'Drink', 185, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\orangejuice.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Cherry Coke', 'Drink', 220, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\cherrycoke.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Sprite', 'Drink', 210, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\sprite.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Donut 6pcs', 'Dessert', 310, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\donut.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Pancake 3pcs', 'Dessert', 290, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\pancake.png');
INSERT INTO Products (Name, Category, Price, ImgUrl)
VALUES ('Hot chocolate', 'Dessert', 250, 'C:\Users\Pászti-Nagy Kristóf\Documents\Git\OrderService\External\Images\hotchocolate.png');