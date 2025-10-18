DELIMITER //
CREATE TRIGGER turma_set_datacriacao BEFORE INSERT ON Turma
FOR EACH ROW
BEGIN
    SET NEW.DataCriacao = NOW();
END;
//
DELIMITER ;
