CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  picture VARCHAR(255) COMMENT 'User Picture',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
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
  is_private BOOLEAN NOT NULL,
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
)

DROP TABLE vaults;

ALTER TABLE vaults ADD COLUMN is_private BOOLEAN DEFAULT true; 


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

    INSERT INTO vaults(name, description, img, is_private, creator_id)
    VALUES("pork chop 2", "i love pork chops", "https://images.unsplash.com/photo-1595429035839-c99c298ffdde?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=500&h=400&q=60", true, "6758a99b25f750aecd7e2ece");

    SELECT 
    vaults.*,
    accounts.*
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creator_id
    WHERE vaults.id = LAST_INSERT_ID();

      SELECT 
      vaults.*,
      accounts.*
      FROM vaults
      JOIN accounts ON accounts.id = vaults.creator_id
      WHERE vaults.creator_id = "6758a99b25f750aecd7e2ece";
      -- 6758aa1356b96ff157d7af1c

