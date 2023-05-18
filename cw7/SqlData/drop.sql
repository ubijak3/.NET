-- foreign keys
ALTER TABLE trip.Country_Trip DROP CONSTRAINT Country_Trip_Country;

ALTER TABLE trip.Country_Trip DROP CONSTRAINT Country_Trip_Trip;

ALTER TABLE trip.Client_Trip DROP CONSTRAINT Table_5_Client;

ALTER TABLE trip.Client_Trip DROP CONSTRAINT Table_5_Trip;

-- tables
DROP TABLE trip.Client;

DROP TABLE trip.Client_Trip;

DROP TABLE trip.Country;

DROP TABLE trip.Country_Trip;

DROP TABLE trip.Trip;

-- End of file.

