DROP TABLE IF EXISTS tag;
CREATE TABLE tag
(
    name     TEXT PRIMARY KEY,
    is_field BOOLEAN
);

DROP TABLE IF EXISTS data_field;
CREATE TABLE data_field
(
    name        TEXT,
    data_source UUID,
    data_table  TEXT,
    nonce       UUID,
    is_subfield BOOLEAN,
    category    UUID,
    field_tags  TEXT[],
    PRIMARY KEY (data_source, data_table, name)
);

DROP TABLE IF EXISTS category;
CREATE TABLE category
(
    id   UUID PRIMARY KEY,
    name TEXT,
    tags TEXT[]
);

DROP TABLE IF EXISTS data_table;
CREATE TABLE data_table
(
    name        TEXT,
    data_source UUID,
    nonce       UUID,
    PRIMARY KEY (data_source, name)
);

DROP TABLE IF EXISTS data_source;
CREATE TABLE data_source
(
    id       UUID PRIMARY KEY,
    name     TEXT,
    info_bag JSONB,
    local    BOOLEAN NOT NULL
);

CREATE INDEX idx_field_tags on data_field USING GIN (field_tags);
CREATE INDEX idx_category_tags on category USING GIN (tags);


-- FKs go in this format for consistency
-- ALTER TABLE category
--    ADD CONSTRAINT fk_category_specifier
--        FOREIGN KEY (specifiers)
--            REFERENCES specifier (id);

ALTER TABLE data_field
    ADD CONSTRAINT fk_field_category
        FOREIGN KEY (category)
            REFERENCES category (id);

ALTER TABLE data_table
    ADD CONSTRAINT fk_data_table_source
        FOREIGN KEY (data_source)
            REFERENCES data_source (id);


ALTER TABLE data_field
    ADD CONSTRAINT fk_field_source
        FOREIGN KEY (data_source)
            REFERENCES data_source (id);

-- test the shit out of this.
ALTER TABLE data_field
    ADD CONSTRAINT fk_field_table
        FOREIGN KEY (data_source, data_table)
            REFERENCES data_table (data_source, name);

-- PROCS

CREATE OR REPLACE PROCEDURE upsert_category(
    p_id UUID,
    p_name TEXT,
    p_tags TEXT[]
)
    LANGUAGE plpgsql
AS
$$
BEGIN
    INSERT INTO category(id, name, tags)
    VALUES (p_id, p_name, p_tags)
    ON CONFLICT ON CONSTRAINT category_pkey
        DO UPDATE SET name = excluded.name,
                      tags = excluded.tags;

END;
$$;
