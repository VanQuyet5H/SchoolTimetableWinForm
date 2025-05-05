IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Schools] (
    [SchoolId] int NOT NULL IDENTITY,
    [SchoolName] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    CONSTRAINT [PK_Schools] PRIMARY KEY ([SchoolId])
);

GO

CREATE TABLE [Subjects] (
    [SubjectId] int NOT NULL IDENTITY,
    [SubjectName] nvarchar(max) NULL,
    CONSTRAINT [PK_Subjects] PRIMARY KEY ([SubjectId])
);

GO

CREATE TABLE [TeachingAssistants] (
    [TaId] int NOT NULL IDENTITY,
    [TaName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    CONSTRAINT [PK_TeachingAssistants] PRIMARY KEY ([TaId])
);

GO

CREATE TABLE [Classes] (
    [ClassId] int NOT NULL IDENTITY,
    [SchoolId] int NOT NULL,
    [ClassName] nvarchar(max) NULL,
    [Grade] nvarchar(max) NULL,
    CONSTRAINT [PK_Classes] PRIMARY KEY ([ClassId]),
    CONSTRAINT [FK_Classes_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([SchoolId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Teachers] (
    [TeacherId] int NOT NULL IDENTITY,
    [SchoolId] int NOT NULL,
    [TeacherName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [IsForeign] bit NOT NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY ([TeacherId]),
    CONSTRAINT [FK_Teachers_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([SchoolId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Timetables] (
    [TimetableId] int NOT NULL IDENTITY,
    [TeacherId] int NOT NULL,
    [TaId] int NULL,
    [ClassId] int NOT NULL,
    [SubjectId] int NOT NULL,
    [DayOfWeek] nvarchar(max) NULL,
    [LessonNumber] int NOT NULL,
    [LessonDate] datetime2 NOT NULL,
    [StartTime] time NOT NULL,
    [EndTime] time NOT NULL,
    [Hours] decimal(18,2) NOT NULL,
    [TeachingAssistantTaId] int NULL,
    CONSTRAINT [PK_Timetables] PRIMARY KEY ([TimetableId]),
    CONSTRAINT [CK_DayOfWeek] CHECK (day_of_week IN ('Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday')),
    CONSTRAINT [FK_Timetables_Classes_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [Classes] ([ClassId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Timetables_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([SubjectId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Timetables_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([TeacherId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Timetables_TeachingAssistants_TeachingAssistantTaId] FOREIGN KEY ([TeachingAssistantTaId]) REFERENCES [TeachingAssistants] ([TaId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Attendances] (
    [AttendanceId] int NOT NULL IDENTITY,
    [TimetableId] int NOT NULL,
    [Attended] bit NOT NULL,
    [Note] nvarchar(max) NULL,
    CONSTRAINT [PK_Attendances] PRIMARY KEY ([AttendanceId]),
    CONSTRAINT [FK_Attendances_Timetables_TimetableId] FOREIGN KEY ([TimetableId]) REFERENCES [Timetables] ([TimetableId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Attendances_TimetableId] ON [Attendances] ([TimetableId]);

GO

CREATE INDEX [IX_Classes_SchoolId] ON [Classes] ([SchoolId]);

GO

CREATE INDEX [IX_Teachers_SchoolId] ON [Teachers] ([SchoolId]);

GO

CREATE INDEX [IX_Timetables_ClassId] ON [Timetables] ([ClassId]);

GO

CREATE INDEX [IX_Timetables_SubjectId] ON [Timetables] ([SubjectId]);

GO

CREATE INDEX [IX_Timetables_TeacherId] ON [Timetables] ([TeacherId]);

GO

CREATE INDEX [IX_Timetables_TeachingAssistantTaId] ON [Timetables] ([TeachingAssistantTaId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250419174113_intial', N'3.1.21');

GO

