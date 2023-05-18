# Ćwiczenia1 - Crawler
Zadanie polega na przygotowaniu prostego "crawlera", który będzie przeszukiwał wybraną stronę WWW i odnajdywał znajdujące się tam adresy email.

### Działanie aplikacji w skrócie
- Program otrzymuje pojedynczy argument, który jest adresem URL strony, która będzie celem skanu "crawlera"
- Za pomocą klasy HttpClient wykonuje żądanie HTTP GET i pobiera kod źródłowy strony internetowej
- Przeszukuje zawartość strony i wypisuje na konsoli wszystkie adresy email, które zostały znalezione na stronie

### Wymagania 
- W sytuacji gdy argument nie został przekazany, powinien zostać zwrócony błąd `ArgumentNullException`
- Jeśli został przekazany argument, który nie jest poprawnym adresem URL, powinien zostać zwrócony błąd `ArgumentException`
- W przypadku, gdy podczas pobierania strony wystąpi błąd (czyli status żądania, który **nie jest** z przedziału 200-299), powinien zostać zwrócony błąd `Exception` z informacją `Błąd w czasie pobierania strony`
- W sytuacji, gdy nie znaleziono żadnych adresów email, powinien zostać zwrócony błąd `Exception` z informacją `Nie znaleziono adresów email`
- Gdy zostały znalezione adresy email, powinny zostać wyświetlone na konsoli. Aplikacja powinna zwracać **tylko** unikalne adresy email

### Uwagi
- Program, który się nie kompiluje - 0 pkt
- Na konsoli, powinny zostać zwrócone tylko adresy email, reszta powinna być obsługiwana za pomocą błędów. Nie dostosowanie się do tej reguły, może skutkować **mniejszą** ilością punktów!

### Pomoc
- Do znalezienia adresów email w kodzie źródłowym strony, można skorzystać z klasy `Regex`. Klasa znajduje się w przestrzeni nazw `System.Text.RegularExpressions`
- Do sprawdzenia poprawność adresu URL, można skorzystać z klasy `Uri`

### Przykład działania aplikacji dla podanych wymagań
- Wymaganie nr1
```
Unhandled exception. System.ArgumentNullException: Value cannot be null.
   at Program.<Main>$(String[] args) in ..\Program.cs:line 8
   at Program.<Main>(String[] args)
```
- Wymaganie nr2
```
Unhandled exception. System.ArgumentException: Niepoprawny url
   at Program.<Main>$(String[] args) in ..\Program.cs:line 10
   at Program.<Main>(String[] args)
```
- Wymaganie nr3
```
Unhandled exception. System.Exception: Błąd w czasie pobierania strony
   at Program.<Main>$(String[] args) in ..\Program.cs:line 17
   at Program.<Main>(String[] args)
```
- Wymaganie nr4
```
Unhandled exception. System.Exception: Nie znaleziono adresów email
   at Program.<Main>$(String[] args) in ..\Program.cs:line 27
   at Program.<Main>(String[] args)
```
- Wymaganie nr5 dla adresu https://pjwstk.edu.pl
```
pjatk@pja.edu.pl
```