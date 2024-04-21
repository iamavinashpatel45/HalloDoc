--3.1
SELECT DISTINCT customer_id FROM payment WHERE amount >=7.99;

--3.2
SELECT title ,rental_rate,replacement_cost FROM film WHERE rental_rate>2.99 OR replacement_cost>19.99;

--4.1
SELECT title,replacement_cost AS "replacement cost",rental_duration AS "rental duration" 
FROM film WHERE rental_duration BETWEEN 4 AND 6 ORDER BY replacement_cost DESC LIMIT 100;

--4.2
SELECT title,rating,length,description FROM film WHERE length>120 AND rating IN ('G','PG') 
AND description LIKE '%Action%';	

--6.2
SELECT first_name,COUNT(*) AS "number of names" FROM actor GROUP BY first_name 
ORDER BY "number of names" DESC LIMIT 5;

--8.1
SELECT f.title,f.rental_rate,l.name FROM language l INNER JOIN film f ON l.language_id=f.language_id;

--8.2
SELECT concat(a.first_name,' ',a.last_name) AS "name",COUNT(f.actor_id) AS "COUNT" FROM film_actor f 
INNER JOIN actor a ON f.actor_id=a.actor_id GROUP BY f.actor_id, name ORDER BY COUNT DESC;

--8.3
SELECT f.rating,COUNT(f.rating) FROM inventory l INNER JOIN film f ON l.film_id=f.film_id 
INNER JOIN rental r ON l.inventory_id = r.inventory_id GROUP BY f.rating ORDER BY COUNT DESC ;

--10.1
SELECT r.rental_date,r.return_date,age(r.return_date,r.rental_date) AS "rent duration",c.first_name,c.lASt_name,c.email 
FROM rental r INNER JOIN customer c ON r.customer_id=c.customer_id WHERE r.return_date IS NOT null 
AND age(r.return_date,r.rental_date) >= interval '7 days' ORDER BY "rent duration";

--10.2
SELECT title,substr(title,11),substr(title,16),substr(title,6,3),substr(title,6,1) FROM film;

--12.1
SELECT concat(c.first_name,' ',c.last_name) AS "Name",c.email,SUM(p.amount) AS "Total Rentals",
CASE
	WHEN COUNT(p.amount)>200 THEN 'Elite'
	WHEN COUNT(p.amount)>=200 AND  COUNT(p.amount)>150 THEN 'Platinum'
	WHEN COUNT(p.amount)>=150 AND  COUNT(p.amount)>100 THEN 'Gold'
	ELSE 'Silver'
END
FROM customer c INNER JOIN payment p ON c.customer_id=p.customer_id 
GROUP BY p.customer_id,"Name",c.email;

--12.2
CREATE VIEW "categories of Customer" AS
SELECT concat(c.first_name,' ',c.last_name) AS "Name",c.email,SUM(p.amount) AS "Total Rentals",
CASE
	WHEN COUNT(p.amount)>200 THEN 'Elite'
	WHEN COUNT(p.amount)>=200 AND  COUNT(p.amount)>150 THEN 'Platinum'
	WHEN COUNT(p.amount)>=150 AND  COUNT(p.amount)>100 THEN 'Gold'
	ELSE 'Silver'
END
FROM customer c INNER JOIN payment p ON c.customer_id=p.customer_id 
GROUP BY p.customer_id,"Name",c.email;

SELECT * FROM "categories of Customer";

--14.1
CREATE DATABASE mycommerce;
CREATE TABLE order_details(
	orderid SERIAL PRIMARY KEY,
	customer_name VARCHAR(40) NOT NULL,
	product_name VARCHAR(40) NOT NULL,
	ordered_from VARCHAR(10),
	order_amount FLOAT NOT NULL,
	order_date DATE NOT NULL,
	delivary_date DATE
);

--14.2
ALTER TABLE order_details RENAME COLUMN customer_name TO customer_first_name;
ALTER TABLE order_details ADD COLUMN cancel_date date;




