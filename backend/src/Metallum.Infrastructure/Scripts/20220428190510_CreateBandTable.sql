CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Bands" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Genre" character varying(100) NOT NULL,
    "Href" character varying(2048) NOT NULL,
    "Location" character varying(200) NOT NULL,
    "MetallumId" character varying(16) NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Status" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (now()),
    "CreatedById" uuid NOT NULL,
    "Deleted" boolean NOT NULL DEFAULT FALSE,
    "DeletedAt" timestamp with time zone NULL,
    "DeletedById" uuid NULL,
    "Key" uuid NOT NULL DEFAULT (uuid_generate_v4()),
    "UpdatedAt" timestamp with time zone NULL,
    "UpdatedById" uuid NULL,
    "Version" integer NOT NULL DEFAULT 0,
    CONSTRAINT "PK_Bands" PRIMARY KEY ("Id")
);

CREATE INDEX "IX_Bands_CreatedById" ON "Bands" ("CreatedById");

CREATE INDEX "IX_Bands_Deleted" ON "Bands" ("Deleted");

CREATE INDEX "IX_Bands_DeletedById" ON "Bands" ("DeletedById");

CREATE INDEX "IX_Bands_Genre" ON "Bands" ("Genre");

CREATE UNIQUE INDEX "IX_Bands_Key" ON "Bands" ("Key");

CREATE INDEX "IX_Bands_Location" ON "Bands" ("Location");

CREATE UNIQUE INDEX "IX_Bands_MetallumId" ON "Bands" ("MetallumId");

CREATE INDEX "IX_Bands_Name" ON "Bands" ("Name");

CREATE INDEX "IX_Bands_Status" ON "Bands" ("Status");

CREATE INDEX "IX_Bands_UpdatedById" ON "Bands" ("UpdatedById");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220428190510_CreateBandTable', '6.0.4');

COMMIT;
