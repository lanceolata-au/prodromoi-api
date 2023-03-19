create table public.audit_entries (
    id serial8 primary key,
    "timestamp" timestamp with time zone not null,
    source_type varchar not null,
    source_id int4 not null,
    actor varchar not null,
    entry text not null
);

create index audit_entries_timestamp_idx on public.audit_entries ("timestamp");
create index audit_entries_source_type_idx on public.audit_entries (source_type);
create index audit_entries_source_id_idx on public.audit_entries (source_id);

create table public.actors (
    id serial4 primary key,
    "name" varchar not null
);