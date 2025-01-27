CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

ALTER TABLE accounts ADD COLUMN cover_img VARCHAR(1000)

CREATE TABLE keeps(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(1000) NOT NULL,
  views INT NOT NULL DEFAULT 0,
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
)

CREATE TABLE vaults(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(1000) NOT NULL,
  is_private BOOLEAN NOT NULL DEFAULT false,
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
)

CREATE TABLE vaultkeeps(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  keep_id INT NOT NULL,
  vault_id INT NOT NULL,
  creator_id VARCHAR(255) NOT NULL, 
  FOREIGN KEY (keep_id) REFERENCES keeps(id) ON DELETE CASCADE,
  FOREIGN KEY (vault_id) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
)

      SELECT
      keeps.*,
      vaultkeeps.*,
      accounts.*
      FROM vaultkeeps
      JOIN keeps ON keeps.id = vaultkeeps.keep_id
      JOIN accounts ON accounts.id = keeps.creator_id
      WHERE vaultkeeps.vault_id = 8;

      SELECT
      vaults.*,
      vaultkeeps.*,
      accounts.*
      FROM vaultkeeps
      JOIN vaults ON vaults.id = vaultkeeps.keep_id
      JOIN accounts ON accounts.id = vaults.creator_id
      WHERE vaultkeeps.vault_id = 8;

SELECT
      vaultkeeps.*,
      accounts.*
      FROM vaultkeeps
      JOIN accounts ON accounts.id = vaultkeeps.creator_id
      WHERE vaultkeeps.vault_id = 2;
INSERT INTO vault_kept(keep_id, vault_id, creator_id)
VALUES();

SELECT
      vaultkeeps.*,
      keeps.*,
      accounts.*
      FROM vaultkeeps
      JOIN keeps ON keeps.id = vaultkeeps.keep_id
      JOIN accounts ON accounts.id = keeps.creator_id
      WHERE vaultkeeps.vault_id = 39;

DROP TABLE vault_kept;

SELECT * FROM keeps;


UPDATE keeps
    SET
    name = "Yikes"
    WHERE id = 13 LIMIT 1