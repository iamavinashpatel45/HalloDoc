create table products(
ProductID int primary key identity(1,1),
ProductName varchar(40) not null,
SupplierID int,
CategaryID int,
QuantityPerUnit int,
UnitPrice int,
UnitsInStock int,
UnitsOnOrder int,
ReorderLevel int,
DiscontiNued bit,
);
 


 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Moniter-LG',5,1,5,40,25,18,1,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Moniter-Samsung',1,1,3,37,22,30,2,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Moniter-MI',9,1,8,30,15,10,3,1);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Moniter-Asus',3,1,3,35,4,6,4,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Moniter-Acer',4,1,6,34,9,2,5,1);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Keyboard-LG',5,2,10,12,50,32,6,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Keyboard-TVS',6,2,15,16,30,44,7,0);


 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Keyboard-Samsung',1,2,5,10,26,20,7,1);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Keyboard-Acer',4,2,15,13,10,15,9,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Keyboard-Asus',3,2,12,11,25,28,10,1);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Mouse-Samsung',1,5,10,15,30,19,11,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Mouse-TVS',6,5,12,14,10,23,12,1);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('Mouse-MI',9,5,5,11,17,15,13,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('CPU-Samsung',1,3,15,50,45,26,14,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('CPU-Asus',3,3,10,45,12,22,15,1);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('CPU-Acer',4,3,7,53,5,9,16,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('CPU-HP',7,3,8,42,33,30,16,1);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('CPU-Dell',8,3,4,47,15,5,17,1);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('headphone-MI',9,6,5,5,45,50,18,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('headphone-TVS',6,6,8,8,23,15,19,1);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('speaker-Boat',10,4,5,10,25,31,20,0);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('speaker-JBL',12,4,7,15,15,13,16,1);

 insert into products (ProductName,SupplierID,CategaryID,
 QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,DiscontiNued)
 values('speaker-TVS',6,4,9,20,5,7,22,0);


-- Querys

--1).Write a query to get a Product list (id, name, unit price) where current products cost less than $20.
select ProductID,ProductName,UnitPrice from products where UnitPrice<20; 

--2).Write a query to get Product list (id, name, unit price) where products cost between $15 and $25
SELECT ProductID,ProductName,UnitPrice FROM products WHERE UnitPrice BETWEEN 15 AND 25;

--3).Write a query to get Product list (name, unit price) of above average price.
SELECT ProductName,UnitPrice FROM products WHERE UnitPrice>(SELECT AVG (UnitPrice) FROM products);

--4).Write a query to get Product list (name, unit price) of ten most expensive products
SELECT TOP 10 ProductName,UnitPrice FROM products ORDER BY UnitPrice DESC;

--5).Write a query to count current and discontinued products
SELECT COUNT(DiscontiNued) AS PRODUCTS ,DiscontiNued FROM products GROUP BY DiscontiNued ;

--6)Write a query to get Product list (name, units on order , units in stock) of stock is less than the quantity on order
SELECT ProductName,UnitsInStock,UnitsOnOrder FROM products WHERE UnitsInStock<UnitsOnOrder;
 