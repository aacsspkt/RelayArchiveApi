CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE relay_infos (
    id uuid NOT NULL,
    chain text NOT NULL,
    emitter_address_hex text NOT NULL,
    sequence numeric(20,0) NOT NULL,
    payload_hex text NOT NULL,
    stream_escrow text NULL,
    status text NOT NULL,
    created_at timestamp with time zone NOT NULL,
    CONSTRAINT "PK_relay_infos" PRIMARY KEY (id)
);

CREATE TABLE signatures (
    value text NOT NULL,
    relay_info_id uuid NOT NULL,
    CONSTRAINT "PK_signatures" PRIMARY KEY (value),
    CONSTRAINT "FK_signatures_relay_infos_relay_info_id" FOREIGN KEY (relay_info_id) REFERENCES relay_infos (id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_relay_infos_chain_emitter_address_hex_sequence" ON relay_infos (chain, emitter_address_hex, sequence);

CREATE INDEX "IX_signatures_relay_info_id" ON signatures (relay_info_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221031062912_Initialize', '6.0.10');

COMMIT;

