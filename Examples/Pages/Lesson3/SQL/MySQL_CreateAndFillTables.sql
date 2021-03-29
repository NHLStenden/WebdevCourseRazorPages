DROP DATABASE IF EXISTS Examples;
CREATE DATABASE IF NOT EXISTS Examples;

USE Examples;

# DROP TABLE IF EXISTS Category, Product CASCADE;

CREATE TABLE IF NOT EXISTS Category
(
    CategoryId INT NOT NULL AUTO_INCREMENT ,
    Name varchar(128) NOT NULL  UNIQUE,

    CONSTRAINT PK_Category_CategoryId PRIMARY KEY (CategoryId),
    CONSTRAINT UQ_Category_Name UNIQUE (Name)
);

CREATE TABLE IF NOT EXISTS Product
(
    ProductId INT NOT NULL AUTO_INCREMENT,
    CategoryId INT NOT NULL,
    Name NVARCHAR(128) NOT NULL,
    Description NVARCHAR(256),
    Price DECIMAL(5, 2) NOT NULL,
    SalePrice decimal,

    CONSTRAINT PK_Product_ProductId PRIMARY KEY (ProductId),
    CONSTRAINT FK_ProductCategory FOREIGN KEY (CategoryId) REFERENCES Category (CategoryId)
);


INSERT INTO category (name) VALUES ('Category 1');
INSERT INTO category (name) VALUES ('Category 2');
INSERT INTO category (name) VALUES ('Category 3');

INSERT INTO product (name, description, price, Saleprice, categoryid)
VALUES ('Product 1', 'Product 1 Desc', 100, 80,
        (SELECT categoryid FROM category WHERE category.name = 'Category 1')
       );

INSERT INTO product (name, description, price, Saleprice, categoryid)
VALUES ('Product 2', 'Product 2 Desc', 80, 70,
        (SELECT categoryid FROM category WHERE category.name = 'Category 1')
       );

INSERT INTO product (name, description, price, Saleprice, categoryid)
VALUES ('Product 3', 'Product 3 Desc', 100, 80,
        (SELECT categoryid FROM category WHERE category.name = 'Category 2')
       );

INSERT INTO product (name, description, price, Saleprice, categoryid)
VALUES ('Product 4', 'Product 4 Desc', 80, 70,
        (SELECT categoryid FROM category WHERE category.name = 'Category 2')
       );

INSERT INTO product (name, description, price, Saleprice, categoryid)
VALUES ('Product 5', 'Product 5 Desc', 100, 80,
        (SELECT categoryid FROM category WHERE category.name = 'Category 3')
       );

INSERT INTO product (name, description, price, Saleprice, categoryid)
VALUES ('Product 6', 'Product 6 Desc', 80, 70,
        (SELECT categoryid FROM category WHERE category.name = 'Category 3')
       );

# JOIN Product with Categories to load all products and related category (see ProductRepository.GetProductWithCategories() method)
SELECT p.ProductId, p.Name, p.Description, p.Price, p.Saleprice, p.CategoryId, c.CategoryId, c.Name
FROM Product p JOIN Category c on c.CategoryId = p.CategoryId;


SELECT c.Name, count(1) as 'NumProductsInCategory', max(p.Price) as MaxPrice, min(p.Price) as MinPrice, round(avg(p.Price), 2) as 'AveragePrice'
FROM Product p JOIN Category c on c.CategoryId = p.CategoryId
GROUP BY c.CategoryId;
