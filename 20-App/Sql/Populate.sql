USE bazDb

INSERT INTO WorkPlans VALUES ('60f9fc29-083f-4ed2-a3e2-3948b503c25f', 'Plan1')
INSERT INTO WorkPlans VALUES ('53c88402-4092-4834-8e7f-6ce70057cdc5', 'Plan2')

GO

PRINT N'Database populated'
GO
WAITFOR DELAY '00:00:01'
