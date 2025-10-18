-- Ajuste Status para INT (se estiver como VARCHAR)
ALTER TABLE Turma MODIFY COLUMN `Status` INT NOT NULL;

-- Ajuste DataCriacao para ser gerada pelo MySQL automaticamente
ALTER TABLE Turma 
  MODIFY COLUMN `DataCriacao` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP;
