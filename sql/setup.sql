-- Setup basic data for testing

CALL upsert_specifier('04e81723-aa5c-4394-9839-4bd447b15eb0', 'person type', ARRAY [
    'internal',
    'external',
    'vendor'
    ]);

CALL upsert_category('3729e111-af53-44ad-ae03-8b5989633361',
                     'person',
                     ARRAY ['text']
     );


-- Remove data

TRUNCATE data_source CASCADE;