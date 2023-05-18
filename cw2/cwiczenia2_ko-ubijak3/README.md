# Ćwiczenia2
Zadanie polega na przygotowaniu aplikacji konsolowej, która służy do obróbki danych.

##### Opis biznesowy:
Okazało się, że na Uniwersytecie XYZ powstała potrzeba eksportu danych i ich odpowiedniego przygotowania do przesłania do Ministerstwa Edukacji i Szkolnictwa Wyższego. Ministerstwo przygotowały system, który pozwala na zaimportowanie pliku JSON w odpowiednim formacie określonym przez Ministerstwo.

System informatyczny stosowany na Uniwersytecie XYZ pozwala na eksport danych wyłącznie w pliku CSV. Niestety wyeksportowane dane zawierają pewne błędy lub brakujące dane. Ponadto ich format nie odpowiada formatowi, którego oczekuje ministerstwo. Musimy stworzyć aplikację konsolową, która pozwoli na poprawne przetworzenie otrzymanego pliku CSV i otrzymanie pliku wynikowego zgodnego z formatem oczekiwanym przez Ministerstwo.

### Działanie aplikacji
1. Program otrzymuje cztery argumenty:
    1. Ścieżka do pliku z danymi np. `"C:\Users\Jan\Desktop\csvData.csv"`
    2. Ścieżka do folderu, gdzie zostanie wyeksportowany plik wynikowy np. `output`
    3. Ścieżka do pliku z logami `logs.txt`
    4. Format danych w jakich plik ma zostać wyeksportowany np. `json`
2. Program odczytuje plik z danymi, gdzie każdy wiersz reprezentuje pojedynczego 
studenta. Każda kolumna jest oddzielona znakiem `,`. Każdy student powinien być opisywany przez 9 kolumn.  
Poniżej zaprezentowany jest pojedynczy wpis w pliku CSV.  
`Paweł,Nowak1,Informatyka dzienne,Dzienne,459,2000-02-12,1@pjwstk.edu.pl,Alina,Adam`
3. Program przetwarza dane w celu usunięcie błędów i brakujących danych.
4. Dane są odpowiednio agregowane.
5. Zapisujemy dane odpowiednio sformatowane zgodnie z rozszerzeniem przekazanym w 4 argumencie do pliku.

##### Przykładowy plik wynikowy `uczelnia.json`
```
{
  "uczelnia": {
    "createdAt": "01.03.2023",
    "author": "Jan Kowalski",
    "students": [
      {
        "indexNumber": "s459",
        "fname": "Paweł",
        "lname": "Nowak1",
        "birthdate": "12.02.2000",
        "email": "1@pjwstk.edu.pl",
        "mothersName": "Alina",
        "fathersName": "Adam",
        "studies": {
          "name": "Informatyka dzienne",
          "mode": "Dzienne"
        }
      },
      {
        "indexNumber": "s4816",
        "fname": "Kaja",
        "lname": "Jankowski29",
        "birthdate": "12.02.2000",
        "email": "29@pjwstk.edu.pl",
        "mothersName": "Alina",
        "fathersName": "Adam",
        "studies": {
          "name": "Sztuka Nowych Mediów dzienne",
          "mode": "Dzienne"
        }
      }
    ],
    "activeStudies": [
      {
        "name": "Informatyka dzienne",
        "numberOfStudents": "1"
      },
      {
        "name": "Sztuka Nowych Mediów dzienne",
        "numberOfStudents": "1"
      }
    ]
  }
}
```

### Wymagania
Ad 1. Aplikacja powinna być odporna na błędy przy podawaniu argumentów:
- W sytuacji, gdy jest przekazana za mała lub za duża ilość argumentów, powinniśmy zgłosić błąd `ArgumentOutOfRangeException`
- Gdy zostanie podana ścieżka do pliku z danymi (argument nr 1), który nie istnieje, powinien zostać zwrócony wyjątek `FileNotFoundException`
- Gdy zostanie podana ścieżka do folderu wynikowego, który nie istnieje powinien zostać zwrócony wyjątek `DirectoryNotFoundException`
- Gdy zostanie podana ścieżka do pliku z logami, który nie istnieje powinien zostać zwrócony wyjątek `FileNotFoundException`
- W przypadku, gdy zostanie podany format nieobsługiwany przez aplikację, powinien zostać zwrócony wyjątek 
`InvalidOperationException`  
  > :warning: **UWAGA** zakładamy, że w pierwszej wersji aplikacji będzie działać tylko typ `json`

Ad 2. Kolumny przez które opisywany jest student to:
- Imie
- Nazwisko
- Kierunek
- Tryb
- NrIndeksu
- Data
- Email
- ImieMatki
- ImieOjca

Ad 3. Podczas przetwarzania danych należy zwrócić uwagę na ich zawartość:
> :warning: **UWAGA** {line} oznacza aktualny wiersz z pliku, który posiada błędy, np.  
> `Paweł,Nowak1,Informatyka dzienne,Dzienne,,2000-02-12,1@pjwstk.edu.pl,Alina,Adam`
- Niektóre wiersze mogą zawierać błędy. Tych studentów, którzy nie są opisywani przez 9 kolumn z danymi **pomijamy**. Informacje o pominiętym studencie traktujemy jako błąd i logujemy do pliku. Informacja zapisywana do pliku powinna być następująca:
`Wiersz nie posiada odpowiedniej ilości kolumn: {line}`
- Jeśli jeden wiersz z danymi posiada w kolumnie pustą wartość - traktujemy taką wartość jako brakującą. W takim wypadku nie dodajemy studenta do zbioru wynikowego i zapisujemy następującą do pliku logów:
`Wiersz nie może posiadać pustych kolumn: {line}`
- Dane mogą zawierać duplikaty informacji o studentach. Musimy zadbać o to, aby nie dodawać do wyniku dwa razy studenta o tym samym imieniu, nazwisku i numerze indeksu. Zawsze pobieramy pierwszego studenta z danym imieniem, nazwiskiem i numerem indeksu. Każde powtórzenie danych o studencie w danych źródłowych traktujemy jako niepoprawny duplikat. Gdzie znalezione duplikaty logujemy do pliku jako:
`Duplikat: {line}`

Ad 4. Chcemy dodatkowo zapisać informację o ilości studentów na danym kierunku. Zbieramy informację o danych kierunkach, które **wystąpiły** w pliku! Do pliku wynikowego chcemy dodać tablicę `activeStudies`, która przechowuje obiekty `{"name": "nazwaKierunku", "numberOfStudents": liczbaStudentowNaKierunku }`:
```
"activeStudies": [
  {
    "name": "Informatyka dzienne",
    "numberOfStudents": "1"
  },
  {
    "name": "Sztuka Nowych Mediów dzienne",
    "numberOfStudents": "1"
  }
]
```

Ad 5. Przetworzone dane, przygotowujemy do zapisu, gdzie wymagana jest specyficzna hierarchia. Zapisywany będzie obiekt `uczelnia`, który ma zawierać:
- pole `createdAt` - data wygenerowania pliku, format `DD.MM.YYYY`
- pole `author` - imię i nazwisko autora tego kodu
- tablicę `students` - tablica obiektów Student, po wstępnym przetworzeniu  
  > :warning: **UWAGA** należy uwzględnić obiekt `studies` w zwracanym pliku
  ```
  "studies": {
    "name": "Informatyka dzienne",
    "mode": "Dzienne"
  }
  ```
- tablicę `activeStudies` - jej opis jest w Ad 4.

Po przygotowaniu danych zapisujemy je do folderu wskazanego przez użytkownika.  
Plik powinien mieć nazwę `uczelnia` i powien być zapisany ze wskazanym rozszerzeniem np. `uczelnia.json`.  

### Uwagi
- Program, który się nie kompiluje - 0 pkt
- Na ten moment program będzie obsługiwał tylko format `json`, ale w przyszłości powinien być gotowy na rozszerzenie o nowe typy danych np. `xml`, `yaml` etc.
- Jeśli jakikolwiek plik do zapisu już istnieje, powinien zostać nadpisany!
- Błędy podczas przetwarzania danych powinny być zapisywane do pliku. Nie muszą być wyświetlane na konsoli

### Przykład działania aplikacji
- Brak przekazanych parametrów
```
Unhandled exception. System.ArgumentOutOfRangeException: Specified argument was out of the range of valid values.
   at Program.<<Main>$>g__CheckParameters|0_0(String[] args) in ..\Program.cs:line 1
```

- Przekazanie ścieżki do pliku z danymi np.`C:\dane`, gdzie plik nie istnieje
```
Unhandled exception. System.IO.FileNotFoundException: Unable to find the specified file.
   at Program.<<Main>$>g__CheckParameters|0_0(String[] args) in ..\Program.cs:line 1
```

- Przekazanie w drugim argumencie folderu, który nie istnieje
```
Unhandled exception. System.IO.DirectoryNotFoundException: Attempted to access a path that is not on the disk.
   at Program.<<Main>$>g__CheckParameters|0_0(String[] args) in ..\Program.cs:line 1
```

- Przekazanie w trzecim argumencie ścieżku do pliku z logami, który nie istnieje
```
Unhandled exception. System.IO.FileNotFoundException: Unable to find the specified file.
   at Program.<<Main>$>g__CheckParameters|0_0(String[] args) in ..\Program.cs:line 1
```

- Przekazanie w czwartym argumencie rozszerzenia, które nie jest obsługiwane przez aplikację
```
Unhandled exception. System.InvalidOperationException: Operation is not valid due to the current state of the object.
   at Program.<<Main>$>g__CheckParameters|0_0(String[] args) in ..\Program.cs:line 1
```

- Dla pliku `dane.csv` i wszystkich poprawnych argumentach, gdzie rozszerzenie to `json`
  1. Plik z logami powinien zostać uzupełniony/nadpisany. Poniżej pokazana jest jego zawartość (5 pierwszych linii + 2 ostatnie)
  ```
  Duplikat: Paweł,Nowak1,Informatyka magisterskie 4 sem,Dzienne,459,2000-02-12,1@pjwstk.edu.pl,Alina,Adam
  Duplikat: Paweł,Nowak1,Informatyka doktoranckie,Dzienne,459,2000-02-12,1@pjwstk.edu.pl,Alina,Adam
  Duplikat: Justyna,Kowalski30,Sztuka Nowych Mediów uzupełniające magisterskie,Dzienne,4749,2000-02-12,30@pjwstk.edu.pl,Alina,Adam
  Duplikat: Michał,Nowak31,Sztuka Nowych Mediów uzupełniające magisterskie,Dzienne,4432,2000-02-12,31@pjwstk.edu.pl,Alina,Adam
  Wiersz nie może posiadać pustych kolumn: Zbigniew,Kowalski35,Sztuka Nowych Mediów dzienne,,4450,2000-02-12,35@pjwstk.edu.pl,Alina,Adam
  .
  .
  .
  Duplikat: Przemysław,Nowak376,Informatyka zaoczne,Zaoczne,4814,2000-02-12,376@pjwstk.edu.pl,Alina,Adam
  Duplikat: Rafał,Nowak381,Informatyka magisterskie 3 sem,Dzienne,4616,2000-02-12,381@pjwstk.edu.pl,Alina,Adam
  ```
  2. Powinien powstać plik `uczelnia.json`. W miejscu kropek, powinny znaleźć się inne obiekty.
  ```
  {
    "uczelnia": {
      "createdAt": "01.03.2023",
      "author": "Jan Kowalski",
      "studenci": [
        .
        .
        .
      ],
      "activeStudies": [
        {
          "name": "Informatyka dzienne",
          "numberOfStudents": "106"
        },
        .
        .
        .
        {
          "name": "Informatyka zaoczne",
          "numberOfStudents": "1"
        }
      ]
    }
  }
  ```