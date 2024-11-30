DROP TABLE IF EXISTS arr;
CREATE TABLE arr
(
    id  UUID PRIMARY KEY,
    ars UUID[]
);

CREATE INDEX idx_test on arr USING GIN (ars);
INSERT INTO arr (id, ars)
VALUES ('71365997-718e-48da-8b1b-545178dcc4ba', ARRAY['71365997-718e-48da-8b1b-545178dcc4ba']::UUID[]);

EXPLAIN
ANALYZE
SELECT *
FROM arr
WHERE ars @ > ARRAY['71365997-718e-48da-8b1b-545178dcc4ba']::UUID[];

insert into arr (id, ars)
select gen_random_uuid(),
       ARRAY[gen_random_uuid()] ::UUID[]
from generate_series(1, 3000000) s(i);

SELECT COUNT(id)
from arr;

DROP table if exists hte;
CREATE TABLE hte
(
    id  TEXT primary key,
    bag HSTORE
);

CREATE INDEX idx_hte on hte USING GIN (bag);

INSERT INTO hte (id, bag)
VALUES ('i', hstore('a', 'b'));
INSERT INTO hte (id, bag)
VALUES ('j', hstore('a', NULL));

WITH Vals(n) AS (SELECT * FROM generate_series(1, 1000000))
INSERT
INTO hte (
  SELECT n AS Id, hstore('a=>'||n||', b=>'||n) AS Values FROM Vals
);

EXPLAIN
ANALYZE
SELECT *
FROM hte
WHERE bag @ > hstore('a', NULL);