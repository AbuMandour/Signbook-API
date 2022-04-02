CREATE TABLE [dbo].[Users] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [UserId]          NVARCHAR (MAX)   NULL,
    [UserName]        NVARCHAR (MAX)   NOT NULL,
    [PhoneNumber]     NVARCHAR (MAX)   NOT NULL,
    [Password]        NVARCHAR (MAX)   NOT NULL,
    [BundleOfMinutes] FLOAT (53)       NOT NULL,
    [AccessToken]     NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

