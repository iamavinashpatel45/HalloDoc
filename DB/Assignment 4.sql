--1>Create a stored procedure in the Northwind database that will calculate the average value of Freight for a specified --  customer.Then, a business rule will be added that will be triggered before every Update and Insert command in the --  Orders controller,and will use the stored procedure to verify that the Freight does not exceed the average freight. --  If it does, a message will be displayed and the command will be cancelled.CREATE PROCEDURE avg_freight
	@customer_id varchar(40)
AS
BEGIN
	select c.CompanyName,avg(Freight) AS "AVERAGE" from Customers c inner join orders o on c.CustomerID=o.CustomerID 
	where o.OrderID=@order_id group by c.CustomerID,CompanyName;
END

CREATE TRIGGER tr_orders_insert
on Orders
for insert
AS
BEGIN
		declare @id VARCHAR(40);
		select @id=CustomerID from inserted;
		declare @freight money;
		select @freight=Freight from inserted;
		EXEC avg_freight @customer_id=@id;
		if(exists(select 1 from orders  WHERE CustomerID=@id HAVING avg(Freight)>@freight))
	    begin 
		print('row is not added' );
		rollback
		end
		else
		begin
		print('row is added');
		print('new order from CustomerID = '+ @id +' at ' + cast(getdate() as varchar(40)) );
		end
END

CREATE TRIGGER tr_orders_update
on Orders
for update
AS
BEGIN
		declare @id VARCHAR(40);
		select @id=CustomerID from inserted;
		declare @freight money;
		select @freight=Freight from inserted;
		EXEC avg_freight @customer_id=@id;
		if(exists(select 1 from orders  WHERE CustomerID=@id HAVING avg(Freight)>@freight))
	    begin 
		print('row is not Updated' );
		rollback
		end
		else
		begin
		print('row is Updated');
		print('new order from CustomerID = '+ @id +' at ' + cast(getdate() as varchar(40)) );
		end
END


--RUN-
exec avg_freight @customer_id='RICSU';


--2>write a SQL query to Create Stored procedure in the Northwind database to retrieve Employee Sales by CountryCREATE PROCEDURE country_sales
	@firstname varchar(40),
	@country varchar(40)
AS
BEGIN

	select o.EmployeeID,e.FirstName,shipCountry,count(ShipCountry) as "Number of Sales" from orders o left join Employees e 
    on o.EmployeeID=e.EmployeeID where e.FirstName=@firstname and shipCountry=@country group by o.EmployeeID,e.FirstName,ShipCountry
END

--RUN-
exec country_sales @firstname='Andrew' ,@country='Denmark';


--3>write a SQL query to Create Stored procedure in the Northwind database to retrieve Sales by Year

CREATE PROCEDURE year_sale
	@year int
AS
BEGIN

	select count(year(OrderDate)) as "No of Sales",year(OrderDate) as "Year" from Orders 
	where year(OrderDate)=@year group by(year(OrderDate));
END

--RUN-
exec year_sale @year=1997;


--4>write a SQL query to Create Stored procedure in the Northwind database to retrieve Sales By Category

CREATE PROCEDURE category_sales
	@name varchar(40)
AS
BEGIN

	select count(OrderID) as "Number of Orders",p.CategoryID,c.CategoryName from Products p inner join [Order Details] od 
    on p.ProductID=od.ProductID inner join Categories c on p.CategoryID=c.CategoryID where c.CategoryName=@name 
	group by p.CategoryID,c.CategoryName;
END

--RUN-
exec category_sales @name='Condiments';


--5>write a SQL query to Create Stored procedure in the Northwind database to retrieve Ten Most Expensive Products

CREATE PROCEDURE expensive
AS
BEGIN
	
	select top 10 ProductName,UnitPrice from Products order by UnitPrice desc;
END

--RUN-
exec expensive;


--6>write a SQL query to Create Stored procedure in the Northwind database to insert Customer Order Details 

CREATE PROCEDURE add_orderDetails
	@order_id int,
	@product_id int,
	@unitPrice float,
	@quantity int,
	@discount float
AS
BEGIN
	
	INSERT "Order Details" VALUES(@order_id,@product_id,@unitPrice,@quantity,@discount);
END

--RUN-
exec add_orderDetails @order_id=10956,@product_id=42,@unitPrice=9.8,@quantity=10,@discount=0;


--7>write a SQL query to Create Stored procedure in the Northwind database to update Customer Order Details

CREATE PROCEDURE update_orderDetails
	@order_id int,
	@product_id int,
	@unitPrice float,
	@quantity int,
	@discount float
AS
BEGIN

	IF(@unitPrice is not NULL)
	BEGIN
	update "Order Details" set UnitPrice=@unitPrice where OrderID=@order_id and ProductID=@product_id;
	END

	IF(@quantity is not NULL)
	BEGIN
	update "Order Details" set quantity=@quantity where OrderID=@order_id and ProductID=@product_id;
	END

	IF(@discount is not NULL)
	BEGIN
	update "Order Details" set discount=@discount where OrderID=@order_id and ProductID=@product_id;
	END
END

--RUN-ANY-
exec update_orderDetails @order_id=10956,@product_id=42,@unitPrice=11,@quantity=565;
exec update_orderDetails @order_id=10956,@product_id=42,@quantity=50;
exec update_orderDetails @order_id=10956,@product_id=42,@discount=0.10;

