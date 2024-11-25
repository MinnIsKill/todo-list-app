CREATE TABLE TodoItems (
    Id TEXT PRIMARY KEY,   -- UUIDv4
    Title TEXT NOT NULL,   -- Task title
    Content TEXT NOT NULL, -- Task description
    State INTEGER NOT NULL -- 1 (open), 2 (in-progress), 3 (finished)
);