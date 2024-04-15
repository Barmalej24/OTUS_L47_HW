CREATE TABLE Orders (
ID int NOT NULL PRIMARY KEY,
CustomerID int NOT NULL,
ProductID int NOT NULL,
Quantity int NOT NULL,
FOREIGN KEY (CustomerID) REFERENCES Customers(id),
FOREIGN KEY (ProductID) REFERENCES Products(id)
);