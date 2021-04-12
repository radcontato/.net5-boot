use DevCars

select * from tb_car
select * from tb_Customer
select * from tb_Order
select * from tb_ExtraOrderItem


--update tb_Car set status = 0

SELECT Id, Brand, Model, Price 
FROM tb_car 
WHERE [Status] = 0