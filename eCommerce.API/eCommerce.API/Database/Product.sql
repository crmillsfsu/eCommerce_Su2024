--CREATE SCHEMA Product

CREATE PROCEDURE Product.InsertProduct 
@Name nvarchar(255)
, @Description nvarchar(max)
, @Quantity int
, @Price numeric(10,2)
, @Id int output
AS
BEGIN
	INSERT INTO PRODUCT ([Name], [Description], Quantity, Price)
	VALUES(@Name, @Description, @Quantity, @Price)

	SET @Id = SCOPE_IDENTITY()
END

declare @newId int
exec Product.InsertProduct @Name = 'SP Product'
, @Description = 'Product inserted from SP'
, @Quantity = 10
, @Price = 1.23
, @Id = @newId out

select @newId

select * from Product

CREATE PROCEDURE Product.UpdateProduct 
@Name nvarchar(255)
, @Description nvarchar(max)
, @Quantity int
, @Price numeric(10,2)
, @Id int
AS
BEGIN
	UPDATE PRODUCT 
	SET
		Name = @Name
		, Description = @Description
		, Quantity = @Quantity
		, Price = @Price
	WHERE
		Id = @Id
END

--TBD: DELETE