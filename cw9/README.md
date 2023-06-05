[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/INqqB0kU)
# Ćwiczenia 9

Wszystkie zadania realizujemy z pomocą projektu z poprzednich ćwiczeń (ćwiczenia 8).

## JWT

Do przygotowanego wcześniej projektu dodajemy uwierzytelnienie z pomocą tokena **JWT**.

1. Zabezpiecz wszystkie końcówki tak, aby były dostępne tylko dla zalogowanych użytkowników (czyli użytkowników przesyłających poprawny token).
2. Dodaj kontroler o nazwie AccountsController z metodą umożliwiającą logowanie (przesłanie loginu i hasła). Jako odpowiedź metoda powinna zwrócić nowy token wraz z refresh token’em. Dodatkowo dodaj nową migrację, która pozwoli na zapisanie użytkownika w bazie danych. Postaraj się przechowywać wrażliwe dane w bazie danych w odpowiedni sposób (SALT, PBKDF2).
3. Przygotuj końcówkę, która pozwoli na uzyskanie nowego access token’a na podstawie refresh token’a.

## Middleware

4. Dodaj własny middleware służący do logowania wszystkich błędów do pliku tekstowego o nazwie `logs.txt`.

## Uwagi

- Program powinien być napisany przy użyciu .NET7. Użycie innej wersji może skutkować utratą punktów
- Program, który się nie kompiluje - 0 pkt
- Należy pamiętać o **poprawnych** kodach HTTP. Niepoprawny kod HTTP jest równoznaczny z utratą punktów
- Pamiętaj o poprawnych nazwach zmiennych/metod/klas
- Wykorzystaj dodatkowe modele dla danych zwracanych i przyjmowanych przez
  końcówki – DTO (ang. Data Transfer Object)
- Pamiętaj o SOLID, DI
