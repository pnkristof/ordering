

DROP TABLE IF EXISTS Orders
DROP TABLE IF EXISTS Users
DROP TABLE IF EXISTS Products
DROP TABLE IF EXISTS Roles

--DROP DATABASE IF EXISTS Ordering

CREATE DATABASE Ordering;

USE Ordering

CREATE TABLE Roles (
    ID int IDENTITY(0,1) PRIMARY KEY,
    RoleName nvarchar
);

CREATE TABLE Users (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar,
    Email nvarchar UNIQUE,
	Permission int
	FOREIGN KEY (Permission) REFERENCES Roles(ID)
);


CREATE TABLE Products (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar,
    Price int,
	ImgUrl nvarchar
);

CREATE TABLE Orders (
    ID int IDENTITY(1,1) PRIMARY KEY,
    CustomerID int,
    CustomerAddress nvarchar,
	ProductSet nvarchar
	FOREIGN KEY (CustomerID) REFERENCES Users(ID)
);

INSERT INTO Roles (RoleName)
VALUES ('administrator');
INSERT INTO Roles (RoleName)
VALUES ('customer');