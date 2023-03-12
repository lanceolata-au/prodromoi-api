create table public.audit_entries (
    id serial8 not null,
    "timestamp" timestamp with time zone not null,
    source_type varchar not null,
    source_id int4 null,
    actor varchar not null,
    entry text not null
);

alter table public.audit_entries add constraint audit_entries_pk primary key (id);
create index audit_timestamp_idx on public.audit_entries ("timestamp");
create index audit_source_type_idx on public.audit_entries (source_type);
create index audit_source_id_idx on public.audit_entries (source_id);