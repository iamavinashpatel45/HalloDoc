create table Department(
dept_id int primary key identity(1,1),
dept_name varchar(40) not null,
);

insert into Department(dept_name) values('Finance');

insert into Department(dept_name) values('Audit');

insert into Department(dept_name) values('Marketing');

insert into Department(dept_name) values('Production');

insert into Department(dept_name) values('HR');

insert into Department(dept_name) values('Developer');

select * from Employee;

create table Employee(
emp_id int primary key identity(1,1),
dept_id int,
mngr_id int,
emp_name varchar(40) not null,
salary int
);

insert into Employee (dept_id,mngr_id,emp_name,salary) values(4,2,'Pit Alex',50000);
insert into Employee (dept_id,mngr_id,emp_name,salary) values(5,1,'Lauson Hen',35500);
insert into Employee (dept_id,mngr_id,emp_name,salary) values(1,3,'Sam Karan',44900);
insert into Employee (dept_id,mngr_id,emp_name,salary) values(6,5,'David Warner',80000);
insert into Employee (dept_id,mngr_id,emp_name,salary) values(3,4,'James Hoog',51500);
insert into Employee (dept_id,mngr_id,emp_name,salary) values(2,6,'Nick Rimando',33350);
insert into Employee (dept_id,mngr_id,emp_name,salary) values(1,3,'Brad Davis',40000);
insert into Employee (dept_id,mngr_id,emp_name,salary) values(2,6,'Graham Zusi',32500);
insert into Employee (dept_id,mngr_id,emp_name,salary) values(4,2,'Fabian Johnson',50500);
insert into Employee (dept_id,mngr_id,emp_name,salary) values(2,6,'Jozy Altidor',33300);


--1)write a SQL query to find Employees who have the biggest salary in their Department

select emp_id,emp_name,salary from Employee where salary in (select max(salary) as "salary" from Employee group by dept_id);

--2)write a SQL query to find Departments that have less than 3 people in it

select d.dept_name,count(e.dept_id) as "Num of People" from Employee e inner join Department d 
on e.dept_id=d.dept_id group by d.dept_name having count(e.dept_id)<3;

--3)write a SQL query to find All Department along with the number of people thereselect d.dept_name,count(e.dept_id) as "Num of People" from Employee e inner join Department d on e.dept_id=d.dept_id group by d.dept_name;--4)write a SQL query to find All Department along with the total salary thereselect d.dept_name,count(e.dept_id) as "Num of People",sum(e.salary) as "total Salary" from Employee e inner join Department d on e.dept_id=d.dept_id group by d.dept_name;