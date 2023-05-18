[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-8d59dc4de5201274e310e4c54b9627a8934c3b88527886e3b421487c677d23eb.svg)](https://classroom.github.com/a/J4oWJ-lp)
# Ćwiczenia3

Zadanie polega na stworzeniu aplikacji WebAPI opartą na stylu REST (ang. Representational State Transfer).

## Działanie aplikacji

W niniejszym zadaniu proszę przygotować nowy projekt aplikacji `Internetowy interfejs API platformy ASP.NET Core`.  
Przygotowywana aplikacja ma wspierać uczelnię w procesie zarządzania danymi studentów.

- Proszę przygotować kontroler o nazwie StudentsController
- Kontroler powinien mieć 5 metod publicznych związanych z zarządzaniem danymi na temat
  studentów.
- Aplikacja powinna zapisywać wszystkie dane w **lokalnej** bazie danych w pliku CSV.

Dane powinny mieć następującą postać.  
`Imie,nazwisko,numerIndeksu,dataUrodzenia,studia,tryb,email,imię ojca,imię matki`  
np.  
`Jan,Kowalski,s1234,3/20/1991,Informatyka,Dzienne,kowalski@wp.pl,Jan,Anna`

Aplikacja powinna implementować poniższe końcówki.

## Końcówki

1. `HTTP GET http://localhost:5000/api/students`  
   Pierwsza końcówka powinna odpowiadać na żądanie typu HTTP GET na adres `api/students`  
   Końcówka powinna zwrócić listę studentów z lokalnej bazy danych – pliku.  
   W przypadku braku studentów będzie zwracana pusta lista.  
2. `HTTP GET http://localhost:5000/api/students/s1234`  
   Druga końcówka powinna pozwalać na przekazanie parametru jako segment adresu
   URL, który daje nam możliwość pobrania konkretnego studenta.  
   W takim wypadku zostanie zwrócony pojedynczy student o konkretnym numerze indeksu.  
   Jeśli dany student nie istnieje powinien zostać zwrócony odpowiedni kod `HTTP 404`.  
3. `HTTP POST http://localhost:5000/api/students`  

   ```
   {
       "firstName": "Jan",
       "lastName": "Kowalski",
       "indexNumber": "s1234",
       "birthDate": "2023-03-01",
       "studyName": "Informatyka",
       "studyMode": "Dzienne",
       "email": "jan@kowalski.com",
       "fathersName": "Adam",
       "mothersName": "Anna"
   }
   ```

   Trzecia końcówka powinna umożliwić dodanie nowego studenta wykonująć żądanie `HTTP POST`.  
   Końcówka przyjmuje dane nowego studenta w ciele żądania (body) w formacie JSON, tak jak powyżej. Przed dodaniem nowego studenta należy sprawdzić:

   - czy wybrany numer indeksu jest unikalny. Jeśli nie trzeba zwrócić kod błędu `HTTP 409`
   - czy wszystkie dane na temat studenta są kompletne. W przeciwnym wypadku trzeba zwrócić kod błędu `HTTP 400`

   Jeśli powyższe wymagania są spełnione, dodajemy studenta do bazy na koniec pliku CSV.  
   W przypadku pomyślnego dodania studenta do bazy należy zwrócić kod `HTTP 201`.  

4. `HTTP PUT http://localhost:5000/api/students/s1234`  

   ```
   {
       "firstName": "Jan",
       "lastName": "Kowalski",
       "birthDate": "2023-03-01",
       "studyName": "Informatyka",
       "studyMode": "Dzienne",
       "email": "jan@kowalski.com",
       "fathersName": "Adam",
       "mothersName": "Anna"
   }
   ```

   Czwarta końcówka odpowiada na żądanie `HTTP PUT`, gdzie pozwala na aktualizację danych konkretnego studenta.  
   Nowe dane są przesyłane w ciele żądania (body) w formacie JSON. Jeśli dane będą niekompletne należy zwrócić kod błędu `HTTP 400`.  
   W przypadku sukcesu, końcówka powinna zwrócić aktualne dane aktualizowanego studenta (`HTTP 200`).  

5. `HTTP DELETE http://localhost:5000/api/students/s1234`  
   Piąta końcówka powinna reagować na żądanie `HTTP DELETE`, gdzie pozwala na usunięcie konkretnego studenta z bazy danych.  
   Jeśli student o podanym numerze indeksu nie istnieje, należy zwrócić błąd `HTTP 404`.  
   W przypadku pomyślnego usunięcia studenta z bazy, powinien zostać zwrócony odpowiedni komunikat za pomocą kodu `HTTP 200`.

## Uwagi

- Program powinien być napisany przy użyciu .NET7. Użycie innej wersji może skutkować utratą punktów
- Program, który się nie kompiluje - 0 pkt
- Należy pamiętać o **poprawnych** kodach HTTP. Niepoprawny kod HTTP jest równoznaczny z utratą punktów
- Należy pamiętać o obsłudze wyjątków
