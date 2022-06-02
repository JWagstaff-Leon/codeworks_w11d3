CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS follows(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,

    follower VARCHAR(255) NOT NULL,
    following VARCHAR(255) NOT NULL,

    FOREIGN KEY (follower) REFERENCES accounts(id) ON DELETE CASCADE,
    FOREIGN KEY (following) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET UTF8;

INSERT
INTO follows
(follower, following)
VALUES
("62755286701a57ffa8c287de", "627552a52ac9b0c117be1a2a");

SELECT 
acc.*,
fol.id AS followId
FROM follows fol
JOIN accounts acc ON fol.following = acc.id
WHERE fol.id = 5;