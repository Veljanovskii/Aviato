# Aviato

## Pokretanje projekta

### Baza podataka

Molim Vas, podatke iz baze podataka korišćene u ovom projektu dobavite na sledeći način:

* `> mongo`
* `> use Aviato`
* `> db.createCollection('Employees')`
* `> db.createCollection('Products')`
* `> db.Employees.insertMany([{'E-mail':'john@doe.com','Password':'john.doe'},{'E-mail':'jane@doe.com','Password':'jane.doe'}])`
* `>`[`skripta`](https://github.com/Veljanovskii/Aviato/blob/master/script.txt)

### Startovanje aplikacije

Projekat je standardna ASP.NET Core Web aplikacija i pokreće se startovanjem `Aviato.sln` datoteke.

## Opis funkcionalnosti

Kao što je navedeno prilikom prijavljivanje projekta, postoje dva tipa korisnika:

* #### Posetilac
Ima funkcionalnosti pregledanja sajta i dodavanja proizvoda u korpu.

* #### Radnik
Ima funkcionalnosti dodavanja novih proizvoda, menjanja i brisanja postojećih.  
**Važno.** Za dobijanje funkcionalnosti prodavca neophodno je ulogovati se korišćenjem jednog od navedenih naloga:  
* "E-mail" : "john<i></i>@doe.com",
"Password" : "john.doe"  
* "E-mail" : "jane<i></i>@doe.com",
"Password" : "jane.doe"
