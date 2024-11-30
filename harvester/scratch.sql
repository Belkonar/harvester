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
