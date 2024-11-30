DROP TABLE IF EXISTS data_field;
CREATE TABLE data_field
(
    name          TEXT,
    data_source   UUID,
    data_table    TEXT,
    specifiers    HSTORE,
    category      UUID,
    category_list UUID[], -- the whole tree
    PRIMARY KEY (data_source, data_table, name)
);

DROP TABLE IF EXISTS category;
CREATE TABLE category
(
    id        UUID PRIMARY KEY,
    parent    UUID REFERENCES category (id),
    name      TEXT,
    parentage JSONB,
    specifier UUID
);

DROP TABLE IF EXISTS specifier;
CREATE TABLE specifier
(
    id      UUID PRIMARY KEY,
    name    TEXT,
    options TEXT[]
);

DROP TABLE IF EXISTS data_table;
CREATE TABLE data_table
(
    name        TEXT,
    data_source UUID,
    PRIMARY KEY (data_source, name)
);

DROP TABLE IF EXISTS data_source;
CREATE TABLE data_source
(
    id       UUID PRIMARY KEY,
    name     TEXT,
    info_bag JSONB,
    local    BOOL NOT NULL
);

CREATE INDEX idx_field_specifiers on data_field USING GIN (specifiers);
CREATE INDEX idx_field_category_list on data_field USING GIN (category_list);

-- FKs go in this format for consistency
ALTER TABLE category
    ADD CONSTRAINT fk_category_specifier
        FOREIGN KEY (specifier)
            REFERENCES specifier (id);

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