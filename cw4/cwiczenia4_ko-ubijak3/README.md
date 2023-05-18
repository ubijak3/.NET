[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-8d59dc4de5201274e310e4c54b9627a8934c3b88527886e3b421487c677d23eb.svg)](https://classroom.github.com/a/8EdDZbYA)
# Ćwiczenia4

Zadanie polega na stworzeniu aplikacji WebAPI opartą na stylu REST (ang. Representational State Transfer). Aplikacja ma umożliwiać wykonanie operacji pozwalających na modyfikowanie danych w bazie SQL Server.

## Baza danych

Razem z zadaniem załączony jest skrypt pozwalający na stworzenie tabelki Animal i wypełnienie jej danymi.

Tabela - **Animal**

| Field       | DataType      | Contraints |
| ----------- | ------------- | :--------: |
| ID          | int           |     PK     |
| Name        | nvarchar(200) |            |
| Description | nvarchar(200) |     N      |
| Category    | nvarchar(200) |            |
| Area        | nvarchar(200) |            |

Do zadania można wykorzystać bazę szkolną: db-mssql.pjwstk.edu.pl lub lokalną bazę **LocalDB**.

Aby skorzystać z bazy lokalnej trzeba będzie pobrać plik instalacyjny ze strony: https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16. Znajduje się on pod nagłówkiem Installation media > SQL Server Express 2019. Po odpaleniu instalatora należy wybrać Download Media > LocalDB, a następnie trzeba wybrać lokalizację gdzie zostanie pobrany plik instalacyjny bazy LocalDB. Po zainstalowaniu bazy i restarcie komputera, można pracować na tej bazie wskazując adres: (localdb)\MSSQLLocalDB

Przykładowy **connectionString** do bazy danych:
`"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=apbd;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"`

## Wymogi zadania

1. Należy dodać kontroler **AnimalsController**
2. Należy skorzystać z wstrzykiwania zależności - **Dependency Injection**
3. Dane **muszą** być walidowane
4. Dodaj metodę/endpoint pozwalającą na uzyskanie listy zwierząt. Końcówka powinna reagować na żądanie typu **HTTP GET** wysłane na adres **/api/animals**

   - Końcówka powinna pozwolić na przyjęcie parametru w query string, który określa sortowanie. Parametr nazywa się **orderBy**. Przykład: **api/animals?orderBy=name**
   - Parametr jako dostępne wartości przyjmuje: name, description, category, area. Możemy sortować wyłącznie po jednej kolumnie. Sortowanie jest zawsze w kierunku „ascending”.
   - Domyślne sortowanie (kiedy w żądaniu nie zostanie przekazany parametr w query string) powinna odbywać się po kolumnie name.

5. Dodaj metodę/endpoint pozwalający na dodanie nowego zwierzęcia.

   ```
    {
        "id": 1,
        "name": "Małpa z dużym nosem",
        "description": "Duży nos",
        "category": "Małpa",
        "area": "Indonezja"
    }
   ```

   - Metoda powinna odpowiadać na żądanie **HTTP POST** na adres **api/animals**
   - Metoda powinna przyjmować dane w postaci **JSON**
   - Należy sprawdzić czy wybrane ID jest unikalne. Jeśli nie trzeba zwrócić kod błędu `HTTP 409`
   - Jeśli dane będą niezgodne z modelem należy zwrócić kod błędu `HTTP 400`.
   - W przypadku pomyślnego dodania zwierzęcia do bazy należy zwrócić kod `HTTP 201`.

6. Dodaj metodę/endpoint pozwalający na aktualizację danych konkretnego zwierzęcia.

   ```
   {
        "name": "Nosacz Sundajski",
        "description": "Nos do interesów",
        "category": "Małpa",
        "area": "Polska"
   }
   ```

   - Metoda powinna odpowiadać na żądanie **HTTP PUT** wysłane na adres
     /**api/animals/{animalID}**
   - Metoda przyjmuje dane w postaci **JSON’a**
   - Zakładamy, że klucze główne nie ulegają modyfikacji (kolumna ID)
   - Nowe dane są przesyłane w ciele żądania (body) w formacie JSON. Jeśli dane będą niezgodne z modelem należy zwrócić kod błędu `HTTP 400`.
   - Jeśli zwierzę o podanym numerze indeksu nie istnieje, należy zwrócić błąd `HTTP 404`.
   - W przypadku sukcesu, końcówka powinna zwrócić aktualne dane aktualizowanego zwierzęcia (`HTTP 200`).

7. Dodaj metodę/endpoint do usuwania danych na temat konkretnego zwierzęcia.
   - Metoda powinna odpowiadać na żądanie **HTTP DELETE** wysłane na adres
     **/api/animals/{animalID}**
   - Jeśli zwierzę o podanym numerze indeksu nie istnieje, należy zwrócić błąd `HTTP 404`.
   - W przypadku pomyślnego usunięcia zwierzęcia z bazy, powinien zostać zwrócony odpowiedni komunikat za pomocą kodu `HTTP 200`.

## Uwagi

- Program powinien być napisany przy użyciu .NET7. Użycie innej wersji może skutkować utratą punktów
- Program, który się nie kompiluje - 0 pkt
- Należy pamiętać o **poprawnych** kodach HTTP. Niepoprawny kod HTTP jest równoznaczny z utratą punktów
- Należy pamiętać o obsłudze wyjątków
- Komunikacja z bazą danych powinna odbywać się poprzez klasy **SqlConnection/SqlCommand**.
