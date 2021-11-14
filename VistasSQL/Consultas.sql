select p.ProductID, p.ProductName, c.CategoryName from Products p
inner join Categories c on c.CategoryID = p.CategoryID
 where Discontinued = 0 
 order by 2
 
 select distinct c.CustomerID, c.CompanyName from Orders o
 inner join Customers c on c.CustomerID = o.CustomerID
 inner join Employees e on e.EmployeeID = o.EmployeeID
 where e.FirstName = 'Nancy' and e.LastName = 'Davolio'
 
 select 
 year(o.OrderDate) Ejercicio,
 sum(od.UnitPrice * od.Quantity) Facturado
 from [Order Details] od
 inner join Orders o on o.OrderID = od.OrderID
 inner join Employees e on e.EmployeeID = o.EmployeeID
 where e.FirstName = 'Steven' and e.LastName = 'Buchanan'
 group by year(o.OrderDate)
 
 select rt.FirstName, rt.LastName from Employees rt
 where rt.ReportsTo = (select e.EmployeeID from Employees e where e.FirstName = 'Andrew' and e.LastName = 'Fuller')
 
select rt.EmployeeID, rt.FirstName, rt.LastName from Employees e
inner join Employees rt on rt.ReportsTo = e.EmployeeID
where e.FirstName = 'Andrew' and e.LastName = 'Fuller'
union
select e.EmployeeID, e.FirstName, e.LastName from Employees e
where e.ReportsTo in (select rt.EmployeeID from Employees e
inner join Employees rt on rt.ReportsTo = e.EmployeeID
where e.FirstName = 'Andrew' and e.LastName = 'Fuller')