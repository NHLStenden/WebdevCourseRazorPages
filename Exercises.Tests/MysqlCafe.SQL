DROP TABLE IF EXISTS OrderLine;
DROP TABLE IF EXISTS Category;
DROP TABLE IF EXISTS CostObject;
DROP TABLE IF EXISTS Product;


CREATE TABLE Category
(
    CategoryId integer AUTO_INCREMENT PRIMARY KEY,
    Name        varchar(128) NOT NULL
);

CREATE TABLE Product
(
    ProductId   INTEGER         AUTO_INCREMENT PRIMARY KEY,
    Name        NVARCHAR(128)   NOT NULL UNIQUE,
    CategoryId  INTEGER         NOT NULL,
    Price       DECIMAL(10,2)   NOT NULL CHECK (price > 0)
);

CREATE TABLE CostObject
(
    CostObjectId INTEGER AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(128) NOT NULL UNIQUE,
    Budget DECIMAL(10,2) NOT NULL,
    Email NVARCHAR(128) NOT NULL,
    TelNr NVARCHAR(128) NULL
);

CREATE TABLE OrderLine
(
    TableNr         INTEGER NOT NULL,
    ProductId       INTEGER NOT NULL,
    Amount          INTEGER NOT NULL,
    CostObjectId    INTEGER NULL, #Let op NULL: dit betekent 0 of 1 associatie/relatie
    CONSTRAINT PK_OrderLine PRIMARY KEY (TableNr, ProductId),

    CONSTRAINT FK_OrderLine_Product
        FOREIGN KEY (ProductId)
            REFERENCES Product (ProductId),

    CONSTRAINT FK_OrderLine_CostObject
        FOREIGN KEY (CostObjectId)
            REFERENCES CostObject (CostObjectId)
);

INSERT INTO Category (Name) VALUES ('Frisdranken');
INSERT INTO Category (Name) VALUES ('Bier');
INSERT INTO Category (Name) VALUES ('Wijnen en aperitieven');
INSERT INTO Category (Name) VALUES ('Warme dranken');
INSERT INTO Category (Name) VALUES ('Speciaal bier');

INSERT INTO Product (Name, CategoryId, Price) VALUES ('Dommelsch 0.22' , (SELECT CategoryId FROM Category WHERE Name = 'Bier'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Dommelsch 0.25' , (SELECT CategoryId FROM Category WHERE Name = 'Bier'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Dommelsch 0.50' , (SELECT CategoryId FROM Category WHERE Name = 'Bier'), 4.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Jupiler N/A 0.0%' , (SELECT CategoryId FROM Category WHERE Name = 'Bier'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Palm' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.40);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Hoegaarden witbier' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.40);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Hoegaarden Radler 0.0%' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.40);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Hoegaarden Radler 2.0%' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.40);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Leffe dubbel' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Leffe blond' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Leffe trippel' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 4.25);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Hoegaarden rosé' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Liefmans fruitesse' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Oud bruin' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Biestheuvel blond 6%' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 4.00);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Biestheuvel IPA 7%' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 4.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Biestheuvel Tripel 9%' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 4.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Coca-cola regular' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Coca-cola light' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Coca-cola zero' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Sprite' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Fanta orange' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Bitter Lemon' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Tonic' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Fanta Cassis' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Chaudfontainte still' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Chaudfontainte sparkling' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Lipton-ice tea regular' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Lipton-ice green' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Appelsap' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Jus d’orange' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Rivella' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Tomatensap' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Chocomel' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Fristi' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Huiswijnen Rood' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Huiswijnen Wit' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Huiswijnen Rose' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Port' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Sherry' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Vermouth' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Kop koffie' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Thee (Lipton)' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Cappuccino' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Latte Macchiato' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Koffie verkeerd' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Espresso' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Warme chocomel' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 3.00);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Warme chocomel met slagroom' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 3.50);

INSERT INTO CostObject (Name, Budget, Email, TelNr) VALUES ('HBO-ICT Kerst Borrel', 100, 'info@hbo-ict.com', '058-2131313');
INSERT INTO CostObject (Name, Budget, Email, TelNr) VALUES ('HBO-ICT Uitje', 100, 'info@hbo-ict.com', '058-2131313');
INSERT INTO CostObject (Name, Budget, Email, TelNr) VALUES ('HBO-ICT Viering 100 jarig bestaan', 1000, 'info@hbo-ict.com', '058-2131313');

INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (1, (SELECT ProductId FROM Product WHERE Name = 'Cappuccino'), 3, (SELECT CostObjectId FROM CostObject WHERE Name = 'HBO-ICT Kerst Borrel'));
INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (1, (SELECT ProductId FROM Product WHERE Name = 'Latte Macchiato'), 2, (SELECT CostObjectId FROM CostObject WHERE Name = 'HBO-ICT Kerst Borrel'));
INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (1, (SELECT ProductId FROM Product WHERE Name = 'Warme chocomel met slagroom'), 4, (SELECT CostObjectId FROM CostObject WHERE Name = 'HBO-ICT Kerst Borrel'));

INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (2, (SELECT ProductId FROM Product WHERE Name = 'Thee (Lipton)'), 4, (SELECT CostObjectId FROM CostObject WHERE Name = 'HBO-ICT Kerst Borrel'));
INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (2, (SELECT ProductId FROM Product WHERE Name = 'Koffie verkeerd'), 4, (SELECT CostObjectId FROM CostObject WHERE Name = 'HBO-ICT Kerst Borrel'));


INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (1, (SELECT ProductId FROM Product WHERE Name = 'Biestheuvel blond 6%'), 3, (SELECT CostObjectId FROM CostObject WHERE Name = 'HBO-ICT Uitje'));
INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (1, (SELECT ProductId FROM Product WHERE Name = 'Dommelsch 0.50'), 2, (SELECT CostObjectId FROM CostObject WHERE Name = 'HBO-ICT Uitje'));
INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (1, (SELECT ProductId FROM Product WHERE Name = 'Huiswijnen Rood'), 4, (SELECT CostObjectId FROM CostObject WHERE Name = 'HBO-ICT Uitje'));

INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (2, (SELECT ProductId FROM Product WHERE Name = 'Biestheuvel blond 6%'), 4, (SELECT CostObjectId FROM CostObject WHERE Name = 'HBO-ICT Uitje'));
INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (2, (SELECT ProductId FROM Product WHERE Name = 'Dommelsch 0.50'), 4, (SELECT CostObjectId FROM CostObject WHERE Name = 'HBO-ICT Uitje'));

INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (6, (SELECT ProductId FROM Product WHERE Name = 'Cappuccino'), 4, NULL);
INSERT INTO OrderLine (TableNr, ProductId, Amount, CostObjectId) VALUES (6, (SELECT ProductId FROM Product WHERE Name = 'Dommelsch 0.50'), 4, NULL);

SELECT CO.Name, SUM(p.Price * OL.Amount) as 'Spend'
 FROM OrderLine OL
     JOIN Product P on OL.ProductId = P.ProductId
        JOIN CostObject CO on CO.CostObjectId = OL.CostObjectId
GROUP BY CO.CostObjectId, CO.Name



